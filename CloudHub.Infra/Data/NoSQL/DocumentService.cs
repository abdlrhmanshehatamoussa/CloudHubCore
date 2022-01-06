using CloudHub.Domain.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Text.Json;

namespace CloudHub.Infra.Data
{
    public class DocumentService : IDocumentsService
    {

        public DocumentService(IMongoConfigurations databaseConfigurations)
        {
            mongoClient = new MongoClient(databaseConfigurations.MongoHost);
            database = mongoClient.GetDatabase(databaseConfigurations.MongoDatabase);
        }

        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;


        public async Task<List<dynamic>> FetchAll(string collectionName, Dictionary<string, string>? filters = null)
        {
            IMongoCollection<dynamic> collection = database.GetCollection<dynamic>(collectionName);

            FilterDefinition<dynamic> myFilter = FilterDefinition<dynamic>.Empty;
            if (filters != null)
            {
                foreach (var k in filters.Keys)
                {
                    var v = filters[k];
                    myFilter &= Builders<dynamic>.Filter.Eq<dynamic>(k, v);
                }
            }
            List<dynamic> results = await collection.Find(myFilter).ToListAsync();
            return results;
        }

        public async Task Add(string collectionName, dynamic document)
        {
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            string documentJsonStr = JsonSerializer.Serialize(document);
            BsonDocument toInsert = BsonSerializer.Deserialize<BsonDocument>(documentJsonStr);
            await collection.InsertOneAsync(toInsert);
        }
    }
}
