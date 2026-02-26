using Auth.Application.Commands;
using Auth.Application.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RegisterHandler _register;
        private readonly LoginHandler _login;

        public AuthController(RegisterHandler register, LoginHandler login)
        {
            _register = register;
            _login = login;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommandd cmd)
        {
            await _register.Handle(cmd);
            return Ok("User created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand cmd)
        {
            var token = await _login.Handle(cmd);
            return Ok(new { token });
        }

        //[Authorize(Roles = "Admin")]
        //[HttpGet("admin-only")]
        //public IActionResult AdminOnly()
        //{
        //    return Ok("Welcome Admin");
        //}
    }
}
