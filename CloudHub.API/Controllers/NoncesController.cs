using CloudHub.Business.Services;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [Route("nonce")]
    public class NoncesController : BasicController
    {
        public NoncesController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        private NonceService nonceService => new NonceService(unitOfWork);
        
        [HttpPost]
        public async Task<dynamic> Post()
        {
            Nonce nonce = await nonceService.GenereateNonce(ClientCredentials);
            return new { 
                token = nonce.Token,
                created_on = nonce.CreatedOn.ToString()
            };
        }
    }
}
