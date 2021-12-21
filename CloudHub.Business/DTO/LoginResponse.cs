namespace CloudHub.Business.DTO
{
    public struct LoginResponse
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LoginTypeName { get; set; }
        public string? ImageURL { get; set; }
        public string TokenBody { get; set; }
        public int TokenRemainingSeconds { get; set; }
        public int TokenAgeSeconds { get; set; }
        public string GlobalId { get; set; }
    }
}
