using System.Text.Json;

namespace CloudHub.Domain.Models
{
    public class PrivateDocument : IBaseEntity
    {
        public int Id { get; set; }
        public JsonDocument Body { get; set; } = null!;
        public int PrivateCollectionId { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual PrivateCollection PrivateCollection { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
