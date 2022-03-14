using Microsoft.AspNetCore.Mvc;
using SocialRofl.Interfaces;

namespace SocialRofl.Exceptions
{
    public class BadAttachmentException : BadRequestException
    {
        public BadAttachmentException()
        {
        }

        public BadAttachmentException(string message)
            : base(message)
        {
        }

        public BadAttachmentException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
