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
            Consumer info = await GetConsumer(consumerCredentials);
            int userId = info.UserToken?.User.Id ?? throw new NotAuthenticatedException();
            int nonceId = info.Nonce.Id;

            List<Purchase> purchases = await _unitOfWork.PurchasesRepository.Where(p => p.UserId == userId, p => p.User, p => p.Feature);

            await ConsumeNonceOrThrow(nonceId);
            await _unitOfWork.Save();

            return purchases;
        }
    }
}
