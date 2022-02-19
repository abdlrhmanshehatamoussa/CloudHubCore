namespace CloudHub.ApiContracts.Requests
{
    public class RegisterRequestContract
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? image_url { get; set; }
        public int login_type { get; set; }
    }
}
