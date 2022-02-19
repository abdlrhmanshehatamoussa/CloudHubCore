namespace CloudHub.BusinessLogic.DTO
{
    public struct RegisterResponse
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string? ImageURL { get; set; }

        public string GlobalId { get; set; }
    }
}
