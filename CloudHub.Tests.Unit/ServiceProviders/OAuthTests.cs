using CloudHub.Domain.Models;
using CloudHub.ServiceProvider;
using Moq;
using NUnit.Framework;
using System;

namespace CloudHub.Tests.Unit.ServiceProviders
{
    internal class OAuthTests
    {
        private readonly OAuthService _service;

        public OAuthTests()
        {
            Mock<IConfigOAuthService> mock = new();
            mock.Setup(m => m.GoogleOAuthUrl).Returns("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=");
            _service = new OAuthService(mock.Object);
        }


        [Test]
        public void GetUserByToken_UnregisteredOAuthExtractor()
        {
            Exception? ex = Assert.ThrowsAsync<Exception>(async () =>
            {
                await _service.GetUserByToken("", ELoginTypes.LOGIN_TYPE_LINKEDIN);
            });
            Assert.NotNull(ex);
            Assert.True(ex!.Message.Contains("LOGIN_TYPE_LINKEDIN"));
        }
    }
}
