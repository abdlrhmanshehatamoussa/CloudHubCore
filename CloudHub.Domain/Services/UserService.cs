using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Domain.Services
{
    public class UserService : BaseService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task RegisterNewUser(int applicationId, RegisterRequest dto)
        {
            //await _unitOfWork.Save();
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> Login(int applicationId, LoginRequest dto)
        {
            //Fetch user from database
            User? user = await _unitOfWork.UsersRepository.FirstWhere(
                (User u) => u.Email == dto.Email && u.ApplicationId == applicationId,
                u => u.Application,
                u => u.UserTokens,
                u => u.Login,
                u => u.Login.LoginType
            );

            //Check user
            if (user == null) { throw new EntityNotFoundException(); }

            //Check user credentials
            if (user.Login.LoginType.Id != dto.LoginTypeId || user.Login.Passcode != dto.Passcode) { throw new InvalidLoginCredentials(); }

            //Delete all existing tokens
            _unitOfWork.UserTokensRepository.DeleteMultiple(user.UserTokens.ToList());

            //Generate new token
            UserToken token = new UserToken() { UserId = user.Id };
            token.GenerateNewToken();

            //Add Token
            token = await _unitOfWork.UserTokensRepository.Add(token);

            //Save
            await _unitOfWork.Save();

            //Check save success
            if (token.Id <= 0) { throw new Exception("Failed to save token"); }


            //Return the result
            return new LoginResponse()
            {
                Email = user.Email,
                TokenBody = token.Token,
                GlobalId = user.GlobalId,
                ImageURL = user.ImageUrl,
                LoginType = user.Login.LoginType.Name,
                Name = user.Name,
                TokenRemainingSeconds = token.RemainingSeconds,
                TokenAgeSeconds = token.AgeSeconds,
            };
        }

    }
}
