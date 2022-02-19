using CloudHub.Domain.DTO;
using CloudHub.Domain.Entities;

namespace CloudHub.BusinessLogic
{
    public interface IOAuthService
    {
        public Task<OAuthUser?> GetUserByToken(string token, ELoginTypes loginType);
    }
}
