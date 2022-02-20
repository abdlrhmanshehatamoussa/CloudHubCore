using CloudHub.API.Contracts;
using CloudHub.Tests.Factories;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;

namespace CloudHub.Tests.Integration
{
    internal class GeneralIntegrationTests
    {
        [Test]
        public void Ping()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                //Arrange
                var application = new CloudHubTestApp();
                var client = application.CreateClient();

                //Act
                var response = await client.GetAsync("/ping");

                //Assert
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                string body = await response.Content.ReadAsStringAsync();
                PingResponseContract pingResponseContract = JsonConvert.DeserializeObject<PingResponseContract>(body);
                Assert.That(pingResponseContract.build_id == CloudHubTestApp.BUILD_ID);
                Assert.That(pingResponseContract.environment == CloudHubTestApp.ENV_NAME);
            });
        }
    }
}
