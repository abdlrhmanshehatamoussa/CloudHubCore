using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Models;

namespace CloudHub.Domain.Services
{
    public class PrivateDataService : BaseService
    {
        public PrivateDataService(IUnitOfWork unitOfWork, IEncryptionService encryptionService) : base(unitOfWork,encryptionService) { }

        public async Task<List<PrivateDocument>> FetchAll(ConsumerCredentials credentials, string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName)) { throw new InvalidCollectionException(); }
            Consumer consumer = await GetConsumer(credentials);
            User user = consumer.UserToken?.User ?? throw new NotAuthenticatedException();

            PrivateCollection? collection = await _unitOfWork.PrivateCollectionsRepository.FirstWhere(c => c.Name == collectionName);
            if (collection == null) { throw new InvalidCollectionException(); }
            List<PrivateDocument> documents = await _unitOfWork.PrivateDocumentsRepository.Where(d => d.UserId == user.Id && d.PrivateCollectionId == collection.Id);

            await ConsumeNonceOrThrow(consumer.Nonce.Id);
            await _unitOfWork.Save();

            return documents;
        }
    }
}
