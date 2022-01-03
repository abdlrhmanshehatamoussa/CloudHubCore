namespace CloudHub.Domain.Services
{
    public interface IDocumentsService
    {
        public Task<List<dynamic>> FetchAll(string collectionName, Dictionary<string, string>? filters = null);
        public Task Add(string collectionName, dynamic document);
    }
}
