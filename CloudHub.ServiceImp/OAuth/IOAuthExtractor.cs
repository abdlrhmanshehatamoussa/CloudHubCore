using CloudHub.Domain.Models;

namespace CloudHub.ServiceImp.OAuth
{
    internal interface IOAuthExtractor
    {
        public string BuildURL(string token);
        public OAuthUser ExtractUser(string bodyJson);
    }
}
