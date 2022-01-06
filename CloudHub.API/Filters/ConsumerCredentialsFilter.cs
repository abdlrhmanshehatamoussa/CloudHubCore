using CloudHub.Domain.DTO;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http.Headers;
using System.Text;

namespace CloudHub.API.Filters
{
    public class ConsumerCredentialsFilter : IAuthorizationFilter
    {
        private AdminCredentials? ExtractAdminCredentials(string header)
        {
            AdminCredentials? adminCredentials = null;
            if (string.IsNullOrWhiteSpace(header)) { return null; }
            try
            {
                AuthenticationHeaderValue headerValueObj = AuthenticationHeaderValue.Parse(header);
                if (headerValueObj.Parameter == null) { return null; }
                byte[] credentialsBytes = Convert.FromBase64String(headerValueObj.Parameter);
                string credentialsStr = Encoding.UTF8.GetString(credentialsBytes);
                string[] credentialsSplitted = credentialsStr.Split(":");
                adminCredentials = new()
                {
                    UserName = credentialsSplitted[0],
                    Password = credentialsSplitted[1]
                };
            }
            catch { }
            return adminCredentials;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string clientKey = context.HttpContext.Request.Headers[Constants.HEADERS_CLIENT_KEY];
            string? nonce = context.HttpContext.Request.Headers[Constants.HEADERS_NONCE];
            string? userToken = context.HttpContext.Request.Headers[Constants.HEADERS_USER_TOKEN];
            string? adminAuth = context.HttpContext.Request.Headers[Constants.HEADERS_ADMIN_AUTH];


            ConsumerCredentials credentials = new()
            {
                ClientKey = clientKey,
                Nonce = nonce,
                UserToken = userToken,
                AdminCredentials = ExtractAdminCredentials(adminAuth)
            };

            context.HttpContext.Items[Constants.HEADERS_ITEMS_CONSUMER_CREDENTIALS] = credentials;
        }
    }
}
