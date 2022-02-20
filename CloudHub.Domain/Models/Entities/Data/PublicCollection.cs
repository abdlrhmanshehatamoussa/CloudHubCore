namespace CloudHub.Domain.Models
{
    public class PublicCollection : IBaseTrackableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TenantId { get; set; }
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual ICollection<PublicDocument> PublicDocuments { get; set; } = new HashSet<PublicDocument>();
        public virtual Tenant Tenant { get; set; } = null!;
    }
}
