using CloudHub.SDK;

namespace CloudHub.Tests.SDK
{
    public static class Helper
    {
        public static CloudHubManager CloudHubManager = new(
            new()
            {
                ApiURL = "http://test.cloudhub.vps238.com",
                ClientKey = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f",
                ClientSecret = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f"
            }
        );
    }
}
