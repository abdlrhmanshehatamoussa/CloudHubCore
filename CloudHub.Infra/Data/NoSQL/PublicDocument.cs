using CloudHub.Domain.Entities;

namespace CloudHub.Infra.Data
{
    public class PublicDocument : IPublicDocument
    {
        public dynamic _id { get; set; } = null!;
        public dynamic Body { get; set; } = null!;
    }
}
