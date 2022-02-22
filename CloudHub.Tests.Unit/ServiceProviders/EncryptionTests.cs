using CloudHub.ServiceProvider;
using NUnit.Framework;

namespace CloudHub.Tests.Unit.ServiceProviders
{
    internal class EncryptionTests
    {
        [Test]
        [TestCase("hi", "8f434346648f6b96df89dda901c5176b10a6d83961dd3c1ac88b59b2dc327aa4")]
        [TestCase("adsasdasda", "f9612a314231b97219661dea4d4d25b623b2a970a4f609d01a2e2c70a0736da5")]
        [TestCase("0", "5feceb66ffc86f38d952786c6d696c79c2dbc239dd4e91b46729d73a27fb57e9")]
        public void Hashing_HappyScenario(string input, string output)
        {
            EncryptionService service = new EncryptionService();
            string result = service.Hash(input);
            Assert.That(result, Is.EqualTo(output));
        }

        [Test]
        [TestCase("hey", "187ey12ib")]
        [TestCase("hey", "hey")]
        public void Encryption_HappyScenario(string message, string key)
        {
            EncryptionService service = new EncryptionService();
            string encrypted = service.Encrypt(message, key);
            string decrypted = service.Decrypt(encrypted, key);
            Assert.IsTrue(decrypted == message);
        }

        [Test]
        [TestCase("hey", "187ey12ib")]
        [TestCase("hey", "hey")]
        public void Encryption_DifferentKey(string message, string key)
        {
            EncryptionService service = new EncryptionService();
            string encrypted = service.Encrypt(message, key);
            string wrongKey = encrypted + message + key;
            string decrypted = service.Decrypt(encrypted, wrongKey);
            Assert.IsTrue(decrypted != message);
        }
    }
}
