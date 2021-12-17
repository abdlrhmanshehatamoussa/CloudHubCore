namespace CloudHub.Domain.Entities
{
    public class UserAction
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public string? Description { get; set; }
        public string? Payload { get; set; }
        public int? AppVersion { get; set; }
        public int ApplicationId { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Application Application { get; set; } = null!;
    }
}
