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

            entityBuilder.Property(c => c.CollectionTypeId)
                .IsRequired()
                .HasColumnName("collection_type_id");

            MappingUtils.MapTrackingAttributes(entityBuilder);
        }

        protected override void MapConstraints(EntityTypeBuilder<Collection> entityBuilder)
        {
            entityBuilder.HasIndex(c => c.Name)
                .IsUnique();

            entityBuilder.HasOne(c => c.CollectionType)
                .WithMany(ct => ct.Collections)
                .HasForeignKey(c => c.CollectionTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("collections_collection_type_id_foreign");
        }
    }
}