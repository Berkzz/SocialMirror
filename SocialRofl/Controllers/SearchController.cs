using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialRofl.Data;
using SocialRofl.Models;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        private DataContext _db;

        public SearchController(DataContext db)
        {
            _db = db;
        }

        [HttpGet("search/user/username/{username}")]
        public IActionResult SearchForUserUsername(string username)
        {
            try
            {
                var users = _db.Users.Where(x => x.UserName.Contains(username)).Take(10);
                return Ok(users.Select(x => new SearchUser
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.UserName,
                    Id = x.Id
                }));
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("search/user/name/{name}")]
        public IActionResult SearchForUserName(string name)
        {
            try
            {
                var users = _db.Users.Where(x => (x.FirstName + " " + x.LastName).Contains(name)).Take(10);
                return Ok(users.Select(x => new SearchUser
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.UserName,
                    Id = x.Id
                }));
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
