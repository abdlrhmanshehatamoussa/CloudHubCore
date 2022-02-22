using CloudHub.Domain.Services;
using Moq;

namespace CloudHub.Tests.Unit
{
    internal class UnitTest
    {
        protected readonly TestUnitOfWork UnitOfWork = new TestUnitOfWork();
        protected UserService UserService
        {
            get
            {
                IOAuthService authService = new Mock<IOAuthService>().Object;
                return new(UnitOfWork, authService);
            }
        }
    }
}
