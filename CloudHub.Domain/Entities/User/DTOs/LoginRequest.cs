namespace CloudHub.Domain.Entities
{
    public struct LoginRequest
    {
        public LoginRequest(string email, string password, LoginTypeValues loginTypeId)
        {
            Email = email;
            Passcode = password;
            LoginTypeId = loginTypeId;
        }
        public string Email { get; set; }
        public string Passcode { get; set; }
        public LoginTypeValues LoginTypeId { get; set; }
    }
}
