namespace CloudHub.Domain.Entities
{
    public class AdminType : ILookupEntity<EAdminTypes>
    {
        public EAdminTypes Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Admin> Admins { get; set; } = new HashSet<Admin>();
    }
}
