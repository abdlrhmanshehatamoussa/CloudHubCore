namespace CloudHub.Domain.DTO
{
    public interface IEnvironmentSettings
    {
        public bool IsProductionModeEnabled { get; }
        public string EnvironmentName { get; }
        public string BuildId { get; }
    }
}
