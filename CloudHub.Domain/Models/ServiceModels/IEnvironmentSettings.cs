namespace CloudHub.Domain.Models
{
    public interface IEnvironmentSettings
    {
        public bool IsProductionModeEnabled { get; }
        public string EnvironmentName { get; }
        public string BuildId { get; }
    }
}
