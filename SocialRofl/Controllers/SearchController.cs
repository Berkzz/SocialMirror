using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialRofl.Data;
using SocialRofl.Logic;
using SocialRofl.Models;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        private SearchLogic _logic;

        public SearchController(SearchLogic logic)
        {
            _logic = logic;
        }

        [HttpGet("search/user")]
        public IActionResult SearchUser(string name, string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                return Ok(_logic.SearchUsername(username));
            }
            if (!string.IsNullOrEmpty(name))
            {
                return Ok(_logic.SearchName(name));
            }
            return BadRequest("No search parameters provided");
        }
    }
}
