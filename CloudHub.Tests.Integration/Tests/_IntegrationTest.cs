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
        protected readonly HttpClient Client;

        public IntegrationTest()
        {
            WebApplicationFactory<Program> factory = new TestAppFactory();
            Client = factory.CreateClient();
        }

        protected async Task<string> GetNonce()
        {
            HttpResponseMessage response1 = await Client.PostAsyncJson("nonce");
            Assert.True(response1.StatusCode == HttpStatusCode.OK);
            NonceResponse nonce = await response1.Parse<NonceResponse>();
            Assert.IsNotNull(nonce.token);
            return nonce.token;
        }

        protected Dictionary<string, string> BuildHeaders(string nonce, string? userToken = null)
        {
            var headers = new Dictionary<string, string>() { { "nonce", nonce } };
            if (userToken != null) headers.Add("user-token", userToken);
            return headers;
        }
    }
}
