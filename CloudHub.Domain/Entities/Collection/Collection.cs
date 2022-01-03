﻿namespace CloudHub.Domain.Entities
{
    public class Collection : IBaseTrackableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? IdentityField { get; set; }
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
