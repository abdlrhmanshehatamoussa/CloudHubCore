using CloudHub.Domain.Entities;

namespace CloudHub.Business.Services
{
    public struct ClientInfo
    { 
        public ClientTypeValues ClientType { get; set; }
        public int ClientId { get; internal set; }
        public int ApplicationId { get; set; }
        public int? NonceId { get; set; }
        public string ApplicationGuid { get; set; }
    }
}
