using System.ComponentModel.DataAnnotations;

namespace SocialRofl.Models.Database
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int OwnerId { get; set; }
        [MaxLength(512)]
        public string? Text { get; set; }
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
