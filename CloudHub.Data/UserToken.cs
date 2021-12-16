using System;
using System.Collections.Generic;

namespace CloudHub.Data
{
    public partial class UserToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool? Active { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
