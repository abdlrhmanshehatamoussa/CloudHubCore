using CloudHub.Domain.Models;
using System.Text.Json;

namespace CloudHub.Factories
{
    public partial class GoogleOAuthExtractor : IOAuthExtractor
    {
        public GoogleOAuthExtractor(IGoogleServicesConfigurations googleServicesConfigurations) => this.googleServicesConfigurations = googleServicesConfigurations;

        private readonly IGoogleServicesConfigurations googleServicesConfigurations;

        public OAuthUser ExtractUser(string body)
        {
            GoogleOAuthUser user = JsonSerializer.Deserialize<GoogleOAuthUser>(body);
            string email = user.email;
            string openId = user.user_id;
            int expiresInSeconds = user.expires_in;
            return new OAuthUser(email, openId, expiresInSeconds);
        }

        public string BuildURL(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) { throw new ArgumentNullException(nameof(token)); }
            string url = string.Format("{0}{1}", googleServicesConfigurations.GoogleTokenInfoApiUrl, token);
            return url;
        }
    }
}
