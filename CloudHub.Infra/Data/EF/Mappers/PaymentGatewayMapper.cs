using CloudHub.Domain.Entities;

namespace CloudHub.Infra.Data
{
    internal class PaymentGatewayMapper : LookupMapper<PaymentGateway, PaymentGatewayValues>
    {
        protected override string TableName => "payment_gateways";
    }
}