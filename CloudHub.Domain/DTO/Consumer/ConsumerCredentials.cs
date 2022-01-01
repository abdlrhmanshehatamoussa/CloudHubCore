namespace CloudHub.Domain.DTO
{
    public struct ConsumerCredentials
    { 
        public string ClientKey { get; set; }
        public string? Nonce { get; set; }
        public string? UserToken { get; set; }
    }
}
