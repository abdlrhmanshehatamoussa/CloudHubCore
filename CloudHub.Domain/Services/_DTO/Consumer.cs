using CloudHub.Domain.Entities;

namespace CloudHub.Domain.Services
{
    internal struct Consumer
    {
        public UserToken? UserToken { get; set; }
        public Nonce Nonce { get; set; }
    }
}
