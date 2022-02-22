using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.ServiceProvider.Data
{
    internal class TenantMapper : BaseMapper<Tenant>
    {
        protected override void MapColumns(EntityTypeBuilder<Tenant> entityBuilder)
        {
            entityBuilder.Property(e => e.Id)
                .HasColumnName("id");

            entityBuilder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("name");

            entityBuilder.Property(e => e.Guid)
                .IsRequired()
                .HasColumnName("guid");

            MappingUtils.MapTrackingAttributes(entityBuilder);
        }

        protected override void MapConstraints(EntityTypeBuilder<Tenant> entityBuilder)
        {
            entityBuilder.HasIndex(e => e.Name, "tenants_name_unique")
            .IsUnique();

            entityBuilder.HasIndex(e => e.Guid, "tenants_guid_unique")
              .IsUnique();
        }

        protected override void MapTable(EntityTypeBuilder<Tenant> entityBuilder)
        {
            entityBuilder.HasIndex(t => t.Name, "tenants_name_unique")
                .IsUnique();
            entityBuilder.ToTable("tenants")
              .HasKey(x => x.Id);
        }
    }
}