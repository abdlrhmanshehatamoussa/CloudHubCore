using CloudHub.Crosscutting;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using NUnit.Framework;
using System;

namespace CloudHub.Tests.User
{
    public class RegisterTests
    {
        private UserService userService = null!;
        private UnitOfWork unitOfWork = Constants.UnitOfWork();

        [SetUp]
        public void Setup()
        {
            userService = new UserService(unitOfWork, Constants.EnvironmentSettings, Constants.AuthenticationService);
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
                    ClientKey = "asdasdasdjkhagsduiasd",
                    ClientSecret = "asdaiu8q73g1urif1y3biy1fb87bf173fb",
                };
                await unitOfWork.ClientsRepository.Add(client);
                await unitOfWork.Save();

                Nonce nonce = new()
                {
                    Token = "asdasd1q23e1u2y4e78gwdiqugiyfg",
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
                string random = Constants.RandomString(8);
                string email = random + "@gmail.com";

                //Act
                RegisterResponse response = await userService.RegisterNewEndUser(credentials, new RegisterRequest
                 (
                     email,
                     "123456789",
                     random,
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
