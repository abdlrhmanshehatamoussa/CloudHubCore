using System;
using System.Collections.Generic;

namespace CloudHub.Data
{
    public partial class Release
    {
        public int Id { get; set; }
        public string ReleaseName { get; set; } = null!;
        public string Notes { get; set; } = null!;
        public int ApplicationId { get; set; }
        public DateOnly ReleaseDate { get; set; }

        public virtual Application Application { get; set; } = null!;
    }
}
