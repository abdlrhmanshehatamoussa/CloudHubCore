using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void RegisterDbContext(this WebApplicationBuilder builder, Configurations configurations)
        {
            builder.Services.AddDbContext<DbContext, PostgreContext>(options => options.UseNpgsql(configurations.MainConnectionString));
        }
    }
}
