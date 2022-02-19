using CloudHub.API.Commons;
using CloudHub.BusinessLogic.DTO;
using CloudHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    public class BasicController : ControllerBase
    {
        protected ConsumerCredentials ConsumerCredentials
        {
            get
            {
                return (ConsumerCredentials?)HttpContext.Items[Constants.ITEMS_CONSUMER_CREDENTIALS] ?? throw new NotAuthenticatedException();
            }
        }
    }
}
