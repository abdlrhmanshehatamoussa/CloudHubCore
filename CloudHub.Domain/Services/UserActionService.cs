using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{

    public class UserActionService : BaseService
    {
        public UserActionService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task SaveActions(ConsumerCredentials credentials, List<UserActionCreation> creationParams)
        {
            ConsumerInfo info = await GetConsumerInfo(credentials);
            int nonceId = info.Nonce?.Id ?? throw new InvalidNonceException();
            int applicationID = info.ClientApplication.ApplicationId;

            List<UserAction> userActions = creationParams.Select(c => c.ToModel(applicationID)).ToList();

            await _unitOfWork.UserActionsRepository.SaveBulk(userActions);
            await ConsumeNonce(nonceId);
            await _unitOfWork.Save();
        }
    }
}
