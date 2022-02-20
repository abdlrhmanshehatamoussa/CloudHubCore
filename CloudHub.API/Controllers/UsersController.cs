using CloudHub.API.Contracts;
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


        [HttpGet]
        public async Task<LoginResponseContract> Fetch()
        {
            UserToken token = await _userService.FetchUser(ConsumerCredentials);
            LoginResponseContract contract = LoginResponseContract.FromUserToken(token);
            return contract;
        }



        [HttpPost]
        [Route("login")]
        public async Task<LoginResponseContract> Login([FromBody] LoginRequestContract requestContract)
        {
            CreateLoginDTO request = requestContract.ToDTO();
            UserToken token = await _userService.Login(ConsumerCredentials, request);
            LoginResponseContract contract = LoginResponseContract.FromUserToken(token);
            return contract;
        }


        [HttpPost]
        public async Task<RegisterResponseContract> Register([FromBody] CreateUserDTO request)
        {
            User response = await _userService.RegisterNewUser(ConsumerCredentials, request);

            return new()
            {
                success = true,
                user = new()
                {
                    email = response.Email,
                    name = response.Name,
                    image_url = response.ImageUrl,
                    global_id = response.GlobalId

                }
            };
        }

    }
}