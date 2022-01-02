using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class NonceService : BaseService
    {
        public NonceService(IUnitOfWork unitOfWork, IEnvironmentSettings productionModeProvider) : base(unitOfWork, productionModeProvider)
        {
        }

        public async Task<Nonce> GenereateNonce(ConsumerCredentials credentials)
        {
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            Nonce nonce = new ();
            nonce.ClientId = consumerInfo.Client.Id;
            nonce.GenerateToken();
            nonce = await _unitOfWork.NoncesRepository.Add(nonce);
            await _unitOfWork.Save();
            return nonce;
        }
    }
}
