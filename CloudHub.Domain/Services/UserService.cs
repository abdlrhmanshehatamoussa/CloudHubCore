using CloudHub.Domain.DTO;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Models;
using CloudHub.Utils;


namespace CloudHub.Domain.Services
{
    public class UserService : BaseService
    {
        public UserService(IUnitOfWork unitOfWork, IEnvironmentSettings productionModeProvider, IOAuthService oAuthService) : base(unitOfWork, productionModeProvider)
        {
            _oAuthService = oAuthService;
        }

        private readonly IOAuthService _oAuthService;

        public async Task<UserToken> FetchUser(ConsumerCredentials consumerCredentials)
        {
            Consumer consumer = await GetConsumer(consumerCredentials);
            UserToken token = consumer.UserToken ?? throw new NotAuthenticatedException();

            //Consume Nonce
            await ConsumeNonceOrThrow(consumer.Nonce.Id);
            await _unitOfWork.Save();

            //Return the result
            return token;
        }

        public async Task<User> RegisterNewUser(ConsumerCredentials credentials, CreateUserDTO dto)
        {
            Consumer consumer = await GetConsumer(credentials);

            //Fetch user from database
            User? user = await _unitOfWork.UsersRepository.FirstWhere((User u) => u.Email == dto.email && u.TenantId == consumer.Client.TenantId);

            //Check user
            if (user != null) { throw new UserExistsException(); }

            //Create user
            user = new User
            {
                Email = dto.email,
                Name = dto.name,
                ImageUrl = dto.image_url,
                TenantId = consumer.Client.TenantId
            };
            double timeStamp = DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalMilliseconds;
            user.GlobalId = SecurityHelper.Hash256(String.Format("{0}{1}", dto.email, timeStamp));

            user = await _unitOfWork.UsersRepository.Add(user);

            //Create Login
            Login login = new();
            login.LoginTypeId = dto.login_type;
            login.User = user;
            string passcode;
            if (dto.login_type == ELoginTypes.LOGIN_TYPE_BASIC)
            {
                passcode = dto.password;
            }
            else
            {
                OAuthUser? oAuthUser = await _oAuthService.GetUserByToken(dto.password, dto.login_type);
                if (oAuthUser == null || oAuthUser.Email != dto.email) { throw new NotAuthenticatedException(); }
                passcode = oAuthUser.OpenId;
            }

            /*
            //TODO: Encrypt password before saving it, using client secret
            string clientSecret = consumerInfo.ClientApplication.Client.ClientSecret;
            login.Passcode = Encrypt(passcode,clientSecret);

            - You will have to decrypt the password in the Login usecase
             */
            login.Passcode = passcode;
            login = await _unitOfWork.LoginsRepository.Add(login);

            //Consume Nonce
            await ConsumeNonceOrThrow(consumer.Nonce.Id);

            //Save
            await _unitOfWork.Save();

            //Return the result
            return user;
        }



        public async Task<UserToken> Login(ConsumerCredentials clientCredentials, CreateLoginDTO dto)
        {
            Consumer consumer = await GetConsumer(clientCredentials);

            //Fetch user from database
            User? user = await _unitOfWork.UsersRepository.FirstWhere(
                (User u) => u.Email == dto.Email && u.TenantId == consumer.Client.TenantId,
                u => u.UserTokens,
                u => u.Login,
                u => u.Login.LoginType
            );

            //Check user
            if (user == null) { throw new UserNotExistsException(); }

            //Check user credentials
            if (user.Login.LoginTypeId != dto.LoginType) { throw new NotAuthenticatedException(); }

            if (dto.LoginType != ELoginTypes.LOGIN_TYPE_BASIC)
            {
                OAuthUser? oAuthUser = await _oAuthService.GetUserByToken(dto.Password, dto.LoginType);
                if (oAuthUser == null || oAuthUser.Email != dto.Email) { throw new NotAuthenticatedException(); }
            }
            else
            {
                if (user.Login.Passcode != dto.Password) { throw new NotAuthenticatedException(); }
            }

            //Delete all existing tokens
            _unitOfWork.UserTokensRepository.DeleteMultiple(user.UserTokens.ToList());

            //Generate new token
            UserToken token = new() { UserId = user.Id };
            token.GenerateNewToken();

            //Add Token
            token = await _unitOfWork.UserTokensRepository.Add(token);

            //Consume Nonce
            await ConsumeNonceOrThrow(consumer.Nonce.Id);

            //Save
            await _unitOfWork.Save();

            //Check save success
            if (token.Id <= 0) { throw new Exception("Failed to save token"); }

            //Return the result
            return token;
        }
    }
}
