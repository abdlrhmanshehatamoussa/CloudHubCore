namespace CloudHub.Domain
{
    public class UserService
    {
        private IUserRepository UserRepository { get; set; }

        public UserService(IUserRepository repo) => UserRepository = repo;

        public async void RegisterNewUser(int applicationId,RegisterDTO dto)
        {
            throw new NotImplementedException();
        }

        public async void Login(int applicationId, LoginDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
