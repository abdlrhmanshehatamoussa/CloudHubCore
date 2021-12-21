using CloudHub.Business.DTO;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Business.Services
{

    public class UserActionService : BaseService
    {
        public UserActionService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<int> SaveActions(ConsumerCredentials credentials, List<UserActionCreationParams> actions)
        {
            ConsumerInfo info = await GetConsumerInfo(credentials);
            int nonceId = info.Nonce?.Id ?? throw new InvalidNonceException();

            //return _unitOfWork.SaveBulk(actions);

            await ConsumeNonce(nonceId);
            await _unitOfWork.Save();
            return 0;
        }
    }
}
