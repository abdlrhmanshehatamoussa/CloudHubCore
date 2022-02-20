using CloudHub.Domain.Services;

namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void RegisterDomainServices(this WebApplicationBuilder builder, Configurations configurations)
        {
            builder.Services.AddScoped<PublicDataService>();
            builder.Services.AddScoped<PrivateDataService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<NonceService>();
            builder.Services.AddScoped<FeatureService>();
            builder.Services.AddScoped<PurchaseService>();
        }
    }
}
