using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;

namespace SocialRofl.Exceptions
{
    public class BadExtensionException : Exception, ILogicException
    {
        private string[] _exts;

        public BadExtensionException(string[] exts)
        {
            _exts = exts;
        }

        public IActionResult GetActionResult()
        {
            return ExceptionHelper.GetResult(400, $"Bad extension. Allowed: {string.Join(", ", _exts)}");
        }
    }
}
