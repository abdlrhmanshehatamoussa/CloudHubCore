using System;
using System.Collections.Generic;

namespace CloudHub.Data
{
    public interface IUser
    {
        bool? Active { get; set; }
        Application Application { get; set; }
        int ApplicationId { get; set; }
        DateTime CreatedOn { get; set; }
        string Email { get; set; }
        string GlobalId { get; set; }
        int Id { get; set; }
        string? ImageUrl { get; set; }
        Login Login { get; set; }
        DateTime ModifiedOn { get; set; }
        string Name { get; set; }
        ICollection<Purchase> Purchases { get; set; }
        ICollection<UserToken> UserTokens { get; set; }
    }

    public partial class User : IUser
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
