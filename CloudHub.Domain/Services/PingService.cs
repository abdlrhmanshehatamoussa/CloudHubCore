using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;

namespace CloudHub.Domain.Services
{
    public class PingService
    {
        public PingService(IEnvironmentSettings evnSettings) => this._envSettings = evnSettings;

        protected readonly IEnvironmentSettings _envSettings;

        public PingDTO Ping()
        {
            string timestamp = DateTime.Now.ToString();
            return new PingDTO(timestamp, _envSettings);
        }

    }
}
