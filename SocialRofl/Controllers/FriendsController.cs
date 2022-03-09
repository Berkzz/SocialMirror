using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialRofl.Data;
using SocialRofl.Extensions;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private DataContext _db;

        public FriendsController(DataContext db)
        {
            _db = db;
        }

        [HttpGet("friends/list")]
        public IActionResult List()
        {
            return List(User.GetId());
        }

        [HttpGet("friends/list/{userId}")]
        public IActionResult List(int userId)
        {
            try
            {
                var user = _db.Users.SingleOrDefault(x => x.Id == userId);
                if(user == null)
                {
                    return NotFound();
                }
                _db.Entry(user).Collection(x => x.Following).Load();
                _db.Entry(user).Collection(x => x.SubscribedTo).Load();
                return Ok(user.Following.Intersect(user.SubscribedTo).Select(x => x.Id));
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        [Authorize]
        [HttpGet("friends/subscribe/{userId}")]
        public IActionResult Subscribe(int userId)
        {
            try
            {
                var user = _db.Users.SingleOrDefault(x => x.Id == userId);
                if (user == null)
                {
                    return NotFound();
                }
                var me = User.GetUser(_db);
                _db.Entry(me).Collection(x => x.SubscribedTo).Load();
                me.SubscribedTo.Add(user);
                _db.SaveChanges();
                return Ok();
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        [Authorize]
        [HttpGet("friends/remove/{userId}")]
        public IActionResult RemoveFriend(int userId)
        {
            try
            {
                var user = _db.Users.SingleOrDefault(x => x.Id == userId);
                if (user == null)
                {
                    return NotFound();
                }
                var me = User.GetUser(_db);
                _db.Entry(me).Collection(x => x.SubscribedTo).Load();
                me.SubscribedTo.Remove(user);
                _db.SaveChanges();
                return Ok();
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
