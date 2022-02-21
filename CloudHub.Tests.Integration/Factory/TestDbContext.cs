using CloudHub.Domain.Models;
using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.Tests.Integration
{
    internal class TestDbContext : PostgreContext
    {
        public TestDbContext(DbContextOptions<PostgreContext> options, string clientKey, string clientSecret) : base(options)
        {
            _clientKey = clientKey;
            _clientSecret = clientSecret;
        }

        private readonly string _clientKey;
        private readonly string _clientSecret;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
            Clients.Add(client);
            SaveChanges();
        }

        private int InsertTenant()
        {
            Tenant tenant = new Tenant() { Name = "Test Tenant" };
            var inserted = Tenants.Add(tenant);
            SaveChanges();
            return inserted.Entity.Id;
        }
    }

}
