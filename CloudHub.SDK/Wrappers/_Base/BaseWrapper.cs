using CloudHub.ApiContracts;
using CloudHub.Utils;
using Newtonsoft.Json;
using System.Text;

namespace CloudHub.SDK
{
    public class BaseWrapper
    {
        public BaseWrapper(ClientInfo info) => Info = info;
        private readonly ClientInfo Info;
        private readonly static HttpClient Client = new();


        #region Foundation
        private string BuildEndpoint(string path)
        {
            return string.Format("{0}/{1}", Info.ApiURL, path);
        }


        private Dictionary<string, string> BasicHeaders => new()
        {
            { "client-key", Info.ClientKey },
            { "client-claim", SecurityHelper.EncryptAES(Info.ClientKey, Info.ClientSecret) }
        };

        private HttpRequestMessage BuildBasicRequest(HttpMethod method, string endpoint, dynamic? data = null, Dictionary<string, string>? headers = null)
        {
            StringContent? content = null;
            if (data != null)
            {
                content = new(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            }
            HttpRequestMessage request = new()
            {
                RequestUri = new Uri(endpoint),
                Method = method,
                Content = content,
            };
            foreach (var header in BasicHeaders)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            return request;
        }
        private async Task<T> Parse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false) { throw new Exception(); }
            string bodyStr = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(bodyStr)) { throw new Exception(); }
            return JsonConvert.DeserializeObject<T>(bodyStr) ?? throw new Exception();
        }
        private async Task<string> GetNonce()
        {
            HttpRequestMessage request = BuildBasicRequest(HttpMethod.Post, NonceEndpoint);
            HttpResponseMessage response = await Client.SendAsync(request);
            NonceResponse? nonceObj = await Parse<NonceResponse>(response);
            string nonceToken = nonceObj?.token ?? throw new Exception("SDK Error");
            return nonceToken;
        }
        private async Task<T> GenericRequest<T>(HttpMethod method, string endpoint, dynamic? data = null, string? userToken = null)
        {
            string nonce = await GetNonce();
            Dictionary<string, string> headers = new()
            {
                { "nonce", nonce }
            };
            if (userToken != null)
            {
                headers.Add("user-token", userToken);
            }
            HttpRequestMessage request = BuildBasicRequest(method, endpoint, data, headers);
            HttpResponseMessage response = await Client.SendAsync(request);
            T parsedResponse = await Parse<T>(response);
            return parsedResponse;
        }
        #endregion


        #region Exposed HTTP Verbs
        protected async Task<T> Post<T>(string endpoint, dynamic? data = null, string? userToken = null) where T : struct
        {
            return await GenericRequest<T>(method: HttpMethod.Post, endpoint: endpoint, data: data, userToken: userToken);
        }
        protected async Task<T> Get<T>(string endpoint, string? userToken = null) where T : struct
        {
            return await GenericRequest<T>(method: HttpMethod.Get, endpoint: endpoint, userToken: userToken);
        }
        #endregion


        #region Endpoints
        protected string PingEndpoint => BuildEndpoint("ping");
        protected string NonceEndpoint => BuildEndpoint("nonce");
        protected string UsersEndpoint => BuildEndpoint("users");
        protected string UsersLoginEndpoint => BuildEndpoint("users/login");
        #endregion
    }
}
