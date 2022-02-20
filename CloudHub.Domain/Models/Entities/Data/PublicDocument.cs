using System.Text.Json;

namespace CloudHub.Domain.Models
{
    public class PublicDocument : IBaseEntity
    {
        public int Id { get; set; }
        public JsonDocument Body { get; set; } = null!;
        public int PublicCollectionId { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual PublicCollection PublicCollection { get; set; } = null!;
        
    }
}
