namespace SocialRofl.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message)
            : base(message)
        {
        }

        public UserNotFoundException(string message, string code)
            : base(message)
        {
            Code = code;
        }

        public UserNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
