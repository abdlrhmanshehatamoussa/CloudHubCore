using CloudHub.API.Contracts;
using CloudHub.Tests.Factories;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Text;

namespace CloudHub.Tests.Integration
{
    internal class UserIntegrationTests
    {
        [Test]
        public void RegisterNewUser()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                //TODO: Implement Integration Tests
                //Arrange
                var application = new CloudHubTestApp();
                var client = application.CreateClient();

                //Act
                RegisterRequestContract contract = new RegisterRequestContract("asd", "asd", "asdsad", "asasd", 5671293);
                var response = await client.PostAsync("/users", new StringContent(JsonConvert.SerializeObject(contract), Encoding.UTF8, "application/json"));

                //Assert
                Assert.True((int)response.StatusCode == 403);
            });
        }
    }
}
