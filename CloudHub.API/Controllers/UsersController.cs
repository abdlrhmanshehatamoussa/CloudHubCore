using CloudHub.Business.DTO;
using CloudHub.Business.Services;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : BasicController
    {
        public UsersController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        private UserService UserService => new UserService(unitOfWork);



        [HttpGet]
        public async Task<dynamic> Fetch()
        {
            LoginResponse response = await UserService.FetchUser(ClientCredentials);

            return new
            {
                email = response.Email,
                name = response.Name,
                login_type = response.LoginTypeName,
                image_url = response.ImageURL,
                user_token = response.TokenBody,
                user_token_expires_in = response.TokenRemainingSeconds,
                global_id = response.GlobalId,
            };
        }



        [HttpPost]
        [Route("login")]
        public async Task<dynamic> Login([FromBody] LoginRequestJson json)
        {
            LoginRequest request = new LoginRequest()
            {
                Email = json.email,
                Passcode = json.password,
                LoginTypeId = json.login_type
            };
            LoginResponse response = await UserService.Login(ClientCredentials, request);

            return new
            {
                email = response.Email,
                name = response.Name,
                login_type = response.LoginTypeName,
                image_url = response.ImageURL,
                user_token = response.TokenBody,
                user_token_expires_in = response.TokenRemainingSeconds,
                global_id = response.GlobalId,
            };
        }


        [HttpPost]
        public async Task<dynamic> Register([FromBody] RegisterRequestJson json)
        {
            RegisterRequest request = new RegisterRequest(json.email, json.password, json.name, json.image_url, json.login_type);
            RegisterResponse response = await UserService.RegisterNewUser(ClientCredentials, request);

            return new
            {
                success = true,
                user = new
                {
                    email = response.Email,
                    name = response.Name,
                    image_url = response.ImageURL,
                    global_id = response.GlobalId

                }
            };
        }

    }

    public struct LoginRequestJson
    {
        public string email { get; set; }
        public string password { get; set; }
        public LoginTypeValues login_type { get; set; }
    }

    public struct RegisterRequestJson
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? image_url { get; set; }
        public LoginTypeValues login_type { get; set; }
    }

}