using CloudHub.Domain.Entities;

namespace CloudHub.Domain.Repositories
{
    public interface IUnitOfWork
    {
        ISQLRepository<User> UsersRepository { get; }
        ISQLRepository<Client> ClientsRepository { get; }
        ISQLRepository<ClientType> ClientTypesRepository { get; }
        ISQLRepository<UserToken> UserTokensRepository { get; }
        ISQLRepository<Login> LoginsRepository { get; }
        ISQLRepository<LoginType> LoginTypesRepository { get; }
        ISQLRepository<Feature> FeaturesRepository { get; }
        ISQLRepository<Nonce> NoncesRepository { get; }
        ISQLRepository<PaymentGateway> PaymentGatewaysRepository { get; }
        ISQLRepository<Purchase> PurchasesRepository { get; }
        ISQLRepository<Collection> CollectionsRepository { get; }
        Task Save();
    }
}
