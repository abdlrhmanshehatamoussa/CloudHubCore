using CloudHub.BusinessLogic.Utils;
using CloudHub.Domain.Entities;

namespace CloudHub.BusinessLogic.DTO
{
    public class LoginRequest
    {
        public LoginRequest(string email, string password, ELoginTypes login_type)
        {
            ValidationUtils.Mandatory(email, nameof(email));
            ValidationUtils.Mandatory(password, nameof(password));
            ValidationUtils.ValidEnumValue<ELoginTypes>(login_type, nameof(login_type));

            this.email = email;
            this.password = password;
            this.login_type = login_type;
        }

        public string email { get; set; }
        public string password { get; set; }
        public ELoginTypes login_type { get; set; }
    }
}
