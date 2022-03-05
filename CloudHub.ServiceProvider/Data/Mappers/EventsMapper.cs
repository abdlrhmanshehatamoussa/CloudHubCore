using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.ServiceProvider.Data
{
    internal class EventsMapper : BaseMapper<Event>
    {
        protected override void MapTable(EntityTypeBuilder<Event> entityBuilder)
        {
            entityBuilder.ToTable("events")
                .HasKey(x => x.Id);
        }

        protected override void MapColumns(EntityTypeBuilder<Event> entity)
        {
            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("category");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("description");

            entity.Property(e => e.Payload)
                    .HasColumnName("payload")
                    .HasColumnType("jsonb");

            entity.Property(e => e.TenantId)
                    .IsRequired()
                    .HasColumnName("tenant_id");

            entity.Property(e => e.BuildId)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("build_id");

            entity.Property(e => e.Source)
                .HasMaxLength(255)
                .HasColumnName("source");

            entity.Property(e => e.CreatedOn)
              .IsRequired()
              .HasDefaultValueSql("now()")
              .HasColumnName("created_on");
        }

        protected override void MapConstraints(EntityTypeBuilder<Event> entityBuilder)
        {
            entityBuilder.HasOne(e => e.Tenant)
            .WithMany(t => t.Events)
            .HasForeignKey(e => e.TenantId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("events_tenant_id_foreign");
        }
    }
}