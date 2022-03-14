namespace SocialRofl.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Code { get; set; }

        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
