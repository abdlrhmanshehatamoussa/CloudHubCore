namespace CloudHub.Domain.DTO
{
    public struct ConsumerCredentials
    {
        public string ClientKey { get; set; }
        public string? Nonce { get; set; }
        public string? UserToken { get; set; }
        public AdminCredentials? AdminCredentials { get; set; }
    }
    public struct AdminCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
