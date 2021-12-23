using CloudHub.Domain.Entities;

namespace CloudHub.Domain.Services
{
    public interface IOAuthService
    {
        public Task<OAuthUser?> GetUserByToken(string token, LoginTypeValues loginType);
    }
}
