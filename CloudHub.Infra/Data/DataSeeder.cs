using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.ServiceProvider.Data
{
    internal static class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            DateTime stamp = new(2022, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<LoginType>().HasData(
                new LoginType() { Active = true, Name = "Google", Id = ELoginTypes.LOGIN_TYPE_GOOGLE, CreatedOn = stamp, ModifiedOn = stamp },
                new LoginType() { Active = true, Name = "Basic", Id = ELoginTypes.LOGIN_TYPE_BASIC, CreatedOn = stamp, ModifiedOn = stamp },
                new LoginType() { Active = true, Name = "Facebook", Id = ELoginTypes.LOGIN_TYPE_FACEBOOK, CreatedOn = stamp, ModifiedOn = stamp },
                new LoginType() { Active = true, Name = "Linked In", Id = ELoginTypes.LOGIN_TYPE_LINKEDIN, CreatedOn = stamp, ModifiedOn = stamp }
            );
            modelBuilder.Entity<PaymentGateway>().HasData(
                new PaymentGateway() { Active = true, Name = "Google Play Billing", Id = PaymentGatewayValues.GOOGLE_PLAY_BILLING, CreatedOn = stamp, ModifiedOn = stamp },
                new PaymentGateway() { Active = true, Name = "Paypal", Id = PaymentGatewayValues.PAYPAL, CreatedOn = stamp, ModifiedOn = stamp }
            );
        }
    }
}
