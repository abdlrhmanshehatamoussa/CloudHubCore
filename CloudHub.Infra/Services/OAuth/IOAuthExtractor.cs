using CloudHub.Domain.DTO;

namespace CloudHub.Infra.Services
{
    internal interface IOAuthExtractor
    {
        public string BuildURL(string token);
        public OAuthUser ExtractUser(string bodyJson);
    }
}
