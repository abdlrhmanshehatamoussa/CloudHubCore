using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using CloudHub.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;

namespace CloudHub.Tests
{
    internal static class Constants
    {
        private const string GOOGLE_TOKEN_INFO_API_URL = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=";

        public static UnitOfWork UnitOfWork()
        {
            DbContextOptionsBuilder builder = new();
            string dbName = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            builder.UseInMemoryDatabase(dbName);
            UnitOfWork uow = new(new PostgreContext(builder.Options));
            return uow;
        }

        public static IEnvironmentSettings EnvironmentSettings
        {
            get
            {
                return new TestSettings();
            }
        }

        public static OAuthService AuthenticationService
        {
            get
            {
                Mock<IGoogleServicesConfigurations> googleMock = new();
                googleMock.Setup(x => x.GoogleTokenInfoApiUrl).Returns(GOOGLE_TOKEN_INFO_API_URL);
                GoogleOAuthExtractor googleOAuthExtractor = new(googleMock.Object);
                OAuthService service = new(googleOAuthExtractor);
                return service;
            }
        }


        private static readonly Random random = new();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }




    internal class TestSettings : IEnvironmentSettings
    {
        public bool IsProductionModeEnabled => false;

        public string EnvironmentName => "Local-Test";

        public string BuildId => "0.0.0";
    }
}
