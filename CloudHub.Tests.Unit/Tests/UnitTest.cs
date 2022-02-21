using CloudHub.Domain.Services;
using CloudHub.Infra.Factories;
using Microsoft.EntityFrameworkCore;
using System;

namespace CloudHub.Tests.Unit
{
    internal class UnitTest
    {
        protected readonly TestUnitOfWork UnitOfWork;
        protected readonly UserService UserService;
        protected readonly OAuthService AuthenticationService;

        public UnitTest()
        {
            DbContextOptionsBuilder<InMemoryContext> builder = new();
            string dbName = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            builder.UseInMemoryDatabase(dbName);
            string googleOauthUrl = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=";
            
            UnitOfWork = new(new InMemoryContext(builder.Options));
            AuthenticationService = new(googleOauthUrl);
            UserService = new(UnitOfWork, AuthenticationService);
        }

    }
}
