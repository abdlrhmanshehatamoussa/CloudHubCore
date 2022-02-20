using CloudHub.API.Contracts;
using CloudHub.Domain.DTO;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("ping")]
    public class PingController : BasicController
    {
        public PingController(PingService pingService) => this.pingService = pingService;
        
        private readonly PingService pingService;

        [HttpGet]
        public PingResponseContract Ping()
        {
            PingDTO dto = pingService.Ping();
            PingResponseContract contract = PingResponseContract.FromDTO(dto);
            return contract;
        }
    }
}