using CloudHub.ServiceProvider;

namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void InjectConfigurations(this WebApplicationBuilder builder, CloudHubApiConfigurations configurations)
        {
            builder.Services.AddSingleton<IConfigOAuthService>(configurations);
            builder.Services.AddSingleton<IEnvironmentInfo>(configurations);
        }
    }
}
