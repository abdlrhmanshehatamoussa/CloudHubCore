namespace CloudHub.Domain.Entities
{
    public class Nonce: IBaseEntity
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public int ClientId { get; set; }
        public DateTime? ConsumedOn { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

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
