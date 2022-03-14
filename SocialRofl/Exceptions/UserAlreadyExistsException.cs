using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;
namespace SocialRofl.Exceptions
{
    public class UserAlreadyExistsException : Exception, ILogicException
    {
        public IActionResult GetActionResult()
        {
            return ExceptionHelper.GetResult(400, "Username already taken");
        }
    }
}
