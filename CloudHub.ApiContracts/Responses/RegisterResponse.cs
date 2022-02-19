namespace CloudHub.ApiContracts.Responses
{
    public struct RegisterResponseContract
    {
        public string email { get; set; }
        public string name { get; set; }
        public string? image_url { get; set; }
        public string global_id { get; set; }
    }
}
