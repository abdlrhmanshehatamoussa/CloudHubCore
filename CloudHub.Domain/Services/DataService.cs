using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class DataService : BaseService
    {
        public DataService(IUnitOfWork unitOfWork,
            IEnvironmentSettings productionModeProvider, IDocumentsService documentsService)
            : base(unitOfWork, productionModeProvider)
        {
            this._documentsService = documentsService;
        }
        private readonly IDocumentsService _documentsService;

        public async Task<List<dynamic>> FetchAll(ConsumerCredentials credentials, string collectionName)
        {
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            Nonce nonce = consumerInfo.Nonce ?? throw new InvalidNonceException();
            Collection? collection = await _unitOfWork.CollectionsRepository.FirstWhere(c => c.Name == collectionName && c.Active == true);
            if (collection == null) { throw new InvalidCollectionException(); }

            Dictionary<string,string> filter = null!;
            if (string.IsNullOrWhiteSpace(collection.IdentityField) == false)
            {
                UserToken token = consumerInfo.UserToken ?? throw new NotAuthenticatedException();
                User user = token.User;
                filter = new Dictionary<string, string>();
                filter[collection.IdentityField] = user.GlobalId;
            }

            dynamic results = await _documentsService.FetchAll(collectionName, filter);
            await ConsumeNonce(nonce.Id);
            await _unitOfWork.Save();
            return results;
        }
    }
}
