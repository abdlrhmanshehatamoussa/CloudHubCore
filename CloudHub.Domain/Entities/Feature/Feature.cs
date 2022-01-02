namespace CloudHub.Domain.Entities
{
    public class Feature : IBaseTrackableEntity
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();
    }
}
