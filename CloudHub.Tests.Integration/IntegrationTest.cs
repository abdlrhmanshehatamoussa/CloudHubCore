using CloudHub.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace CloudHub.Tests.Integration
{
    internal class IntegrationTest
    {
        public const string BUILD_ID = "0.0.1";
        public const string ENV_NAME = "IntegrationTestApp";

        protected readonly HttpClient Client;
        protected readonly ClientInfo ClientInfo = new()
        {
            ClientKey = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f",
            ClientSecret = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f"
        };
        private readonly Configurations Configurations = new()
        {
            BuildId = BUILD_ID,
            EnvironmentName = ENV_NAME,
            GoogleOAuthUrl = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=",
            MainConnectionString = "Host=127.0.0.1;Database=cloudhub-api-core-local;Username=postgres;Password=123456",
            IsProductionModeEnabled = false,
        };

        public IntegrationTest()
        {
            Client = BuildClient();
        }
        private HttpClient BuildClient()
        {
            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>().WithWebHostBuilder(BuildApp);
            return factory.CreateClient();
        }

        private void BuildApp(IWebHostBuilder builder)
        {
            builder.ConfigureServices(RegisterServices);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton(Configurations);
        }
    }
}
