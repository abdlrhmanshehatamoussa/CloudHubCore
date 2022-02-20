using CloudHub.Domain.Models;

namespace CloudHub.API.Contracts
{
    public struct NonceResponse
    {
        public string token { get; set; }
        public string created_on { get; set; }

        public static NonceResponse FromNonce(Nonce nonce)
        {
            return new()
            {
                token = nonce.Token,
                created_on = nonce.CreatedOn.ToString()
            };
        }
    }
}
