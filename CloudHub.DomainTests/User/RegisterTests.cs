using CloudHub.API.Commons;
using CloudHub.API.Domain.DTO;
using CloudHub.API.Domain.Models;
using CloudHub.API.Domain.Services;
using CloudHub.API.ServicesImplementation;
using NUnit.Framework;
using System;

namespace CloudHub.DomainTests
{
    public class RegisterTests
    {
        private readonly UnitOfWork unitOfWork = Helper.UnitOfWork();
        private UserService userService = null!;

        [SetUp]
        public void Setup()
        {
            userService = new UserService(unitOfWork, Helper.EnvironmentSettings, Helper.AuthenticationService);
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
                string random = Helper.RandomString(8);
                string password = Helper.RandomString(10);
                string email = string.Format("{0}@domain.com", random);

                //Act
                RegisterResponse response = await userService.RegisterNewUser(credentials, new RegisterRequest
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
