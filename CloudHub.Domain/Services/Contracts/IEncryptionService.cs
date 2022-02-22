namespace CloudHub.Domain.Services
{
    public interface IEncryptionService
    {
        public string Hash(string message);
        public string Encrypt(string message, string key);
        public string? Decrypt(string message, string key);
    }
}
