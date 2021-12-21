using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [Route("releases")]
    public class ReleasesController : BasicController
    {
        public ReleasesController(ReleaseService releaseService) => _releaseService = releaseService;

        private readonly ReleaseService _releaseService;
        private const string DATE_FORMAT = "yyyy-MM-dd";

        [HttpPost]
        public async Task<dynamic> Save([FromBody] ReleaseCreation request)
        {
            Release release = await _releaseService.Save(ConsumerCredentials, request);
            return new
            {
                inserted = release.Id
            };
        }

        [HttpGet]
        public async Task<dynamic> Fetch()
        {
            List<Release> releases = await _releaseService.Fetch(ConsumerCredentials);
            return releases.Select(r => GetResponse(r));
        }

        [HttpGet]
        [Route("latest")]
        public async Task<dynamic> GetLatest()
        {
            Release? release = await _releaseService.GetLatest(ConsumerCredentials);
            if (release == null) { return null!; }
            return GetResponse(release);
        }

        private dynamic GetResponse(Release release)
        {
            return new
            {
                release_name = release.ReleaseName,
                release_notes = release.Notes,
                release_date = release.ReleaseDate.ToString(DATE_FORMAT)
            };
        }
    }
}
