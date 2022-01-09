namespace CloudHub.Domain.Entities
{
    public class Role : ILookupEntity<ERoles>
    {
        public ERoles Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
