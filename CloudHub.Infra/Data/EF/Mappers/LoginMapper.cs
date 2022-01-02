using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
{
    internal class LoginMapper : BaseMapper<Login>
    {
        protected override void MapTable(EntityTypeBuilder<Login> entityBuilder)
        {
            entityBuilder.ToTable("logins")
                .HasKey(x => x.Id);
        }
        protected override void MapColumns(EntityTypeBuilder<Login> entityBuilder)
        {
            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entityBuilder.Property(e => e.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            entityBuilder.Property(e => e.Passcode)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("passcode");

            entityBuilder.Property(e => e.LoginTypeId)
                .IsRequired()
                .HasColumnName("login_type_id");
        }

        protected override void MapConstraints(EntityTypeBuilder<Login> entityBuilder)
        {
            entityBuilder.HasIndex(e => e.UserId, "logins_user_id_unique")
                .IsUnique();

            entityBuilder.HasOne(d => d.LoginType)
                .WithMany(p => p.Logins)
                .HasForeignKey(d => d.LoginTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("logins_login_type_id_foreign");

            entityBuilder.HasOne(d => d.User)
                .WithOne(p => p.Login)
                .HasForeignKey<Login>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("logins_user_id_foreign");
        }
    }
}