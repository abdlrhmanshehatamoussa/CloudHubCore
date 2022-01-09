using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class FeatureService : BaseService
    {
        public FeatureService(IUnitOfWork unitOfWork, IEnvironmentSettings productionModeProvider) : base(unitOfWork, productionModeProvider)
        {
        }

        public async Task<List<Feature>> Fetch(ConsumerCredentials consumerCredentials)
        {
            Consumer consumer = await GetConsumer(consumerCredentials);

            List<Feature> features = await _unitOfWork.FeaturesRepository.GetAll();

            await ConsumeNonceOrThrow(consumer.Nonce.Id);
            await _unitOfWork.Save();

            return features;
        }
    }
}