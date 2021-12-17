namespace CloudHub.Domain.Entities
{
    public class User
    {
        public User()
        {
            Purchases = new HashSet<Purchase>();
            UserTokens = new HashSet<UserToken>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Email { get; set; } = null!;
        public string GlobalId { get; set; } = null!;
        public int ApplicationId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? Active { get; set; }

        public virtual Application Application { get; set; } = null!;
        public virtual Login Login { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
