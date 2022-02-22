using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.ServiceProvider.OAuth;
using System.Net;

namespace CloudHub.ServiceProvider
{
    public interface IConfigOAuthService
    {
        public string GoogleOAuthUrl { get; set; }
    }
    public class OAuthService : IOAuthService
    {
        public OAuthService(IConfigOAuthService configurations)
        {
            GoogleOAuthExtractor googleOAuthExtractor = new GoogleOAuthExtractor(configurations.GoogleOAuthUrl);
            this.Extractors.Add(ELoginTypes.LOGIN_TYPE_GOOGLE, googleOAuthExtractor);
        }

        private readonly Dictionary<ELoginTypes, IOAuthExtractor> Extractors = new();

        private IOAuthExtractor GetExtractor(ELoginTypes loginType)
        {
            if (Extractors.ContainsKey(loginType) == false) { throw new Exception(string.Format("Cannot find OAuth extractor for Login type [{0}]", loginType.ToString())); }
            return this.Extractors[loginType];
        }

        public async Task<OAuthUser?> GetUserByToken(string token, ELoginTypes loginType)
        {
            IOAuthExtractor extractor = GetExtractor(loginType);
            if (string.IsNullOrWhiteSpace(token)) { return null; }
            HttpClient client = new();
            string url = extractor.BuildURL(token);
            HttpResponseMessage response = await client.GetAsync(url);
            string body = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrWhiteSpace(body))
            {
                return null;
            }
            return extractor.ExtractUser(body);
        }
    }
}
