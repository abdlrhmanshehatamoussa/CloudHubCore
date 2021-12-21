using CloudHub.Business.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Business.Services
{

    public class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<ConsumerInfo> GetConsumerInfo(ConsumerCredentials credentials)
        {
            Client? client = await _unitOfWork.ClientsRepository.FirstWhere(c => c.ClientKey == credentials.ClientKey, c => c.ClientsApplications);
            if (client == null) { throw new NotAuthenticatedException(); }

            Application? app = await _unitOfWork.ApplicationsRepository.FirstWhere(a => a.Guid.ToString() == credentials.ApplicationGuid);
            if (app == null) { throw new NotAuthenticatedException(); }

            bool appAvailable = client.ClientsApplications.Any(ca => ca.ApplicationId == app.Id);
            if (appAvailable == false) { throw new NotAuthenticatedException(); }
            ClientApplicationRelation clientApplication = client.ClientsApplications.First(ca => ca.ApplicationId == app.Id);

            Nonce? nonce = null;
            if (credentials.Nonce != null)
            {
                nonce = await _unitOfWork.NoncesRepository.FirstWhere(n => n.Token == credentials.Nonce && n.ApplicationId == app.Id);
                if (nonce == null) { throw new InvalidNonceException(); }
                if (nonce.ConsumedOn.HasValue) { throw new ConsumedNonceException(); }
            }

            UserToken? userToken = null;
            if (credentials.UserToken != null)
            {
                userToken = await _unitOfWork.UserTokensRepository.FirstWhere(t => t.Token == credentials.UserToken, t => t.User, t => t.User.Login, t => t.User.Login.LoginType);
                if (userToken == null) { throw new NotAuthenticatedException(); }
                if (userToken.Active != true || userToken.RemainingSeconds <= 30) { throw new ExpiredTokenException(); }

            }

            return new ConsumerInfo()
            {
                ClientApplication = clientApplication,
                UserToken = userToken,
                Nonce = nonce
            };
        }

        protected async Task ConsumeNonce(int nonceId)
        {
            Nonce? nonce = await _unitOfWork.NoncesRepository.GetByPk(nonceId);
            if (nonce == null) { throw new Exception("Nonce not found"); }
            nonce.ConsumedOn = DateTime.UtcNow;
            _unitOfWork.NoncesRepository.Update(nonce);
        }
    }
}
