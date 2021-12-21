using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class NonceService : BaseService
    {
        public NonceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Nonce> GenereateNonce(ConsumerCredentials credentials)
        {
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            Nonce nonce = new Nonce();
            nonce.ClientId = consumerInfo.ClientApplication.ClientId;
            nonce.ApplicationId = consumerInfo.ClientApplication.ApplicationId;
            nonce.GenerateToken();
            nonce = await _unitOfWork.NoncesRepository.Add(nonce);
            await _unitOfWork.Save();
            return nonce;
        }
    }
}
