using CloudHub.Domain.Models;

namespace CloudHub.Tests.Factories
{
    internal class TestSettings : IEnvironmentSettings
    {
        public bool IsProductionModeEnabled => false;

        public string EnvironmentName => "Local-Test";

        public string BuildId => "0.0.0";
    }
}
