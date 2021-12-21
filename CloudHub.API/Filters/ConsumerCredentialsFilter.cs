using CloudHub.Business.DTO;
using CloudHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudHub.API.Filters
{
    public class ConsumerCredentialsFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string clientKey = context.HttpContext.Request.Headers[Constants.HEADERS_CLIENT_KEY];
            string applicationGuid = context.HttpContext.Request.Headers[Constants.HEADERS_APP_GUID];
            string nonce = context.HttpContext.Request.Headers[Constants.HEADERS_NONCE];
            string userToken = context.HttpContext.Request.Headers[Constants.HEADERS_USER_TOKEN];

            if (string.IsNullOrWhiteSpace(clientKey) || string.IsNullOrWhiteSpace(applicationGuid)) { throw new NotAuthenticatedException(); }

            ConsumerCredentials credentials = new ConsumerCredentials()
            {
                ApplicationGuid = applicationGuid,
                ClientKey = clientKey,
                Nonce = nonce,
                UserToken = userToken
            };

            context.HttpContext.Items[Constants.HEADERS_ITEMS_CONSUMER_CREDENTIALS] = credentials;
        }
    }
}
