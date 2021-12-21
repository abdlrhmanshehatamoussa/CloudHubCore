using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;

namespace CloudHub.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        MyDbContext _dbContext;

        public UnitOfWork(MyDbContext context) => _dbContext = context;


        public IRepository<User> UsersRepository => new Repository<User>(_dbContext);
        public IRepository<Application> ApplicationsRepository => new Repository<Application>(_dbContext);
        public IRepository<Client> ClientsRepository => new Repository<Client>(_dbContext);
        public IRepository<ApplicationSecret> ApplicationSecretsRepository => new Repository<ApplicationSecret>(_dbContext);
        public IRepository<ClientType> ClientTypesRepository => new Repository<ClientType>(_dbContext);
        public IRepository<ClientApplicationRelation> ClientApplicationRelationsRepository => new Repository<ClientApplicationRelation>(_dbContext);
        public IRepository<UserToken> UserTokensRepository => new Repository<UserToken>(_dbContext);
        public IRepository<Login> LoginsRepository => new Repository<Login>(_dbContext);
        public IRepository<LoginType> LoginTypesRepository => new Repository<LoginType>(_dbContext);
        public IRepository<UserAction> UserActionsRepository => new Repository<UserAction>(_dbContext);
        public IRepository<Feature> FeaturesRepository => new Repository<Feature>(_dbContext);
        public IRepository<Nonce> NoncesRepository => new Repository<Nonce>(_dbContext);
        public IRepository<PaymentGateway> PaymentGatewaysRepository => new Repository<PaymentGateway>(_dbContext);
        public IRepository<Purchase> PurchasesRepository => new Repository<Purchase>(_dbContext);
        public IRepository<Release> ReleasesRepository => new Repository<Release>(_dbContext);


        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
