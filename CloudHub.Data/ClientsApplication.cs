﻿using System;
using System.Collections.Generic;

namespace CloudHub.Data
{
    public partial class ClientsApplication
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int ClientId { get; set; }
        public bool? Active { get; set; }

        public virtual Application Application { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
    }
}
