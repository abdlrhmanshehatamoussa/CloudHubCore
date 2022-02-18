using CloudHub.Crosscutting;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using NUnit.Framework;
using System;

namespace CloudHub.Tests
{
    public class FetchUserTests
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
                    Tenant = new() { Id = 1, Name = "Tenant 1" },
                    TenantId = 1
                };
                await unitOfWork.ClientsRepository.Add(client);
                await unitOfWork.Save();

                //Create a nonce
                Nonce nonce1 = new()
                {
                    Token = Guid.NewGuid().ToString(),
                    ClientId = client.Id,
                    CreatedOn = DateTime.Now
                };
                await unitOfWork.NoncesRepository.Add(nonce1);
                await unitOfWork.Save();

                Nonce nonce2 = new()
                {
                    Token = Guid.NewGuid().ToString(),
                    ClientId = client.Id,
                    CreatedOn = DateTime.Now
                };
                await unitOfWork.NoncesRepository.Add(nonce2);
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
                    Role = new () { Id = ERoles.EndUser, Name = "EndUser" },
                    TenantId = client.TenantId
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
                    Nonce = nonce1.Token
                };
                LoginResponse response = await userService.Login(credentials, new LoginRequest(
                    email,
                    password,
                    ELoginTypes.LOGIN_TYPE_BASIC
                    ));

                //Assert
                string token = response.TokenBody;
                Assert.NotNull(token);

                credentials = new()
                {
                    ClientKey = client.ClientKey,
                    ClientClaim = SecurityHelper.EncryptAES(client.ClientKey, client.ClientSecret),
                    Nonce = nonce2.Token,
                    UserToken = token
                };
                response = await userService.FetchUser(credentials);
                Assert.NotNull(response);
                Assert.NotNull(response.Email);
                Assert.That(response.Email == email);
            });
        }
    }
}
