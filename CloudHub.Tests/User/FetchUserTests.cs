using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace CloudHub.Tests.User
{
    public class FetchUserTests
    {
        private readonly UserService userService = null!;
        private NonceService nonceService = null!;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder<PostgreDatabase> builder = new();
            builder.UseNpgsql(Constants.PSQL_HOST);
            IUnitOfWork unitOfWork = new UnitOfWork(new PostgreDatabase(builder.Options));
            Mock<IEnvironmentSettings> mock = new();
            mock.Setup(x => x.IsProductionModeEnabled).Returns(false);
            //userService = new UserService(unitOfWork, mock.Object);
            nonceService = new NonceService(unitOfWork, mock.Object);
        }

        [Test]
        public void HappyScenario()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                ConsumerCredentials credentials = new()
                {
                    ClientKey = "f7ebe638-3f34-4dbe-b0c7-65104794ce9e",
                    UserToken = "f139e8d9-1acb-444b-9903-62df96913e26e8d270e7-1aa9-41b4-8de8-ca0b00798a04"
                };
                Nonce nonce = await nonceService.GenereateNonce(credentials);
                credentials.Nonce = nonce.Token;
                LoginResponse response = await userService.FetchUser(credentials);
                Assert.That(response.Email == "abdlrhmanshehata@gmail.com");
                Assert.IsNotNull(response.GlobalId);
            });
        }
    }
}
