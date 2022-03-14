namespace SocialRofl.Exceptions
{
    public class NonAuthorizedException : Exception
    {
        public NonAuthorizedException()
        {
        }

        public NonAuthorizedException(string message)
            : base(message)
        {
        }

        public NonAuthorizedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
