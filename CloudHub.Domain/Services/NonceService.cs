using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class NonceService
    {
        public NonceService(IUnitOfWork unitOfWork, IEnvironmentSettings productionModeProvider)
        {
            _unitOfWork = unitOfWork;
            this.productionModeProvider = productionModeProvider;
        }


        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IEnvironmentSettings productionModeProvider;

        public async Task<Nonce> GenereateNonce(ConsumerCredentials credentials)
        {
            if (string.IsNullOrWhiteSpace(credentials.ClientKey) || string.IsNullOrWhiteSpace(credentials.ClientClaim)) { throw new MissingParameterException("client"); }
            Client? client = await _unitOfWork.ClientsRepository.FirstWhere(c => c.ClientKey == credentials.ClientKey);
            if (client == null) { throw new NotAuthenticatedException(); }
            /*
             TODO: 
            string decryptedClientClaim = Decrypt(request.ClientClaim, client.ClientSecret);
             */
            string decryptedClientClaim = credentials.ClientKey;
            if (decryptedClientClaim != credentials.ClientClaim) { throw new NotAuthenticatedException(); }
            Nonce nonce = new();
            nonce.ClientId = client.Id;
            nonce.GenerateToken();
            nonce = await _unitOfWork.NoncesRepository.Add(nonce);
            await _unitOfWork.Save();
            return nonce;
        }
    }
}
