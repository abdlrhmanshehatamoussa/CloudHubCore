using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class PublicDataService : DocumentService
    {
        public PublicDataService(IUnitOfWork unitOfWork, IDocumentRepository documentRepository, IEnvironmentSettings productionModeProvider) : base(unitOfWork, documentRepository, productionModeProvider)
        {
        }

        public async Task<dynamic> FetchAll(ConsumerCredentials credentials, string collectionName)
        {
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            Nonce nonce = consumerInfo.Nonce ?? throw new InvalidNonceException();
            Collection? collection = await _unitOfWork.CollectionsRepository.FirstWhere(c => c.Name == collectionName && c.CollectionTypeId == CollectionTypeValues.PUBLIC);
            if (collection == null) { throw new InvalidCollectionException(); }
            var results = await _documentRepository.FetchAll(collectionName);
            await ConsumeNonce(nonce.Id);
            await _unitOfWork.Save();
            return results;
        }
    }
}
