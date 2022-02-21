using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using CloudHub.Infra.Factories;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void RegisterDbContext(this WebApplicationBuilder builder, CloudHubApiConfigurations configurations)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<DbContext, PostgreContext>(options => options.UseNpgsql(configurations.MainConnectionString));
        }
    }
}
