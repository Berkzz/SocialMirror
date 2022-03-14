using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialRofl.Extensions;
using SocialRofl.Logic;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserLogic _logic;

        public UserController(UserLogic logic)
        {
            _logic = logic;
        }

        [Authorize]
        [HttpPut("users/setphoto")]
        public IActionResult SetMainPhoto(string hash)
        {
            _logic.SetMainPhoto(User.GetId(), hash);
            return Ok();
        }

        [Authorize]
        [HttpGet("users/info")]
        public IActionResult GetUserInfo() => GetUserInfo(User.GetId());

        [HttpGet("users/info/{userId}")]
        public IActionResult GetUserInfo(int userId)
        {
            return Ok(_logic.GetUserInfo(userId));
        }
    }
}
