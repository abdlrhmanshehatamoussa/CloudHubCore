using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net;

namespace CloudHub.Tests.Integration
{
    internal class UserIntegrationTests
    {
        [Test]
        public void RegisterNewUser()
        {
            //TODO: Implement Integration Tests
            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
            var client = application.CreateClient();
            Assert.DoesNotThrowAsync(async () =>
            {
                var response = await client.GetAsync("/ping");
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                Assert.NotNull(response.Content);
            });
        }
    }
}
