namespace CloudHub.Domain.DTO
{
    public class CreateEventDTO
    {
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string BuildId { get; set; } = null!;
        public string? Source { get; set; }
        public string? Payload { get; set; }
        public string? CreatedOn { get; set; }
    }
}
