using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
{
    internal class UserMapper : BaseMapper<User>
    {
        protected override void MapTable(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("users")
                .HasKey(e => e.Id);
        }


        protected override void MapColumns(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("name");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("email");

            entity.Property(e => e.GlobalId)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("global_id");

            entity.Property(e => e.RoleId)
                .IsRequired()
                .HasColumnName("role_id");

            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");

            MappingUtils.MapTrackingAttributes(entity);
        }

        protected override void MapConstraints(EntityTypeBuilder<User> entity)
        {
            entity.HasIndex(e => e.GlobalId, "users_global_id_unique")
              .IsUnique();

            entity.HasIndex(e => e.Email, "users_email_unique")
              .IsUnique();

            entity.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("users_role_id_foreign");
        }

    }
}