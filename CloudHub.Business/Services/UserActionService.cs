using CloudHub.Business.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Business.Services
{

    public class UserActionService : BaseService
    {
        public UserActionService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task SaveActions(ConsumerCredentials credentials, List<UserActionCreationParams> creationParams)
        {
            ConsumerInfo info = await GetConsumerInfo(credentials);
            int nonceId = info.Nonce?.Id ?? throw new InvalidNonceException();

            List<UserAction> userActions = creationParams.Select(c => c.ToModel(info.ClientApplication.ApplicationId)).ToList();

            await _unitOfWork.UserActionsRepository.SaveBulk(userActions);
            //await ConsumeNonce(nonceId);
            await _unitOfWork.Save();
        }
    }
}
