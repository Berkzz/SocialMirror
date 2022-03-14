using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialRofl.Extensions;
using SocialRofl.Logic;
using SocialRofl.Models;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class WallController : ControllerBase
    {
        private WallLogic _logic;

        public WallController(WallLogic logic)
        {
            _logic = logic;
        }

        [Authorize]
        [HttpPost("post/add")]
        public IActionResult AddPost(PostModel post)
        {
            _logic.AddPost(post, User.GetId());
            return Ok();
        }

        [Authorize]
        [HttpGet("post/user")]
        public IActionResult GetPosts(int start = 0, int count = 10) => GetPosts(User.GetId(), start, count);

        [HttpGet("post/user/{userId}")]
        public IActionResult GetPosts(int userId, int start = 0, int count = 10)
        {
            return Ok(_logic.GetUserPosts(userId, start, count));
        }

        [HttpGet("post/{id}")]
        public IActionResult GetPost(int id)
        {
            return Ok(_logic.GetPost(id));
        }

        [Authorize]
        [HttpDelete("post/remove")]
        public IActionResult RemovePost(int postId)
        {
            _logic.DeletePost(postId, User.GetId());
            return Ok();
        }
    }
}
