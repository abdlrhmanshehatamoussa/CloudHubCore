namespace CloudHub.ApiContracts
{
    public struct PingResponseContract
    {
        public string timestamp { get; set; }
        public string build_id { get; set; }
        public bool production_mode { get; set; }
        public string environment { get; set; }
    }
}
