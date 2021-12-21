using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("ping")]
    public class PingController : BasicController
    {
        public PingController(APISettings apiSettings, BaseService baseService)
        {
            this.apiSettings = apiSettings;
            BaseService = baseService;
        }

        private readonly APISettings apiSettings;
        private readonly BaseService BaseService;


        [HttpGet]
        public async Task<dynamic> Ping()
        {
            ConsumerInfo info = await BaseService.GetConsumerInfo(ConsumerCredentials);
            return new
            {
                timestamp = DateTime.Now,
                application = info.ClientApplication.Application.Name,
                build_id = apiSettings.BuildId,
                environment = apiSettings.Environment
            };
        }
    }
}