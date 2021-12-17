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
            //Check Application
            Application? app = await _unitOfWork.ApplicationsRepository.GetByPk(applicationId);
            if (app == null)
            {
                throw new EntityNotFoundException("Application not found");
            }

            //Check LoginType
            LoginType? loginType = await _unitOfWork.LoginTypesRepository.GetByPk(dto.LoginTypeId);
            if (loginType == null)
            {
                throw new EntityNotFoundException("LoginType not found");
            }

            //Check User Email
            User? user = await _unitOfWork.UsersRepository.FirstWhere((User u) => u.Email == dto.Email && u.ApplicationId == app.Id);
            if (user == null)
            {
                throw new EntityNotFoundException("User not found");
            }

            //Check user credentials
            Login? dbLogin = await _unitOfWork.LoginsRepository.FirstWhere(l => l.UserId == user.Id && l.Passcode == dto.Passcode && l.LoginTypeId == dto.LoginTypeId);
            if (dbLogin == null)
            {
                throw new InvalidLoginCredentials("Invalid Login Credentials");
            }

            //Delete all existing tokens
            List<UserToken> allUserTokens = await _unitOfWork.UserTokensRepository.Where(t => t.UserId == user.Id);
            _unitOfWork.UserTokensRepository.DeleteMultiple(allUserTokens);

            //Generate new token
            UserToken token = new UserToken() { UserId = user.Id };
            token.GenerateNewToken();

            //Add Token
            token = await _unitOfWork.UserTokensRepository.Add(token);
            
            //Save
            await _unitOfWork.Save();
            if (token.Id <= 0)
            {
                throw new Exception("Failed to save token");
            }


            //Return the result
            return new LoginResponse()
            {
                Email = user.Email,
                TokenBody = token.Token,
                GlobalId = user.GlobalId,
                ImageURL = user.ImageUrl,
                LoginType = loginType.Name,
                Name = user.Name,
                TokenRemainingSeconds = token.RemainingSeconds,
                TokenAgeSeconds = token.AgeSeconds,
            };
        }

    }
}
