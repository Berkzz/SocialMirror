using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SocialRofl.Models.Database
{
    public class Photo
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        [MaxLength(64)]
        public string Hash { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
