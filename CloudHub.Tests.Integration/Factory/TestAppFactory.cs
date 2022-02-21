using CloudHub.API;
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
    internal class TestAppFactory : WebApplicationFactory<Program>
    {
        public static string BUILD_ID = "0.0.0";
        public static string ENV_NAME = "IntegrationTestApp";
        private const string CLIENT_KEY = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f";
        private const string CLIENT_CLAIM = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f";

        private readonly CloudHubApiConfigurations MyConfigurations = new()
        {
            BuildId = BUILD_ID,
            EnvironmentName = ENV_NAME,
            GoogleOAuthUrl = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=",
            MainConnectionString = "Host=127.0.0.1;Database=cloudhub-integration-tests;Username=postgres;Password=123456",
            IsProductionModeEnabled = false,
        };


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
            InjectConfigurations(services);
            RegisterDatabase(services);
            MigrateAndSeed(services);
        }

        private void InjectConfigurations(IServiceCollection services)
        {
            UnRegister<CloudHubApiConfigurations>(services);
            services.AddSingleton(MyConfigurations);
        }
        private void RegisterDatabase(IServiceCollection services)
        {
            UnRegister<DbContextOptions<PostgreContext>>(services);
            services.AddDbContext<DbContext, PostgreContext>(options => options.UseNpgsql(MyConfigurations.MainConnectionString));
        }
        private void MigrateAndSeed(IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IServiceScopeFactory serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>() ?? throw new Exception("Error while applying migrations, Failed to create Service Scope Factory");
            IServiceScope serviceScope = serviceScopeFactory.CreateScope();
            PostgreContext context = serviceScope.ServiceProvider.GetService<PostgreContext>() ?? throw new Exception("Error while applying migrations, Failed to get DbContext");
            new TestDatabaseMigraionHandler(context).HandleMigrations();
            new TestDataSeeder(context, CLIENT_KEY, CLIENT_CLAIM).Seed();
        }

        private void UnRegister<T>(IServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
            if (serviceDescriptor != null) services.Remove(serviceDescriptor);
        }
    }
}
