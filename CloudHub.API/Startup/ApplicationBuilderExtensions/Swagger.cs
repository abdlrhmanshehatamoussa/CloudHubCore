﻿namespace CloudHub.API.Startup
{
    public static partial class ApplicationBuilderExtensions
    {
        public static void ConfigureSwagger(this WebApplication app, CloudHubApiConfigurations settings)
        {
            if (settings.IsProductionModeEnabled == false)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
