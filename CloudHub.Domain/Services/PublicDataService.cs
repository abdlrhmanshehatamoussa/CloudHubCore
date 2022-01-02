using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class PublicDataService : BaseService
    {
        public PublicDataService(IUnitOfWork unitOfWork, IServiceConfigurations productionModeProvider) : base(unitOfWork, productionModeProvider)
        {
        }

        public async Task<dynamic> FetchAll(ConsumerCredentials credentials, string collectionName)
        {
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            Nonce nonce = consumerInfo.Nonce ?? throw new InvalidNonceException();
            Collection? collection = await _unitOfWork.CollectionsRepository.FirstWhere(c => c.Name == collectionName && c.CollectionTypeId == CollectionTypeValues.PUBLIC);
            if (collection == null) { throw new InvalidCollectionException(); }

            await ConsumeNonce(nonce.Id);
            return new { };
        }
    }
}
