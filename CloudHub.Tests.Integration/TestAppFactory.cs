using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.ServiceProvider;
using CloudHub.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace CloudHub.Tests.Integration
{
    internal class TestAppFactory : WebApplicationFactory<Program>
    {
        private readonly string ClientKey;
        private readonly string ClientSecret;
        private readonly IEncryptionService EncryptionService;

        public TestAppFactory()
        {
            ClientKey = Guid.NewGuid().ToString();
            ClientSecret = Guid.NewGuid().ToString();
            this.EncryptionService = new EncryptionService();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureTestServices(services =>
            {
                SeedTestData(services);
            });
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
            client.DefaultRequestHeaders.Add("client-key", ClientKey);
            client.DefaultRequestHeaders.Add("client-claim", EncryptionService.Encrypt(ClientKey, ClientSecret));
        }

        private void SeedTestData(IServiceCollection services)
        {
            IUnitOfWork _context = services.BuildServiceProvider().GetService<IUnitOfWork>() ?? throw new Exception("Failed to get unit of work");
            Client client = new Client()
            {
                Tenant = new Tenant()
                {
                    Name = HelperFunctions.RandomString(10)
                },
                Name = HelperFunctions.RandomString(10),
                ClientKey = ClientKey,
                ClientSecret = ClientSecret
            };
            _context.ClientsRepository.Add(client).Wait();
            _context.Save().Wait();
        }
    }
}
