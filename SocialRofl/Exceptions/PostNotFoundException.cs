namespace SocialRofl.Exceptions
{
    public class PostNotFoundException : NotFoundException
    {
        public PostNotFoundException()
        {
        }

        public PostNotFoundException(string message)
            : base(message)
        {
        }

        public PostNotFoundException(string message, string code)
            : base(message)
        {
            Code = code;
        }

        public PostNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
