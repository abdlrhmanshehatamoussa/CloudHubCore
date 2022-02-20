using CloudHub.API.Contracts;
using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [Route("nonce")]
    public class NoncesController : BasicController
    {
        public NoncesController(NonceService nonceService) => this._nonceService = nonceService;

        private readonly NonceService _nonceService;


        [HttpPost]
        public async Task<NonceResponse> Post()
        {
            Nonce nonce = await _nonceService.GenereateNonce(ConsumerCredentials);
            return NonceResponse.FromNonce(nonce);
        }
    }
}
