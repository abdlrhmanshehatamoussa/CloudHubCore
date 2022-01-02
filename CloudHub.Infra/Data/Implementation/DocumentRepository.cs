using MongoDB.Driver;
using System.Linq.Expressions;

namespace CloudHub.Infra.Data
{
    public class DocumentRepository
    {

        public DocumentRepository(IMongoConfigurations databaseConfigurations)
        {
            mongoClient = new MongoClient(databaseConfigurations.MongoHost);
            database = mongoClient.GetDatabase(databaseConfigurations.MongoDatabase);
        }

        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;


        public async Task<List<T>> FetchAll<T>(string collectionName, Expression<Func<T, bool>>? filter = null)
        {
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);
            List<T> results = await collection.Find(filter).ToListAsync();
            return results;
        }
    }
}
