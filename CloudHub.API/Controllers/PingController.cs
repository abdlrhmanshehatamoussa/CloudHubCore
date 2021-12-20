using CloudHub.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("ping")]
    public class PingController : BasicController
    {
        private readonly APISettings apiSettings;

        public PingController(IUnitOfWork unitOfWork, APISettings apiSettings) : base(unitOfWork) => this.apiSettings = apiSettings;

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