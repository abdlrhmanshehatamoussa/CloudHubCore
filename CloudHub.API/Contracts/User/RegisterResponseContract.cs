using CloudHub.Domain.Models;

namespace CloudHub.API.Contracts
{
    public struct RegisterResponseContract
    {
        public RegisterResponseContract(bool success, UserContract user)
        {
            this.success = success;
            this.user = user;
        }

        public bool success { get; set; }
        public UserContract user { get; set; }

        public static RegisterResponseContract FromUser(User user)
        {
            return new()
            {
                success = true,
                user = new()
                {
                    email = user.Email,
                    name = user.Name,
                    image_url = user.ImageUrl,
                    global_id = user.GlobalId

                }
            };
        }
    }

    public struct UserContract
    {
        public UserContract(string email, string name, string? image_url, string global_id)
        {
            this.email = email;
            this.name = name;
            this.image_url = image_url;
            this.global_id = global_id;
        }

        public string email { get; set; }
        public string name { get; set; }
        public string? image_url { get; set; }
        public string global_id { get; set; }
    }
}
