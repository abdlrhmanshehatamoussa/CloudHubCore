namespace CloudHub.SDK
{
    public class CloudHubUsersWrapper
    {
        public CloudHubUsersWrapper(CloudHubApiSettings settings) => _settings = settings;
        private readonly CloudHubApiSettings _settings;

        public async Task RegisterEndUser()
        {
            HttpClient client = new HttpClient();
            await client.PostAsync(_settings.ApiURL, null);
        }
        public async Task LoginUser()
        {
            HttpClient client = new HttpClient();
            await client.PostAsync(_settings.ApiURL, null);
        }

    }
}