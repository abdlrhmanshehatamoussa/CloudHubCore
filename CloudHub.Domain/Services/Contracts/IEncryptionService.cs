namespace CloudHub.Domain.Services
{
    internal interface IEncryptionService
    {
        public string Hash256(string message);
    }
}
