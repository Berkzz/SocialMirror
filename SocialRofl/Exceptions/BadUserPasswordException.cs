using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;

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

        public BadUserPasswordException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
