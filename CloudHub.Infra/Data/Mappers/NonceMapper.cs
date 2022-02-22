using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.ServiceProvider.Data
{
    internal class NonceMapper : BaseMapper<Nonce>
    {
        protected override void MapTable(EntityTypeBuilder<Nonce> entityBuilder)
        {
            entityBuilder.ToTable("nonces")
                .HasKey(n => n.Id);
        }

        protected override void MapColumns(EntityTypeBuilder<Nonce> entityBuilder)
        {

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id");

            entityBuilder.Property(e => e.ClientId)
                .HasColumnName("client_id")
                .IsRequired();

            entityBuilder.Property(e => e.ConsumedOn)
                .HasColumnName("consumed_on");

            entityBuilder.Property(e => e.CreatedOn)
                .HasColumnName("created_on")
                .IsRequired()
                .HasDefaultValueSql("now()");

            entityBuilder.Property(e => e.Token)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("token");
        }

        protected override void MapConstraints(EntityTypeBuilder<Nonce> entityBuilder)
        {
            entityBuilder.HasIndex(e => e.Token, "nonces_tokens_unique")
               .IsUnique();

            entityBuilder.HasOne(d => d.Client)
                .WithMany(p => p.Nonces)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nonces_client_id_foreign");
        }
    }
}