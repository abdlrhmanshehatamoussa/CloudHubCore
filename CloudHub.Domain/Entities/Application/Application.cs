namespace CloudHub.Domain.Entities
{
    public class Application
    {
        public Application()
        {
            Actions = new HashSet<UserAction>();
            ApplicationSecrets = new HashSet<ApplicationSecret>();
            ClientsApplications = new HashSet<ClientApplicationRelation>();
            Features = new HashSet<Feature>();
            Nonces = new HashSet<Nonce>();
            Releases = new HashSet<Release>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid Guid { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<UserAction> Actions { get; set; }
        public virtual ICollection<ApplicationSecret> ApplicationSecrets { get; set; }
        public virtual ICollection<ClientApplicationRelation> ClientsApplications { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
        public virtual ICollection<Nonce> Nonces { get; set; }
        public virtual ICollection<Release> Releases { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
