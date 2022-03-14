using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;

namespace SocialRofl.Exceptions
{
    public class BadAttachmentException : Exception, ILogicException
    {
        public IActionResult GetActionResult()
        {
            return ExceptionHelper.GetResult(400, "Bad attachment");
        }
    }
}
