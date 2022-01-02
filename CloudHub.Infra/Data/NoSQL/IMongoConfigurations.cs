namespace CloudHub.Infra.Data
{
    public interface IMongoConfigurations
    {
        public string MongoHost { get; }
        public string MongoDatabase { get; }
    }
}
