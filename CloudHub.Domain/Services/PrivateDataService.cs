using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class PrivateDataService : BaseService
    {
        public PrivateDataService(IUnitOfWork unitOfWork, IEnvironmentSettings productionModeProvider) : base(unitOfWork, productionModeProvider)
        {
        }

        public async Task<List<PrivateDocument>> FetchAll(ConsumerCredentials credentials, string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName)) { throw new InvalidCollectionException(); }
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            Nonce nonce = consumerInfo.Nonce ?? throw new InvalidNonceException();
            User user = consumerInfo.UserToken?.User ?? throw new NotAuthenticatedException();

            PrivateCollection? collection = await _unitOfWork.PrivateCollectionsRepository.FirstWhere(c => c.Name == collectionName);
            if (collection == null) { throw new InvalidCollectionException(); }
            List<PrivateDocument> documents = await _unitOfWork.PrivateDocumentsRepository.Where(d => d.UserId == user.Id && d.PrivateCollectionId == collection.Id);

            await ConsumeNonce(nonce.Id);
            await _unitOfWork.Save();

            return documents;
        }
    }
}
