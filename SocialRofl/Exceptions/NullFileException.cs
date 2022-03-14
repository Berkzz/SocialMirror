namespace SocialRofl.Exceptions
{
    public class NullFileException : BadRequestException
    {
        public NullFileException()
        {
        }

        public NullFileException(string message)
            : base(message)
        {
        }

        public NullFileException(string message, string code)
            : base(message)
        {
            Code = code;
        }

        public NullFileException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
