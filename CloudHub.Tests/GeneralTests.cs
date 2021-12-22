using CloudHub.Crosscutting;
using NUnit.Framework;

namespace CloudHub.Tests
{
    public class GeneralTests
    {
        [Test]
        public void TestHash()
        {
            string input = "abdlrhmanshehata@gmail.com212345679798";
            string hash = Utils.Hash256(input);
            System.Console.WriteLine(hash);
            Assert.IsNotNull(hash);
        }

        [Test]
        public void Test()
        {
            Assert.Pass();
        }
    }
}
