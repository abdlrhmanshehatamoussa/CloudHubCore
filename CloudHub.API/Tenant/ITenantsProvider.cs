namespace CloudHub.API.Tenant
{
    public interface ITenantsProvider
    {
        public Task<Tenant> GetTenant(string tenantId);
    }
}
