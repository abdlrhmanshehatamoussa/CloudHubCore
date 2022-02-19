using CloudHub.ApiContracts;
using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : BasicController
    {
        public UsersController(UserService userService) => _userService = userService;

        private readonly UserService _userService;


        private LoginResponseContract MapLoginResponse(LoginResponse response)
        {
            return new()
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
        private LoginRequest MapLoginRequest(LoginRequestContract contract)
        {
            ELoginTypes loginType = Enum.Parse<ELoginTypes>(contract.login_type.ToString());
            LoginRequest request = new LoginRequest(contract.email, contract.password, loginType);
            return request;
        }

        [HttpGet]
        public async Task<LoginResponseContract> Fetch()
        {
            LoginResponse response = await _userService.FetchUser(ConsumerCredentials);
            var contract = MapLoginResponse(response);
            return contract;
        }



        [HttpPost]
        [Route("login")]
        public async Task<LoginResponseContract> Login([FromBody] LoginRequestContract requestContract)
        {
            LoginRequest request = MapLoginRequest(requestContract);
            LoginResponse response = await _userService.Login(ConsumerCredentials, request);
            LoginResponseContract contract = MapLoginResponse(response);
            return contract;
        }


        [HttpPost]
        public async Task<RegisterResponseContract> Register([FromBody] RegisterRequest request)
        {
            RegisterResponse response = await _userService.RegisterNewUser(ConsumerCredentials, request);

            return new()
            {
                success = true,
                user = new()
                {
                    email = response.Email,
                    name = response.Name,
                    image_url = response.ImageURL,
                    global_id = response.GlobalId

                }
            };
        }

    }
}