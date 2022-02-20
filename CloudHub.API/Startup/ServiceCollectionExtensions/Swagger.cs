namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static void ConfigureSwaggerServices(this WebApplicationBuilder builder, Configurations settings)
        {
            if (settings.IsProductionModeEnabled == false)
            {
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }
        }
    }
}
