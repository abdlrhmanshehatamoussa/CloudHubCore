namespace CloudHub.Domain.Entities
{
    public class Client
    {
        public Client()
        {
            ClientsApplications = new HashSet<ClientApplicationRelation>();
            Nonces = new HashSet<Nonce>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ClientKey { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public ClientTypeValues ClientTypeId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? Active { get; set; }

        public virtual ClientType ClientType { get; set; } = null!;
        public virtual ICollection<ClientApplicationRelation> ClientsApplications { get; set; }
        public virtual ICollection<Nonce> Nonces { get; set; }
    }
}
