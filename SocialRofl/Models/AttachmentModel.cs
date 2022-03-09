using SocialRofl.Models.Database;

namespace SocialRofl.Models
{
    public class AttachmentModel
    {
        public AttachmentType Type { get; set; }
        public string Hash { get; set; } = null!;
    }
}
