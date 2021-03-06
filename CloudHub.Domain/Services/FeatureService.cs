using CloudHub.Domain.Models;

namespace CloudHub.Domain.Services
{
    public class FeatureService : BaseService
    {
        public FeatureService(IUnitOfWork unitOfWork, IEncryptionService encryptionService) : base(unitOfWork, encryptionService) { }

        public async Task<List<Feature>> Fetch(ConsumerCredentials consumerCredentials)
        {
            Consumer consumer = await GetConsumer(consumerCredentials);

            List<Feature> features = await _unitOfWork.FeaturesRepository.Where(f => f.TenantId == consumer.Client.TenantId);

            await ConsumeNonceOrThrow(consumer.Nonce.Id);
            await _unitOfWork.Save();

            return features;
        }
    }
}