using CloudHub.Crosscutting;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using NUnit.Framework;
using System;

namespace CloudHub.Tests.UserTests
{
    public class LoginTests
    {
        private readonly UnitOfWork unitOfWork = Constants.UnitOfWork();
        private UserService userService = null!;

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

                //Create a client
                Client client = new()
                {
                    Name = "testclient",
                    ClientKey = Guid.NewGuid().ToString(),
                    ClientSecret = Guid.NewGuid().ToString(),
                };
                await unitOfWork.ClientsRepository.Add(client);
                await unitOfWork.Save();

                //Create a nonce
                Nonce nonce = new()
                {
                    Token = Guid.NewGuid().ToString(),
                    ClientId = client.Id,
                    CreatedOn = DateTime.Now
                };
                await unitOfWork.NoncesRepository.Add(nonce);
                await unitOfWork.Save();

                //Create a user
                string random = Constants.RandomString(8);
                string email = string.Format("{0}@domain.com", random);
                string password = Constants.RandomString(10);
                User user = new()
                {
                    Email = email,
                    Name = random,
                    GlobalId = SecurityHelper.Hash256(random + email),
                    RoleId = ERoles.EndUser,
                    Role = new () { Id = ERoles.EndUser, Name = "EndUser" }
                };
                await unitOfWork.UsersRepository.Add(user);
                Login login = new ()
                {
                    UserId = user.Id,
                    LoginTypeId = ELoginTypes.LOGIN_TYPE_BASIC,
                    Passcode = password,
                    LoginType = new LoginType() { Name = "Basic", Id = ELoginTypes.LOGIN_TYPE_BASIC }
                };
                await unitOfWork.LoginsRepository.Add(login);
                await unitOfWork.Save();

                //Act
                ConsumerCredentials credentials = new()
                {
                    ClientKey = client.ClientKey,
                    ClientClaim = SecurityHelper.EncryptAES(client.ClientKey, client.ClientSecret),
                    Nonce = nonce.Token
                };
                LoginResponse response = await userService.Login(credentials, new LoginRequest(
                    email,
                    password,
                    ELoginTypes.LOGIN_TYPE_BASIC
                    ));

                //Assert
                Assert.NotNull(response.TokenBody);
            });
        }
    }
}