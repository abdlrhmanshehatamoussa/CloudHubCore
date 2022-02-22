using CloudHub.Domain.Exceptions;
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
            //Create a client
            Client client = NewClient();
            await UnitOfWork.ClientsRepository.Add(client);
            await UnitOfWork.Save();

            //Create a nonce
            Nonce nonce = NewNonce(client.Id);
            await UnitOfWork.NoncesRepository.Add(nonce);
            await UnitOfWork.Save();

            //Create a user
            string random = HelperFunctions.RandomString(8);
            string email = string.Format("{0}@domain.com", random);
            string password = HelperFunctions.RandomString(10);
            User user = NewBasicUser(email, password, client.TenantId);
            await UnitOfWork.UsersRepository.Add(user);
            await UnitOfWork.Save();

            //Create User Token
            UserToken token = new UserToken()
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
            };
            await UnitOfWork.UserTokensRepository.Add(token);
            await UnitOfWork.Save();

            ConsumerCredentials credentials = new()
            {
                ClientKey = client.ClientKey,
                Nonce = EncryptionService.Encrypt(nonce.Token, client.ClientSecret),
                UserToken = token.Token
            };
            UserToken fetchResponse = await UserService.FetchUser(credentials);
            Assert.NotNull(fetchResponse);
            string returnedMail = fetchResponse.User.Email;
            Assert.NotNull(returnedMail);
            Assert.That(returnedMail == email);
        }

        [Test]
        public async Task UnEncryptedNonce()
        {
            //Create a client
            Client client = NewClient();
            await UnitOfWork.ClientsRepository.Add(client);
            await UnitOfWork.Save();

            //Create a nonce
            Nonce nonce = NewNonce(client.Id);
            await UnitOfWork.NoncesRepository.Add(nonce);
            await UnitOfWork.Save();

            //Create a user
            string random = HelperFunctions.RandomString(8);
            string email = string.Format("{0}@domain.com", random);
            string password = HelperFunctions.RandomString(10);
            User user = NewBasicUser(email, password, client.TenantId);
            await UnitOfWork.UsersRepository.Add(user);
            await UnitOfWork.Save();

            //Create User Token
            UserToken token = new UserToken()
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
            };
            await UnitOfWork.UserTokensRepository.Add(token);
            await UnitOfWork.Save();

            ConsumerCredentials credentials = new()
            {
                ClientKey = client.ClientKey,
                Nonce = nonce.Token, //Unencrypted
                UserToken = token.Token
            };
            InvalidNonceException? ex = Assert.ThrowsAsync<InvalidNonceException>(async () =>
            {
                UserToken fetchResponse = await UserService.FetchUser(credentials);
            });
        }

    }
}
