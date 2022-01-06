using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
{
    internal class AdminMapper : BaseMapper<Admin>
    {
        protected override void MapTable(EntityTypeBuilder<Admin> entityBuilder)
        {
            entityBuilder.ToTable("admins")
                .HasKey(a => a.Id);
        }
        protected override void MapColumns(EntityTypeBuilder<Admin> entityBuilder)
        {
            entityBuilder.Property(e => e.Id)
                .HasColumnName("id");

            entityBuilder.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("username");

            entityBuilder.Property(e => e.Password)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("password");

            entityBuilder.Property(e => e.AdminTypeId)
                .IsRequired()
                .HasColumnName("admin_type_id");

            MappingUtils.MapTrackingAttributes(entityBuilder);
        }

        protected override void MapConstraints(EntityTypeBuilder<Admin> entityBuilder)
        {
            entityBuilder.HasIndex(e => e.UserName, "admins_username_unique")
              .IsUnique();


            entityBuilder.HasOne(d => d.AdminType)
                .WithMany(p => p.Admins)
                .HasForeignKey(d => d.AdminTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("admins_admin_type_id_foreign");
        }
    }
}