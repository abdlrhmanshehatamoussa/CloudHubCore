﻿using System.Security.Cryptography;
using System.Text;

namespace CloudHub.Crosscutting
{
    public class SecurityHelper
    {
        public static string Hash256(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string DecryptAES(string text, string encryptionKey)
        {
            return text;
        }
    }
}