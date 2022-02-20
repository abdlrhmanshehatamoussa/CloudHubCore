using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using CloudHub.Infra.Factories;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.API.Startup
{
    public static class ServiceRegistration
    {
        public static void RegisterDomainServices(this WebApplicationBuilder builder, Configurations configurations)
        {
            //Databases
            builder.Services.AddDbContext<DbContext, PostgreContext>(options => options.UseNpgsql(configurations.MainConnectionString));

            //Services
            builder.Services.AddSingleton<IOAuthService, OAuthService>(o => new OAuthService(configurations.GoogleOAuthUrl));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEnvironmentSettings>(_ => configurations);

            builder.Services.AddScoped<PublicDataService>();
            builder.Services.AddScoped<PrivateDataService>();
            builder.Services.AddScoped<PingService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<NonceService>();
            builder.Services.AddScoped<FeatureService>();
            builder.Services.AddScoped<PurchaseService>();
        }
    }
}
