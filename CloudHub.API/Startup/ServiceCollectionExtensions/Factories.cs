using CloudHub.Domain.Services;
using CloudHub.Infra.Factories;

namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void RegisterFactories(this WebApplicationBuilder builder, CloudHubApiConfigurations configurations)
        {
            builder.Services.AddSingleton<IOAuthService, OAuthService>(o => new OAuthService(configurations.GoogleOAuthUrl));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
