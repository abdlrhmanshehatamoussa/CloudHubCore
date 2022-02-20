using CloudHub.Domain.Models;

namespace CloudHub.Domain.Services
{
    public interface IOAuthService
    {
        public Task<OAuthUser?> GetUserByToken(string token, ELoginTypes loginType);
    }
}
