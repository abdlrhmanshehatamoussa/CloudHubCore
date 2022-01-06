namespace CloudHub.Domain.Entities
{
    public class ClientType : ILookupEntity<EClientTypes>
    {
        public EClientTypes Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Client> Clients { get; set; } = new HashSet<Client>();
    }
}
