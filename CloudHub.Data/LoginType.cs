using System;
using System.Collections.Generic;

namespace CloudHub.Data
{
    public partial class LoginType
    {
        public LoginType()
        {
            Logins = new HashSet<Login>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? Active { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}
