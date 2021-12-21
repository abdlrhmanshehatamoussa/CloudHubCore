using CloudHub.Domain.Entities;

namespace CloudHub.Domain.Services
{
    public struct ConsumerInfo
    { 
        public ClientApplicationRelation ClientApplication { get; set; }
        public Nonce? Nonce { get; set; }
        public UserToken? UserToken { get; set; }
    }
}
