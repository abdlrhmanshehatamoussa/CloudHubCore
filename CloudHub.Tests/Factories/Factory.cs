using CloudHub.Domain.Services;
using CloudHub.Infra.Factories;
using Microsoft.EntityFrameworkCore;
using System;

namespace CloudHub.Tests.Factories
{
    internal static class Factory
    {
        public static UnitOfWork UnitOfWork()
        {
            DbContextOptionsBuilder builder = new();
            string dbName = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            builder.UseInMemoryDatabase(dbName);
            UnitOfWork uow = new(new InMemoryContext(builder.Options));
            return uow;
        }

        public static UserService UserService() => new(UnitOfWork(), AuthenticationService());

        public static OAuthService AuthenticationService()
        {
            string googleOauthUrl = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=";
            OAuthService service = new(googleOauthUrl);
            return service;
        }
    }
}
