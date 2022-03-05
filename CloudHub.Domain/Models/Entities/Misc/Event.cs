using CloudHub.Domain.DTO;
using CloudHub.Domain.Exceptions;
using System.Globalization;
using System.Text.Json;

namespace CloudHub.Domain.Models
{
    public class Event : IBaseEntity
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public string Description { get; set; } = null!;
        public JsonDocument? Payload { get; set; }
        public int TenantId { get; set; }
        public string BuildId { get; set; } = null!;
        public string? Source { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual Tenant Tenant { get; set; } = null!;

        public static Event FromDTO(CreateEventDTO dto, int tenantId)
        {
            JsonDocument? doc = null;
            DateTime timestamp = DateTime.UtcNow;
            if (dto.Payload != null) doc = JsonDocument.Parse(dto.Payload);
            if (dto.CreatedOn != null)
            {
                bool validTimestamp = DateTime.TryParse(dto.CreatedOn, out timestamp);
                if (validTimestamp) timestamp = new DateTime(timestamp.Ticks, DateTimeKind.Utc);
            }
            if (dto.Description == null) throw new MissingParameterException();
            if (dto.Category == null) throw new MissingParameterException();
            if (dto.BuildId == null) throw new MissingParameterException();

            return new Event()
            {
                BuildId = dto.BuildId,
                Category = dto.Category,
                Description = dto.Description,
                Payload = doc,
                TenantId = tenantId,
                CreatedOn = timestamp,
                Source = dto.Source
            };
        }
    }
}
