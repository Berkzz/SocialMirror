namespace SocialRofl.Models
{
    public class PostModel
    {
        public string? Text { get; set; }
        public IEnumerable<AttachmentModel> Attachments { get; set; }
    }
}
