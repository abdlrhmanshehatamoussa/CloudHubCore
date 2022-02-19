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
        public dynamic Ping()
        {
            return new
            {
                timestamp = DateTime.Now,
                build_id = envSettings.BuildId,
                production_mode=envSettings.IsProductionModeEnabled,
                environment = envSettings.EnvironmentName
            };
        }
    }
}