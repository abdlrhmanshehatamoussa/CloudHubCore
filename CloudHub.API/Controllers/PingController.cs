using CloudHub.ApiContracts;
using CloudHub.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("ping")]
    public class PingController : BasicController
    {
        public PingController(IEnvironmentSettings envSettings)
        {
            this.envSettings = envSettings;
        }

        private readonly IEnvironmentSettings envSettings;


        [HttpGet]
        public PingResponseContract Ping()
        {
            return new()
            {
                timestamp = DateTime.Now.ToLongTimeString(),
                build_id = envSettings.BuildId,
                production_mode=envSettings.IsProductionModeEnabled,
                environment = envSettings.EnvironmentName
            };
        }
    }
}