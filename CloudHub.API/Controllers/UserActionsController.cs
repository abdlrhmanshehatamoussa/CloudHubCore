using CloudHub.API.DTO;
using CloudHub.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [Route("actions")]
    public class UserActionsController : BasicController
    {
        public UserActionsController(UserActionService actionService) => _userActionService = actionService;

        private readonly UserActionService _userActionService;

        [HttpPost]
        public async Task<dynamic> SaveActions([FromBody] ActionsSaveRequest request)
        {
            var creationParams = request.actions.Select(a => a.ToObject()).ToList();
            await _userActionService.SaveActions(ConsumerCredentials, creationParams);
            return new
            {
                inserted = 0
            };
        }
    }
}
