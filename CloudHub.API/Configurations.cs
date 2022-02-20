using CloudHub.API.Exceptions;

namespace CloudHub.API
{
    public class Configurations
    {
        public string EnvironmentName { get; private set; } = null!;
        public string BuildId { get; private set; } = null!;
        public string MainConnectionString { get; private set; } = null!;
        public bool IsProductionModeEnabled { get; private set; } = false;
        public string GoogleOAuthUrl { get; private set; } = null!;


        private const string KEY_BUILD_ID = "BUILD_ID";
        private const string KEY_PROD_MODE = "PRODUCTION_MODE";
        private const string KEY_ENV_NAME = "ASPNETCORE_ENVIRONMENT";
        private const string KEY_GOOGLE_OAUTH_URL = "GOOGLE_TOKEN_INFO_API_URL";
        private const string KEY_MAIN_CONN_STR = "MAIN_CONNECTION_STRING";


        public static Configurations Load(string jsonFile)
        {
            try
            {
                return FromEnvironment();
            }
            catch
            {
                return FromJson(jsonFile);
            }
        }

        private static Configurations FromEnvironment()
        {
            bool isProduction = bool.Parse(GetEnvVar(KEY_PROD_MODE));
            string buildId = GetEnvVar(KEY_BUILD_ID);
            string envName = GetEnvVar(KEY_ENV_NAME);
            string googleTokenInfoApiUrl = GetEnvVar(KEY_GOOGLE_OAUTH_URL);
            string connectionString = GetEnvVar(KEY_MAIN_CONN_STR);
            return new()
            {
                BuildId = buildId,
                IsProductionModeEnabled = isProduction,
                EnvironmentName = envName,
                GoogleOAuthUrl = googleTokenInfoApiUrl,
                MainConnectionString = connectionString
            };
        }

        private static Configurations FromJson(string jsonFile)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(jsonFile);
            IConfigurationRoot configuration = builder.Build();
            bool isProduction = configuration.GetValue<bool>(KEY_PROD_MODE);
            string buildId = configuration.GetValue<string>(KEY_BUILD_ID);
            string envName = configuration.GetValue<string>(KEY_ENV_NAME);
            string googleTokenInfoApiUrl = configuration.GetValue<string>(KEY_GOOGLE_OAUTH_URL);
            string mainConnectionString = configuration.GetValue<string>(KEY_MAIN_CONN_STR);
            return new()
            {
                BuildId = buildId,
                IsProductionModeEnabled = isProduction,
                EnvironmentName = envName,
                GoogleOAuthUrl = googleTokenInfoApiUrl,
                MainConnectionString = mainConnectionString
            };
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
