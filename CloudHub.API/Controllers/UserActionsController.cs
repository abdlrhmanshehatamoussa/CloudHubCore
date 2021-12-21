using CloudHub.Domain.DTO;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [Route("actions")]
    public class UserActionsController : BasicController
    {
        public UserActionsController(UserActionService actionService) => _userActionService = actionService;

        private readonly UserActionService _userActionService;

        [HttpPost]
        public async Task<dynamic> SaveActions([FromBody] UserActionCreationArrayWrapper request)
        {
            await _userActionService.SaveActions(ConsumerCredentials, request.actions);
            return new
            {
                inserted = request.actions.Count()
            };
        }
    }
    public struct UserActionCreationArrayWrapper
    {
        public List<UserActionCreation> actions { get; set; }
    }
}
