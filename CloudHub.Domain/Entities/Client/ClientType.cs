namespace CloudHub.Domain.Entities
{
    public enum ClientTypeValues
    {
        ADMIN = 7061601,
        DASHBOARD = 41596505,
        APP = 38359937,
    }
    public class ClientType
    {
        public ClientType()
        {
            Clients = new HashSet<Client>();
        }

        public ClientTypeValues Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
