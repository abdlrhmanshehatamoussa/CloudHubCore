using CloudHub.API.Contracts;
using CloudHub.Domain.DTO;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController : BasicController
    {
        public EventsController(EventsService _service) => service = _service;

        private readonly EventsService service;

        [HttpPost]
        public async Task LogSingle([FromBody] CreateEventRequest request)
        {
            CreateEventDTO dto = request.ToDTO();
            await service.LogSingle(ConsumerCredentials, dto);
            throw new EmptyResponseException();
        }
        [HttpPatch]
        public async Task LogBulk([FromBody] List<CreateEventRequest> requests)
        {
            List<CreateEventDTO> dtos = requests.Select(e => e.ToDTO()).ToList();
            await service.LogBulk(ConsumerCredentials, dtos);
            throw new EmptyResponseException();
        }
    }
}
