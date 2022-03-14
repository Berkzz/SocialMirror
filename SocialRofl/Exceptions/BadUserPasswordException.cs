namespace SocialRofl.Exceptions
{
    public class BadUserPasswordException : BadRequestException
    {
        public BadUserPasswordException()
        {
        }

        public BadUserPasswordException(string message)
            : base(message)
        {
        }

        public BadUserPasswordException(string message, string code)
            : base(message)
        {
            Code = code;
        }

        public BadUserPasswordException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
