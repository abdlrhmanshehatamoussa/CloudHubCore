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

        protected ClientCredentials ClientCredentials
        {
            get
            {
                string clientKey = HttpContext.Request.Headers[Constants.HEADERS_CLIENT_KEY];
                string applicationGuid = HttpContext.Request.Headers[Constants.HEADERS_APP_GUID];
                string nonce = HttpContext.Request.Headers[Constants.HEADERS_NONCE];

                if (string.IsNullOrWhiteSpace(clientKey) || string.IsNullOrWhiteSpace(applicationGuid)) { throw new NotAuthenticatedException(); }

                return new ClientCredentials(
                    applicationGuid: applicationGuid,
                    clientKey: clientKey,
                    nonce: nonce
                );
            }
        }

        //protected UserCredentials UserCredentials
        //{
        //    get
        //    {
        //        string clientKey = HttpContext.Request.Headers[Constants.HEADERS_CLIENT_KEY];
        //        string applicationGuid = HttpContext.Request.Headers[Constants.HEADERS_APP_GUID];
        //        string nonce = HttpContext.Request.Headers[Constants.HEADERS_NONCE];

        //        if (string.IsNullOrWhiteSpace(clientKey) || string.IsNullOrWhiteSpace(applicationGuid)) { throw new NotAuthenticatedException(); }

        //        //return new UserCredentials(applicationGuid, clientKey, nonce);
        //    }
        //}
    }
}
