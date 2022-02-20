namespace CloudHub.API.Startup
{
    public static class SwaggerConfigurations
    {
        public static void ConfigureSwagger(this WebApplication app, Configurations settings)
        {
            if (settings.IsProductionModeEnabled == false)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }

        public static void ConfigureSwaggerServices(this WebApplicationBuilder builder, Configurations settings)
        {
            //Swagger
            if (settings.IsProductionModeEnabled == false)
            {
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }
        }
    }
}
