using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void ApplyDbMigrations(this WebApplicationBuilder builder, Configurations configurations)
        {
            IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
            IServiceScopeFactory serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>() ?? throw new Exception("Error while applying migrations, Failed to create Service Scope Factory");
            IServiceScope serviceScope = serviceScopeFactory.CreateScope();
            PostgreContext context = serviceScope.ServiceProvider.GetService<PostgreContext>() ?? throw new Exception("Error while applying migrations, Failed to get DbContext");
            int migrationsCount = context.Database.GetAppliedMigrations().Count();
            if (migrationsCount == 0)
            {
                context.Database.Migrate();
            }
        }
    }
}
