using CloudHub.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("ping")]
    public class PingController : BasicController
    {
        public PingController(IEnvironmentInfo env) => _env = env;

        private readonly IEnvironmentInfo _env;

        [HttpGet]
        public PingResponseContract Ping()
        {
            return new PingResponseContract(
                timestamp: DateTime.Now.ToString(),
                build_id: _env.BuildId,
                production_mode: _env.IsProduction,
                environment: _env.EnvironmentName);
        }
    }
}