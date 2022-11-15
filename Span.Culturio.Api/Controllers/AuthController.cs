using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Span.Culturio.Api.Handler;
using Span.Culturio.Api.Models;
using Span.Culturio.Api.Service.User;

namespace Span.Culturio.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthHandler _authHandler;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IAuthHandler authHandler, IConfiguration configuration)
        {
            _userService = userService;
            _authHandler = authHandler;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody]RegisterUserDto user)
        {
            var userDto = await _userService.CreateUser(user);

            if (userDto == null)
            {
                return BadRequest("Username already exists");
            }

            return userDto;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto login)
        {
            var user = await _userService.GetUserByUsername(login.Username);
            if (user is null)
            {
                return BadRequest("Bad username or password");
            }
                

            var verifyLogin = await _userService.Login(user, login.Password);
            if (!verifyLogin)
            {
                return BadRequest("Bad username or password");
            }

            string token = _authHandler.CreateToken(login.Username, $"{user.Id}", _configuration.GetSection("Jwt:Key").Value);
            return Ok(token);
        }
    }
}
