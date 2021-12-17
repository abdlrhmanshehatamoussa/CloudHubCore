using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class BaseService
    {
        protected IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    }
}
