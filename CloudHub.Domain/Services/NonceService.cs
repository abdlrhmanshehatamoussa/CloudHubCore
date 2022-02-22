using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Models;

namespace CloudHub.Domain.Services
{
    public class NonceService
    {
        

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IEncryptionService _encryptionService;

        public NonceService(IUnitOfWork unitOfWork, IEncryptionService encryptionService)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
        }

        public async Task<Nonce> GenereateNonce(ConsumerCredentials credentials)
        {
            if (string.IsNullOrWhiteSpace(credentials.ClientKey) || string.IsNullOrWhiteSpace(credentials.ClientClaim)) { throw new MissingParameterException("client"); }
            Client? client = await _unitOfWork.ClientsRepository.FirstWhere(c => c.ClientKey == credentials.ClientKey);
            if (client == null) { throw new NotAuthenticatedException(); }

            string? decryptedClientClaim = _encryptionService.Decrypt(credentials.ClientClaim, client.ClientSecret);
            if (decryptedClientClaim != credentials.ClientKey) { throw new NotAuthenticatedException(); }
            Nonce nonce = new();
            nonce.ClientId = client.Id;
            nonce.GenerateToken();
            nonce = await _unitOfWork.NoncesRepository.Add(nonce);
            await _unitOfWork.Save();
            return nonce;
        }
    }
}
