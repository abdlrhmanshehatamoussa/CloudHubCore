using CloudHub.Business.DTO;
using CloudHub.Business.Services;
using CloudHub.Data;
using CloudHub.Data.Repositories;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace CloudHub.Tests
{
    public class RegisterTests
    {
        private UserService userService = null!;
        private NonceService nonceService = null!;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder<MyDbContext> builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseNpgsql(Constants.PSQL_HOST);
            IUnitOfWork unitOfWork = new UnitOfWork(new MyDbContext(builder.Options));
            userService = new UserService(unitOfWork);
            nonceService = new NonceService(unitOfWork);
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
                string random = RandomString(8);
                string email = random + "@gmail.com";
                RegisterResponse response = await userService.RegisterNewUser(credentials, new RegisterRequest
                 (
                     email,
                     "123456789",
                     random,
                     "",
                     LoginTypeValues.LOGIN_TYPE_BASIC
                 ));
                Assert.That(response.Email == email);
                Assert.IsNotNull(response.GlobalId);
            });
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
