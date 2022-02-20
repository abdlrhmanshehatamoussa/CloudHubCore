using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Models;
using System.Text.Json;

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
            Consumer consumer = await GetConsumer(credentials);

            PublicCollection? collection = await _unitOfWork.PublicCollectionsRepository.FirstWhere(c => c.Name == collectionName && c.TenantId == consumer.Client.TenantId);
            if (collection == null) { throw new InvalidCollectionException(); }
            List<PublicDocument> documents = await _unitOfWork.PublicDocumentsRepository.Where(d => d.PublicCollectionId == collection.Id);

            await ConsumeNonceOrThrow(consumer.Nonce.Id);
            await _unitOfWork.Save();

            return documents;
        }

        public async Task AddBulk(ConsumerCredentials credentials, string collectionName, List<dynamic> bodies)
        {
            if (string.IsNullOrWhiteSpace(collectionName)) { throw new InvalidCollectionException(); }
            Consumer consumer = await GetConsumer(credentials);
            PublicCollection? collection = await _unitOfWork.PublicCollectionsRepository.FirstWhere(c => c.Name == collectionName && c.TenantId == consumer.Client.TenantId);
            if (collection == null) { throw new InvalidCollectionException(); }
            try
            {
                foreach (var body in bodies)
                {
                    string bodyStr = (body as object).ToString() ?? throw new UnprocessableEntityException();
                    PublicDocument document = new()
                    {
                        Body = JsonDocument.Parse(bodyStr),
                        PublicCollectionId = collection.Id
                    };
                    await _unitOfWork.PublicDocumentsRepository.Add(document);
                }
            }
            catch (Exception)
            {
                throw new UnprocessableEntityException();
            }
            await ConsumeNonceOrThrow(consumer.Nonce.Id);
            await _unitOfWork.Save();
        }

        public async Task Add(ConsumerCredentials credentials, string collectionName, dynamic data)
        {
            if (string.IsNullOrWhiteSpace(collectionName)) { throw new InvalidCollectionException(); }
            Consumer consumer = await GetConsumer(credentials);
            PublicCollection? collection = await _unitOfWork.PublicCollectionsRepository.FirstWhere(c => c.Name == collectionName && c.TenantId == consumer.Client.TenantId);
            if (collection == null) { throw new InvalidCollectionException(); }
            string dataStr = (data as object).ToString() ?? throw new UnprocessableEntityException();
            try
            {
                PublicDocument document = new()
                {
                    Body = JsonDocument.Parse(dataStr),
                    PublicCollectionId = collection.Id
                };
                await _unitOfWork.PublicDocumentsRepository.Add(document);
            }
            catch (Exception)
            {
                throw new UnprocessableEntityException();
            }
            await ConsumeNonceOrThrow(consumer.Nonce.Id);
            await _unitOfWork.Save();
        }
    }
}
