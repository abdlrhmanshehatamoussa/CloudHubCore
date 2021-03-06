using CloudHub.Domain.Models;

namespace CloudHub.ServiceProvider.Data
{
    internal class PaymentGatewayMapper : LookupMapper<PaymentGateway, PaymentGatewayValues>
    {
        protected override string TableName => "payment_gateways";
    }
}