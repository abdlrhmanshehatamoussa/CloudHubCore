using CloudHub.Domain.Services;
using CloudHub.Infra.Services;

namespace CloudHub.API
{
    public class APIConfigurations : IEnvironmentSettings, IGoogleServicesConfigurations
    {
        private APIConfigurations(string envName, string buildId, bool isProduction, string connectionString, string googleTokenInfoApiUrl)
        {
            EnvironmentName = envName;
            BuildId = buildId;
            ConnectionString = connectionString;
            IsProductionModeEnabled = isProduction;
            GoogleTokenInfoApiUrl = googleTokenInfoApiUrl;
        }

        public string EnvironmentName { get; set; }
        public string BuildId { get; set; }
        public string ConnectionString { get; set; }
        public bool IsProductionModeEnabled { get; set; }
        public string GoogleTokenInfoApiUrl { get; set; }

        private const string KEY_CONNECTION_STRING = "API_DATABASE";
        private const string KEY_BUILD_ID = "BUILD_ID";
        private const string KEY_PROD_MODE = "PRODUCTION_MODE";
        private const string KEY_ENV_NAME = "ASPNETCORE_ENVIRONMENT";
        private const string KEY_GOOGLE_TOKEN_URL = "GOOGLE_TOKEN_INFO_API_URL";
        
        public static APIConfigurations Load()
        {
            try
            {
                return FromEnvironment();
            }
            catch
            {
                return FromJson();
            }
        }

        private static APIConfigurations FromEnvironment()
        {
            bool isProduction = bool.Parse(GetEnvVar(KEY_PROD_MODE));
            string buildId = GetEnvVar(KEY_BUILD_ID);
            string envName = GetEnvVar(KEY_ENV_NAME);
            string connectionString = GetEnvVar(KEY_CONNECTION_STRING);
            string googleTokenInfoApiUrl = GetEnvVar(KEY_GOOGLE_TOKEN_URL);
            return new(envName, buildId, isProduction, connectionString, googleTokenInfoApiUrl);
        }

        private static APIConfigurations FromJson()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            bool isProduction = configuration.GetValue<bool>(KEY_PROD_MODE);
            string buildId = configuration.GetValue<string>(KEY_BUILD_ID);
            string envName = configuration.GetValue<string>(KEY_ENV_NAME);
            string connectionString = configuration.GetValue<string>(KEY_CONNECTION_STRING);
            string googleTokenInfoApiUrl = configuration.GetValue<string>(KEY_GOOGLE_TOKEN_URL);
            return new(envName, buildId, isProduction, connectionString, googleTokenInfoApiUrl);
        }

        private static string GetEnvVar(string var, string? defaultValue = null)
        {
            string? value = Environment.GetEnvironmentVariable(var);
            if (value != null) { return value; }
            if (defaultValue != null) { return defaultValue; }
            throw new MissingEnvironmentVariableException(var);
        }
    }
}
