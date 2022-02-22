using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.ServiceProvider;
using CloudHub.Utils;
using NUnit.Framework;
using System;

namespace CloudHub.Tests.Unit.Domain
{
    internal class DomainUnitTest
    {
        protected readonly IEncryptionService EncryptionService = new EncryptionService();
        protected readonly IOAuthService OAuthService = new FakeOAuthService();
        protected FakeUnitOfWork UnitOfWork = null!;
        protected UserService UserService = null!;
        protected NonceService NonceService = null!;

        [SetUp]
        public void Setup()
        {
            UnitOfWork = new();
            UserService = new(UnitOfWork, OAuthService, EncryptionService);
            NonceService = new(UnitOfWork, EncryptionService);
        }


        protected User NewBasicUser(string email, string password, int tenantId)
        {
            string name = HelperFunctions.RandomString(10);
            return new()
            {
                Email = email,
                Name = name,
                GlobalId = EncryptionService.Hash(name + email),
                TenantId = tenantId,
                Login = new()
                {
                    Passcode = password,
                    LoginType = new LoginType() { Name = "Basic", Id = ELoginTypes.LOGIN_TYPE_BASIC }
                }
            };
        }

        protected Client NewClient() => new()
        {
            Name = HelperFunctions.RandomString(10),
            ClientKey = Guid.NewGuid().ToString(),
            ClientSecret = Guid.NewGuid().ToString(),
            Tenant = new() { Name = HelperFunctions.RandomString(10) }
        };

        protected Nonce NewNonce(int clientId) => new()
        {
            Token = Guid.NewGuid().ToString(),
            ClientId = clientId,
            CreatedOn = DateTime.Now
        };
    }
}
