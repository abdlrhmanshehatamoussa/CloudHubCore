using CloudHub.Utils;
using CloudHub.Factories;
using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using NUnit.Framework;
using System;

namespace CloudHub.DomainTests
{
    public class LoginTests
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
                Nonce nonce = new()
                {
                    Token = Guid.NewGuid().ToString(),
                    ClientId = client.Id,
                    CreatedOn = DateTime.Now
                };
                await unitOfWork.NoncesRepository.Add(nonce);
                await unitOfWork.Save();

                //Create a user
                string random = Helper.RandomString(8);
                string email = string.Format("{0}@domain.com", random);
                string password = Helper.RandomString(10);
                User user = new()
                {
                    Email = email,
                    Name = random,
                    GlobalId = SecurityHelper.Hash256(random + email),
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
                    Nonce = nonce.Token
                };
                UserToken response = await userService.Login(credentials, new CreateLoginDTO(
                    email,
                    password,
                    ELoginTypes.LOGIN_TYPE_BASIC
                    ));

                //Assert
                Assert.NotNull(response.Token);
            });
        }
    }
}