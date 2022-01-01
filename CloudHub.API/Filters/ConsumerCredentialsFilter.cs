using CloudHub.Domain.DTO;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudHub.API.Filters
{
    public class ConsumerCredentialsFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string clientKey = context.HttpContext.Request.Headers[Constants.HEADERS_CLIENT_KEY];
            string nonce = context.HttpContext.Request.Headers[Constants.HEADERS_NONCE];
            string userToken = context.HttpContext.Request.Headers[Constants.HEADERS_USER_TOKEN];


            ConsumerCredentials credentials = new ()
            {
                ClientKey = clientKey,
                Nonce = nonce,
                UserToken = userToken
            };

            context.HttpContext.Items[Constants.HEADERS_ITEMS_CONSUMER_CREDENTIALS] = credentials;
        }
    }
}
