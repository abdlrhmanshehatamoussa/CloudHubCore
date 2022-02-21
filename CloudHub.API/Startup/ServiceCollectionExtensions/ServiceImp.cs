using CloudHub.Domain.Services;
using CloudHub.ServiceImp.OAuth;

namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void RegisterServiceImplementations(this WebApplicationBuilder builder, CloudHubApiConfigurations configurations)
        {
            builder.Services.AddSingleton<IOAuthService, OAuthService>(o => new OAuthService(configurations.GoogleOAuthUrl));
        }
    }
}
