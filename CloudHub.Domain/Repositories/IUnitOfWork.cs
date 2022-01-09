using CloudHub.Domain.Entities;

namespace CloudHub.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> UsersRepository { get; }
        IRepository<Client> ClientsRepository { get; }
        IRepository<UserToken> UserTokensRepository { get; }
        IRepository<Login> LoginsRepository { get; }
        IRepository<Feature> FeaturesRepository { get; }
        IRepository<Nonce> NoncesRepository { get; }
        IRepository<PaymentGateway> PaymentGatewaysRepository { get; }
        IRepository<Purchase> PurchasesRepository { get; }
        IRepository<PublicDocument> PublicDocumentsRepository { get; }
        IRepository<PublicCollection> PublicCollectionsRepository { get; }
        IRepository<PrivateDocument> PrivateDocumentsRepository { get; }
        IRepository<PrivateCollection> PrivateCollectionsRepository { get; }

        Task Save();
    }
}
