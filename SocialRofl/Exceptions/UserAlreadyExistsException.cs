namespace SocialRofl.Exceptions
{
    public class UserAlreadyExistsException : BadRequestException
    {
        public UserAlreadyExistsException()
        {
        }

        public UserAlreadyExistsException(string message)
            : base(message)
        {
        }

        public UserAlreadyExistsException(string message, string code)
            : base(message)
        {
            Code = code;
        }

        public UserAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
