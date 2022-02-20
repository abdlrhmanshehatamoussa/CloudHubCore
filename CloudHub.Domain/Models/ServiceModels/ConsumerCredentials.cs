namespace CloudHub.Domain.Models
{
    public struct ConsumerCredentials
    {
        public string? ClientKey { get; set; }
        public string? ClientClaim { get; set; }
        public string? Nonce { get; set; }
        public string? UserToken { get; set; }
    } 
}
