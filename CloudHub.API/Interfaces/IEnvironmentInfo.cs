namespace CloudHub.API
{
    public interface IEnvironmentInfo
    {
        public string EnvironmentName { get; set; }
        public string BuildId { get; set; }
        public bool IsProduction { get; set; }
    }
}
