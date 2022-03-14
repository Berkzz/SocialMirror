using SocialRofl.Exceptions;
using SocialRofl.Models;

namespace SocialRofl.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private async Task WriteCode(HttpContext context, int code, string? message, string? codeString)
        {
            context.Response.StatusCode = code;
            await context.Response.WriteAsJsonAsync<ErrorModel>(new ErrorModel
            {
                Code = codeString ?? "ANOTHER_ERROR",
                Message = message ?? ""
            });
        }

        private async Task ProcessException(HttpContext context, Exception exception)
        {
            if (exception is NotFoundException notFound)
            {
                await WriteCode(context, 404, notFound.Message, notFound.Code);
                return;
            }
            if (exception is BadRequestException badRequest)
            {
                await WriteCode(context, 400, badRequest.Message, badRequest.Code);
                return;
            }
            if (exception is NonAuthorizedException nonAuth)
            {
                await WriteCode(context, 401, nonAuth.Message, nonAuth.Code);
                return;
            }
            await WriteCode(context, 500, null, null);
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
