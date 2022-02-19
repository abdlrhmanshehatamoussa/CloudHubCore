namespace CloudHub.API.Domain.Models
{
    public class Login : IBaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Passcode { get; set; } = null!;
        public ELoginTypes LoginTypeId { get; set; }

        public virtual LoginType LoginType { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
