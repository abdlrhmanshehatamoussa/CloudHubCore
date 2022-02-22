using CloudHub.API;
using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.ServiceProvider;
using CloudHub.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;

namespace CloudHub.Tests.Integration
{
    internal class TestConfigurations : IConfigOAuthService, IConfigDatabase, IEnvironmentInfo
    {
        public string EnvironmentName { get; set; } = null!;
        public string BuildId { get; set; } = null!;
        public bool IsProduction { get; set; } = false;
        public string ConnectionString { get; set; } = null!;
        public string GoogleOAuthUrl { get; set; } = null!;
    }

    internal class TestAppFactory : WebApplicationFactory<Program>
    {
        public static string BUILD_ID = "0.0.0";
        public static string ENV_NAME = "IntegrationTestApp";

        private readonly string ClientKey;
        private readonly string ClientSecret;

        public TestAppFactory()
        {
            ClientKey = Guid.NewGuid().ToString();
            ClientSecret = Guid.NewGuid().ToString();
        }

        protected readonly TestConfigurations MyConfigurations = new()
        {
            BuildId = BUILD_ID,
            EnvironmentName = ENV_NAME,
            GoogleOAuthUrl = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=",
            ConnectionString = "Host=127.0.0.1;Database=cloudhub-integration-tests;Username=postgres;Password=123456",
            IsProduction = false
        };

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureTestServices(RegisterServices);
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
            client.DefaultRequestHeaders.Add("client-key", ClientKey);
            client.DefaultRequestHeaders.Add("client-claim", SecurityHelper.EncryptAES(ClientKey, ClientSecret));
        }

        private void RegisterServices(IServiceCollection services)
        {
            InjectConfigurations(services);
            RegisterUnitOfWork(services);
        }

        //General
        private void UnRegister<T>(IServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
            if (serviceDescriptor != null) services.Remove(serviceDescriptor);
        }

        private void InjectConfigurations(IServiceCollection services)
        {
            UnRegister<IConfigOAuthService>(services);
            services.AddSingleton<IConfigOAuthService>(MyConfigurations);

            UnRegister<IEnvironmentInfo>(services);
            services.AddSingleton<IEnvironmentInfo>(MyConfigurations);

            UnRegister<IConfigDatabase>(services);
            services.AddSingleton<IConfigDatabase>(MyConfigurations);
        }

        private void RegisterUnitOfWork(IServiceCollection services)
        {
            UnRegister<IUnitOfWork>(services);
            Seed();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private void Seed()
        {
            UnitOfWork _context = new UnitOfWork(MyConfigurations);
            Tenant tenant = new Tenant() { Name = HelperFunctions.RandomString(10) };
            Tenant inserted = _context.TenantsRepository.Add(tenant).Result;
            _context.Save().Wait();

            Client client = new Client() { TenantId = inserted.Id, Name = HelperFunctions.RandomString(10), ClientKey = ClientKey, ClientSecret = ClientSecret };
            _context.ClientsRepository.Add(client).Wait();
            _context.Save().Wait();
        }
    }
}
