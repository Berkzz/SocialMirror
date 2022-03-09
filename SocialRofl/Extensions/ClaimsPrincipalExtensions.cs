using SocialRofl.Data;
using SocialRofl.Models.Database;
using System.Security.Claims;

namespace SocialRofl.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetId(this ClaimsPrincipal claims)
        {
            return int.Parse(claims.FindFirstValue("id"));
        }

        public static User GetUser(this ClaimsPrincipal claims, DataContext db)
        {
            return db.Users.SingleOrDefault(x => x.Id == GetId(claims));
        }
    }
}
