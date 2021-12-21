using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class ReleaseService : BaseService
    {
        public ReleaseService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Release> Save(ConsumerCredentials consumerCredentials, ReleaseCreation request)
        {
            ConsumerInfo info = await GetConsumerInfo(consumerCredentials);
            int nonceId = info.Nonce?.Id ?? throw new InvalidNonceException();
            int applicationId = info.ClientApplication.ApplicationId;

            bool existing = await _unitOfWork.ReleasesRepository.Any(r => r.ApplicationId == applicationId && r.ReleaseName == request.release_name);
            if (existing) { throw new UnprocessableEntityException(); }

            Release release = await _unitOfWork.ReleasesRepository.Add(request.ToModel(applicationId));

            await ConsumeNonce(nonceId);
            await _unitOfWork.Save();

            return release;
        }

        public async Task<List<Release>> Fetch(ConsumerCredentials consumerCredentials)
        {
            ConsumerInfo info = await GetConsumerInfo(consumerCredentials);
            int nonceId = info.Nonce?.Id ?? throw new InvalidNonceException();
            List<Release> result = await _unitOfWork.ReleasesRepository.Where(r => r.ApplicationId == info.ClientApplication.ApplicationId);

            await ConsumeNonce(nonceId);
            await _unitOfWork.Save();

            return result;
        }

        public async Task<Release?> GetLatest(ConsumerCredentials consumerCredentials)
        {
            ConsumerInfo info = await GetConsumerInfo(consumerCredentials);
            int nonceId = info.Nonce?.Id ?? throw new InvalidNonceException();

            List<Release> releases = await _unitOfWork.ReleasesRepository.Where(r => r.ApplicationId == info.ClientApplication.ApplicationId);
            Release? latestRelease = null!;
            if (releases.Count() > 0)
            {
                releases = releases.OrderByDescending(r => r.ReleaseDate).ToList();
                latestRelease = releases.First();
            }

            await ConsumeNonce(nonceId);
            await _unitOfWork.Save();

            return latestRelease;
        }
    }
}