using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialRofl.Data;
using SocialRofl.Extensions;
using SocialRofl.Logic;
using SocialRofl.Models;
using SocialRofl.Models.Database;

namespace SocialRofl.Controllers
{
    [ApiController]
    public class WallController : ControllerBase
    {
        private DataContext _db;
        private AttachmentChecker _checker;

        public WallController(DataContext db, AttachmentChecker checker)
        {
            _db = db;
            _checker = checker;
        }

        [Authorize]
        [HttpPost("post/add")]
        public IActionResult AddPost(PostModel post)
        {
            try
            {
                foreach(var attach in post.Attachments)
                {
                    if(!_checker.Exists(attach.Type, attach.Hash))
                    {
                        return BadRequest("Bad attachment");
                    }
                }
                var dbPost = new Post
                {
                    Attachments = post.Attachments.Select(x => new Attachment { AttachmentHash = x.Hash, Type = x.Type }).ToList(),
                    CreatedDate = DateTime.Now,
                    OwnerId = User.GetId(),
                    Text = post.Text
                };
                _db.Posts.Add(dbPost);
                _db.SaveChanges();
                return Ok();
            } 
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        [Authorize]
        [HttpGet("post/user")]
        public IActionResult GetPosts(int start = 0, int count = 10) => GetPosts(User.GetId(), start, count);

        [HttpGet("post/user/{id}")]
        public IActionResult GetPosts(int id, int start = 0, int count = 10)
        {
            try
            {
                var posts = _db.Posts.Where(x => x.OwnerId == id).Skip(start).Take(count);
                return Ok(posts.Select(x => new PostModel
                {
                    Text = x.Text,
                    Attachments = x.Attachments.Select(y => new AttachmentModel
                    {
                        Hash = y.AttachmentHash,
                        Type = y.Type
                    })
                }));
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("post/{id}")]
        public IActionResult GetPost(int id)
        {
            try
            {
                var post = _db.Posts.SingleOrDefault(x => x.Id == id);
                if(post == null)
                {
                    return NotFound();
                }
                return Ok(new PostModel
                {
                    Text = post.Text,
                    Attachments = post.Attachments.Select(x => new AttachmentModel
                    {
                        Hash = x.AttachmentHash,
                        Type = x.Type
                    })
                });
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }

        [Authorize]
        [HttpDelete("post/remove")]
        public IActionResult RemovePost(int postId)
        {
            try
            {
                var post = _db.Posts.SingleOrDefault(x => x.Id == postId);
                if(post == null)
                {
                    return NotFound();
                }
                if(post.OwnerId != User.GetId())
                {
                    return Unauthorized();
                }
                _db.Posts.Remove(post);
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
