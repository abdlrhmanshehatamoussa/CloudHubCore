using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

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


        internal async Task<ConsumerInfo> GetConsumerInfo(ConsumerCredentials credentials)
        {
            Client? client = await _unitOfWork.ClientsRepository.FirstWhere(c => c.ClientKey == credentials.ClientKey);
            if (client == null) { throw new NotAuthenticatedException(); }

            Nonce? nonce = null;
            if (credentials.Nonce != null)
            {
                nonce = await _unitOfWork.NoncesRepository.FirstWhere(n => n.Token == credentials.Nonce);
                if (nonce == null) { throw new InvalidNonceException(); }
                if (nonce.ConsumedOn.HasValue) { throw new ConsumedNonceException(); }
            }

            UserToken? userToken = null;
            if (credentials.UserToken != null)
            {
                userToken = await _unitOfWork.UserTokensRepository.FirstWhere(t => t.Token == credentials.UserToken, t => t.User, t => t.User.Login, t => t.User.Login.LoginType);
                if (userToken == null) { throw new NotAuthenticatedException(); }
                if (userToken.RemainingSeconds <= 30) { throw new ExpiredTokenException(); }

            }

            Admin? admin = null;
            if (credentials.AdminCredentials != null)
            {
                admin = await _unitOfWork.AdminsRepository.FirstWhere(a => a.Active == true
                && a.UserName == credentials.AdminCredentials.Value.UserName
                && a.Password == credentials.AdminCredentials.Value.Password);
                if (admin == null) { throw new NotAuthenticatedException(); }
            }

            return new ConsumerInfo()
            {
                Client = client,
                UserToken = userToken,
                Nonce = nonce,
                Admin = admin
            };
        }

        protected async Task ConsumeNonce(int nonceId)
        {
            if (productionModeProvider.IsProductionModeEnabled == false) { return; }
            Nonce? nonce = await _unitOfWork.NoncesRepository.GetByPk(nonceId);
            if (nonce == null) { throw new Exception("Nonce not found"); }
            nonce.ConsumedOn = DateTime.UtcNow;
            _unitOfWork.NoncesRepository.Update(nonce);

        }
    }
}
