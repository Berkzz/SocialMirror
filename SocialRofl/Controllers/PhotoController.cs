using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MlkPwgen;
using SocialRofl.Data;
using SocialRofl.Extensions;
using SocialRofl.Logic;
using SocialRofl.Models;
using SocialRofl.Models.Database;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private PhotoLogic _logic;

        public PhotoController(PhotoLogic uploader)
        {
            _logic = uploader;
        }

        [Authorize]
        [HttpPost("photos/upload")]
        public IActionResult Upload(IFormFile file, string description)
        {
            return Ok(_logic.UploadPhoto(file, description, User.GetId()));
        }

        [HttpGet("photos/{hash}")]
        public IActionResult Get(string hash)
        {
            return File(_logic.GetPhoto(hash), "image/jpeg");
        }

        [Authorize]
        [HttpGet("photos/getall")]
        public IActionResult GetUserPhotos() => GetUserPhotos(User.GetId());

        [Authorize]
        [HttpGet("photos/getall/{userId}")]
        public IActionResult GetUserPhotos(int userId)
        {
            return Ok(_logic.GetUserPhotos(userId));
        }
    }
}
