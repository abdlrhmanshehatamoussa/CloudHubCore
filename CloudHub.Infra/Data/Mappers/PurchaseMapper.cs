using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.ServiceProvider.Data
{
    internal class PurchaseMapper : BaseMapper<Purchase>
    {


        protected override void MapTable(EntityTypeBuilder<Purchase> entity)
        {
            entity.ToTable("purchases")
                .HasKey(p => p.Id);
        }

        protected override void MapColumns(EntityTypeBuilder<Purchase> entity)
        {
            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.FeatureId)
                .IsRequired()
                .HasColumnName("feature_id");

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            entity.Property(e => e.PaymentGatewayId)
                .IsRequired()
                .HasColumnName("payment_gateway_id");

            entity.Property(e => e.Payload)
                .IsRequired()
                .HasColumnName("payload");

            entity.Property(e => e.Validation)
                .IsRequired()
                .HasColumnName("validation");

            entity.Property(e => e.CreatedOn)
                .HasColumnName("created_on")
                .IsRequired()
                .HasDefaultValueSql("now()");
        }

        protected override void MapConstraints(EntityTypeBuilder<Purchase> entity)
        {
            entity.HasIndex(e => new { e.FeatureId, e.UserId }, "purchases_user_id_feature_id_unique")
              .IsUnique();

            entity.HasOne(d => d.Feature)
                .WithMany(p => p.Purchases)
                .HasForeignKey(d => d.FeatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchases_feature_id_foreign");

            entity.HasOne(d => d.PaymentGateway)
                .WithMany(p => p.Purchases)
                .HasForeignKey(d => d.PaymentGatewayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchases_payment_gateway_id_foreign");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Purchases)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchases_user_id_foreign");
        }
    }
}