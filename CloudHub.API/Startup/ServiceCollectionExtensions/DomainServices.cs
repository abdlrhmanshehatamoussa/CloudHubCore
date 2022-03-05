using CloudHub.Domain.Services;

namespace CloudHub.API.Startup
{
    internal static partial class ServiceCollectionExtensions
    {
        internal static void RegisterDomainServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<PublicDataService>();
            builder.Services.AddScoped<PrivateDataService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<NonceService>();
            builder.Services.AddScoped<FeatureService>();
            builder.Services.AddScoped<EventsService>();
            builder.Services.AddScoped<PurchaseService>();
        }
    }
}
