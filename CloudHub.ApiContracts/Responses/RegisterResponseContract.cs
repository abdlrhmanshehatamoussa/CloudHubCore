namespace CloudHub.ApiContracts
{
    public struct RegisterResponseContract
    {
        public bool success { get; set; }
        public UserContract user { get; set; }
    }
}
