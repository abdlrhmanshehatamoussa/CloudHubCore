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


        public static APIConfigurations FromEnvironment()
        {
            bool isProduction = bool.Parse(GetEnvVar("PRODUCTION_MODE"));
            string buildId = GetEnvVar("BUILD_ID");
            string envName = GetEnvVar("ASPNETCORE_ENVIRONMENT");
            string connectionString = GetEnvVar("API_DATABASE");
            string googleTokenInfoApiUrl = GetEnvVar("GOOGLE_TOKEN_INFO_API_URL");
            return new(envName, buildId, isProduction, connectionString, googleTokenInfoApiUrl);
        }

        public static APIConfigurations Local()
        {
            bool isProduction = false;
            string buildId = "0.0.0";
            string envName = "Local";
            string connectionString = "Host=127.0.0.1;Database=cloudhub-api-core-local;Username=postgres;Password=123456";
            string googleTokenInfoApiUrl = "";
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
