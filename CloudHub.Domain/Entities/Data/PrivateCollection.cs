namespace CloudHub.Domain.Entities
{
    public class PrivateCollection : IBaseTrackableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual ICollection<PrivateDocument> PrivateDocuments { get; set; } = new HashSet<PrivateDocument>();
    }
}