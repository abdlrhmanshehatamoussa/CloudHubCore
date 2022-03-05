using Newtonsoft.Json;
using System.Text;

namespace CloudHub.Utils
{
    public static class HttpClientExtensions
    {
        private static readonly Dictionary<string, string> EmptyDictionary = new();

        public static async Task<T> Parse<T>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false) { throw new Exception(); }
            string bodyStr = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(bodyStr)) { throw new Exception(); }
            return JsonConvert.DeserializeObject<T>(bodyStr) ?? throw new Exception();
        }

        //Basic
        private static async Task<HttpResponseMessage> SendAsyncJson(this HttpClient client, HttpMethod method, string endpoint, dynamic? data, Dictionary<string, string> headers)
        {
            StringContent? content = null;
            if (data != null) content = new(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            if (client.BaseAddress == null) throw new Exception();
            HttpRequestMessage request = new() { RequestUri = new Uri(client.BaseAddress, endpoint), Method = method, Content = content };
            foreach (var header in headers) request.Headers.Add(header.Key, header.Value);
            return await client.SendAsync(request);
        }
        private static async Task<HttpResponseMessage> SendAsyncJson(this HttpClient client, HttpMethod method, string endpoint, dynamic data)
            => await SendAsyncJson(client, method, endpoint, headers: EmptyDictionary, data: data);

        private static async Task<HttpResponseMessage> SendAsyncJson(this HttpClient client, HttpMethod method, string endpoint)
            => await SendAsyncJson(client, method, endpoint, headers: EmptyDictionary, data: null);


        //Patch
        public static async Task<HttpResponseMessage> PatchAsyncJson(this HttpClient client, string endpoint, dynamic? data, Dictionary<string, string> headers)
            => await SendAsyncJson(client, HttpMethod.Patch, endpoint, data: data, headers: headers);
        public static async Task<HttpResponseMessage> PatchAsyncJson(this HttpClient client, string endpoint, dynamic? data)
            => await PatchAsyncJson(client, endpoint, headers: EmptyDictionary, data: data);
        public static async Task<HttpResponseMessage> PatchAsyncJson(this HttpClient client, string endpoint)
            => await PatchAsyncJson(client, endpoint, headers: EmptyDictionary, data: null);


        //Post
        public static async Task<HttpResponseMessage> PostAsyncJson(this HttpClient client, string endpoint, dynamic? data, Dictionary<string, string> headers)
            => await SendAsyncJson(client, HttpMethod.Post, endpoint, data: data, headers: headers);
        public static async Task<HttpResponseMessage> PostAsyncJson(this HttpClient client, string endpoint, dynamic? data)
            => await PostAsyncJson(client, endpoint, headers: EmptyDictionary, data: data);
        public static async Task<HttpResponseMessage> PostAsyncJson(this HttpClient client, string endpoint)
            => await PostAsyncJson(client, endpoint, headers: EmptyDictionary, data: null);



        //Get
        public static async Task<HttpResponseMessage> GetAsyncCustom(this HttpClient client, string endpoint, Dictionary<string, string> headers)
            => await SendAsyncJson(client: client, method: HttpMethod.Get, endpoint: endpoint, data: null, headers: headers);
        public static async Task<HttpResponseMessage> GetAsyncCustom(this HttpClient client, string endpoint)
            => await GetAsyncCustom(client: client, endpoint: endpoint, headers: EmptyDictionary);

    }
}