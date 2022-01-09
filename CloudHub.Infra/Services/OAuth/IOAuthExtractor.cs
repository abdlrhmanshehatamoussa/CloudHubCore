using CloudHub.Domain.Services;

namespace CloudHub.Infra.Services
{
    internal interface IOAuthExtractor
    {
        public string BuildURL(string token);
        public OAuthUser ExtractUser(string bodyJson);
    }
}
