using CloudHub.BusinessLogic.DTO;
using CloudHub.BusinessLogic.Services;
using CloudHub.Crosscutting;
using CloudHub.Domain.Entities;
using CloudHub.Infra.Data;
using NUnit.Framework;
using System;

namespace CloudHub.Tests.Domain
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
                string random = GlobalHelpers.RandomString(8);
                string password = GlobalHelpers.RandomString(10);
                string email = string.Format("{0}@domain.com", random);

                //Act
                RegisterResponse response = await userService.RegisterNewEndUser(credentials, new RegisterRequest
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
