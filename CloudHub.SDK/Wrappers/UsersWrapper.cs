using CloudHub.API.Domain.DTO;

namespace CloudHub.SDK
{
    public class UsersWrapper : BaseWrapper
    {
        public UsersWrapper(ClientInfo info) : base(info) { }


        public async Task<RegisterResponse> RegisterEndUser(RegisterRequest registerRequest)
        {
            return await Post<RegisterResponse>(UsersEndpoint, registerRequest);
        }
        public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        {
            return await Post<LoginResponse>(UsersLoginEndpoint, loginRequest);
        }

        public async Task<LoginResponse> FetchUser(string userToken)
        {
            return await Get<LoginResponse>(UsersEndpoint, userToken);
        }

    }
}