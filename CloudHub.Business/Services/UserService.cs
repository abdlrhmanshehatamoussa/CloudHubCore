﻿using CloudHub.Business.DTO;
using CloudHub.Crosscutting;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Repositories;

namespace CloudHub.Business.Services
{
    public class UserService : BaseService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<LoginResponse> FetchUser(ConsumerCredentials consumerCredentials)
        {
            ConsumerInfo consumerInfo = await GetConsumerInfo(consumerCredentials);
            Nonce nonce = consumerInfo.Nonce ?? throw new InvalidNonceException();
            UserToken token = consumerInfo.UserToken ?? throw new NotAuthenticatedException();
            User user = token.User;


            //Consume Nonce
            await ConsumeNonce(nonce.Id);
            await _unitOfWork.Save();

            //Return the result
            return new LoginResponse()
            {
                Email = user.Email,
                TokenBody = token.Token,
                GlobalId = user.GlobalId,
                ImageURL = user.ImageUrl,
                LoginTypeName = user.Login.LoginType.Name,
                Name = user.Name,
                TokenRemainingSeconds = token.RemainingSeconds,
                TokenAgeSeconds = token.AgeSeconds,
            };
        }

        public async Task<RegisterResponse> RegisterNewUser(ConsumerCredentials credentials, RegisterRequest dto)
        {
            ConsumerInfo consumerInfo = await GetConsumerInfo(credentials);
            int nonceId = consumerInfo.Nonce?.Id ?? throw new InvalidNonceException();

            //Fetch user from database
            User? user = await _unitOfWork.UsersRepository.FirstWhere((User u) => u.Email == dto.Email && u.ApplicationId == consumerInfo.ClientApplication.ApplicationId);

            //Check user
            if (user != null) { throw new UserExistsException(); }

            //Create user
            user = new User();
            user.Email = dto.Email;
            user.Name = dto.Name;
            user.ImageUrl = dto.ImageUrl;
            user.ApplicationId = consumerInfo.ClientApplication.ApplicationId;
            double timeStamp = DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalMilliseconds;
            user.GlobalId = Utils.Hash256(String.Format("{0}{1}{2}", dto.Email, consumerInfo.ClientApplication.ApplicationId, timeStamp));

            user = await _unitOfWork.UsersRepository.Add(user);

            //Create Login
            Login login = new Login();
            login.LoginTypeId = dto.LoginTypeId;
            login.User = user;
            if (dto.LoginTypeId == LoginTypeValues.LOGIN_TYPE_BASIC)
            {
                login.Passcode = dto.Passcode;
            }
            else
            {
                //OAuthInfo info  = await ExchangeOAuthAccessToken(dto.Passcode, dto.LoginTypeId);
                //if (info.Email != dto.Email) { throw new NotAuthenticatedException(); }
                //login.Passcode = info.OAuthOpenId;
                throw new NotImplementedException();
            }

            login = await _unitOfWork.LoginsRepository.Add(login);

            //Consume Nonce
            await ConsumeNonce(nonceId);

            //Save
            await _unitOfWork.Save();

            //Return the result
            return new RegisterResponse() { Email = user.Email, Name = user.Name, ImageURL = user.ImageUrl, GlobalId = user.GlobalId };
        }



        public async Task<LoginResponse> Login(ConsumerCredentials clientCredentials, LoginRequest dto)
        {
            ConsumerInfo consumerInfo = await GetConsumerInfo(clientCredentials);
            int nonceId = consumerInfo.Nonce?.Id ?? throw new InvalidNonceException();

            //Fetch user from database
            User? user = await _unitOfWork.UsersRepository.FirstWhere(
                (User u) => u.Email == dto.Email && u.ApplicationId == consumerInfo.ClientApplication.ApplicationId,
                u => u.Application,
                u => u.UserTokens,
                u => u.Login,
                u => u.Login.LoginType
            );

            //Check user
            if (user == null) { throw new NotAuthenticatedException(); }

            //Check user credentials
            if (user.Login.LoginTypeId != dto.LoginTypeId) { throw new NotAuthenticatedException(); }

            if (dto.LoginTypeId != LoginTypeValues.LOGIN_TYPE_BASIC)
            {
                //OAuthInfo info  = await ExchangeOAuthAccessToken(dto.Passcode, dto.LoginTypeId);
                //if (info.Email != dto.Email) { throw new NotAuthenticatedException(); }
                throw new NotImplementedException();
            }
            else
            {
                if (user.Login.Passcode != dto.Passcode) { throw new NotAuthenticatedException(); }
            }

            //Delete all existing tokens
            _unitOfWork.UserTokensRepository.DeleteMultiple(user.UserTokens.ToList());

            //Generate new token
            UserToken token = new UserToken() { UserId = user.Id };
            token.GenerateNewToken();

            //Add Token
            token = await _unitOfWork.UserTokensRepository.Add(token);

            //Consume Nonce
            await ConsumeNonce(nonceId);

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
                LoginTypeName = user.Login.LoginType.Name,
                Name = user.Name,
                TokenRemainingSeconds = token.RemainingSeconds,
                TokenAgeSeconds = token.AgeSeconds,
            };
        }
    }
}