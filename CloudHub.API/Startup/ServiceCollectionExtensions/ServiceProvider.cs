using CloudHub.Domain.Services;
using CloudHub.ServiceProvider;

namespace CloudHub.API.Startup
{
    internal static partial class ServiceCollectionExtensions
    {
        internal static void RegisterServiceProviders(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IOAuthService, OAuthService>();
            builder.Services.AddSingleton<IEncryptionService, EncryptionService>();
        }
    }
}
