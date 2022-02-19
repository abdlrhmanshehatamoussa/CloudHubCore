namespace CloudHub.API.Domain.Models
{
    public class PaymentGateway : ILookupEntity<PaymentGatewayValues>
    {
        public PaymentGatewayValues Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();
    }
}
