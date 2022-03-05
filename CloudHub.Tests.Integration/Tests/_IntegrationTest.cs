using CloudHub.API.Contracts;
using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.ServiceProvider;
using CloudHub.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudHub.Tests.Integration
{
    internal class IntegrationTest
    {
        private Client _tenantClient = null!;
        protected HttpClient _myHttpClient = null!;


        [SetUp]
        public void Setup()
        {
            _tenantClient = new Client()
            {
                Tenant = new Tenant()
                {
                    Name = HelperFunctions.RandomString(10)
                },
                Name = HelperFunctions.RandomString(10),
                ClientKey = Guid.NewGuid().ToString(),
                ClientSecret = Guid.NewGuid().ToString()
            };
            TestAppFactory factory = new (_tenantClient);
            _myHttpClient = factory.CreateClient();
        }

        protected async Task<string> GetNonce()
        {
            HttpResponseMessage response1 = await _myHttpClient.PostAsyncJson("nonce");
            Assert.True(response1.StatusCode == HttpStatusCode.OK);
            NonceResponse nonce = await response1.Parse<NonceResponse>();
            Assert.IsNotNull(nonce.token);
            return nonce.token;
        }

        protected Dictionary<string, string> BuildHeaders(string nonce, string? userToken = null)
        {
            IEncryptionService encryptionService = new EncryptionService();
            string? encryptedNonce = encryptionService.Encrypt(nonce, _tenantClient.ClientSecret);
            var headers = new Dictionary<string, string>() { { "nonce", encryptedNonce } };
            if (userToken != null) headers.Add("user-token", userToken);
            return headers;
        }
    }
}
