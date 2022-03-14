using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;
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

        public PostNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
