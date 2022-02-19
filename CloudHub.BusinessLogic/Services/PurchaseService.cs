﻿using CloudHub.BusinessLogic.DTO;
using CloudHub.BusinessLogic.Repositories;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;

namespace CloudHub.BusinessLogic.Services
{
    public class PurchaseService : BaseService
    {
        public PurchaseService(IUnitOfWork unitOfWork, IEnvironmentSettings productionModeProvider) : base(unitOfWork, productionModeProvider)
        {
        }

        public async Task<List<Purchase>> FetchAll(ConsumerCredentials consumerCredentials)
        {
            Consumer consumer = await GetConsumer(consumerCredentials);
            User user = consumer.UserToken?.User ?? throw new NotAuthenticatedException();
            int nonceId = consumer.Nonce.Id;

            List<Purchase> purchases = await _unitOfWork.PurchasesRepository.Where(p => p.UserId == user.Id,
                p => p.User,
                p => p.Feature);

            await ConsumeNonceOrThrow(nonceId);
            await _unitOfWork.Save();

            return purchases;
        }
    }
}