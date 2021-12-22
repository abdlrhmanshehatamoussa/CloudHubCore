namespace CloudHub.Domain.Entities
{
    public enum PaymentGatewayValues
    {
        GOOGLE_PLAY_BILLING = 1593267,
        PAYPAL = 4863519,
    }
    public class PaymentGateway
    {
        public PaymentGateway()
        {
            Purchases = new HashSet<Purchase>();
        }

        public PaymentGatewayValues Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
