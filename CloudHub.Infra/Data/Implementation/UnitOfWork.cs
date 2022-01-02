using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;

namespace CloudHub.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgreDatabase _dbContext;

        public UnitOfWork(PostgreDatabase context) => _dbContext = context;


        public ISQLRepository<User> UsersRepository => new SQLRepository<User>(_dbContext);
        public ISQLRepository<Client> ClientsRepository => new SQLRepository<Client>(_dbContext);
        public ISQLRepository<ClientType> ClientTypesRepository => new SQLRepository<ClientType>(_dbContext);
        public ISQLRepository<UserToken> UserTokensRepository => new SQLRepository<UserToken>(_dbContext);
        public ISQLRepository<Login> LoginsRepository => new SQLRepository<Login>(_dbContext);
        public ISQLRepository<LoginType> LoginTypesRepository => new SQLRepository<LoginType>(_dbContext);
        public ISQLRepository<Feature> FeaturesRepository => new SQLRepository<Feature>(_dbContext);
        public ISQLRepository<Nonce> NoncesRepository => new SQLRepository<Nonce>(_dbContext);
        public ISQLRepository<PaymentGateway> PaymentGatewaysRepository => new SQLRepository<PaymentGateway>(_dbContext);
        public ISQLRepository<Purchase> PurchasesRepository => new SQLRepository<Purchase>(_dbContext);
        public ISQLRepository<Collection> CollectionsRepository => new SQLRepository<Collection>(_dbContext);

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
