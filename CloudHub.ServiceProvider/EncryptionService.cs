using CloudHub.Domain.Services;
using System.Security.Cryptography;
using System.Text;

namespace CloudHub.ServiceProvider
{
    public class EncryptionService : IEncryptionService
    {
        //TODO: Implement Encryption
        public string Decrypt(string message, string key)
        {
            return message.Replace("|" + key, "");
        }

        public string Encrypt(string message, string key)
        {
            return message + "|" + key;
        }

        public string Hash(string text)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
