using System;
using System.Collections.Generic;

namespace CloudHub.Data
{
    public partial class Migration
    {
        public int Id { get; set; }
        public long Timestamp { get; set; }
        public string Name { get; set; } = null!;
    }
}
