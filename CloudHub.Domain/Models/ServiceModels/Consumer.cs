namespace CloudHub.Domain.Models
{
    public struct Consumer
    {
        public Client Client { get; set; }
        public UserToken? UserToken { get; set; }
        public Nonce Nonce { get; set; }
    }
}
