namespace CloudHub.API
{
    public class CloudHubApiConfigurations
    {
        public string EnvironmentName { get; set; } = null!;
        public string BuildId { get; set; } = null!;
        public string MainConnectionString { get; set; } = null!;
        public bool IsProductionModeEnabled { get; set; } = false;
        public string GoogleOAuthUrl { get; set; } = null!;
    }
}
