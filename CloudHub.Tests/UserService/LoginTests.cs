using CloudHub.Business.DTO;
using CloudHub.Business.Services;
using CloudHub.Data;
using CloudHub.Data.Repositories;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;
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
        public void InValidApplicationValidLogin()
        {
            ClientCredentials clientCredentials = new ClientCredentials("asdasd", "f7ebe638-3f34-4dbe-b0c7-65104794ce9e");
            LoginRequest dto = new LoginRequest(
                "abdlrhmanshehata@gmail.com",
                "123456789",
                LoginTypeValues.LOGIN_TYPE_BASIC
            );
            NotAuthenticatedException? ex = Assert.ThrowsAsync<NotAuthenticatedException>(async () =>
            {
                await service.Login(clientCredentials, dto);
            });
        }

        [Test]
        public void ValidApplicationIdInvalidLogin()
        {
            ValidAppInvaldLoginRequest(
                new LoginRequest
                 (
                     "abdlrhmanshehata@gmail.com",
                     "123456789",
                     0
                 )
            );
        }

        [Test]
        public void ValidApplicationInvalidEmail()
        {
            ValidAppInvaldLoginRequest(
                 new LoginRequest
                  (
                      "asdasd",
                      "123456789",
                      LoginTypeValues.LOGIN_TYPE_BASIC
                  )
             );
        }

        [Test]
        public void ValidApplicationInvalidPassword()
        {
            ValidAppInvaldLoginRequest(
                new LoginRequest
                 (
                     "abdlrhmanshehata@gmail.com",
                     "123123",
                     LoginTypeValues.LOGIN_TYPE_BASIC
                 )
            );
        }


        [Test]
        public void HappyScenario()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                ClientCredentials clientCredentials = new ClientCredentials("12910e89-564c-42c8-ad0b-8529d4cd5e04", "f7ebe638-3f34-4dbe-b0c7-65104794ce9e");
                string nonce = await nonceService.GenereateNonce(clientCredentials);
                clientCredentials.Nonce = nonce;
                LoginResponse response = await service.Login(clientCredentials, new LoginRequest
                 (
                     "abdlrhmanshehata@gmail.com",
                     "123456789",
                     LoginTypeValues.LOGIN_TYPE_BASIC
                 ));
                Assert.That(response.Email == "abdlrhmanshehata@gmail.com");
                Assert.That(response.LoginTypeName == "Basic");
            });
        }

        private void ValidAppInvaldLoginRequest(LoginRequest dto)
        {

            Assert.ThrowsAsync<NotAuthenticatedException>(async () =>
            {
                ClientCredentials clientCredentials = new ClientCredentials("12910e89-564c-42c8-ad0b-8529d4cd5e04", "f7ebe638-3f34-4dbe-b0c7-65104794ce9e");
                string nonce = await nonceService.GenereateNonce(clientCredentials);
                clientCredentials.Nonce = nonce;
                LoginResponse response = await service.Login(clientCredentials, dto);
                Assert.That(response.Email == "abdlrhmanshehata@gmail.com");
                Assert.That(response.LoginTypeName == "Basic");
            });
        }
    }
}