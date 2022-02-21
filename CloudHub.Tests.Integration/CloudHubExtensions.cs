using CloudHub.API.Contracts;
using CloudHub.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudHub.Tests.Integration
{
    public struct ClientInfo
    {
        public string ClientKey { get; set; }
        public string ClientSecret { get; set; }
    }
    public static class CloudHubExtensions
    {
        public static HttpRequestMessage BuildBasicRequest(HttpMethod method, string endpoint, dynamic? data = null, Dictionary<string, string>? headers = null)
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
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            return request;
        }
        public static async Task<T> Parse<T>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false) { throw new Exception(); }
            string bodyStr = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(bodyStr)) { throw new Exception(); }
            return JsonConvert.DeserializeObject<T>(bodyStr) ?? throw new Exception();
        }
        public static async Task<HttpResponseMessage> CloudHubRequest(this HttpClient client, ClientInfo clientInfo, HttpMethod method, string endpoint, dynamic? data = null, string? userToken = null)
        {
            Dictionary<string, string> basicHeaders = new()
            {
                { "client-key", clientInfo.ClientKey },
                { "client-claim", SecurityHelper.EncryptAES(clientInfo.ClientKey, clientInfo.ClientSecret) }
            };
            if (client.BaseAddress == null) { throw new Exception("Invalid Client"); }
            string nonceEndpoint = new Uri(client.BaseAddress, "nonce").ToString();
            HttpRequestMessage request = BuildBasicRequest(HttpMethod.Post, endpoint: nonceEndpoint, headers: basicHeaders);
            HttpResponseMessage response = await client.SendAsync(request);
            NonceResponse? nonceObj = await response.Parse<NonceResponse>();
            string nonce = nonceObj?.token ?? throw new Exception("Failed to get nonce");
            basicHeaders.Add("nonce", nonce);
            if (userToken != null)
            {
                basicHeaders.Add("user-token", userToken);
            }
            HttpRequestMessage request2 = BuildBasicRequest(method, endpoint, data, headers: basicHeaders);
            HttpResponseMessage response2 = await client.SendAsync(request2);
            return response2;
        }
    }
}