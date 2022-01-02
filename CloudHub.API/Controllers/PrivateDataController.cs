using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("data/private")]
    public class PrivateDataController : BasicController
    {
        public PrivateDataController(PrivateDataService privateDataService) => _privateDataService = privateDataService;

        private readonly PrivateDataService _privateDataService;


        [HttpGet]
        [Route("{collection}")]
        public async Task<dynamic> FetchAll(string collection)
        {
            if (collection == null) { throw new MissingParameterException("collection"); }
            var results = await _privateDataService.FetchAll(ConsumerCredentials, collection);
            return results;
        }
    }
}
