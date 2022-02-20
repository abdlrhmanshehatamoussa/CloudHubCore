using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;

namespace CloudHub.API.Contracts
{
    public class RegisterRequestContract
    {
        public RegisterRequestContract(string name, string email, string password, string? image_url, int login_type)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.image_url = image_url;
            this.login_type = login_type;
        }

        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? image_url { get; set; }
        public int login_type { get; set; }

        public CreateUserDTO ToDTO()
        {
            ELoginTypes loginType = Enum.Parse<ELoginTypes>(login_type.ToString());
            return new(name, email, password, image_url, loginType);
        }
    }
}
