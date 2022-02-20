using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
{
    internal class PrivateDocumentMapper : BaseMapper<PrivateDocument>
    {
        protected override void MapTable(EntityTypeBuilder<PrivateDocument> entityBuilder)
        {
            entityBuilder.ToTable("private_documents")
                .HasKey(c => c.Id);
        }

        protected override void MapColumns(EntityTypeBuilder<PrivateDocument> entityBuilder)
        {
            entityBuilder.Property(c => c.Id)
                .IsRequired();

            entityBuilder.Property(e => e.Body)
                .IsRequired()
                .HasColumnName("body")
                .HasColumnType("jsonb");

            entityBuilder.Property(e => e.PrivateCollectionId)
                .IsRequired()
                .HasColumnName("private_collection_id");

            entityBuilder.Property(e => e.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            entityBuilder.Property(e => e.CreatedOn)
              .IsRequired()
              .HasDefaultValueSql("now()")
              .HasColumnName("created_on");

            entityBuilder.Property(e => e.ModifiedOn)
                .IsRequired()
                .HasDefaultValueSql("now()")
                .HasColumnName("modified_on");
        }

        protected override void MapConstraints(EntityTypeBuilder<PrivateDocument> entityBuilder)
        {
            entityBuilder.HasOne(d => d.PrivateCollection)
               .WithMany(p => p.PrivateDocuments)
               .HasForeignKey(d => d.PrivateCollectionId)
               .OnDelete(DeleteBehavior.Restrict)
               .HasConstraintName("private_documents_private_collection_id_foreign");

            entityBuilder.HasOne(d => d.User)
               .WithMany(p => p.PrivateDocuments)
               .HasForeignKey(d => d.UserId)
               .OnDelete(DeleteBehavior.Restrict)
               .HasConstraintName("private_documents_user_id_foreign");
        }
    }
}