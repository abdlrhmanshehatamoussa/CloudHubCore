using CloudHub.Crosscutting;
using CloudHub.Domain.Services;
using NUnit.Framework;

namespace CloudHub.Tests
{
    public class GeneralTests
    {
        [Test]
        public void TestHash()
        {
            string input = "abdlrhmanshehata@gmail.com212345679798";
            string hash = SecurityHelper.Hash256(input);
            System.Console.WriteLine(hash);
            Assert.IsNotNull(hash);
        }

        [Test]
        public void Test()
        {
            Assert.DoesNotThrow(() =>
            {
                RegisterRequest request = new(name: "asdasd", email:"egeg.com", password: "12345678", null, Domain.Entities.ELoginTypes.LOGIN_TYPE_GOOGLE);
            });
        }
    }
}
