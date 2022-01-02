using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.Infra.Data
{
    internal abstract class LookupMapper<T, E> : IBaseMapper where T : class, ITrackableEntity, ILookupEntity<E> where E : struct
    {
        protected abstract string TableName { get; }

        public void Map(ModelBuilder modelBuilder)
        {
            string tableName = TableName;
            modelBuilder.Entity<T>(entity =>
            {
                entity.ToTable(tableName)
                .HasKey(c => c.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .IsRequired()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Active)
                        .IsRequired()
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                entity.Property(e => e.CreatedOn)
                    .IsRequired()
                    .HasDefaultValueSql("now()")
                    .HasColumnName("created_on");

                entity.Property(e => e.ModifiedOn)
                    .IsRequired()
                    .HasDefaultValueSql("now()")
                    .HasColumnName("modified_on");

                entity.HasIndex(e => e.Name, $"{tableName}_name_unique")
                .IsUnique();
            });
        }
    }
}
