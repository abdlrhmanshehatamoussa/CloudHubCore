using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.ServiceProvider.Data;

namespace CloudHub.ServiceProvider
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(MyContext dbContext) => _dbContext = dbContext;
        
        private readonly MyContext _dbContext;

        public IRepository<User> UsersRepository => new GenericRepository<User>(_dbContext);
        public IRepository<Client> ClientsRepository => new GenericRepository<Client>(_dbContext);
        public IRepository<UserToken> UserTokensRepository => new GenericRepository<UserToken>(_dbContext);
        public IRepository<Login> LoginsRepository => new GenericRepository<Login>(_dbContext);
        public IRepository<Tenant> TenantsRepository => new GenericRepository<Tenant>(_dbContext);
        public IRepository<Feature> FeaturesRepository => new GenericRepository<Feature>(_dbContext);
        public IRepository<Nonce> NoncesRepository => new GenericRepository<Nonce>(_dbContext);
        public IRepository<PaymentGateway> PaymentGatewaysRepository => new GenericRepository<PaymentGateway>(_dbContext);
        public IRepository<Purchase> PurchasesRepository => new GenericRepository<Purchase>(_dbContext);
        public IRepository<PublicDocument> PublicDocumentsRepository => new GenericRepository<PublicDocument>(_dbContext);
        public IRepository<PublicCollection> PublicCollectionsRepository => new GenericRepository<PublicCollection>(_dbContext);
        public IRepository<PrivateDocument> PrivateDocumentsRepository => new GenericRepository<PrivateDocument>(_dbContext);
        public IRepository<PrivateCollection> PrivateCollectionsRepository => new GenericRepository<PrivateCollection>(_dbContext);


        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
