using CloudHub.API.Domain.DTO;
using CloudHub.API.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHub.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : BasicController
    {
        public UsersController(UserService userService) => _userService = userService;

        private readonly UserService _userService;


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
        public async Task<dynamic> Login([FromBody] LoginRequest request)
        {
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
        public async Task<dynamic> Register([FromBody] RegisterRequest request)
        {
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