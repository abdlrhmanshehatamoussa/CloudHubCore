using CloudHub.SDK;
using System;
using System.Linq;

namespace CloudHub.Tests.SDK
{
    public static class Helper
    {
        public static CloudHubManager CloudHubManager = new(
            new()
            {
                ApiURL = "http://test.cloudhub.vps238.com",
                ClientKey = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f",
                ClientSecret = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f"
            }
        );

        private static readonly Random random = new();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
