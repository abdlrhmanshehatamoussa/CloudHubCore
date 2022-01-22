﻿using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class BaseService
    {
        public BaseService(IUnitOfWork unitOfWork, IEnvironmentSettings productionModeProvider)
        {
            _unitOfWork = unitOfWork;
            this.productionModeProvider = productionModeProvider;
        }


        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IEnvironmentSettings productionModeProvider;


        internal async Task<Consumer> GetConsumer(ConsumerCredentials credentials)
        {
            if (string.IsNullOrWhiteSpace(credentials.ClientKey) || string.IsNullOrWhiteSpace(credentials.Nonce)) { throw new MissingParameterException(""); }

            Client? client = await _unitOfWork.ClientsRepository.FirstWhere(c => c.ClientKey == credentials.ClientKey);
            if (client == null) { throw new NotAuthenticatedException(); }
            /*
             TODO:
            string decryptedNonce = Decrypt(credentials.Nonce,client.ClientSecret);
             */
            string decryptedNonce = credentials.Nonce;
            Nonce? nonce = await _unitOfWork.NoncesRepository.FirstWhere(n => n.Token == decryptedNonce && n.ClientId == client.Id, n => n.Client);
            if (nonce == null) { throw new InvalidNonceException(); }
            if (nonce.ConsumedOn.HasValue) { throw new ConsumedNonceException(); }

            UserToken? userToken = null;
            if (credentials.UserToken != null)
            {
                userToken = await _unitOfWork.UserTokensRepository.FirstWhere(t => t.Token == credentials.UserToken,
                    t => t.User,
                    t => t.User.Role,
                    t => t.User.Login,
                    t => t.User.Login.LoginType);
                if (userToken == null) { throw new NotAuthenticatedException(); }
                if (userToken.RemainingSeconds <= 30) { throw new ExpiredTokenException(); }
            }

            return new Consumer()
            {
                UserToken = userToken,
                Nonce = nonce,
            };
        }

        protected async Task ConsumeNonceOrThrow(int nonceId)
        {
            if (productionModeProvider.IsProductionModeEnabled == false) { return; }
            Nonce? nonce = await _unitOfWork.NoncesRepository.GetByPk(nonceId);
            if (nonce == null) { throw new Exception("Nonce not found"); }
            nonce.ConsumedOn = DateTime.UtcNow;
            _unitOfWork.NoncesRepository.Update(nonce);

        }
    }
}