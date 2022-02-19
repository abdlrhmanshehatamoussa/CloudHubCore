using CloudHub.BusinessLogic;
using CloudHub.Infra.Data;
using CloudHub.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace CloudHub.Tests.Domain
{
    internal static class Helper
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
    }




    internal class TestSettings : IEnvironmentSettings
    {
        public bool IsProductionModeEnabled => false;

        public string EnvironmentName => "Local-Test";

        public string BuildId => "0.0.0";
    }
}
