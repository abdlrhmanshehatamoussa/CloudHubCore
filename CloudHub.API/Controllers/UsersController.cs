using CloudHub.API.DTO;
using CloudHub.Business.DTO;
using CloudHub.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : BasicController
    {
        public UsersController(UserService userService) => _userService = userService;

        private UserService _userService;


        [HttpGet]
        public async Task<dynamic> Fetch()
        {
            LoginResponse response = await _userService.FetchUser(ConsumerCredentials);

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
            LoginResponse response = await _userService.Login(ConsumerCredentials, request);

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
            RegisterResponse response = await _userService.RegisterNewUser(ConsumerCredentials, request);

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
}