using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("data/public")]
    public class PublicDataController : BasicController
    {
        public PublicDataController(PublicDataService publicDataService) => _publicDataService = publicDataService;

        private readonly PublicDataService _publicDataService;


        [HttpGet]
        [Route("{collection}")]
        public async Task<dynamic> FetchAll(string collection)
        {
            if (collection == null) { throw new MissingParameterException("collection"); }
            var results = await _publicDataService.FetchAll(ConsumerCredentials, collection);
            return results;
        }
    }
}
