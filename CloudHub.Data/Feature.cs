using System;
using System.Collections.Generic;

namespace CloudHub.Data
{
    public partial class Feature
    {
        public Feature()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string GlobalId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ApplicationId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? Active { get; set; }

        public virtual Application Application { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
