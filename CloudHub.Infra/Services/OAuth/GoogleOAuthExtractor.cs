using CloudHub.Domain.Services;
using System.Text.Json;

namespace CloudHub.Infra.Services
{
    internal class GoogleOAuthExtractor : IOAuthExtractor
    {

        //TODO: Pass this as a constructor class (IGoogleOAuthExtractorConfigurations)
        private const string GOOGLE_OAUTH_BASE_URL = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=";

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
            if (string.IsNullOrWhiteSpace(token)) { throw new ArgumentNullException("OAuth token cannot be empty"); }
            string url = string.Format("{0}{1}", GOOGLE_OAUTH_BASE_URL, token);
            return url;
        }


        private struct GoogleOAuthUser
        {
            public string issued_to { get; set; }
            public string audience { get; set; }
            public string user_id { get; set; }
            public string scope { get; set; }
            public int expires_in { get; set; }
            public string email { get; set; }
            public bool verified_email { get; set; }
            public string access_type { get; set; }

        }
    }
}
