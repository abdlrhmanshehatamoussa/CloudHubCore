namespace CloudHub.Business.DTO
{
    public struct UserActionCreationParams
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public string AppVersion { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Payload { get; set; }

    }
}
