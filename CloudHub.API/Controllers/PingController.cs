using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("ping")]
    public class PingController : BasicController
    {
        public PingController(APIConfigurations apiSettings)
        {
            this.apiSettings = apiSettings;
        }

        private readonly APIConfigurations apiSettings;


        [HttpGet]
        public dynamic Ping()
        {
            return new
            {
                timestamp = DateTime.Now,
                build_id = apiSettings.BuildId,
                environment = apiSettings.Environment
            };
        }
    }
}