using System.ComponentModel.DataAnnotations;

namespace SocialRofl.Models.Database
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        public AttachmentType Type { get; set; }
        [MaxLength(64)]
        public string AttachmentHash { get; set; } = null!;
        public Post Post { get; set; } = null!;
    }
}
