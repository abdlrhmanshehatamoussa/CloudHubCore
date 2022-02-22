using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.Utils;
using NUnit.Framework;
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
            Client client = NewClient();
            await UnitOfWork.ClientsRepository.Add(client);
            await UnitOfWork.Save();

            //Create a nonce
            Nonce nonce = NewNonce(client.Id);
            await UnitOfWork.NoncesRepository.Add(nonce);
            await UnitOfWork.Save();

            //Create a user
            string email = string.Format("{0}@domain.com", HelperFunctions.RandomString(10));
            string password = HelperFunctions.RandomString(10);
            User user = NewBasicUser(email, password, client.TenantId);
            await UnitOfWork.UsersRepository.Add(user);
            await UnitOfWork.Save();

            //Act
            ConsumerCredentials credentials = new()
            {
                ClientKey = client.ClientKey,
                Nonce = EncryptionService.Encrypt(nonce.Token, client.ClientSecret)
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