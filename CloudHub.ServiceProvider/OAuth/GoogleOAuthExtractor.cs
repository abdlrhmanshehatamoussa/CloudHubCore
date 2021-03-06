using CloudHub.Domain.Models;
using System.Text.Json;

namespace CloudHub.ServiceProvider.OAuth
{
    internal class GoogleOAuthExtractor : IOAuthExtractor
    {

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


        public GoogleOAuthExtractor(string _googleOAuthURL) => this.googleOAuthURL = _googleOAuthURL;

        private readonly string googleOAuthURL;

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
            string url = string.Format("{0}{1}", googleOAuthURL, token);
            return url;
        }
    }
}
