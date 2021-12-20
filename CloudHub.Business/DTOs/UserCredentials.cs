namespace CloudHub.Business.DTO
{
    public class UserCredentials
    {
        public UserCredentials(ClientCredentials clientCredentials, string userToken, string? nonce = null)
        {
            ClientCredentials = clientCredentials;
            UserToken = userToken;
            Nonce = nonce;
        }

        public ClientCredentials ClientCredentials { get; set; }
        
        public string UserToken { get; set; }
        public string? Nonce { get; set; }
    }
}
