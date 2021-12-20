using CloudHub.Domain.Exceptions;
using System.Net;

namespace CloudHub.API.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                string message = string.Empty;
                switch (error)
                {
                    case EntityNotFoundException:
                        response.StatusCode = 422;
                        break;
                    case NotAuthenticatedException:
                        response.StatusCode = 403;
                        break;
                    case ConsumedNonceException:
                        response.StatusCode =  496;
                        break;
                    case InvalidNonceException:
                        response.StatusCode =  497;
                        break;
                    case UserExistsException:
                        response.StatusCode = 494;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = "Unhandled Error";
                        break;
                }
                await response.WriteAsync(message);
            }
        }
    }
}
