using CloudHub.BusinessLogic.DTO;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudHub.API.Commons
{
    public class ConsumerCredentialsFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string clientKey = context.HttpContext.Request.Headers[Constants.HEADERS_CLIENT_KEY];
            string? nonce = context.HttpContext.Request.Headers[Constants.HEADERS_NONCE];
            string? userToken = context.HttpContext.Request.Headers[Constants.HEADERS_USER_TOKEN];
            string? clientClaim = context.HttpContext.Request.Headers[Constants.HEADERS_CLIENT_CLAIM];

            ConsumerCredentials credentials = new()
            {
                ClientKey = clientKey,
                Nonce = nonce,
                UserToken = userToken,
                ClientClaim = clientClaim
            };
            context.HttpContext.Items[Constants.ITEMS_CONSUMER_CREDENTIALS] = credentials;
        }
    }
}
