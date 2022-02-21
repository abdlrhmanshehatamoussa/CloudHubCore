using CloudHub.API.Contracts;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;

namespace CloudHub.Tests.Integration
{
    internal class PingTests : IntegrationTest
    {
        [Test]
        public void Ping_Success()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                //Act
                var response = await Client.GetAsync("/ping");

                //Assert
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                string body = await response.Content.ReadAsStringAsync();
                PingResponseContract pingResponseContract = JsonConvert.DeserializeObject<PingResponseContract>(body);
                Assert.That(pingResponseContract.build_id == TestAppFactory.BUILD_ID);
                Assert.That(pingResponseContract.environment == TestAppFactory.ENV_NAME);
            });
        }
    }
}
