using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Services;
using System.Net;

namespace CloudHub.Infra.Services
{

    public class OAuthService : IOAuthService
    {
        public OAuthService(GoogleOAuthExtractor googleOAuthExtractor)
        {
            this.Extractors.Add(LoginTypeValues.LOGIN_TYPE_GOOGLE, googleOAuthExtractor);
        }

        private readonly Dictionary<LoginTypeValues, IOAuthExtractor> Extractors = new();

        public async Task<OAuthUser?> GetUserByToken(string token, LoginTypeValues loginType)
        {
            if (string.IsNullOrWhiteSpace(token)) { return null; }
            IOAuthExtractor extractor = GetExtractor(loginType);
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

        private IOAuthExtractor GetExtractor(LoginTypeValues loginType)
        {
            if (Extractors.ContainsKey(loginType) == false) { throw new Exception(string.Format("Cannot find OAuth extractor for Login type [{0}]", loginType.ToString())); }
            return this.Extractors[loginType];
        }
    }
}
