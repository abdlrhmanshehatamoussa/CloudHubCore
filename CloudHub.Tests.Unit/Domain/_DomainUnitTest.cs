using CloudHub.Domain.Services;

namespace CloudHub.Tests.Unit.Domain
{
    internal class DomainUnitTest
    {
        protected readonly FakeUnitOfWork UnitOfWork = new FakeUnitOfWork();
        protected readonly IEncryptionService EncryptionService = new FakeEncryptionService();
        protected readonly IOAuthService OAuthService = new FakeOAuthService();

        protected UserService UserService => new(UnitOfWork, OAuthService, EncryptionService);
    }
}
