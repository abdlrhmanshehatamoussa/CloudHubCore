namespace CloudHub.SDK
{
    public class GeneralWrapper : BaseWrapper
    {
        public GeneralWrapper(ClientInfo info) : base(info) { }

        public async Task<dynamic?> Ping()
        {
            return await Get<EmptyResponse>(PingEndpoint);
        }
    }
}