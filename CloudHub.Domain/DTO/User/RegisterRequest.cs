using CloudHub.Domain.Entities;

namespace CloudHub.Domain.DTO
{
    public class RegisterRequest
    {
        public RegisterRequest(string name, string email, string password, string? image_url, LoginTypeValues login_type)
        {
            ValidationUtils.Mandatory(email, nameof(email));
            ValidationUtils.Mandatory(name, nameof(name));
            ValidationUtils.Mandatory(password, nameof(password));
            ValidationUtils.ValidEnumValue<LoginTypeValues>(login_type, nameof(login_type));

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
        public LoginTypeValues login_type { get; set; }
    }
}
