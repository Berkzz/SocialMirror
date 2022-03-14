using Microsoft.EntityFrameworkCore;
using SocialRofl.Data;
using SocialRofl.Exceptions;
using SocialRofl.Models;

namespace SocialRofl.Logic
{
    public class UserLogic
    {
        private DataContext _db;

        public UserLogic(DataContext db)
        {
            _db = db;
        }

        public void SetMainPhoto(int ownerId, string hash)
        {
            var photo = _db.Photos.SingleOrDefault(x => x.Hash == hash);
            var me = _db.Users.SingleOrDefault(x => x.Id == ownerId);
            if (photo == null)
            {
                throw new PhotoNotFoundException("Photo not found");
            }
            if (me == null)
            {
                throw new UserNotFoundException("User not found");
            }
            me.MainPhoto = photo;
            _db.SaveChanges();
        }

        public UserView GetUserInfo(int userId)
        {
            var user = _db.Users.Where(x => x.Id == userId).Include(x => x.MainPhoto).FirstOrDefault();
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }
            return new UserView
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                MainPhoto = user.MainPhoto?.Hash,
                Birth = user.Birth,
                Username = user.UserName
            };
        }
    }
}
