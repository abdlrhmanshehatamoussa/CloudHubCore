using CloudHub.API.Contracts;
using CloudHub.Domain.Services;
using CloudHub.ServiceProvider;
using CloudHub.Utils;
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
        public readonly string ClientKey;
        public readonly string ClientSecret;

        public IntegrationTest()
        {
            TestAppFactory factory = new TestAppFactory();
            Client = factory.CreateClient();
            ClientKey = factory.ClientKey;
            ClientSecret = factory.ClientSecret;
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
            IEncryptionService encryptionService = new EncryptionService();
            string? encryptedNonce = encryptionService.Encrypt(nonce, ClientSecret);
            var headers = new Dictionary<string, string>() { { "nonce", encryptedNonce } };
            if (userToken != null) headers.Add("user-token", userToken);
            return headers;
        }
    }
}
