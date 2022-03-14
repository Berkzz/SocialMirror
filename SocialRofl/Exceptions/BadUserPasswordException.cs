using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;

namespace SocialRofl.Exceptions
{
    public class BadUserPasswordException : Exception, ILogicException
    {
        public IActionResult GetActionResult()
        {
            return ExceptionHelper.GetResult(400, "Username and password doesn't match");
        }
    }
}
