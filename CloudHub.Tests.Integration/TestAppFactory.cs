using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.ServiceProvider;
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
        public TestAppFactory(Client client) => _client = client;
        
        private readonly Client _client;

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
            IEncryptionService encryptionService = new EncryptionService();
            client.DefaultRequestHeaders.Add("client-key", _client.ClientKey);
            client.DefaultRequestHeaders.Add("client-claim", encryptionService.Encrypt(_client.ClientKey, _client.ClientSecret));
        }

        private void SeedTestData(IServiceCollection services)
        {
            IUnitOfWork _context = services.BuildServiceProvider().GetService<IUnitOfWork>() ?? throw new Exception("Failed to get unit of work");
            _context.ClientsRepository.Add(_client).Wait();
            _context.Save().Wait();
        }
    }
}
