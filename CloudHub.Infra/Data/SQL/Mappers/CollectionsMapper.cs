using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
{
    internal class CollectionsMapper : BaseMapper<Collection>
    {
        protected override void MapTable(EntityTypeBuilder<Collection> entityBuilder)
        {
            entityBuilder.ToTable("collections")
                .HasKey(c => c.Id);
        }

        protected override void MapColumns(EntityTypeBuilder<Collection> entityBuilder)
        {
            entityBuilder.Property(c => c.Id)
                .IsRequired()
                .HasColumnName("id");

            entityBuilder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("name");

            entityBuilder.Property(c => c.IsPrivate)
                .IsRequired()
                .HasDefaultValue(true)
                .HasColumnName("is_private");

            MappingUtils.MapTrackingAttributes(entityBuilder);
        }

        protected override void MapConstraints(EntityTypeBuilder<Collection> entityBuilder)
        {
            entityBuilder.HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}