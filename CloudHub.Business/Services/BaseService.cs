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

        protected async Task<ClientInfo> GetClientInfo(ClientCredentials credentials)
        {
            Client? client = await _unitOfWork.ClientsRepository.FirstWhere(c => c.ClientKey == credentials.ClientKey, c => c.ClientsApplications);
            if (client == null) { throw new NotAuthenticatedException(); }

            Application? app = await _unitOfWork.ApplicationsRepository.FirstWhere(a => a.Guid.ToString() == credentials.ApplicationGuid);
            if (app == null) { throw new NotAuthenticatedException(); }

            bool appAvailable = client.ClientsApplications.Any(ca => ca.ApplicationId == app.Id);
            if (appAvailable == false) { throw new NotAuthenticatedException(); }

            int? nonceId = null;
            if (credentials.Nonce != null)
            {
                Nonce? nonce = await _unitOfWork.NoncesRepository.FirstWhere(n => n.Token == credentials.Nonce && n.ApplicationId == app.Id);
                if (nonce == null) { throw new InvalidNonceException(); }
                if (nonce.ConsumedOn.HasValue) { throw new ConsumedNonceException(); }
                nonceId = nonce.Id;
            }

            return new ClientInfo()
            {
                ClientType = client.ClientTypeId,
                ApplicationId = app.Id,
                ClientId = client.Id,
                ApplicationGuid = app.Guid.ToString(),
                NonceId = nonceId
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
