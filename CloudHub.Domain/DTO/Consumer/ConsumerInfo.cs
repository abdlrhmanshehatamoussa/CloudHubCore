using CloudHub.Domain.Entities;

namespace CloudHub.Domain.Services
{
    internal struct ConsumerInfo
    {
        public Client Client { get; set; }
        public Nonce? Nonce { get; set; }
        public UserToken? UserToken { get; set; }
        public Admin? Admin { get; set; }
    }
}
