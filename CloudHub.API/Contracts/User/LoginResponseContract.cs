using CloudHub.Domain.Models;

namespace CloudHub.API.Contracts
{
    public struct LoginResponseContract
    {
        public string email { get; set; }
        public string name { get; set; }
        public string login_type { get; set; }
        public string? image_url { get; set; }
        public string user_token { get; set; }
        public int user_token_expires_in { get; set; }
        public string global_id { get; set; }

        public static LoginResponseContract FromUserToken(UserToken token)
        {
            User user = token.User;
            return new()
            {
                email = user.Email,
                name = user.Name,
                login_type = user.Login.LoginType.Name,
                image_url = user.ImageUrl,
                user_token = token.Token,
                user_token_expires_in = token.RemainingSeconds,
                global_id = user.GlobalId,
            };
        }

    }
}