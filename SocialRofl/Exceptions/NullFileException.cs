using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;
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

        public NullFileException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
