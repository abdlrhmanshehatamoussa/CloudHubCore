using CloudHub.Domain.Services;

namespace CloudHub.API
{
    public class APISettings: IProductionModeProvider
    {
        public APISettings(string environment, string buildId, bool isProduction, string connectionString)
        {
            Environment = environment;
            BuildId = buildId;
            IsProduction = isProduction;
            ConnectionString = connectionString;
        }

        public string Environment { get; set; }
        public string BuildId { get; set; }
        public bool IsProduction { get; set; }
        public string ConnectionString { get; set; }

        public bool IsProductionModeEnabled => IsProduction;
    }
}
