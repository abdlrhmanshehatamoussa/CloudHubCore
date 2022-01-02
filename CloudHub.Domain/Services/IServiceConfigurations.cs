namespace CloudHub.Domain.Services
{
    public interface IEnvironmentSettings
    {
        public bool IsProductionModeEnabled { get; }
        public string EnvironmentName { get; }
        public string BuildId { get; }
    }
}
