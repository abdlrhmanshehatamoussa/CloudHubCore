using CloudHub.API.Contracts;
using CloudHub.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudHub.Tests.Integration
{
    internal class IntegrationTest
    {
        public const string BUILD_ID = "0.0.0";
        public const string ENV_NAME = "IntegrationTestApp";

        protected readonly HttpClient Client;

        public IntegrationTest()
        {
            Client = GetApiClient();
        }
        private HttpClient GetApiClient()
        {
            WebApplicationFactory<Program> factory = new MyAppFactory(new()
            {
                BuildId = BUILD_ID,
                EnvironmentName = ENV_NAME,
                GoogleOAuthUrl = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=",
                MainConnectionString = "Host=127.0.0.1;Database=cloudhub-integration-tests;Username=postgres;Password=123456",
                IsProductionModeEnabled = false,
            });
            HttpClient client = factory.CreateClient();
            return client;
        }

        protected async Task<string> GetNonce()
        {
            HttpResponseMessage response1 = await Client.PostAsyncJson("nonce");
            NonceResponse nonce = await response1.Parse<NonceResponse>();
            Assert.True(response1.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(nonce.token);
            return nonce.token;
        }

        protected Dictionary<string, string> BuildHeaders(string nonce, string? userToken = null)
        {
            var headers = new Dictionary<string, string>()
            {
                { "nonce",nonce }
            };
            if (userToken != null)
            {
                headers.Add("user-token", userToken);
            }
            return headers;
        }
    }
}
