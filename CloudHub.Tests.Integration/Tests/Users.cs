using CloudHub.API.Contracts;
using CloudHub.Domain.Models;
using CloudHub.Utils;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudHub.Tests.Integration
{
    internal class UsersTests : IntegrationTest
    {
        [Test]
        public async Task RegisterLoginFetchScenario()
        {
            //Arrange
            string email = string.Format("{0}@gmail.com", HelperFunctions.RandomString(10));
            string password = HelperFunctions.RandomString(10);
            int loginType = (int)ELoginTypes.LOGIN_TYPE_BASIC;
            RegisterRequestContract registerContract = new(HelperFunctions.RandomString(10), email, password, "", loginType);
            string nonce = await GetNonce();

            //Act
            HttpResponseMessage response2 = await _myHttpClient.PostAsyncJson("users", registerContract, BuildHeaders(nonce));

            //Assert
            Assert.That(response2.IsSuccessStatusCode, Is.True);
            RegisterResponseContract registerResponse = await response2.Parse<RegisterResponseContract>();
            Assert.That(registerResponse.user.email == email, Is.True);

            //=====================

            //Arrange
            nonce = await GetNonce();

            //Act
            LoginRequestContract loginContract = new (email, password, loginType);
            HttpResponseMessage response3 = await _myHttpClient.PostAsyncJson("users/login", loginContract, BuildHeaders(nonce));

            //Assert
            Assert.That(response3.IsSuccessStatusCode, Is.True);
            LoginResponseContract loginResponse = await response3.Parse<LoginResponseContract>();
            Assert.That(loginResponse.email, Is.EqualTo(email));
            Assert.IsNotNull(loginResponse.user_token);

            //=====================

            //Arrange
            nonce = await GetNonce();
            string userToken = loginResponse.user_token;

            //Act
            HttpResponseMessage response4 = await _myHttpClient.GetAsyncCustom("users", BuildHeaders(nonce, userToken));
            LoginResponseContract fetchResponse = await response4.Parse<LoginResponseContract>();

            //Assert
            Assert.That(response4.IsSuccessStatusCode, Is.True);
            Assert.That(fetchResponse.email, Is.EqualTo(email));
        }
    }
}
