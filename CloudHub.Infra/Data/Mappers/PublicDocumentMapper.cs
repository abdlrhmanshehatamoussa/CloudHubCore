using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
{
    internal class PublicDocumentMapper : BaseMapper<PublicDocument>
    {
        protected override void MapTable(EntityTypeBuilder<PublicDocument> entityBuilder)
        {
            entityBuilder.ToTable("public_documents")
                .HasKey(c => c.Id);
        }

        protected override void MapColumns(EntityTypeBuilder<PublicDocument> entityBuilder)
        {
            entityBuilder.Property(c => c.Id)
                .IsRequired();

            entityBuilder.Property(e => e.Body)
                .IsRequired()
                .HasColumnName("body")
                .HasColumnType("jsonb");

            entityBuilder.Property(e => e.PublicCollectionId)
                .IsRequired()
                .HasColumnName("public_collection_id");

            entityBuilder.Property(e => e.CreatedOn)
              .IsRequired()
              .HasDefaultValueSql("now()")
              .HasColumnName("created_on");

            entityBuilder.Property(e => e.ModifiedOn)
                .IsRequired()
                .HasDefaultValueSql("now()")
                .HasColumnName("modified_on");
        }

        protected override void MapConstraints(EntityTypeBuilder<PublicDocument> entityBuilder)
        {
            entityBuilder.HasOne(d => d.PublicCollection)
               .WithMany(p => p.PublicDocuments)
               .HasForeignKey(d => d.PublicCollectionId)
               .OnDelete(DeleteBehavior.Restrict)
               .HasConstraintName("public_documents_public_collection_id_foreign");
        }
    }
}