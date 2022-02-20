using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.Infra.Factories;
using CloudHub.Tests.Factories;
using CloudHub.Tests.Utils;
using CloudHub.Utils;
using NUnit.Framework;
using System;

namespace CloudHub.Tests.Unit
{
    public class RegisterTests
    {
        private readonly UnitOfWork unitOfWork = Factory.UnitOfWork();
        private UserService userService = null!;

        [SetUp]
        public void Setup()
        {
            userService = new UserService(unitOfWork, Factory.AuthenticationService());
        }

        [Test]
        public void HappyScenario()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                //Arrange
                Client client = new()
                {
                    Name = "testclient",
                    ClientKey = Guid.NewGuid().ToString(),
                    ClientSecret = Guid.NewGuid().ToString(),
                    Tenant = new() { Id = 1, Name = "Tenant 1" },
                    TenantId = 1
                };
                await unitOfWork.ClientsRepository.Add(client);
                await unitOfWork.Save();

                Nonce nonce = new()
                {
                    Token = Guid.NewGuid().ToString(),
                    ClientId = client.Id,
                    CreatedOn = DateTime.Now
                };
                await unitOfWork.NoncesRepository.Add(nonce);
                await unitOfWork.Save();

                ConsumerCredentials credentials = new()
                {
                    ClientKey = client.ClientKey,
                    ClientClaim = SecurityHelper.EncryptAES(client.ClientKey, client.ClientSecret),
                    Nonce = nonce.Token
                };
                string random = HelperFunctions.RandomString(8);
                string password = HelperFunctions.RandomString(10);
                string email = string.Format("{0}@domain.com", random);

                //Act
                User response = await userService.RegisterNewUser(credentials, new CreateUserDTO
                 (
                    "Test User",
                     email,
                     password,
                     "",
                     ELoginTypes.LOGIN_TYPE_BASIC
                 ));

                //Assert
                Assert.That(response.Email == email);
                Assert.IsNotNull(response.GlobalId);
            });
        }

    }
}
