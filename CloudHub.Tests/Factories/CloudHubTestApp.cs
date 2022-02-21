using Microsoft.AspNetCore.Mvc.Testing;
using CloudHub.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace CloudHub.Tests.Factories
{
    internal class CloudHubTestApp : WebApplicationFactory<Program>
    {
        public const string BUILD_ID = "0.0.1";
        public const string ENV_NAME = "IntegrationTestApp";
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices((IServiceCollection services) =>
            {
                services.AddSingleton(new Configurations()
                {
                    BuildId = BUILD_ID,
                    EnvironmentName = ENV_NAME,
                    GoogleOAuthUrl = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=",
                    MainConnectionString = "Host=127.0.0.1;Database=cloudhub-integration-tests;Username=postgres;Password=123456",
                    IsProductionModeEnabled = false,
                });
            });
        }
    }
}
