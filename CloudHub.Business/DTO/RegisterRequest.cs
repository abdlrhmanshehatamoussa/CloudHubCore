using CloudHub.Domain.Entities;

namespace CloudHub.Business.DTO
{
    public struct RegisterRequest
    {
        public RegisterRequest(string email, string password, string name, string? imageUrl, LoginTypeValues loginTypeId)
        {
            ImageUrl = imageUrl;
            Email = email;
            Passcode = password;
            LoginTypeId = loginTypeId;
            Name = name;
        }

        public string? ImageUrl { get; set; }
        public string Email { get; set; }
        public string Passcode { get; set; }
        public LoginTypeValues LoginTypeId { get; set; }
        public string Name { get; set; }
    }
}
