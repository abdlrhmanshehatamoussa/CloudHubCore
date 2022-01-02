using CloudHub.Domain.Entities;

namespace CloudHub.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> UsersRepository { get; }
        IRepository<Client> ClientsRepository { get; }
        IRepository<ClientType> ClientTypesRepository { get; }
        IRepository<UserToken> UserTokensRepository { get; }
        IRepository<Login> LoginsRepository { get; }
        IRepository<LoginType> LoginTypesRepository { get; }
        IRepository<Feature> FeaturesRepository { get; }
        IRepository<Nonce> NoncesRepository { get; }
        IRepository<PaymentGateway> PaymentGatewaysRepository { get; }
        IRepository<Purchase> PurchasesRepository { get; }
        IRepository<Collection> CollectionsRepository { get; }
        Task Save();
    }
}
