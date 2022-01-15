using CloudHub.Domain.Entities;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace CloudHub.Tests.User
{
    public class RegisterTests
    {
        private readonly UserService userService = null!;
        private NonceService nonceService = null!;

        [SetUp]
        public void Setup()
        {
            Mock<IEnvironmentSettings> mock2 = new();
            mock2.Setup(x => x.IsProductionModeEnabled).Returns(false);
            nonceService = new NonceService(Constants.UnitOfWork, mock2.Object);
        }

        [Test]
        public void HappyScenario()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                ConsumerCredentials credentials = new()
                {
                    ClientKey = "f7ebe638-3f34-4dbe-b0c7-65104794ce9e"
                };
                Nonce nonce = await nonceService.GenereateNonce(credentials);
                credentials.Nonce = nonce.Token;
                string random = RandomString(8);
                string email = random + "@gmail.com";
                RegisterResponse response = await userService.RegisterNewEndUser(credentials, new RegisterRequest
                 (
                     email,
                     "123456789",
                     random,
                     "",
                     ELoginTypes.LOGIN_TYPE_BASIC
                 ));
                Assert.That(response.Email == email);
                Assert.IsNotNull(response.GlobalId);
            });
        }

        private static readonly Random random = new();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
