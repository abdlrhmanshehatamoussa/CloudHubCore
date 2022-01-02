using CloudHub.Domain.Entities;

namespace CloudHub.Infra.Data
{
    public class PrivateDocument : IPrivateDocument
    {
        public dynamic _id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public dynamic Body { get; set; } = null!;
    }
}
