using CloudHub.Data;
using CloudHub.Data.Repositories;
using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;
using CloudHub.Domain.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CloudHub.Tests
{
    public class LoginTests
    {
        private UserService service = null!;
        private NonceService nonceService = null!;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder<MyDbContext> builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseNpgsql(Constants.PSQL_HOST);
            IUnitOfWork unitOfWork = new UnitOfWork(new MyDbContext(builder.Options));
            service = new UserService(unitOfWork);
            nonceService = new NonceService(unitOfWork);
        }

        [Test]
        public void InValidApplicationGuidValidLogin()
        {
            ConsumerCredentials credentials = new ConsumerCredentials()
            {
                ApplicationGuid = "asdasd",
                ClientKey = "f7ebe638-3f34-4dbe-b0c7-65104794ce9e"
            };
            LoginRequest dto = new LoginRequest("abdlrhmanshehata@gmail.com", "123456789", LoginTypeValues.LOGIN_TYPE_BASIC);
            NotAuthenticatedException? ex = Assert.ThrowsAsync<NotAuthenticatedException>(async () =>
            {
                await service.Login(credentials, dto);
            });
        }

        [Test]
        public void ValidApplicationIdInvalidLogin()
        {
            ValidAppInvaldLoginRequest(
                 new LoginRequest("abdlrhmanshehata@gmail.com", "123456789", 0)
            );
        }

        [Test]
        public void ValidApplicationInvalidEmail()
        {
            ValidAppInvaldLoginRequest(
                  new LoginRequest("!", "123456789", LoginTypeValues.LOGIN_TYPE_BASIC)
             );
        }

        [Test]
        public void ValidApplicationInvalidPassword()
        {
            ValidAppInvaldLoginRequest(
                new LoginRequest("abdlrhmanshehata@gmail.com", "123123", LoginTypeValues.LOGIN_TYPE_BASIC)
            );
        }


        [Test]
        public void HappyScenario()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                ConsumerCredentials credentials = new ConsumerCredentials()
                {
                    ApplicationGuid = "12910e89-564c-42c8-ad0b-8529d4cd5e04",
                    ClientKey = "f7ebe638-3f34-4dbe-b0c7-65104794ce9e"
                };
                Nonce nonce = await nonceService.GenereateNonce(credentials);
                credentials.Nonce = nonce.Token;
                LoginResponse response = await service.Login(credentials, new LoginRequest("abdlrhman.shehata@gmail.com", "123456789", LoginTypeValues.LOGIN_TYPE_BASIC));
                Assert.That(response.Email == "abdlrhman.shehata@gmail.com");
                Assert.That(response.LoginTypeName == "Basic");
            });
        }

        private void ValidAppInvaldLoginRequest(LoginRequest dto)
        {

            Assert.ThrowsAsync<NotAuthenticatedException>(async () =>
            {
                ConsumerCredentials credentials = new ConsumerCredentials()
                {
                    ApplicationGuid = "12910e89-564c-42c8-ad0b-8529d4cd5e04",
                    ClientKey = "f7ebe638-3f34-4dbe-b0c7-65104794ce9e"
                };
                Nonce nonce = await nonceService.GenereateNonce(credentials);
                credentials.Nonce = nonce.Token;
                LoginResponse response = await service.Login(credentials, dto);
                Assert.That(response.Email == "abdlrhmanshehata@gmail.com");
                Assert.That(response.LoginTypeName == "Basic");
            });
        }
    }
}