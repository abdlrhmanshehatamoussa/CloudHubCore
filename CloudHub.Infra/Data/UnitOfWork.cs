using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;

namespace CloudHub.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgreDatabase _dbContext;

        public UnitOfWork(PostgreDatabase context) => _dbContext = context;


        public IRepository<User> UsersRepository => new Repository<User>(_dbContext);
        public IRepository<Client> ClientsRepository => new Repository<Client>(_dbContext);
        public IRepository<UserToken> UserTokensRepository => new Repository<UserToken>(_dbContext);
        public IRepository<Login> LoginsRepository => new Repository<Login>(_dbContext);
        public IRepository<Feature> FeaturesRepository => new Repository<Feature>(_dbContext);
        public IRepository<Nonce> NoncesRepository => new Repository<Nonce>(_dbContext);
        public IRepository<PaymentGateway> PaymentGatewaysRepository => new Repository<PaymentGateway>(_dbContext);
        public IRepository<Purchase> PurchasesRepository => new Repository<Purchase>(_dbContext);
        public IRepository<Admin> AdminsRepository => new Repository<Admin>(_dbContext);
        public IRepository<PublicDocument> PublicDocumentsRepository => new Repository<PublicDocument>(_dbContext);
        public IRepository<PublicCollection> PublicCollectionsRepository => new Repository<PublicCollection>(_dbContext);
        public IRepository<PrivateDocument> PrivateDocumentsRepository => new Repository<PrivateDocument>(_dbContext);
        public IRepository<PrivateCollection> PrivateCollectionsRepository => new Repository<PrivateCollection>(_dbContext);

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
