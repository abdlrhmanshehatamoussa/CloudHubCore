﻿using System;
using System.Collections.Generic;

namespace CloudHub.Data
{
    public partial class ClientType
    {
        public ClientType()
        {
            Clients = new HashSet<Client>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
