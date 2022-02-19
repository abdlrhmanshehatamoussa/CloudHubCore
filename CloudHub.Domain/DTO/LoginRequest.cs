using CloudHub.Domain.Models;
using CloudHub.Domain.Utils;

namespace CloudHub.Domain.DTO
{
    public class LoginRequest
    {
        public LoginRequest(string email, string password, ELoginTypes login_type)
        {
            ValidationUtils.Mandatory(email, nameof(email));
            ValidationUtils.Mandatory(password, nameof(password));
            ValidationUtils.ValidEnumValue<ELoginTypes>(login_type, nameof(login_type));

            this.Email = email;
            this.Password = password;
            this.LoginType = login_type;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public ELoginTypes LoginType { get; set; }
    }
}
