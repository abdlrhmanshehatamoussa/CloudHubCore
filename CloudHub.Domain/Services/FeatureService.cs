using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class FeatureService : BaseService
    {
        public FeatureService(IUnitOfWork unitOfWork, IServiceConfigurations productionModeProvider) : base(unitOfWork, productionModeProvider)
        {
        }

        public async Task<List<Feature>> Fetch(ConsumerCredentials consumerCredentials)
        {
            ConsumerInfo info = await GetConsumerInfo(consumerCredentials);
            int nonceId = info.Nonce?.Id ?? throw new InvalidNonceException(nameof(info.Nonce));

            List<Feature> features = await _unitOfWork.FeaturesRepository.GetAll();

            await ConsumeNonce(nonceId);
            await _unitOfWork.Save();

            return features;
        }
    }
}