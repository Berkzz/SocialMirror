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
            try
            {
                var result = _logic.Create(register);
                if(!result.Success)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            try
            {
                var result = _logic.Login(login.UserName, login.Password);
                if(!result.Success)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
