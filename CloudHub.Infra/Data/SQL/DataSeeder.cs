using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.Infra.Data
{
    internal static class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            DateTime stamp = new DateTime(2022, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<ClientType>().HasData(
                new ClientType() { Active = true, Name = "Admin", Id = ClientTypeValues.ADMIN, CreatedOn = stamp, ModifiedOn = stamp },
                new ClientType() { Active = true, Name = "Application", Id = ClientTypeValues.APP, CreatedOn = stamp, ModifiedOn = stamp },
                new ClientType() { Active = true, Name = "Dashboard", Id = ClientTypeValues.DASHBOARD, CreatedOn = stamp, ModifiedOn = stamp }
            );
            modelBuilder.Entity<LoginType>().HasData(
                new LoginType() { Active = true, Name = "Google", Id = LoginTypeValues.LOGIN_TYPE_GOOGLE, CreatedOn = stamp, ModifiedOn = stamp },
                new LoginType() { Active = true, Name = "Basic", Id = LoginTypeValues.LOGIN_TYPE_BASIC, CreatedOn = stamp, ModifiedOn = stamp },
                new LoginType() { Active = true, Name = "Facebook", Id = LoginTypeValues.LOGIN_TYPE_FACEBOOK, CreatedOn = stamp, ModifiedOn = stamp },
                new LoginType() { Active = true, Name = "Linked In", Id = LoginTypeValues.LOGIN_TYPE_LINKEDIN, CreatedOn = stamp, ModifiedOn = stamp }
            );
            modelBuilder.Entity<PaymentGateway>().HasData(
                new PaymentGateway() { Active = true, Name = "Google Play Billing", Id = PaymentGatewayValues.GOOGLE_PLAY_BILLING, CreatedOn = stamp, ModifiedOn = stamp },
                new PaymentGateway() { Active = true, Name = "Paypal", Id = PaymentGatewayValues.PAYPAL, CreatedOn = stamp, ModifiedOn = stamp }
            );
        }
    }
}
