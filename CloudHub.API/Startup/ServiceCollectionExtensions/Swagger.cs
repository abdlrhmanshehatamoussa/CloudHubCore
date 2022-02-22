namespace CloudHub.API.Startup
{
    internal static partial class ServiceCollectionExtensions
    {
        internal static void ConfigureSwaggerServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
