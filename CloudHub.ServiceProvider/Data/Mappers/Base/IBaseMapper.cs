using Microsoft.EntityFrameworkCore;

namespace CloudHub.ServiceProvider.Data
{
    internal interface IBaseMapper
    {
        public void Map(ModelBuilder modelBuilder);
    }
}
