namespace CloudHub.ApiContracts
{
    public class LoginRequestContract
    {
        public LoginRequestContract(string email, string password, int login_type)
        {
            this.email = email;
            this.password = password;
            this.login_type = login_type;
        }

        public string email { get; set; }
        public string password { get; set; }
        public int login_type { get; set; }
    }
}
