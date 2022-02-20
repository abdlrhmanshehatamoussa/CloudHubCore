namespace CloudHub.API.Contracts
{
    public struct RegisterResponseContract
    {
        public bool success { get; set; }
        public UserContract user { get; set; }
    }
}
