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
            var application = new CloudHubTestApp();
            var client = application.CreateClient();
            Assert.DoesNotThrowAsync(async () =>
            {
                var response = await client.GetAsync("/ping");
                var body = await response.Content.ReadAsStringAsync();
                PingResponseContract pingResponseContract = JsonConvert.DeserializeObject<PingResponseContract>(body);
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                Assert.NotNull(body);
                Assert.That(pingResponseContract.build_id == CloudHubTestApp.BUILD_ID);
                Assert.That(pingResponseContract.environment == CloudHubTestApp.ENV_NAME);
            });
        }
    }
}
