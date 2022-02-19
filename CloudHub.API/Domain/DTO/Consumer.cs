using CloudHub.API.Domain.Models;

namespace CloudHub.API.Domain.DTO
{
    public struct Consumer
    {
        public Client Client { get; set; }
        public UserToken? UserToken { get; set; }
        public Nonce Nonce { get; set; }
    }
}
