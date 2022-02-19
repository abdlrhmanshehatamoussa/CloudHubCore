using CloudHub.Domain.Entities;

namespace CloudHub.BusinessLogic.DTO
{
    public struct Consumer
    {
        public Client Client { get; set; }
        public UserToken? UserToken { get; set; }
        public Nonce Nonce { get; set; }
    }
}
