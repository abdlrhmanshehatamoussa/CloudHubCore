namespace CloudHub.Domain.Services
{
    public interface IServiceConfigurations
    {
        bool IsProductionModeEnabled { get; }
    }
}
