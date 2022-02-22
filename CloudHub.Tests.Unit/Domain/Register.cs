using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;
using CloudHub.Utils;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CloudHub.Tests.Unit.Domain
{
    internal class RegisterTests : DomainUnitTest
    {
        [Test]
        public async Task HappyScenario()
        {
            //Arrange
            Client client = NewClient();
            await UnitOfWork.ClientsRepository.Add(client);
            await UnitOfWork.Save();

            Nonce nonce = NewNonce(client.Id);
            await UnitOfWork.NoncesRepository.Add(nonce);
            await UnitOfWork.Save();

            ConsumerCredentials credentials = new()
            {
                ClientKey = client.ClientKey,
                Nonce = EncryptionService.Encrypt(nonce.Token, client.ClientSecret)
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
