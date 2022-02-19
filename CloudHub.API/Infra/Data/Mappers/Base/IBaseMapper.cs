using Microsoft.EntityFrameworkCore;

namespace CloudHub.Infra.Data
{
    internal interface IBaseMapper
    {
        public void Map(ModelBuilder modelBuilder);
    }
}
