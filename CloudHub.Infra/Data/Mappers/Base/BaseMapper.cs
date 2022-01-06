using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudHub.Infra.Data
{
    internal abstract class BaseMapper<T> : IBaseMapper where T : class
    {
        abstract protected void MapTable(EntityTypeBuilder<T> entityBuilder);
        abstract protected void MapColumns(EntityTypeBuilder<T> entityBuilder);
        abstract protected void MapConstraints(EntityTypeBuilder<T> entityBuilder);


        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>(entity => MapTable(entity));
            modelBuilder.Entity<T>(entity => MapColumns(entity));
            modelBuilder.Entity<T>(entity => MapConstraints(entity));
        }
    }
}
