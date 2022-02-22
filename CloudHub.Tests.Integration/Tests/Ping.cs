using CloudHub.API.Contracts;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace CloudHub.Tests.Integration
{
    internal class PingTests : IntegrationTest
    {
        [Test]
        public async Task Ping_Success()
        {
            //Act
            var response = await _myHttpClient.GetAsync("/ping");

            //Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            string body = await response.Content.ReadAsStringAsync();
            PingResponseContract pingResponseContract = JsonConvert.DeserializeObject<PingResponseContract>(body);
            Assert.NotNull(pingResponseContract);
        }
    }
}
