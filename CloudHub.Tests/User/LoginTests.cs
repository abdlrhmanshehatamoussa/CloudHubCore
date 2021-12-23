using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace CloudHub.Tests
{
    public class LoginTests
    {
        private readonly UserService service = null!;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder<MyDbContext> builder = new();
            //TODO: Replace database connection with test stubs
            builder.UseNpgsql(Constants.PSQL_HOST);

            IUnitOfWork unitOfWork = new UnitOfWork(new MyDbContext(builder.Options));
            Mock<IServiceConfigurations> mock = new();
            mock.Setup(x => x.IsProductionModeEnabled).Returns(false);

            //TODO: 
            //service = new UserService(unitOfWork, mock.Object);
        }

        [Test]
        public void InValidApplicationGuidValidLogin()
        {
            ConsumerCredentials credentials = new()
            {
                ApplicationGuid = "asdasd",
                ClientKey = "f7ebe638-3f34-4dbe-b0c7-65104794ce9e"
            };
            LoginRequest dto = new("abdlrhmanshehata@gmail.com", "123456789", LoginTypeValues.LOGIN_TYPE_BASIC);
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
                ConsumerCredentials credentials = new()
                {
                    ApplicationGuid = "12910e89-564c-42c8-ad0b-8529d4cd5e04",
                    ClientKey = "f7ebe638-3f34-4dbe-b0c7-65104794ce9e",
                    Nonce = "66fd8c4e-7612-455d-bdf2-0bcc6b8baf7df8e15778-b20a-4fe3-a46a-460010fcf9924def0e64-ce76-4513-aa3f-9d0405d78b8c"
                };
                LoginResponse response = await service.Login(credentials, new LoginRequest("abdlrhman.shehata@gmail.com", "123456789", LoginTypeValues.LOGIN_TYPE_BASIC));
                Assert.That(response.Email == "abdlrhman.shehata@gmail.com");
                Assert.That(response.LoginTypeName == "Basic");
            });
        }

        private void ValidAppInvaldLoginRequest(LoginRequest dto)
        {

            Assert.ThrowsAsync<NotAuthenticatedException>(async () =>
            {
                ConsumerCredentials credentials = new()
                {
                    ApplicationGuid = "12910e89-564c-42c8-ad0b-8529d4cd5e04",
                    ClientKey = "f7ebe638-3f34-4dbe-b0c7-65104794ce9e",
                    Nonce = "66fd8c4e-7612-455d-bdf2-0bcc6b8baf7df8e15778-b20a-4fe3-a46a-460010fcf9924def0e64-ce76-4513-aa3f-9d0405d78b8c"
                };
                LoginResponse response = await service.Login(credentials, dto);
                Assert.That(response.Email == "abdlrhmanshehata@gmail.com");
                Assert.That(response.LoginTypeName == "Basic");
            });
        }
    }
}