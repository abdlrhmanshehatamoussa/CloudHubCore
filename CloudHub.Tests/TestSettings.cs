using CloudHub.Domain.Services;

namespace CloudHub.Tests
{
    public class TestSettings : IEnvironmentSettings
    {
        public bool IsProductionModeEnabled => false;

        public string EnvironmentName => "Local-Test";

        public string BuildId => "0.0.0";
    }
}
