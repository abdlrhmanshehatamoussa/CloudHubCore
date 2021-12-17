namespace CloudHub.Domain.Entities
{
    public enum LoginTypeValues
    {
        LOGIN_TYPE_BASIC = 5671293,
        LOGIN_TYPE_GOOGLE = 1932278,
        LOGIN_TYPE_FACEBOOK = 2404369,
        LOGIN_TYPE_LINKEDIN = 3658418,
    }
    public class LoginType
    {
        public LoginType()
        {
            Logins = new HashSet<Login>();
        }

        public LoginTypeValues Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? Active { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}
