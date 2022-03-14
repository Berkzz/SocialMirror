namespace SocialRofl.Exceptions
{
    public class BadAttachmentException : BadRequestException
    {
        public BadAttachmentException()
        {
        }

        public BadAttachmentException(string message)
            : base(message)
        {
        }

        public BadAttachmentException(string message, string code)
            : base(message)
        {
            Code = code;
        }

        public BadAttachmentException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
