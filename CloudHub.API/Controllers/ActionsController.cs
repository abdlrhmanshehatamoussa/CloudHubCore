using CloudHub.API.DTO;
using CloudHub.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [Route("actions")]
    public class ActionsController : BasicController
    {
        public ActionsController(UserActionService actionService) => _userActionService = actionService;

        private readonly UserActionService _userActionService;

        [HttpPost]
        public async Task<dynamic> SaveActions([FromBody] ActionsSaveRequest request)
        {
            var userActions = request.actions.Select(a => a.ToObject()).ToList();
            int count = await _userActionService.SaveActions(ConsumerCredentials, userActions);
            return new
            {
                inserted = count
            };
        }
    }
}
