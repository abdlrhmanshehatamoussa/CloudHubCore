namespace CloudHub.API.Domain.Models
{
    public class Feature : IBaseTrackableEntity
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TenantId { get; set; }
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual Tenant Tenant { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();
    }
}
