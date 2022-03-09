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
        private DataContext _db;
        private PhotoUploader _uploader;
        private string[] _allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp" };

        public PhotoController(DataContext db, PhotoUploader uploader)
        {
            _db = db;
            _uploader = uploader;
        }

        [Authorize]
        [HttpPost("photos/upload")]
        public IActionResult Upload(IFormFile file, string description)
        {
            try
            {
                if (file == null)
                {
                    return BadRequest("No file");
                }
                var ext = Path.GetExtension(file.FileName);
                if(!_allowedExtensions.Contains(ext))
                {
                    return BadRequest($"Only {string.Join(", ", _allowedExtensions)} extensions allowed");
                }
                var hash = PasswordGenerator.Generate(length: 16, allowed: Sets.Alphanumerics);
                _uploader.Upload(hash, file);
                _db.Photos.Add(new Photo { Description = description, Hash = hash, User = User.GetUser(_db) });
                _db.SaveChanges();
                return Ok(new PhotoUploadResult { Hash = hash });
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("photos/{hash}")]
        public IActionResult Get(string hash)
        {
            try
            {
                var photo = _db.Photos.SingleOrDefault(x => x.Hash == hash);
                if(photo == null)
                {
                    return NotFound();
                }
                var image = System.IO.File.OpenRead(@$"{Environment.CurrentDirectory}/Photos/{hash}.jpeg");
                return File(image, "image/jpeg");
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        [Authorize]
        [HttpGet("photos/getall")]
        public IActionResult GetUserPhotos() => GetUserPhotos(User.GetId());

        [Authorize]
        [HttpGet("photos/getall/{userId}")]
        public IActionResult GetUserPhotos(int userId)
        {
            try
            {
                var user = _db.Users.SingleOrDefault(x => x.Id == userId);
                if (user == null)
                {
                    return NotFound();
                }
                _db.Entry(user).Collection(x => x.Photos).Load();
                var photos = user.Photos.Select(x => x.Hash).ToList();
                return Ok(new AllPhotos { Hashes = photos });
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
