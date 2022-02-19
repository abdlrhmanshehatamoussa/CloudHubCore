using CloudHub.ApiContracts;
using NUnit.Framework;

namespace CloudHub.Tests.SDK
{
    public class GeneralTests
    {

        [Test]
        public void Ping()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                PingResponseContract result = await Helper.CloudHubManager.General.Ping();
                Assert.NotNull(result);
                Assert.IsTrue(result.production_mode);
            });
        }
    }
}
