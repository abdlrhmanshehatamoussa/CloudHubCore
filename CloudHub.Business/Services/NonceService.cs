using CloudHub.Business.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;

namespace CloudHub.Business.Services
{
    public class NonceService : BaseService
    {
        public NonceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Nonce> GenereateNonce(ClientCredentials credentials)
        {
            ClientInfo clientInfo = await GetClientInfo(credentials);
            Nonce nonce = new Nonce();
            nonce.ClientId = clientInfo.ClientId;
            nonce.ApplicationId = clientInfo.ApplicationId;
            nonce.GenerateToken();
            nonce = await _unitOfWork.NoncesRepository.Add(nonce);
            await _unitOfWork.Save();
            return nonce;
        }
    }
}
