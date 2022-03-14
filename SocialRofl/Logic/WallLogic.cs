using SocialRofl.Data;
using SocialRofl.Exceptions;
using SocialRofl.Models;
using SocialRofl.Models.Database;

namespace SocialRofl.Logic
{
    public class WallLogic
    {
        private DataContext _db;
        private AttachmentChecker _checker;

        public WallLogic(DataContext db, AttachmentChecker checker)
        {
            _db = db;
            _checker = checker;
        }

        public void AddPost(PostModel post, int ownerId)
        {
            foreach (var attach in post.Attachments)
            {
                if (!_checker.Exists(attach.Type, attach.Hash))
                {
                    throw new BadAttachmentException("Bad attachment");
                }
            }
            var dbPost = new Post
            {
                Attachments = post.Attachments.Select(x => new Attachment { AttachmentHash = x.Hash, Type = x.Type }).ToList(),
                CreatedDate = DateTime.Now,
                OwnerId = ownerId,
                Text = post.Text
            };
            _db.Posts.Add(dbPost);
            _db.SaveChanges();
        }

        public IEnumerable<PostModel> GetUserPosts(int userId, int start, int count)
        {
            var posts = _db.Posts.Where(x => x.OwnerId == userId).Skip(start).Take(count);
            return posts.Select(x => new PostModel
            {
                Text = x.Text,
                Attachments = x.Attachments.Select(y => new AttachmentModel
                {
                    Hash = y.AttachmentHash,
                    Type = y.Type
                })
            });
        }

        public PostModel GetPost(int id)
        {
            var post = _db.Posts.SingleOrDefault(x => x.Id == id);
            if (post == null)
            {
                throw new PostNotFoundException("Post not found");
            }
            return new PostModel
            {
                Text = post.Text,
                Attachments = post.Attachments.Select(x => new AttachmentModel
                {
                    Hash = x.AttachmentHash,
                    Type = x.Type
                })
            };
        }

        public void DeletePost(int id, int currentUserId)
        {
            var post = _db.Posts.SingleOrDefault(x => x.Id == id);
            if (post == null)
            {
                throw new PostNotFoundException("Post not found");
            }
            if (post.OwnerId != currentUserId)
            {
                throw new BadUserException("No permissions");
            }
            _db.Posts.Remove(post);
            _db.SaveChanges();
        }
    }
}
