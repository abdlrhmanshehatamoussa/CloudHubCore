using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;
using CloudHub.Utils;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CloudHub.Tests.Unit.Domain
{
    internal class FetchUserTests : DomainUnitTest
    {
        [Test]
        public async Task HappyScenario()
        {
            //TODO: This whole test needs to be revamped, this is not a unit test, this is a full secnario

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
            Nonce nonce1 = new()
            {
                Token = Guid.NewGuid().ToString(),
                ClientId = client.Id,
                CreatedOn = DateTime.Now
            };
            await UnitOfWork.NoncesRepository.Add(nonce1);
            await UnitOfWork.Save();

            Nonce nonce2 = new()
            {
                Token = Guid.NewGuid().ToString(),
                ClientId = client.Id,
                CreatedOn = DateTime.Now
            };
            await UnitOfWork.NoncesRepository.Add(nonce2);
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
                Nonce = nonce1.Token
            };

            UserToken token = await UserService.Login(credentials, new CreateLoginDTO(
                email,
                password,
                ELoginTypes.LOGIN_TYPE_BASIC
                ));

            //Assert
            Assert.NotNull(token);
            Assert.NotNull(token.Token);

            credentials = new()
            {
                ClientKey = client.ClientKey,
                ClientClaim = EncryptionService.Encrypt(client.ClientKey, client.ClientSecret),
                Nonce = nonce2.Token,
                UserToken = token.Token
            };
            token = await UserService.FetchUser(credentials);
            Assert.NotNull(token);
            string returnedMail = token.User.Email;
            Assert.NotNull(returnedMail);
            Assert.That(returnedMail == email);
        }
    }
}
