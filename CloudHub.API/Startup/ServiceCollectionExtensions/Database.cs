using CloudHub.Domain.Services;
using CloudHub.ServiceProvider;
using CloudHub.ServiceProvider.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void RegisterIUnitOfWork(this WebApplicationBuilder builder, CloudHubApiConfigurations configurations)
        {
            builder.Services.AddDbContext<DbContext, MyContext>(options => options.UseNpgsql(configurations.MainConnectionString));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Migration
            IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
            MyContext context = serviceProvider.GetService<MyContext>() ?? throw new Exception("Error while applying migrations, Failed to get PostgreContext");
            int migrationsCount = context.Database.GetAppliedMigrations().Count();
            if (migrationsCount == 0) context.Database.Migrate();
        }
    }
}
