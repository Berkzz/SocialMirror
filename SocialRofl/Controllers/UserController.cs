using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialRofl.Data;
using SocialRofl.Extensions;
using SocialRofl.Models;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private DataContext _db;

        public UserController(DataContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpPut("users/setphoto")]
        public IActionResult SetMainPhoto(string hash)
        {
            try
            {
                var photo = _db.Photos.SingleOrDefault(x => x.Hash == hash);
                if(photo == null)
                {
                    return NotFound();
                }
                User.GetUser(_db).MainPhoto = photo;
                _db.SaveChanges();
                return Ok();
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        [Authorize]
        [HttpGet("users/info")]
        public IActionResult GetUserInfo() => GetUserInfo(User.GetId());

        [HttpGet("users/info/{userId}")]
        public IActionResult GetUserInfo(int userId)
        {
            try
            {
                var user = _db.Users.Where(x => x.Id == userId).Include(x => x.MainPhoto).FirstOrDefault();
                if(user == null)
                {
                    return NotFound();
                }
                return Ok(new UserView
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MainPhoto = user.MainPhoto?.Hash,
                    Birth = user.Birth,
                    Username = user.UserName
                });
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
