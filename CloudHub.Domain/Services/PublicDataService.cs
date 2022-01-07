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

        public async Task<List<PublicDocument>> FetchAll(ConsumerCredentials credentials, string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName)) { throw new InvalidCollectionException(); }
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            Nonce nonce = consumerInfo.Nonce ?? throw new InvalidNonceException();

            PublicCollection? collection = await _unitOfWork.PublicCollectionsRepository.FirstWhere(c => c.Name == collectionName);
            if (collection == null) { throw new InvalidCollectionException(); }
            List<PublicDocument> documents = await _unitOfWork.PublicDocumentsRepository.Where(d => d.PublicCollectionId == collection.Id);

            await ConsumeNonce(nonce.Id);
            await _unitOfWork.Save();

            return documents;
        }
    }
}
