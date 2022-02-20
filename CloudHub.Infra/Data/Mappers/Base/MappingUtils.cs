using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
{
    internal static class MappingUtils
    {
        public static void MapTrackingAttributes<T>(EntityTypeBuilder<T> entity) where T : class, ITrackableEntity
        {
            entity.Property(e => e.Active)
                       .IsRequired()
                       .HasColumnName("active")
                       .HasDefaultValueSql("true");

            entity.Property(e => e.ModifiedOn)
                .IsRequired()
                .HasDefaultValueSql("now()")
                .HasColumnName("modified_on");

            entity.Property(e => e.CreatedOn)
                .IsRequired()
                .HasDefaultValueSql("now()")
                .HasColumnName("created_on");
        }
    }
}
