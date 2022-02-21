using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void ApplyDbMigrations(this WebApplicationBuilder builder)
        {
            IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
            PostgreContext context = serviceProvider.GetService<PostgreContext>() ?? throw new Exception("Error while applying migrations, Failed to get PostgreContext");
            int migrationsCount = context.Database.GetAppliedMigrations().Count();
            if (migrationsCount == 0)
            {
                context.Database.Migrate();
            }
        }
    }
}
