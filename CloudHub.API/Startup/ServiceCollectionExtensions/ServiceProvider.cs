using CloudHub.Domain.Services;
using CloudHub.ServiceProvider;

namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void RegisterServiceImplementations(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IOAuthService, OAuthService>();
        }
    }
}
