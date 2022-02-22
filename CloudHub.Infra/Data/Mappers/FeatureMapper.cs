using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.ServiceProvider.Data
{
    internal class FeatureMapper : BaseMapper<Feature>
    {
        protected override void MapTable(EntityTypeBuilder<Feature> entityBuilder)
        {
            entityBuilder.ToTable("features")
                .HasKey(x => x.Id);
        }

        protected override void MapColumns(EntityTypeBuilder<Feature> entity)
        {
            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("name");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("description");


            entity.Property(e => e.GlobalId)
                .IsRequired()
                .HasColumnName("global_id");

            entity.Property(e => e.TenantId)
                    .IsRequired()
                    .HasColumnName("tenant_id");

            MappingUtils.MapTrackingAttributes(entity);
        }

        protected override void MapConstraints(EntityTypeBuilder<Feature> entityBuilder)
        {
            entityBuilder.HasIndex(f => f.GlobalId, "features_global_id_unique")
                .IsUnique();

            entityBuilder.HasIndex(new string[] { "Name", "TenantId" }, "features_name_per_tenant_unique")
                .IsUnique();

            entityBuilder.HasOne(f => f.Tenant)
            .WithMany(t => t.Features)
            .HasForeignKey(f => f.TenantId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("features_tenant_id_foreign");
        }
    }
}