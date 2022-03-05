using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;

namespace CloudHub.API.Contracts
{
#pragma warning disable IDE1006 // Naming Styles
    public class LoginRequestContract
    {
        public LoginRequestContract(string email, string password, int login_type)
        {
            this.email = email;
            this.password = password;
            this.login_type = login_type;
        }

        public string email { get; set; }
        public string password { get; set; }
        public int login_type { get; set; }

        public CreateLoginDTO ToDTO()
        {
            ELoginTypes loginType = Enum.Parse<ELoginTypes>(login_type.ToString());
            CreateLoginDTO request = new(email, password, loginType);
            return request;
        }
    }
}
