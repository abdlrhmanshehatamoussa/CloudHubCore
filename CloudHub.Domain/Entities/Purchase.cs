namespace CloudHub.Domain.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public int UserId { get; set; }
        public int PaymentGatewayId { get; set; }
        public string Payload { get; set; } = null!;
        public string Validation { get; set; } = null!;
        public DateTime CreatedOn { get; set; }

        public virtual Feature Feature { get; set; } = null!;
        public virtual PaymentGateway PaymentGateway { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
