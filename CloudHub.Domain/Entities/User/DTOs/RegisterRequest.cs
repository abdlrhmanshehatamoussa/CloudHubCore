namespace CloudHub.Domain.Entities
{
    public struct RegisterRequest
    {
        public RegisterRequest(string imageUrl, string email, string password, int loginTypeId, string name)
        {
            ImageUrl = imageUrl;
            Email = email;
            Passcode = password;
            LoginTypeId = loginTypeId;
            Name = name;
        }

        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Passcode { get; set; }
        public int LoginTypeId { get; set; }
        public string Name { get; set; }
    }
}
