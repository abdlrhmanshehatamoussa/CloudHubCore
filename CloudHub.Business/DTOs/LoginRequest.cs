using CloudHub.Domain.Entities;

namespace CloudHub.Business.DTO
{
    public struct LoginRequest
    { 
        public string Email { get; set; }
        public string Passcode { get; set; }
        public LoginTypeValues LoginTypeId { get; set; }
    }
}
