namespace CloudHub.Domain.Entities
{
    public class Collection
    {
        public int Id { get; set; }
        public CollectionTypeValues CollectionTypeId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ModifiedOn { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
