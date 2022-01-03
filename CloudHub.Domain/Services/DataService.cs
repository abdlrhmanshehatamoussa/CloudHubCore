using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;
using System.Dynamic;

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
            if (string.IsNullOrWhiteSpace(collectionName)) { throw new MissingParameterException("collection"); }
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            Nonce nonce = consumerInfo.Nonce ?? throw new InvalidNonceException();
            Collection? collection = await _unitOfWork.CollectionsRepository.FirstWhere(c => c.Name == collectionName && c.Active == true);
            if (collection == null) { throw new InvalidCollectionException(); }

            Dictionary<string, string> filter = new();
            if (collection.IsPrivate)
            {
                UserToken token = consumerInfo.UserToken ?? throw new NotAuthenticatedException();
                User user = token.User;
                filter["user_id"] = user.GlobalId;
            }

            List<dynamic> results = await _documentsService.FetchAll(collectionName, filter);
            await ConsumeNonce(nonce.Id);
            await _unitOfWork.Save();
            return results;
        }

        public async Task Add(ConsumerCredentials credentials, string collectionName, dynamic data)
        {
            if (string.IsNullOrWhiteSpace(collectionName)) { throw new MissingParameterException("collection"); }
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            Nonce nonce = consumerInfo.Nonce ?? throw new InvalidNonceException();
            Collection? collection = await _unitOfWork.CollectionsRepository.FirstWhere(c => c.Name == collectionName && c.Active == true);
            if (collection == null) { throw new InvalidCollectionException(); }

            dynamic toAdd;
            if (collection.IsPrivate)
            {
                UserToken token = consumerInfo.UserToken ?? throw new NotAuthenticatedException();
                User user = token.User;
                toAdd = new { body = data, user_id = user.GlobalId };
            }
            else
            {
                toAdd = new { body = data };
            }
            await _documentsService.Add(collectionName, toAdd);
            await ConsumeNonce(nonce.Id);
            await _unitOfWork.Save();
        }
    }
}
