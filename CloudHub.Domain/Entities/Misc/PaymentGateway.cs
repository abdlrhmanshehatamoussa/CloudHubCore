namespace CloudHub.Domain.Entities
{
    public class PaymentGateway
    {
        public PaymentGateway()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
