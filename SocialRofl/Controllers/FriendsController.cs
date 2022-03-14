using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialRofl.Extensions;
using SocialRofl.Logic;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private FriendsLogic _logic;

        public FriendsController(FriendsLogic logic)
        {
            _logic = logic;
        }

        [HttpGet("friends/list")]
        public IActionResult List()
        {
            return List(User.GetId());
        }

        [HttpGet("friends/list/{userId}")]
        public IActionResult List(int userId)
        {
            return Ok(_logic.GetFriendList(userId));
        }

        [Authorize]
        [HttpGet("friends/subscribe/{userId}")]
        public IActionResult Subscribe(int userId)
        {
            _logic.Subscribe(User.GetId(), userId);
            return Ok();
        }

        [Authorize]
        [HttpGet("friends/remove/{userId}")]
        public IActionResult RemoveFriend(int userId)
        {
            _logic.Remove(User.GetId(), userId);
            return Ok();
        }
    }
}
