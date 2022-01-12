using CloudHub.Domain.Entities;

namespace CloudHub.Domain.Services
{
    public interface ITenantsService
    {
        public void SetTenant(string tenantId);
        public Tenant? CurrentTenant { get; }
    }
}
