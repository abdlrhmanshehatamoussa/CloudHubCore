namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void ConfigureSwaggerServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
