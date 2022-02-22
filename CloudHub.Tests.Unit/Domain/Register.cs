using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;
using CloudHub.Utils;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CloudHub.Tests.Unit.Domain
{
    internal class RegisterTests : DomainUnitTest
    {
        [Test]
        public async Task HappyScenario()
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
            await UnitOfWork.ClientsRepository.Add(client);
            await UnitOfWork.Save();

            Nonce nonce = new()
            {
                Token = Guid.NewGuid().ToString(),
                ClientId = client.Id,
                CreatedOn = DateTime.Now
            };
            await UnitOfWork.NoncesRepository.Add(nonce);
            await UnitOfWork.Save();

            ConsumerCredentials credentials = new()
            {
                ClientKey = client.ClientKey,
                ClientClaim = EncryptionService.Encrypt(client.ClientKey, client.ClientSecret),
                Nonce = nonce.Token
            };
            string random = HelperFunctions.RandomString(8);
            string password = HelperFunctions.RandomString(10);
            string email = string.Format("{0}@domain.com", random);

            //Act
            User response = await UserService.RegisterNewUser(credentials, new CreateUserDTO
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
        }

    }
}
