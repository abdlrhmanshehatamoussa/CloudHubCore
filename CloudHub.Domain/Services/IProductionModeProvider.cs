namespace CloudHub.Domain.Services
{
    public interface IProductionModeProvider
    {
        bool IsProductionModeEnabled { get; }
    }
}
