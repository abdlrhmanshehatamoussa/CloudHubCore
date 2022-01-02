using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class PurchaseService : BaseService
    {
        public PurchaseService(IUnitOfWork unitOfWork, IEnvironmentSettings productionModeProvider) : base(unitOfWork, productionModeProvider)
        {
        }

        public async Task<List<Purchase>> FetchAll(ConsumerCredentials consumerCredentials)
        {
            ConsumerInfo info = await GetConsumerInfo(consumerCredentials);
            int userId = info.UserToken?.UserId ?? throw new NotAuthenticatedException();
            int nonceId = info.Nonce?.Id ?? throw new InvalidNonceException();

            List<Purchase> purchases = await _unitOfWork.PurchasesRepository.Where(p => p.UserId == userId, p => p.User, p => p.Feature);

            await ConsumeNonce(nonceId);
            await _unitOfWork.Save();

            return purchases;
        }
    }
}
