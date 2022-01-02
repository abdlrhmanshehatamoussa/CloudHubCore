namespace CloudHub.Domain.Entities
{
    public interface IPrivateDocument
    {
        public string UserId { get; set; }
        public dynamic Body { get; set; }
    }
}
