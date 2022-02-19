namespace CloudHub.API.Domain.Models
{
    public class Purchase: IBaseEntity
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public int UserId { get; set; }
        public PaymentGatewayValues PaymentGatewayId { get; set; }
        public string Payload { get; set; } = null!;
        public string Validation { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual Feature Feature { get; set; } = null!;
        public virtual PaymentGateway PaymentGateway { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
