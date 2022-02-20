using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
{
    internal class ClientMapper : BaseMapper<Client>
    {

        protected override void MapTable(EntityTypeBuilder<Client> entityBuilder)
        {
            entityBuilder.ToTable("clients")
                .HasKey(x => x.Id);
        }

        protected override void MapColumns(EntityTypeBuilder<Client> entity)
        {
            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("name");

            entity.Property(e => e.ClientKey)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("client_key");

            entity.Property(e => e.ClientSecret)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("client_secret");

            entity.Property(e => e.TenantId)
            .IsRequired()
            .HasColumnName("tenant_id");

            MappingUtils.MapTrackingAttributes(entity);
        }

        protected override void MapConstraints(EntityTypeBuilder<Client> entity)
        {
            entity.HasIndex(e => e.Name, "clients_name_unique")
            .IsUnique();

            entity.HasIndex(e => e.ClientKey, "clients_client_key_unique")
            .IsUnique();

            entity.HasIndex(e => e.ClientSecret, "clients_client_secret_unique")
            .IsUnique();

            entity.HasOne(c => c.Tenant)
            .WithMany(t => t.Clients)
            .HasForeignKey(c => c.TenantId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("clients_tenant_id_foreign");
        }
    }


}
