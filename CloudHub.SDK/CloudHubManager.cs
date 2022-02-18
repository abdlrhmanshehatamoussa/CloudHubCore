namespace CloudHub.SDK
{
    public class CloudHubManager
    {
        public CloudHubManager(CloudHubApiSettings settings) => _settings = settings;
        private readonly CloudHubApiSettings _settings;

        public CloudHubUsersWrapper CloudHubUsersWrapper => new CloudHubUsersWrapper(_settings);
    }
}