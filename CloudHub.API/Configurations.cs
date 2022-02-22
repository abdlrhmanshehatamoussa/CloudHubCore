using CloudHub.API.Exceptions;
using CloudHub.ServiceProvider;

namespace CloudHub.API
{
    internal class CloudHubApiConfigurations : IConfigOAuthService, IConfigDatabase, IEnvironmentInfo
    {
        public string EnvironmentName { get; set; } = null!;
        public string BuildId { get; set; } = null!;
        public bool IsProduction { get; set; } = true;
        public string ConnectionString { get; set; } = null!;
        public string GoogleOAuthUrl { get; set; } = null!;
    
        public static CloudHubApiConfigurations Load(ConfigurationManager configurationManager)
        {
            T GetOrThrow<T>(string key)
            {
                return configurationManager.GetValue<T>(key) ?? throw new MissingEnvironmentVariableException(key);
            }
            bool isProduction = GetOrThrow<bool>("PRODUCTION_MODE");
            string buildId = GetOrThrow<string>("BUILD_ID");
            string envName = GetOrThrow<string>("ASPNETCORE_ENVIRONMENT");
            string googleTokenInfoApiUrl = GetOrThrow<string>("GOOGLE_TOKEN_INFO_API_URL");
            string mainConnectionString = GetOrThrow<string>("MAIN_CONNECTION_STRING");
            CloudHubApiConfigurations configurations = new()
            {
                BuildId = buildId,
                IsProduction = isProduction,
                EnvironmentName = envName,
                GoogleOAuthUrl = googleTokenInfoApiUrl,
                ConnectionString = mainConnectionString
            };
            return configurations;
        }
    }
}
