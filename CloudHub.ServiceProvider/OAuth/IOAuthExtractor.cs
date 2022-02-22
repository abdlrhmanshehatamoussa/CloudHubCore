using CloudHub.Domain.Models;

namespace CloudHub.ServiceProvider.OAuth
{
    internal interface IOAuthExtractor
    {
        public string BuildURL(string token);
        public OAuthUser ExtractUser(string bodyJson);
    }
}
