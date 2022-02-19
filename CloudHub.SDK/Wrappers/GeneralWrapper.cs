using CloudHub.ApiContracts;

namespace CloudHub.SDK
{
    public class GeneralWrapper : BaseWrapper
    {
        public GeneralWrapper(ClientInfo info) : base(info) { }

        public async Task<PingResponseContract> Ping()
        {
            return await Get<PingResponseContract>(PingEndpoint);
        }
    }
}