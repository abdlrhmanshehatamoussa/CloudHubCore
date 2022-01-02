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

        public async Task<List<IPrivateDocument>> FetchAll(ConsumerCredentials credentials, string collectionName)
        {
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            Nonce nonce = consumerInfo.Nonce ?? throw new InvalidNonceException();
            Collection? collection = await _unitOfWork.CollectionsRepository.FirstWhere(c => c.Name == collectionName && c.CollectionTypeId == CollectionTypeValues.PRIVATE);
            UserToken token = consumerInfo.UserToken ?? throw new NotAuthenticatedException();
            User user = token.User;

            if (collection == null) { throw new InvalidCollectionException(); }
            dynamic results = null;
            throw new NotImplementedException();

            await ConsumeNonce(nonce.Id);
            await _unitOfWork.Save();
            return results;
        }
    }
}
