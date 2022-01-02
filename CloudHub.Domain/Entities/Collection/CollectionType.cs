namespace CloudHub.Domain.Entities
{
    public class CollectionType : ILookupEntity<CollectionTypeValues>
    {
        public CollectionTypeValues Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public virtual List<Collection> Collections { get; set; } = new();
    }
}
