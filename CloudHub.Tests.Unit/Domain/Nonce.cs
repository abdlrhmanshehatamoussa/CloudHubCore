using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CloudHub.Tests.Unit.Domain
{
    internal class NonceTests : DomainUnitTest
    {

        [Test]
        public async Task GenerateNonce_HappyScenario()
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
                ClientClaim = EncryptionService.Encrypt(client.ClientKey, client.ClientSecret),
                Nonce = nonce.Token
            };

            //Act
            Assert.DoesNotThrowAsync(async () =>
            {
                Nonce createdNonce = await NonceService.GenereateNonce(credentials);
                Assert.IsNotNull(createdNonce);
            });
        }

        [Test]
        public async Task GenerateNonce_WrongCredentials()
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
                ClientClaim = client.ClientKey,//Wrong
                Nonce = nonce.Token
            };

            //Act
            NotAuthenticatedException? ex = Assert.ThrowsAsync<NotAuthenticatedException>(async () =>
            {
                await NonceService.GenereateNonce(credentials);
            });
            Assert.IsNotNull(ex);
        }
    }
}
