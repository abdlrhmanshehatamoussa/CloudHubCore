using CloudHub.Domain.Services;

namespace CloudHub.Tests.Unit.Domain
{
    internal class FakeEncryptionService : IEncryptionService
    {
        public string Decrypt(string message, string key)
        {
            return message;
        }

        public string Encrypt(string message, string key)
        {
            return message;
        }

        public string Hash(string message)
        {
            return message;
        }
    }
}
