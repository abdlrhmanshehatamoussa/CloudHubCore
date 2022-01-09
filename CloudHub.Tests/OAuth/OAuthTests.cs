using CloudHub.Domain.Entities;
using CloudHub.Domain.Services;
using CloudHub.Infra.Services;
using Moq;
using NUnit.Framework;
using System;

namespace CloudHub.Tests.OAuth
{
    public class OAuthTests
    {
        private OAuthService oAuthService = null!;

        [SetUp]
        public void Setup()
        {
            Mock<IGoogleServicesConfigurations> mock = new();
            mock.Setup(x => x.GoogleTokenInfoApiUrl).Returns("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=");
            oAuthService = new OAuthService(new GoogleOAuthExtractor(mock.Object));
        }

        [Test]
        public void UnRegisteredOAuthExtractor()
        {
            Exception? ex = Assert.ThrowsAsync<Exception>(async () =>
            {
                string testToken = "ya29.a0ARrdaM-3GuDHx8FqkLg56ObadhGgKMCaFcKLdLAmVc5P184DlypEB9aSiovD_9VmdlfhnHulgm3FiA0UPlaBbhYy_kb-zTrlPo-YiDebHc5OPYRqC0V5pmXgclo98DGMtH0M-L7iZC2yphrbSK-zl6ZY24kW5R0";
                OAuthUser? user = await oAuthService.GetUserByToken(testToken, ELoginTypes.LOGIN_TYPE_FACEBOOK);
            });

            Assert.That(ex?.Message.Contains("LOGIN_TYPE_FACEBOOK") == true);
        }

        [Test]
        public void HappyScenarioGoogle()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                string testToken = "ya29.a0ARrdaM-xXIC9l7CsL6u0Lxby__Ez8qSbZ7WOjauKphP0Fbz8OwBsuT9wRDdPQ9HXc6DqxU8d12wKetNg_CByejknahfsdUrnqmvAk4cir1dj92YxGrI8CI8Z-Q5qQJZ9YQd2v4yElBErxGtIa6skhmwlilf2W1I";
                OAuthUser? user = await oAuthService.GetUserByToken(testToken, ELoginTypes.LOGIN_TYPE_GOOGLE);
                Assert.IsNotNull(user);
                Assert.That(user?.Email == "abdlrhmanshehata@gmail.com");
            });
        }
    }
}
