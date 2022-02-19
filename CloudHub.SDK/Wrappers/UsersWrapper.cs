using CloudHub.ApiContracts;

namespace CloudHub.SDK
{
    public class UsersWrapper : BaseWrapper
    {
        public UsersWrapper(ClientInfo info) : base(info) { }


        public async Task<RegisterResponseContract> RegisterEndUser(RegisterRequestContract registerRequest)
        {
            return await Post<RegisterResponseContract>(UsersEndpoint, registerRequest);
        }
        public async Task<LoginResponseContract> LoginUser(LoginRequestContract loginRequest)
        {
            return await Post<LoginResponseContract>(UsersLoginEndpoint, loginRequest);
        }

        public async Task<LoginResponseContract> FetchUser(string userToken)
        {
            return await Get<LoginResponseContract>(UsersEndpoint, userToken);
        }

    }
}