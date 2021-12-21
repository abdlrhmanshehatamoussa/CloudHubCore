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

        protected async Task<ConsumerInfo> GetConsumerInfo(ConsumerCredentials credentials)
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

            User? user = null;
            if (credentials.UserToken != null)
            {
                UserToken? token = await _unitOfWork.UserTokensRepository.FirstWhere(t => t.Token == credentials.UserToken, t => t.User);
                if (token == null) { throw new NotAuthenticatedException(); }
                if (token.Active != true || token.RemainingSeconds <= 30) { throw new ExpiredTokenException(); }
                user = token.User;
            }

            return new ConsumerInfo()
            {
                ClientApplication = clientApplication,
                User = user,
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
