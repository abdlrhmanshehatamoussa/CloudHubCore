using CloudHub.Domain.Entities;

namespace CloudHub.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> UsersRepository { get; }
        IRepository<Application> ApplicationsRepository { get; }
        IRepository<Client> ClientsRepository { get; }
        IRepository<ApplicationSecret> ApplicationSecretsRepository { get; }
        IRepository<ClientType> ClientTypesRepository { get; }
        IRepository<ClientApplicationRelation> ClientApplicationRelationsRepository { get; }
        IRepository<UserToken> UserTokensRepository { get; }
        IRepository<Login> LoginsRepository { get; }
        IRepository<LoginType> LoginTypesRepository { get; }
        IRepository<UserAction> UserActionsRepository { get; }
        IRepository<Feature> FeaturesRepository { get; }
        IRepository<Nonce> NoncesRepository { get; }
        IRepository<PaymentGateway> PaymentGatewaysRepository { get; }
        IRepository<Purchase> PurchasesRepository { get; }
        IRepository<Release> ReleasesRepository { get; }
        Task Save();
    }
}
