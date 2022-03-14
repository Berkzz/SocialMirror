using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;

namespace SocialRofl.Exceptions
{
    public class BadUserException : NonAuthorizedException
    {
        public BadUserException()
        {
        }

        public BadUserException(string message)
            : base(message)
        {
        }

        public BadUserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
