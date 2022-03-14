namespace SocialRofl.Exceptions
{
    public class PhotoNotFoundException : NotFoundException
    {
        public PhotoNotFoundException()
        {
        }

        public PhotoNotFoundException(string message)
            : base(message)
        {
        }

        public PhotoNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
