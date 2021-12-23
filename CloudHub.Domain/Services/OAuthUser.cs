using CloudHub.Domain.Exceptions;

namespace CloudHub.Domain.Services
{
    public class OAuthUser
    {
        public OAuthUser(string email, string openId, int expiresInSeconds)
        {
            //TODO: Throw proper exceptions
            if (email == null) { throw new NotAuthenticatedException(); }
            if (openId == null) { throw new NotAuthenticatedException(); }

            Email = email;
            OpenId = openId;
            ExpiresInSeconds = expiresInSeconds;
        }

        public string Email { get; set; }
        public string OpenId { get; set; }
        public int ExpiresInSeconds { get; set; }
    }
}
