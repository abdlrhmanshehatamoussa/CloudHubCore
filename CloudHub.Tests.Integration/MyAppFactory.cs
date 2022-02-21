using CloudHub.API;
using CloudHub.Domain.Models;
using CloudHub.Infra.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;

namespace CloudHub.Tests.Integration
{
    internal class MyAppFactory : WebApplicationFactory<Program>
    {
        public MyAppFactory(Configurations myConfigurations) => MyConfigurations = myConfigurations;
        
        
        private const string CLIENT_KEY = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f";
        private const string CLIENT_CLAIM = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f";
        private readonly Configurations MyConfigurations;


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureTestServices(RegisterServices);
            
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
            client.DefaultRequestHeaders.Add("client-key", CLIENT_KEY);
            client.DefaultRequestHeaders.Add("client-claim", CLIENT_CLAIM);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services = CustomRemove<Configurations>(services);
            services.AddSingleton(MyConfigurations);

            services = CustomRemove<DbContextOptions<PostgreContext>>(services);
            services.AddDbContext<DbContext, PostgreContext>(options => options.UseNpgsql(MyConfigurations.MainConnectionString));

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IServiceScopeFactory serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>() ?? throw new Exception("Error while applying migrations, Failed to create Service Scope Factory");
            IServiceScope serviceScope = serviceScopeFactory.CreateScope();
            PostgreContext context = serviceScope.ServiceProvider.GetService<PostgreContext>() ?? throw new Exception("Error while applying migrations, Failed to get DbContext");
            context.Database.EnsureDeleted();
            int migrationsCount = context.Database.GetAppliedMigrations().Count();
            if (migrationsCount == 0)
            {
                context.Database.Migrate();
            }
            Tenant tenant = new Tenant()
            {
                Name = "Test Tenant"
            };
            var inserted = context.Tenants.Add(tenant);
            context.SaveChanges();
            Client client = new Client()
            {
                TenantId = inserted.Entity.Id,
                Name = "Test Client",
                ClientKey = CLIENT_KEY,
                ClientSecret = CLIENT_CLAIM,
            };
            context.Clients.Add(client);
            context.SaveChanges();
        }

        private IServiceCollection CustomRemove<T>(IServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
            if (serviceDescriptor != null) services.Remove(serviceDescriptor);
            return services;
        }
    }
}
