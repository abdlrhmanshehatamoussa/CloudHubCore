using CloudHub.ApiContracts;
using CloudHub.Domain.Models;
using NUnit.Framework;

namespace CloudHub.Tests.SDK
{
    public class UsersWrapperTests
    {
        [Test]
        public void RegisterThenLogin()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                string email = string.Format("{0}@domain.com", Helper.RandomString(15));
                string password = Helper.RandomString(10);
                RegisterRequestContract registerRequestContract = new()
                {
                    email = email,
                    password = password,
                    login_type = (int)ELoginTypes.LOGIN_TYPE_BASIC,
                    name = "Test",
                    image_url = string.Empty
                };
                RegisterResponseContract result = await Helper.CloudHubManager.Users.RegisterEndUser(registerRequestContract);
                Assert.NotNull(result);
                bool? success = result.success;
                UserContract user = result.user;
                Assert.NotNull(success);
                Assert.NotNull(user);
                Assert.IsTrue(success);

                LoginResponseContract loginResult = await Helper.CloudHubManager.Users.LoginUser(new(
                   email: email,
                   password: password,
                   login_type: (int)ELoginTypes.LOGIN_TYPE_BASIC
                   ));
                Assert.NotNull(loginResult);
                dynamic token = loginResult.user_token;
                Assert.NotNull(token);

                LoginResponseContract fetchResult = await Helper.CloudHubManager.Users.FetchUser(token);
                Assert.NotNull(fetchResult);
                string returnedEmail = fetchResult.email;
                Assert.NotNull(returnedEmail);
                Assert.That(returnedEmail == email);
            });
        }
    }
}
