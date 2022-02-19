using CloudHub.API.Domain.DTO;
using CloudHub.API.Domain.Models;

namespace CloudHub.API.Domain.Services
{
    public interface IOAuthService
    {
        public Task<OAuthUser?> GetUserByToken(string token, ELoginTypes loginType);
    }
}
