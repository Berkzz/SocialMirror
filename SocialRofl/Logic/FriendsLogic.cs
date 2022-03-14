using SocialRofl.Data;
using SocialRofl.Exceptions;
using SocialRofl.Models;

namespace SocialRofl.Logic
{
    public class FriendsLogic
    {
        private DataContext _db;

        public FriendsLogic(DataContext db)
        {
            _db = db;
        }

        public FriendList GetFriendList(int userId)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }
            _db.Entry(user).Collection(x => x.Following).Load();
            _db.Entry(user).Collection(x => x.SubscribedTo).Load();
            return new FriendList { Ids = user.Following.Intersect(user.SubscribedTo).Select(x => x.Id) };
        }

        public void Subscribe(int ownerId, int userId)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == userId);
            var me = _db.Users.SingleOrDefault(x => x.Id == ownerId);
            if (user == null || me == null)
            {
                throw new UserNotFoundException("User not found");
            }
            _db.Entry(me).Collection(x => x.SubscribedTo).Load();
            me.SubscribedTo.Add(user);
            _db.SaveChanges();
        }

        public void Remove(int ownerId, int userId)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == userId);
            var me = _db.Users.SingleOrDefault(x => x.Id == ownerId);
            if (user == null || me == null)
            {
                throw new UserNotFoundException("User not found");
            }
            _db.Entry(me).Collection(x => x.SubscribedTo).Load();
            me.SubscribedTo.Remove(user);
            _db.SaveChanges();
        }
    }
}
