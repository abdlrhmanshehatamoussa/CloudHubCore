using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.Infra.Data
{
    public class SQLUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public SQLUnitOfWork(DbContext context) => _dbContext = context;


        public IRepository<User> UsersRepository => new SQLRepository<User>(_dbContext);
        public IRepository<Client> ClientsRepository => new SQLRepository<Client>(_dbContext);
        public IRepository<UserToken> UserTokensRepository => new SQLRepository<UserToken>(_dbContext);
        public IRepository<Login> LoginsRepository => new SQLRepository<Login>(_dbContext);
        public IRepository<Tenant> TenantsRepository => new SQLRepository<Tenant>(_dbContext);
        public IRepository<Feature> FeaturesRepository => new SQLRepository<Feature>(_dbContext);
        public IRepository<Nonce> NoncesRepository => new SQLRepository<Nonce>(_dbContext);
        public IRepository<PaymentGateway> PaymentGatewaysRepository => new SQLRepository<PaymentGateway>(_dbContext);
        public IRepository<Purchase> PurchasesRepository => new SQLRepository<Purchase>(_dbContext);
        public IRepository<PublicDocument> PublicDocumentsRepository => new SQLRepository<PublicDocument>(_dbContext);
        public IRepository<PublicCollection> PublicCollectionsRepository => new SQLRepository<PublicCollection>(_dbContext);
        public IRepository<PrivateDocument> PrivateDocumentsRepository => new SQLRepository<PrivateDocument>(_dbContext);
        public IRepository<PrivateCollection> PrivateCollectionsRepository => new SQLRepository<PrivateCollection>(_dbContext);


        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
