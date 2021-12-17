using CloudHub.Data.Repositories;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;
using CloudHub.Domain.Services;
using NUnit.Framework;

namespace CloudHub.Tests
{
    public class UserServiceTests
    {
        private UserService userService = null!;

        [SetUp]
        public void Setup()
        {
            IUnitOfWork uow = new UnitOfWork("Host=127.0.0.1;Database=cloudhub-api2;Username=postgres;Password=123456");
            userService = new UserService(uow);
        }

        [Test]
        public void Login_InValidApplicationValidLogin()
        {
            LoginRequest dto = new LoginRequest()
            {
                Email = "abdelrahman.shehata@gmail.com",
                Passcode = "123456",
                LoginTypeId = LoginTypeValues.LOGIN_TYPE_GOOGLE,
            };
            EntityNotFoundException? ex = Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await userService.Login(0, dto);
            });

            Assert.That(ex?.Message == "Application not found");
        }

        [Test]
        public void Login_ValidApplicationIdInvalidLogin()
        {
            LoginRequest dto = new LoginRequest()
            {
                Email = "abdelrahman.shehata@gmail.com",
                Passcode = "123456",
                LoginTypeId = 0,
            };
            EntityNotFoundException? ex = Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await userService.Login(3, dto);
            });
            Assert.That(ex?.Message == "LoginType not found");
        }

        [Test]
        public void Login_ValidApplicationInvalidEmail()
        {
            LoginRequest dto = new LoginRequest()
            {
                Email = "asdasdasd",
                Passcode = "123456",
                LoginTypeId = LoginTypeValues.LOGIN_TYPE_GOOGLE,
            };

            EntityNotFoundException? ex = Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await userService.Login(3, dto);
            });
            Assert.That(ex?.Message == "User not found");
        }

        [Test]
        public void Login_ValidApplicationInvalidPassword()
        {
            LoginRequest dto = new LoginRequest()
            {
                Email = "abdlrhmanshehata@gmail.com",
                Passcode = "asdasd",
                LoginTypeId = LoginTypeValues.LOGIN_TYPE_BASIC,
            };

            InvalidLoginCredentials? ex = Assert.ThrowsAsync<InvalidLoginCredentials>(async () =>
            {
                await userService.Login(3, dto);
            });
        }


        [Test]
        public void Login_Valid()
        {
            LoginRequest dto = new LoginRequest()
            {
                Email = "abdlrhmanshehata@gmail.com",
                Passcode = "123456789",
                LoginTypeId = LoginTypeValues.LOGIN_TYPE_BASIC,
            };

            Assert.DoesNotThrowAsync(async () =>
            {
                LoginResponse response = await userService.Login(3, dto);
                Assert.That(response.Email == "abdlrhmanshehata@gmail.com");
                Assert.That(response.LoginType == "Basic");
            });
        }
    }
}