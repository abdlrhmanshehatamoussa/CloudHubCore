using System;
using System.Collections.Generic;

namespace CloudHub.Data
{
    public partial class Login
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Passcode { get; set; } = null!;
        public int LoginTypeId { get; set; }

        public virtual LoginType LoginType { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
