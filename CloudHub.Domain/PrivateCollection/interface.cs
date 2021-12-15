namespace CloudHub.Domain
{
    public interface IPrivateCollection
    {
        int ID { get; set; }
        int ApplicationId { get; set; }
        string Name { get; set; }
        DateTime CreatedOn { get; set; }
        bool Active { get; set; }
    }
}
