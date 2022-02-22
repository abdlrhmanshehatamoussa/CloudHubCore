using CloudHub.Domain.Services;
using CloudHub.ServiceProvider;

namespace CloudHub.API.Startup
{
    internal static partial class ServiceCollectionExtensions
    {
        internal static void RegisterIUnitOfWork(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
