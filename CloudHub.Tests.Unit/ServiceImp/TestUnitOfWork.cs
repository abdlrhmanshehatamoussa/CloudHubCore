using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CloudHub.Tests.Unit
{
    public class TestUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public TestUnitOfWork(DbContext context) => _dbContext = context;


        public IRepository<User> UsersRepository => new TestRepository<User>(_dbContext);
        public IRepository<Client> ClientsRepository => new TestRepository<Client>(_dbContext);
        public IRepository<UserToken> UserTokensRepository => new TestRepository<UserToken>(_dbContext);
        public IRepository<Login> LoginsRepository => new TestRepository<Login>(_dbContext);
        public IRepository<Tenant> TenantsRepository => new TestRepository<Tenant>(_dbContext);
        public IRepository<Feature> FeaturesRepository => new TestRepository<Feature>(_dbContext);
        public IRepository<Nonce> NoncesRepository => new TestRepository<Nonce>(_dbContext);
        public IRepository<PaymentGateway> PaymentGatewaysRepository => new TestRepository<PaymentGateway>(_dbContext);
        public IRepository<Purchase> PurchasesRepository => new TestRepository<Purchase>(_dbContext);
        public IRepository<PublicDocument> PublicDocumentsRepository => new TestRepository<PublicDocument>(_dbContext);
        public IRepository<PublicCollection> PublicCollectionsRepository => new TestRepository<PublicCollection>(_dbContext);
        public IRepository<PrivateDocument> PrivateDocumentsRepository => new TestRepository<PrivateDocument>(_dbContext);
        public IRepository<PrivateCollection> PrivateCollectionsRepository => new TestRepository<PrivateCollection>(_dbContext);


        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
