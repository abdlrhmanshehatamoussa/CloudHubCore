using CloudHub.Domain.DTO;

namespace CloudHub.API.Contracts
{
#pragma warning disable IDE1006 // Naming Styles
    public class CreateEventRequest
    {
        public string description { get; set; } = null!;
        public string category { get; set; } = null!;
        public string build_id { get; set; } = null!;
        public string? source { get; set; }
        public string? payload { get; set; }
        public string? created_on { get; set; }

        public CreateEventDTO ToDTO() => new()
        {
            Category = category,
            Description = description,
            Payload = payload,
            CreatedOn = created_on,
            Source = source,
            BuildId = build_id
        };
    }
}
