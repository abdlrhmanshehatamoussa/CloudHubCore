using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Services;

namespace CloudHub.Infra.Services
{
    public class TenantsService : ITenantsService
    {
        public TenantsService(List<Tenant> tenants) => _tenants = tenants;

        private readonly List<Tenant> _tenants;

        public Tenant? CurrentTenant { get; private set; }

        public void SetTenant(string tenantId)
        {
            Tenant? target = _tenants.FirstOrDefault(x => x.Id == tenantId);
            if (target == null) { throw new InvalidTenantException(); }
            CurrentTenant = target;
        }

        public static List<Tenant> LoadTenants(string jsonFile)
        {
            string json = File.ReadAllText(jsonFile);
            List<Tenant> tenants = new ()
            {
                new Tenant(){ Id = "5097",ConnectionString = "Host=127.0.0.1;Database=cloudhub-api-core-local;Username=postgres;Password=123456" },
                new Tenant(){ Id = "5098",ConnectionString = "Host=127.0.0.1;Database=cloudhub-api-core-local2;Username=postgres;Password=123456" }
            };
            return tenants;
        }
    }
}
