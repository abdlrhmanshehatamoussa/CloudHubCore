using CloudHub.API.Domain.Models;
using CloudHub.API.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [Route("features")]
    public class FeaturesController : BasicController
    {
        public FeaturesController(FeatureService actionService) => _featureService = actionService;

        private readonly FeatureService _featureService;

        [HttpGet]
        public async Task<dynamic> Fetch()
        {
            List<Feature> features = await _featureService.Fetch(ConsumerCredentials);
            return features.Select(f => new
            {
                feature_name = f.Name,
                feature_description = f.Description,
                feature_id = f.GlobalId,
                active = f.Active,
            });
        }
    }
}
