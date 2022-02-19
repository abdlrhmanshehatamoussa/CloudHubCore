namespace CloudHub.Domain.Models
{
    public class LoginType : ILookupEntity<ELoginTypes>
    {
        public ELoginTypes Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Login> Logins { get; set; } = new HashSet<Login>();
    }
}
