using SocialRofl.Data;
using SocialRofl.Models;
using SocialRofl.Models.Database;

namespace SocialRofl.Logic
{
    public class SearchLogic
    {
        private DataContext _db;

        public SearchLogic(DataContext db)
        {
            _db = db;
        }

        public IEnumerable<SearchUser> SearchUsername(string username)
        {
            return SearchUser(x => x.UserName.Contains(username), 10);
        }

        public IEnumerable<SearchUser> SearchName(string name)
        {
            return SearchUser(x => (x.FirstName + " " + x.LastName).Contains(name), 10);
        }

        private IEnumerable<SearchUser> SearchUser(Func<User, bool> predicate, int take)
        {
            var users = _db.Users.Where(predicate).Take(take);
            return users.Select(x => new SearchUser
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Username = x.UserName,
                Id = x.Id
            });
        }
    }
}
