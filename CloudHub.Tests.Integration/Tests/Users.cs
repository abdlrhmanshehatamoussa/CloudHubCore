using CloudHub.API.Contracts;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudHub.Tests.Integration
{
    internal class UsersTests : IntegrationTest
    {
        [Test]
        public async Task RegisterNewUser()
        {
            //Act
            string email = "email@gmail.com";
            RegisterRequestContract contract = new RegisterRequestContract("asd", email, "assdasdasdasdsad", "", 5671293);
            HttpResponseMessage response = await Client.CloudHubRequest(ClientInfo, HttpMethod.Post, "users", contract);

            //Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.DoesNotThrowAsync(async () =>
            {
                RegisterResponseContract registerResponse = await response.Parse<RegisterResponseContract>();
            });
        }
    }
}
