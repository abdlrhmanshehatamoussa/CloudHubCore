using CloudHub.BusinessLogic.DTO;
using CloudHub.Domain.Entities;

namespace CloudHub.BusinessLogic.Services
{
    public interface IOAuthService
    {
        public Task<OAuthUser?> GetUserByToken(string token, ELoginTypes loginType);
    }
}
