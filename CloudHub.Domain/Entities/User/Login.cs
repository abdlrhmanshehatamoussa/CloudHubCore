namespace CloudHub.Domain.Entities
{
    public class Login
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Passcode { get; set; } = null!;
        public LoginTypeValues LoginTypeId { get; set; }

        public virtual LoginType LoginType { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
