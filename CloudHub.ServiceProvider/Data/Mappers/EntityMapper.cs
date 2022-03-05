using Microsoft.EntityFrameworkCore;

namespace CloudHub.ServiceProvider.Data
{
    internal class EntityMapper
    {
        private static readonly List<IBaseMapper> Mappers = new()
        {
            new ClientMapper(),
            new FeatureMapper(),
            new LoginMapper(),
            new LoginTypeMapper(),
            new NonceMapper(),
            new PaymentGatewayMapper(),
            new PurchaseMapper(),
            new EventsMapper(),
            new UserMapper(),
            new UserTokenMapper(),
            new TenantMapper(),
            new PublicCollectionMapper(),
            new PublicDocumentMapper(),
            new PrivateCollectionMapper(),
            new PrivateDocumentMapper(),
        };

        public static void MapEntities(ModelBuilder modelBuilder)
        {
            foreach (var mapper in Mappers)
            {
                mapper.Map(modelBuilder);
            }
        }
    }
}
