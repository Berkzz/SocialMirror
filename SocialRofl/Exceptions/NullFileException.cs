using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;
namespace SocialRofl.Exceptions
{
    public class NullFileException : Exception, ILogicException
    {
        public IActionResult GetActionResult()
        {
            return ExceptionHelper.GetResult(400, "No file provided");
        }
    }
}
