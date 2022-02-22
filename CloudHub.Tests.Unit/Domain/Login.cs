using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.Utils;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CloudHub.Tests.Unit.Domain
{
    internal class LoginTests : DomainUnitTest
    {
        [Test]
        public async Task HappyScenario()
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
            await UnitOfWork.ClientsRepository.Add(client);
            await UnitOfWork.Save();

            //Create a nonce
            Nonce nonce = new()
            {
                Token = Guid.NewGuid().ToString(),
                ClientId = client.Id,
                CreatedOn = DateTime.Now
            };
            await UnitOfWork.NoncesRepository.Add(nonce);
            await UnitOfWork.Save();

            //Create a user
            string random = HelperFunctions.RandomString(8);
            string email = string.Format("{0}@domain.com", random);
            string password = HelperFunctions.RandomString(10);
            User user = new()
            {
                Email = email,
                Name = random,
                GlobalId = EncryptionService.Hash(random + email),
                TenantId = client.TenantId
            };
            await UnitOfWork.UsersRepository.Add(user);
            Login login = new()
            {
                UserId = user.Id,
                LoginTypeId = ELoginTypes.LOGIN_TYPE_BASIC,
                Passcode = password,
                LoginType = new LoginType() { Name = "Basic", Id = ELoginTypes.LOGIN_TYPE_BASIC }
            };
            await UnitOfWork.LoginsRepository.Add(login);
            await UnitOfWork.Save();

            //Act
            ConsumerCredentials credentials = new()
            {
                ClientKey = client.ClientKey,
                ClientClaim = EncryptionService.Encrypt(client.ClientKey, client.ClientSecret),
                Nonce = nonce.Token
            };
            UserToken response = await UserService.Login(credentials, new CreateLoginDTO(
                email,
                password,
                ELoginTypes.LOGIN_TYPE_BASIC
                ));

            //Assert
            Assert.NotNull(response.Token);
        }
    }
}