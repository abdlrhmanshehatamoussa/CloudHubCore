﻿using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext context) => _dbContext = context;


        public IRepository<User> UsersRepository => new Repository<User>(_dbContext);
        public IRepository<Client> ClientsRepository => new Repository<Client>(_dbContext);
        public IRepository<UserToken> UserTokensRepository => new Repository<UserToken>(_dbContext);
        public IRepository<Login> LoginsRepository => new Repository<Login>(_dbContext);
        public IRepository<Feature> FeaturesRepository => new Repository<Feature>(_dbContext);
        public IRepository<Nonce> NoncesRepository => new Repository<Nonce>(_dbContext);
        public IRepository<PaymentGateway> PaymentGatewaysRepository => new Repository<PaymentGateway>(_dbContext);
        public IRepository<Purchase> PurchasesRepository => new Repository<Purchase>(_dbContext);

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
