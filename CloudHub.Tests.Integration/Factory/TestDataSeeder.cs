using CloudHub.Domain.Models;
using CloudHub.Infra.Data;

namespace CloudHub.Tests.Integration
{
    class TestDataSeeder
    {
        public TestDataSeeder(PostgreContext context, string clientKey, string clientSecret)
        {
            _context = context;
            _clientKey = clientKey;
            _clientSecret = clientSecret;
        }

        private readonly PostgreContext _context;
        private readonly string _clientKey;
        private readonly string _clientSecret;


        public void Seed()
        {
            int tenantId = InsertTenant();
            InsertClient(tenantId);
        }

        private void InsertClient(int tenantId)
        {
            Client client = new Client()
            {
                TenantId = tenantId,
                Name = "Test Client",
                ClientKey = _clientKey,
                ClientSecret = _clientSecret,
            };
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        private int InsertTenant()
        {
            Tenant tenant = new Tenant() { Name = "Test Tenant" };
            var inserted = _context.Tenants.Add(tenant);
            _context.SaveChanges();
            return inserted.Entity.Id;
        }
    }

}
