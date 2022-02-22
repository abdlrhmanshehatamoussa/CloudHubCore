using CloudHub.Domain.Services;
using CloudHub.ServiceProvider;

namespace CloudHub.API.Startup
{
    internal static partial class ServiceCollectionExtensions
    {
        internal static void RegisterServiceImplementations(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IOAuthService, OAuthService>();
        }
    }
}
