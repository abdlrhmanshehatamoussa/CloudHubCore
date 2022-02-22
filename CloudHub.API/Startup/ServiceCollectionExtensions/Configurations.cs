using CloudHub.ServiceProvider;

namespace CloudHub.API.Startup
{
    internal static partial class ServiceCollectionExtensions
    {
        internal static void InjectConfigurations(this WebApplicationBuilder builder, CloudHubApiConfigurations configurations)
        {
            builder.Services.AddSingleton<IConfigOAuthService>(configurations);
            builder.Services.AddSingleton<IEnvironmentInfo>(configurations);
            builder.Services.AddSingleton<IConfigDatabase>(configurations);
        }
    }
}
