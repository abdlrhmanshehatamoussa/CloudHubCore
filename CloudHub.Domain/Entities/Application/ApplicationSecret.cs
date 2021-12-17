namespace CloudHub.Domain.Entities
{
    public class ApplicationSecret
    {
        public int Id { get; set; }
        public string SecretKey { get; set; } = null!;
        public string SecretValue { get; set; } = null!;
        public int ApplicationId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? Active { get; set; }

        public virtual Application Application { get; set; } = null!;
    }
}
