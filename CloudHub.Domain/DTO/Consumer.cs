using CloudHub.Domain.Models;

namespace CloudHub.Domain.DTO
{
    public struct Consumer
    {
        public Client Client { get; set; }
        public UserToken? UserToken { get; set; }
        public Nonce Nonce { get; set; }
    }
}
