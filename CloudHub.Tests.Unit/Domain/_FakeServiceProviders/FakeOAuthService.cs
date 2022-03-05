using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using System.Threading.Tasks;

namespace CloudHub.Tests.Unit.Domain
{
    internal class FakeOAuthService : IOAuthService
    {
        public async Task<OAuthUser?> GetUserByToken(string token, ELoginTypes loginType)
        {
            await Task.Delay(100);
            return null;
        }
    }
}
