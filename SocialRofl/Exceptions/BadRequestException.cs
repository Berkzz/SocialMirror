namespace SocialRofl.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Code { get; set; }

        public BadRequestException()
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string message, string code)
            : base(message)
        {
            Code = code;
        }

        public BadRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
