using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class FeatureService : BaseService
    {
        public FeatureService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<Feature>> Fetch(ConsumerCredentials consumerCredentials)
        {
            ConsumerInfo info = await GetConsumerInfo(consumerCredentials);
            int applicationId = info.ClientApplication.ApplicationId;
            int nonceId = info.Nonce?.Id ?? throw new InvalidNonceException(nameof(info.Nonce));

            List<Feature> features = await _unitOfWork.FeaturesRepository.Where(f => f.ApplicationId == applicationId);

            await ConsumeNonce(nonceId);
            await _unitOfWork.Save();

            return features;
        }
    }
}