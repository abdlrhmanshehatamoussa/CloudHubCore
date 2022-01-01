namespace CloudHub.Domain.Entities
{
    public class Feature
    {
        public Feature()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string GlobalId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
