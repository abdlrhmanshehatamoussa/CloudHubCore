namespace CloudHub.Domain.Entities
{
    public class Tenant : IBaseTrackableEntity
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Feature> Features { get; set; } = new HashSet<Feature>();
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
        public virtual ICollection<Client> Clients { get; set; } = new HashSet<Client>();

    }
}
