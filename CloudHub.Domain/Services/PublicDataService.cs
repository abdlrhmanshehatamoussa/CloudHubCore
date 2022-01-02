using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class PublicDataService : BaseService
    {
        public PublicDataService(IUnitOfWork unitOfWork, IEnvironmentSettings productionModeProvider) : base(unitOfWork, productionModeProvider)
        {
        }

        public async Task<List<IPublicDocument>> FetchAll(ConsumerCredentials credentials, string collectionName)
        {
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            Nonce nonce = consumerInfo.Nonce ?? throw new InvalidNonceException();
            Collection? collection = await _unitOfWork.CollectionsRepository.FirstWhere(c => c.Name == collectionName && c.CollectionTypeId == CollectionTypeValues.PUBLIC);
            if (collection == null) { throw new InvalidCollectionException(); }
            
            List<IPublicDocument> results = null!;
            throw new NotImplementedException();
            
            await ConsumeNonce(nonce.Id);
            await _unitOfWork.Save();
            return results;
        }
    }
    
}
