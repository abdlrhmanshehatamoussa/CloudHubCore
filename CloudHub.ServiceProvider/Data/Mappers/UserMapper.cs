using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.ServiceProvider.Data
{
    internal class UserMapper : BaseMapper<User>
    {
        protected override void MapTable(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("users")
                .HasKey(e => e.Id);
        }


        protected override void MapColumns(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("name");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("email");

            entity.Property(e => e.GlobalId)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("global_id");

            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");

            entity.Property(e => e.TenantId)
                 .IsRequired()
                 .HasColumnName("tenant_id");

            MappingUtils.MapTrackingAttributes(entity);
        }

        protected override void MapConstraints(EntityTypeBuilder<User> entity)
        {
            entity.HasIndex(e => e.GlobalId, "users_global_id_unique")
              .IsUnique();

            entity.HasIndex(new string[] { "Email", "TenantId" }, "users_email_per_tenant_unique")
              .IsUnique();

            entity.HasOne(u => u.Tenant)
            .WithMany(t => t.Users)
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("users_tenant_id_foreign");
        }

    }
}