namespace SocialRofl.Models
{
    public class UserView
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public DateTime? Birth { get; set; }
        public string? MainPhoto { get; set; }
    }
}
