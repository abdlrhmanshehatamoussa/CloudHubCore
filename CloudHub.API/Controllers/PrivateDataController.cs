using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("data/private")]
    public class PrivateDataController : BasicController
    {
        public PrivateDataController(PrivateDataService _service) => service = _service;

        private readonly PrivateDataService service;


        [HttpGet]
        [Route("{collection}")]
        public async Task<dynamic> FetchAll(string collection)
        {
            if (collection == null) { throw new MissingParameterException("collection"); }
            List<PrivateDocument> results = await service.FetchAll(ConsumerCredentials, collection);
            return results.Select(r => r.Body.RootElement).ToList();
        }

        [HttpPost]
        [Route("{collection}")]
        public Task<dynamic> Add(string collection, [FromBody] dynamic data)
        {
            if (string.IsNullOrWhiteSpace(collection)) { throw new MissingParameterException("collection"); }
            throw new NotImplementedException();
        }
    }
}
