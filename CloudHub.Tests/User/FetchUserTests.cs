using CloudHub.Data;
using CloudHub.Data.Repositories;
using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;
using CloudHub.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace CloudHub.Tests
{
    public class FetchUserTests
    {
        private UserService userService = null!;
        private NonceService nonceService = null!;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder<MyDbContext> builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseNpgsql(Constants.PSQL_HOST);
            IUnitOfWork unitOfWork = new UnitOfWork(new MyDbContext(builder.Options));
            Mock<IProductionModeProvider> mock = new Mock<IProductionModeProvider>();
            mock.Setup(x => x.IsProductionModeEnabled).Returns(false);
            userService = new UserService(unitOfWork, mock.Object);
            nonceService = new NonceService(unitOfWork, mock.Object);
        }

        [Test]
        public void HappyScenario()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                ConsumerCredentials credentials = new ConsumerCredentials()
                {
                    ApplicationGuid = "12910e89-564c-42c8-ad0b-8529d4cd5e04",
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
