using CloudHub.API.Commons;
using CloudHub.API.Domain.Models;

namespace CloudHub.API.Domain.DTO
{
    public class RegisterRequest
    {
        public RegisterRequest(string name, string email, string password, string? image_url, ELoginTypes login_type)
        {
            ValidationUtils.Mandatory(email, nameof(email));
            ValidationUtils.ValidEmail(email, nameof(email));
            ValidationUtils.Mandatory(name, nameof(name));
            ValidationUtils.Mandatory(password, nameof(password));
            ValidationUtils.MinLength(password,8, nameof(password));
            ValidationUtils.ValidEnumValue<ELoginTypes>(login_type, nameof(login_type));

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
        public ELoginTypes login_type { get; set; }
    }
}
