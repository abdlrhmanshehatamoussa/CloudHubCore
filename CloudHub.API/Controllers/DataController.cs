using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("data")]
    public class DataController : BasicController
    {
        public DataController(DataService privateDataService) => _dataService = privateDataService;

        private readonly DataService _dataService;


        [HttpGet]
        [Route("{collection}")]
        public async Task<dynamic> FetchAll(string collection)
        {
            if (collection == null) { throw new MissingParameterException("collection"); }
            var results = await _dataService.FetchAll(ConsumerCredentials, collection);
            return results;
        }
    }
}
