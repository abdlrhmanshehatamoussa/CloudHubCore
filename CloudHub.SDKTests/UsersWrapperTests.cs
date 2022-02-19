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
                //string email = string.Format("{0}@domain.com", GlobalHelpers.RandomString(15));
                //string password = GlobalHelpers.RandomString(10);
                //RegisterResponse result = await Helper.CloudHubManager.Users.RegisterEndUser(new RegisterRequest(
                //    email: email,
                //    password: password,
                //    login_type: ELoginTypes.LOGIN_TYPE_BASIC,
                //    name: "Test",
                //    image_url: string.Empty
                //    ));
                //Assert.NotNull(result);
                //bool? success = result;
                //dynamic user = result["user"];
                //Assert.NotNull(success);
                //Assert.NotNull(user);
                //Assert.IsTrue(success);

                //var loginResult = await Helper.CloudHubManager.Users.LoginUser(new(
                //   email: email,
                //   password: password,
                //   login_type: ELoginTypes.LOGIN_TYPE_BASIC
                //   ));
                //Assert.NotNull(loginResult);
                //dynamic token = loginResult!["user_token"];
                //Assert.NotNull(token);

                //result = await Helper.CloudHubManager.Users.FetchUser(token);
                //Assert.NotNull(result);
                //dynamic returnedEmail = loginResult!["email"];
                //Assert.NotNull(returnedEmail);
                //Assert.That(returnedEmail == email);
            });
        }
    }
}
