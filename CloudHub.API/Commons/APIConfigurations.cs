using CloudHub.Domain.Services;
using CloudHub.Infra.Services;

namespace CloudHub.API
{
    public class APIConfigurations : IServiceConfigurations, IGoogleServicesConfigurations
    {
        public APIConfigurations(string environment, string buildId, bool isProduction, string connectionString, string googleTokenInfoApiUrl)
        {
            Environment = environment;
            BuildId = buildId;
            ConnectionString = connectionString;
            IsProductionModeEnabled = isProduction;
            GoogleTokenInfoApiUrl = googleTokenInfoApiUrl;
        }
        public string Environment { get; set; }
        public string BuildId { get; set; }
        public string ConnectionString { get; set; }
        public bool IsProductionModeEnabled { get; set; }
        public string GoogleTokenInfoApiUrl { get; set; }
    }
}
