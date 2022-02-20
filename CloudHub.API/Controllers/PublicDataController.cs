using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("data/public")]
    public class PublicDataController : BasicController
    {
        public PublicDataController(PublicDataService _service) => service = _service;

        private readonly PublicDataService service;


        [HttpGet]
        [Route("{collection}")]
        public async Task<dynamic> FetchAll(string collection)
        {
            if (collection == null) { throw new MissingParameterException("collection"); }
            List<PublicDocument> results = await service.FetchAll(ConsumerCredentials, collection);
            return results.Select(r => r.Body.RootElement).ToList();
        }

        [HttpPatch]
        [Route("{collection}")]
        public async Task<dynamic> AddBulk(string collection, [FromBody] List<dynamic> data)
        {
            if (string.IsNullOrWhiteSpace(collection)) { throw new MissingParameterException("collection"); }
            await service.AddBulk(ConsumerCredentials, collection, data);
            throw new EmptyResponseException();
        }

        [HttpPost]
        [Route("{collection}")]
        public async Task<dynamic> Add(string collection, [FromBody] dynamic data)
        {
            if (string.IsNullOrWhiteSpace(collection)) { throw new MissingParameterException("collection"); }
            await service.Add(ConsumerCredentials, collection, data);
            throw new EmptyResponseException();
        }
    }
}
