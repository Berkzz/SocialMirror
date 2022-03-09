namespace SocialRofl.Models.Database
{
    public class Follower
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int SubscribedToId { get; set; }
        public User SubscribedTo { get; set; } = null!;
    }
}
