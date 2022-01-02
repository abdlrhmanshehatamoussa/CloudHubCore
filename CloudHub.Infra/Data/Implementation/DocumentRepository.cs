using CloudHub.Domain.Repositories;

namespace CloudHub.Infra.Data
{
    public class DocumentRepository : IDocumentRepository
    {
        public DocumentRepository(IDocumentDatabaseConfigurations databaseConfigurations)
        {
        
        }


        public async Task<dynamic> FetchAll(string collection)
        {
            return new { };
        }
    }
}
