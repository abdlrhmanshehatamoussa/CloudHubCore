﻿using CloudHub.API.Domain.Models;

namespace CloudHub.API.Domain.Services
{
    public interface IUnitOfWork
    {
        IRepository<User> UsersRepository { get; }
        IRepository<Client> ClientsRepository { get; }
        IRepository<UserToken> UserTokensRepository { get; }
        IRepository<Login> LoginsRepository { get; }
        IRepository<Tenant> TenantsRepository { get; }
        IRepository<Feature> FeaturesRepository { get; }
        IRepository<Nonce> NoncesRepository { get; }
        IRepository<PaymentGateway> PaymentGatewaysRepository { get; }
        IRepository<Purchase> PurchasesRepository { get; }

        Task Save();
    }
}
