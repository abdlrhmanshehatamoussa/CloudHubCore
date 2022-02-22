using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.ServiceProvider.Data
{
    internal class UserTokenMapper : BaseMapper<UserToken>
    {

        protected override void MapTable(EntityTypeBuilder<UserToken> entity)
        {
            entity.ToTable("user_tokens")
                .HasKey(x => x.Id);
        }

        protected override void MapColumns(EntityTypeBuilder<UserToken> entity)
        {
            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id");

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasColumnName("user_id");


            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("token");

            entity.Property(e => e.CreatedOn)
                .HasColumnName("created_on")
                .IsRequired()
                .HasDefaultValueSql("now()");

            entity.Property(e => e.ExpiryDate)
                .HasColumnName("expiry_date")
                .IsRequired()
                .HasDefaultValueSql("now()");
        }

        protected override void MapConstraints(EntityTypeBuilder<UserToken> entity)
        {
            entity.HasIndex(e => e.Token, "user_tokens_token_unique")
                .IsUnique();

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_tokens_user_id_foreign");
        }

    }
}