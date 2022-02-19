namespace CloudHub.SDK
{
    public class CloudHubManager
    {
        public CloudHubManager(ClientInfo info) => _info = info;
        private readonly ClientInfo _info;

        public UsersWrapper Users => new (_info);
        public GeneralWrapper General => new (_info);
    }
}