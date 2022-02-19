namespace CloudHub.ApiContracts
{
    public struct LoginResponseContract
    {
        public string email { get; set; }
        public string name { get; set; }
        public string login_type { get; set; }
        public string? image_url { get; set; }
        public string user_token { get; set; }
        public int user_token_expires_in { get; set; }
        public string global_id { get; set; }
    }
}