namespace CloudHub.Domain.Entities
{
    public class User : IBaseEntity, ITrackableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Email { get; set; } = null!;
        public string GlobalId { get; set; } = null!;

        public virtual Login Login { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();
        public virtual ICollection<UserToken> UserTokens { get; set; } = new HashSet<UserToken>();
        public virtual ICollection<PrivateDocument> PrivateDocuments { get; set; } = new HashSet<PrivateDocument>();
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
