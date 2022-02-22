using CloudHub.Domain.Models;
using CloudHub.ServiceProvider;
using NUnit.Framework;
using System;

namespace CloudHub.Tests.Integration
{
    internal class OAuthTests
    {
        protected OAuthService oAuthService = new OAuthService("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=");

        [Test]
        public void UnRegisteredOAuthExtractor()
        {
            string testToken = "";
            Exception? ex = Assert.ThrowsAsync<Exception>(async () =>
            {
                OAuthUser? user = await oAuthService.GetUserByToken(testToken, ELoginTypes.LOGIN_TYPE_FACEBOOK);
            });
            Assert.That(ex?.Message.Contains("LOGIN_TYPE_FACEBOOK") == true);
        }

        //[Test]
        //public void HappyScenarioGoogle()
        //{
        //Assert.DoesNotThrowAsync(async () =>
        //{
        //    string testToken = "ya29.A0ARrdaM_0FxDN4BGGQ2O5GOJPqcALh3ceCac7K7ioAjd1pNSLHFlNRzEOtwu-SHQONhI86mNNJ0kwhsk3FhOqGdxtD_unTS_ySyxpC2SiwhX0AQojq2A36tWncNYSknsEN8pOh1F97nZ9qRqFX4D0DOhe5Ele";
        //    OAuthUser? user = await oAuthService.GetUserByToken(testToken, ELoginTypes.LOGIN_TYPE_GOOGLE);
        //    Assert.IsNotNull(user);
        //    Assert.That(user?.Email == "abdlrhmanshehata@gmail.com");
        //});
        //}
    }
}
