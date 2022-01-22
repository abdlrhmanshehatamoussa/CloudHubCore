﻿namespace CloudHub.Domain.Entities
{
    public class Client: IBaseTrackableEntity
    {
        public int Id { get ; set ; }
        public string Name { get; set; } = null!;
        public string ClientKey { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public bool Active { get ; set ; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get ; set ; } = DateTime.UtcNow;

        public virtual ICollection<Nonce> Nonces { get; set; } = new HashSet<Nonce>();
    }
}