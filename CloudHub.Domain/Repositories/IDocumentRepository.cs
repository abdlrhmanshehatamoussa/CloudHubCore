namespace CloudHub.Domain.Repositories
{
    public interface IDocumentRepository
    {
        public Task<dynamic> FetchAll(string collection);
    }
}
