using Microsoft.AspNetCore.Mvc;
using SocialRofl.Logic;
using SocialRofl.Models;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private Auth _logic;

        public AuthController(Auth logic)
        {
            _logic = logic;
        }

        [HttpPost("register")]
        public IActionResult Register(Register register)
        {
            _logic.Create(register);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            return Ok(_logic.Login(login.UserName, login.Password));
        }
    }
}
