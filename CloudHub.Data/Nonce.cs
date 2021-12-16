using System;
using System.Collections.Generic;

namespace CloudHub.Data
{
    public partial class Nonce
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Token { get; set; } = null!;
        public int ClientId { get; set; }
        public DateTime? ConsumedOn { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Application Application { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
    }
}
