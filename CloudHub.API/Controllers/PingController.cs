using CloudHub.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("ping")]
    public class PingController : BasicController
    {
        public PingController(CloudHubApiConfigurations configurations) => this.configurations = configurations;

        private readonly CloudHubApiConfigurations configurations;

        [HttpGet]
        public PingResponseContract Ping()
        {
            return new PingResponseContract(
                timestamp: DateTime.Now.ToString(),
                build_id: configurations.BuildId,
                production_mode: configurations.IsProductionModeEnabled,
                environment: configurations.EnvironmentName
                );
        }
    }
}