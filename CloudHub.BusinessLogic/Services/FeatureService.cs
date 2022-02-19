using CloudHub.BusinessLogic.DTO;
using CloudHub.BusinessLogic.Repositories;
using CloudHub.Domain.Entities;

namespace CloudHub.BusinessLogic.Services
{
    public class FeatureService : BaseService
    {
        public FeatureService(IUnitOfWork unitOfWork, IEnvironmentSettings productionModeProvider) : base(unitOfWork, productionModeProvider)
        {
        }

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