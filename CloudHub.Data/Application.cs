using System;
using System.Collections.Generic;

namespace CloudHub.Data
{
    public partial class Application
    {
        public Application()
        {
            Actions = new HashSet<Action>();
            ApplicationSecrets = new HashSet<ApplicationSecret>();
            ClientsApplications = new HashSet<ClientsApplication>();
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

        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<ApplicationSecret> ApplicationSecrets { get; set; }
        public virtual ICollection<ClientsApplication> ClientsApplications { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
        public virtual ICollection<Nonce> Nonces { get; set; }
        public virtual ICollection<Release> Releases { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
