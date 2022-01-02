using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class DocumentService : BaseService
    {
        protected readonly IDocumentRepository _documentRepository;

        public DocumentService(IUnitOfWork unitOfWork, IDocumentRepository documentRepository, IEnvironmentSettings productionModeProvider) : base(unitOfWork, productionModeProvider)
        {
            this._documentRepository = documentRepository;
        }
    }
}
