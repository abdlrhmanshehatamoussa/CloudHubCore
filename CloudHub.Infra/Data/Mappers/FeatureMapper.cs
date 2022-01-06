using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
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

            MappingUtils.MapTrackingAttributes(entity);
        }

        protected override void MapConstraints(EntityTypeBuilder<Feature> entityBuilder)
        {
            entityBuilder.HasIndex(x => x.GlobalId, "features_global_id_unique")
                .IsUnique();
        }
    }
}