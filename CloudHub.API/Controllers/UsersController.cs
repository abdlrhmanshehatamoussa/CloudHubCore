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
        public async Task<RegisterResponseContract> Register([FromBody] RegisterRequestContract requestContract)
        {
            CreateUserDTO dto = requestContract.ToDTO();
            User user = await _userService.RegisterNewUser(ConsumerCredentials, dto);
            RegisterResponseContract contract = RegisterResponseContract.FromUser(user);
            return contract;
        }

    }
}