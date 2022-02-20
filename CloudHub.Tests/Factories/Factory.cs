using CloudHub.Domain.Models;
using CloudHub.Infra.Factories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace CloudHub.Tests.Factories
{
    internal static class Factory
    {

        public static UnitOfWork UnitOfWork
        {
            get
            {
                DbContextOptionsBuilder builder = new();
                string dbName = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
                builder.UseInMemoryDatabase(dbName);
                UnitOfWork uow = new(new InMemoryContext(builder.Options));
                return uow;
            }
        }

        public static IEnvironmentSettings EnvironmentSettings => new TestSettings();

        public static OAuthService AuthenticationService
        {
            get
            {
                string googleOauthUrl = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=";
                Mock<IGoogleServicesConfigurations> googleMock = new();
                googleMock.Setup(x => x.GoogleTokenInfoApiUrl).Returns(googleOauthUrl);
                GoogleOAuthExtractor googleOAuthExtractor = new(googleMock.Object);
                OAuthService service = new(googleOAuthExtractor);
                return service;
            }
        }
    }
}
