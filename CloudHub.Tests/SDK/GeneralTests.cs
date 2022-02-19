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
                var result = await Helper.CloudHubManager.General.Ping();
                Assert.NotNull(result);
            });
        }
    }
}
