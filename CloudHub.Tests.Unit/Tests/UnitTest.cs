using CloudHub.Domain.Services;
using CloudHub.ServiceImp.OAuth;

namespace CloudHub.Tests.Unit
{
    internal class UnitTest
    {
        protected readonly TestUnitOfWork UnitOfWork;
        protected readonly UserService UserService;
        protected readonly IOAuthService AuthenticationService;

        public UnitTest()
        {
            UnitOfWork = new TestUnitOfWork();
            AuthenticationService = new OAuthService("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=");
            UserService = new(UnitOfWork, AuthenticationService);
        }

    }
}
