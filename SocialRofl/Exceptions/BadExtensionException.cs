namespace SocialRofl.Exceptions
{
    public class BadExtensionException : BadRequestException
    {
        public BadExtensionException()
        {
        }

        public BadExtensionException(string message)
            : base(message)
        {
        }

        public BadExtensionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
