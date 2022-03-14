using Microsoft.AspNetCore.Mvc;

namespace SocialRofl.Exceptions
{
    public static class ExceptionHelper
    {
        public static IActionResult GetResult(int code, string? message)
        {
            return new ContentResult
            {
                StatusCode = code,
                Content = message,
                ContentType = "text/plain"
            };
        }
    }
}
