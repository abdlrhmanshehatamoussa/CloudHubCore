using System.Text;

namespace CloudHub.Domain.Models
{
    public class UserToken: IBaseEntity
    {
        public const int TOKEN_EXPIRES_AFTER_DAYS = 30;

        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(TOKEN_EXPIRES_AFTER_DAYS);
        
        public virtual User User { get; set; } = null!;

        public int RemainingSeconds
        {
            get
            {
                TimeSpan timespan = ExpiryDate - DateTime.UtcNow;
                double seconds = timespan.TotalSeconds;
                seconds = Math.Round(seconds, 0);
                return int.Parse(seconds.ToString());
            }
        }

        public int AgeSeconds
        {
            get
            {
                TimeSpan timespan = DateTime.UtcNow - CreatedOn;
                double seconds = timespan.TotalSeconds;
                seconds = Math.Round(seconds, 0);
                return int.Parse(seconds.ToString());
            }
        }

        public void GenerateNewToken()
        {
            StringBuilder builder = new ();
            for (int i = 0; i < 2; i++)
            {
                builder.AppendJoin("-", Guid.NewGuid());
            }
            this.Token = builder.ToString();
        }
    }
}
