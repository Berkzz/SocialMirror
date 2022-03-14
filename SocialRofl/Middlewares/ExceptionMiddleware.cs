using SocialRofl.Exceptions;
using System.Text;

namespace SocialRofl.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private async Task WriteCode(HttpContext context, int code, string? message)
        {
            context.Response.StatusCode = code;
            if (message != null)
            {
                await context.Response.WriteAsync(message);
            }
        }

        private async Task ProcessException(HttpContext context, Exception exception)
        {
            if(exception is NotFoundException notFound)
            {
                await WriteCode(context, 404, notFound.Message);
                return;
            }
            if (exception is BadRequestException badRequest)
            {
                await WriteCode(context, 400, badRequest.Message);
                return;
            }
            if (exception is NonAuthorizedException nonAuth)
            {
                await WriteCode(context, 401, nonAuth.Message);
                return;
            }
            await WriteCode(context, 500, null);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await ProcessException(context, ex);
            }
        }
    }
}
