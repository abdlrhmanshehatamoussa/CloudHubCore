using CloudHub.API.Exceptions;

namespace CloudHub.API.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static Configurations InjectConfigurations(this WebApplicationBuilder builder)
        {
            bool isProduction = builder.Configuration.GetOrThrow<bool>("PRODUCTION_MODE");
            string buildId = builder.Configuration.GetOrThrow<string>("BUILD_ID");
            string envName = builder.Configuration.GetOrThrow<string>("ASPNETCORE_ENVIRONMENT");
            string googleTokenInfoApiUrl = builder.Configuration.GetOrThrow<string>("GOOGLE_TOKEN_INFO_API_URL");
            string mainConnectionString = builder.Configuration.GetOrThrow<string>("MAIN_CONNECTION_STRING");
            Configurations configurations = new()
            {
                BuildId = buildId,
                IsProductionModeEnabled = isProduction,
                EnvironmentName = envName,
                GoogleOAuthUrl = googleTokenInfoApiUrl,
                MainConnectionString = mainConnectionString
            };
            builder.Services.AddSingleton(configurations);
            return configurations;
        }


        private static T GetOrThrow<T>(this ConfigurationManager manager, string key)
        {
            return manager.GetValue<T>(key) ?? throw new MissingEnvironmentVariableException(key);
        }
    }
}
