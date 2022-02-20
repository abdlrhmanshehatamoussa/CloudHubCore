using CloudHub.Domain.Models;

namespace CloudHub.Infra.Factories
{
    public interface IOAuthExtractor
    {
        public string BuildURL(string token);
        public OAuthUser ExtractUser(string bodyJson);
    }
}
