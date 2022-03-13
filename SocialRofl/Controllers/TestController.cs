using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialRofl.Data;
using SocialRofl.Extensions;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private IConfiguration _config;
        private DataContext _db;

        public TestController(IConfiguration config, DataContext db)
        {
            _config = config;
            _db = db;
        }

        [HttpGet("connectionstring")]
        public IActionResult GetConnectionString()
        {
            return Ok(_config.GetConnectionString("Default"));
        }

        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == User.GetId());
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet("throwexception")]
        public IActionResult ThrowException()
        {
            throw new Exception("Test exception");
        }
    }
}
