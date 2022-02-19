using CloudHub.Domain.Exceptions;
using System.Net;

namespace CloudHub.API.Commons
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
                    case UnprocessableEntityException:
                        response.StatusCode = 422;
                        break;
                    case NotAuthenticatedException:
                        response.StatusCode = 403;
                        break;
                    case ConsumedNonceException:
                        response.StatusCode = 496;
                        break;
                    case InvalidNonceException:
                        response.StatusCode = 497;
                        break;
                    case UserExistsException:
                        response.StatusCode = 494;
                        break;
                    case UserNotExistsException:
                        response.StatusCode = 495;
                        break;
                    case ExpiredTokenException:
                        response.StatusCode = 498;
                        break;
                    case MissingParameterException:
                        response.StatusCode = 423;
                        message = error.Message;
                        break;
                    case EmptyResponseException:
                        response.StatusCode = 204;
                        break;
                    default:
                        if (error.Source == "Microsoft.Extensions.DependencyInjection.Abstractions")
                        {
                            response.StatusCode = 598;
                        }
                        else
                        {
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        }
                        break;
                }
                await response.WriteAsync(message);
            }
        }
    }
}
