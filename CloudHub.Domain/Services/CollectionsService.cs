using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class CollectionsService:BaseService
    {
        public CollectionsService(IUnitOfWork unitOfWork, IServiceConfigurations productionModeProvider) : base(unitOfWork, productionModeProvider)
        {
        }

        public async Task<dynamic> FetchPublic(ConsumerCredentials consumerCredentials,string collectionName)
        {
            ConsumerInfo consumerInfo = await GetConsumerInfo(consumerCredentials);
            Nonce nonce = consumerInfo.Nonce ?? throw new InvalidNonceException();
            Collection? collection = await _unitOfWork.CollectionsRepository.FirstWhere(c => c.Name == collectionName && c.CollectionTypeId == CollectionTypeValues.PUBLIC);
            if(collection == null) { throw new InvalidCollectionException(); }
            await ConsumeNonce(nonce.Id);
            throw new NotImplementedException();
        }
    }
}
