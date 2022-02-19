using CloudHub.Utils;
using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;
using CloudHub.Domain.Exceptions;

namespace CloudHub.Domain.Services
{
    public class BaseService
    {
        public BaseService(IUnitOfWork unitOfWork, IEnvironmentSettings productionModeProvider)
        {
            _unitOfWork = unitOfWork;
            this.productionModeProvider = productionModeProvider;
        }


        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IEnvironmentSettings productionModeProvider;


        internal async Task<Consumer> GetConsumer(ConsumerCredentials credentials)
        {
            if (string.IsNullOrWhiteSpace(credentials.ClientKey) || string.IsNullOrWhiteSpace(credentials.Nonce)) { throw new MissingParameterException(""); }

            Client? client = await _unitOfWork.ClientsRepository.FirstWhere(c => c.ClientKey == credentials.ClientKey, c => c.Tenant);
            if (client == null) { throw new NotAuthenticatedException(); }

            string decryptedNonce = SecurityHelper.DecryptAES(credentials.Nonce, client.ClientSecret);
            Nonce? nonce = await _unitOfWork.NoncesRepository.FirstWhere(n => n.Token == decryptedNonce && n.ClientId == client.Id, n => n.Client);
            if (nonce == null) { throw new InvalidNonceException(); }
            if (nonce.ConsumedOn.HasValue) { throw new ConsumedNonceException(); }

            UserToken? userToken = null;
            if (credentials.UserToken != null)
            {
                userToken = await _unitOfWork.UserTokensRepository.FirstWhere(t => t.Token == credentials.UserToken,
                    t => t.User,
                    t => t.User.Login,
                    t => t.User.Tenant,
                    t => t.User.Login.LoginType);
                if (userToken == null) { throw new NotAuthenticatedException(); }
                if (userToken.RemainingSeconds <= 30) { throw new ExpiredTokenException(); }
                if (userToken.User.TenantId != client.TenantId) { throw new NotAuthenticatedException(); }
            }


            return new Consumer()
            {
                UserToken = userToken,
                Nonce = nonce,
                Client = client,
            };
        }

        protected async Task ConsumeNonceOrThrow(int nonceId)
        {
            if (productionModeProvider.IsProductionModeEnabled == false) { return; }
            Nonce? nonce = await _unitOfWork.NoncesRepository.GetByPk(nonceId);
            if (nonce == null) { throw new Exception("Nonce not found"); }
            nonce.ConsumedOn = DateTime.UtcNow;
            _unitOfWork.NoncesRepository.Update(nonce);

        }
    }
}
