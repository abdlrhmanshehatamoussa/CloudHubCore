using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
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
        }

    }
}