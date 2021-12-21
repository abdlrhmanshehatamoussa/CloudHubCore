using CloudHub.Business.DTO;
using CloudHub.Domain.Entities;

namespace CloudHub.API.DTO
{
    public struct RegisterRequestJson : IJson<RegisterRequest>
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? image_url { get; set; }
        public LoginTypeValues login_type { get; set; }

        public RegisterRequest ToObject()
        {
            return new RegisterRequest()
            {
                Email = email,
                LoginTypeId = login_type,
                Name = name,
                Passcode = password,
                ImageUrl = image_url
            };
        }
    }



    public struct LoginRequestJson : IJson<LoginRequest>
    {
        public string email { get; set; }
        public string password { get; set; }
        public LoginTypeValues login_type { get; set; }

        public LoginRequest ToObject()
        {
            return new LoginRequest()
            {
                Email = email,
                Passcode = password,
                LoginTypeId = login_type
            };
        }
    }


}