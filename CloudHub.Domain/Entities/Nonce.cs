namespace CloudHub.Domain.Entities
{
    public class Nonce
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Token { get; set; } = null!;
        public int ClientId { get; set; }
        public DateTime? ConsumedOn { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual Application Application { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;

        public void GenerateToken()
        {
            string token = string.Empty;
            for (int i = 0; i < 3; i++)
            {
                token += Guid.NewGuid().ToString();
            }
            this.Token = token;
        }
    }
}
