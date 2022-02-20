using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [Route("purchases")]
    public class PurchasesController : BasicController
    {

        public PurchasesController(PurchaseService purchaseService) => _purchaseService = purchaseService;

        private readonly PurchaseService _purchaseService;


        [HttpGet]
        public async Task<dynamic> FetchAll()
        {
            List<Purchase> purchases = await _purchaseService.FetchAll(ConsumerCredentials);
            return purchases.Select(p => new
            {
                feature_id = p.Feature.GlobalId,
                user_id = p.User.GlobalId,
                purchased_on = p.CreatedOn,
            });
        }

        //TODO: Implement Purchase Endpoint
        [HttpPost]
        public Task<dynamic> Purchase()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("validate")]
        public Task<dynamic> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
