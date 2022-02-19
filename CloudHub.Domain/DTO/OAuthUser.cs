using CloudHub.Domain.Exceptions;

namespace CloudHub.Domain.DTO
{
    public class OAuthUser
    {
        public OAuthUser(string email, string openId, int expiresInSeconds)
        {
            Email = email ?? throw new NotAuthenticatedException();
            OpenId = openId ?? throw new NotAuthenticatedException();
            ExpiresInSeconds = expiresInSeconds;
        }

        public string Email { get; set; }
        public string OpenId { get; set; }
        public int ExpiresInSeconds { get; set; }
    }
}
