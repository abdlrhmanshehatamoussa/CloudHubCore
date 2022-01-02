using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;

namespace CloudHub.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgreDatabase _dbContext;

        public UnitOfWork(PostgreDatabase context) => _dbContext = context;


        public IRepository<User> UsersRepository => new SQLRepository<User>(_dbContext);
        public IRepository<Client> ClientsRepository => new SQLRepository<Client>(_dbContext);
        public IRepository<ClientType> ClientTypesRepository => new SQLRepository<ClientType>(_dbContext);
        public IRepository<UserToken> UserTokensRepository => new SQLRepository<UserToken>(_dbContext);
        public IRepository<Login> LoginsRepository => new SQLRepository<Login>(_dbContext);
        public IRepository<LoginType> LoginTypesRepository => new SQLRepository<LoginType>(_dbContext);
        public IRepository<Feature> FeaturesRepository => new SQLRepository<Feature>(_dbContext);
        public IRepository<Nonce> NoncesRepository => new SQLRepository<Nonce>(_dbContext);
        public IRepository<PaymentGateway> PaymentGatewaysRepository => new SQLRepository<PaymentGateway>(_dbContext);
        public IRepository<Purchase> PurchasesRepository => new SQLRepository<Purchase>(_dbContext);
        public IRepository<Collection> CollectionsRepository => new SQLRepository<Collection>(_dbContext);

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
