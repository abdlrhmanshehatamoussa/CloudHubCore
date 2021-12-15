namespace CloudHub.Domain
{
    public interface PrivateDocument
    {
        int ID { get; set; }
        int PrivateCollectionId { get; set; }
        int UserId { get; set; }
        dynamic Body { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
