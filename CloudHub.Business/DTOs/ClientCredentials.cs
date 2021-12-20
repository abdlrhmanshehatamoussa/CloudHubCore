namespace CloudHub.Business.DTO
{
    public struct ClientCredentials
    { 
        public string ApplicationGuid { get; set; }
        public string ClientKey { get; set; }

        public string? Nonce { get; set; }
    }
}
