using CloudHub.Business.DTO;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    public class BasicController : ControllerBase
    {
        public BasicController(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        protected readonly IUnitOfWork unitOfWork;

        protected ConsumerCredentials ClientCredentials
        {
            get
            {
                string clientKey = HttpContext.Request.Headers[Constants.HEADERS_CLIENT_KEY];
                string applicationGuid = HttpContext.Request.Headers[Constants.HEADERS_APP_GUID];
                string nonce = HttpContext.Request.Headers[Constants.HEADERS_NONCE];
                string userToken = HttpContext.Request.Headers[Constants.HEADERS_USER_TOKEN];

                if (string.IsNullOrWhiteSpace(clientKey) || string.IsNullOrWhiteSpace(applicationGuid)) { throw new NotAuthenticatedException(); }

                return new ConsumerCredentials()
                {
                    ApplicationGuid = applicationGuid,
                    ClientKey = clientKey,
                    Nonce = nonce,
                    UserToken = userToken
                };
            }
        }
    }
}
