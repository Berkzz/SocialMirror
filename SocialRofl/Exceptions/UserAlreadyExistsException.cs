using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;
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

        public UserAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
