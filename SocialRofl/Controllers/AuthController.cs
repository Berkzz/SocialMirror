using Microsoft.AspNetCore.Mvc;
using SocialRofl.Models;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private Logic.Auth _logic;

        public AuthController(Logic.Auth logic)
        {
            _logic = logic;
        }

        [HttpPost("register")]
        public IActionResult Register(Register register)
        {
            return Ok(_logic.Create(register));
        }

        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            return Ok(_logic.Login(login.UserName, login.Password));
        }
    }
}
