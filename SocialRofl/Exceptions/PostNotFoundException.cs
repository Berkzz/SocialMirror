using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;
namespace SocialRofl.Exceptions
{
    public class PostNotFoundException : Exception, ILogicException
    {
        public IActionResult GetActionResult()
        {
            return ExceptionHelper.GetResult(404, "Post not found");
        }
    }
}
