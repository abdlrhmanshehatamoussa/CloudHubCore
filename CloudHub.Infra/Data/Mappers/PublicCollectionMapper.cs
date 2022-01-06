using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
{
    internal class PublicCollectionMapper : BaseMapper<PublicCollection>
    {
        protected override void MapTable(EntityTypeBuilder<PublicCollection> entityBuilder)
        {
            entityBuilder.ToTable("public_collections")
                .HasKey(c => c.Id);
        }
        protected override void MapColumns(EntityTypeBuilder<PublicCollection> entityBuilder)
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

        protected override void MapConstraints(EntityTypeBuilder<PublicCollection> entityBuilder)
        {

            entityBuilder.HasIndex(e => e.Name, "public_collections_name_unique")
            .IsUnique();
        }

    }
}