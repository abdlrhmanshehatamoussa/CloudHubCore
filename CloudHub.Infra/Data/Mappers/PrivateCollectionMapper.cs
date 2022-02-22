using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.ServiceProvider.Data
{
    internal class PrivateCollectionMapper : BaseMapper<PrivateCollection>
    {
        protected override void MapTable(EntityTypeBuilder<PrivateCollection> entityBuilder)
        {
            entityBuilder.ToTable("private_collections")
                .HasKey(c => c.Id);
        }
        protected override void MapColumns(EntityTypeBuilder<PrivateCollection> entityBuilder)
        {
            entityBuilder.Property(e => e.Id)
                   .IsRequired()
                   .HasColumnName("id");

            entityBuilder.Property(e => e.TenantId)
                   .IsRequired()
                   .HasColumnName("tenant_id");

            entityBuilder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("name");

            MappingUtils.MapTrackingAttributes(entityBuilder);
        }

        protected override void MapConstraints(EntityTypeBuilder<PrivateCollection> entityBuilder)
        {

            entityBuilder.HasIndex(e => e.Name, "private_collections_name_unique")
            .IsUnique();

            entityBuilder.HasOne(c => c.Tenant)
                .WithMany(t => t.PrivateCollections)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(c=>c.TenantId)
                .HasConstraintName("private_collections_tenant_id_foreign");
        }

    }
}