using Doctrina.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Routing
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<ApiExceptionMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                if (ex is ValidationException)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteJsonAsync(new { failures = ((ValidationException)ex).Failures });
                    return;
                }
                else if (ex is BadRequestException
                    || ex is IOException /* Invalid formattet HTTP requests */
                    || ex is InvalidDataException /* Form section has invalid Content-Disposition value */)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteJsonAsync(new { message = ex.Message });
                    return;
                }
                else if (ex is NotFoundException)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.Response.WriteJsonAsync(new { message = ex.Message });
                    return;
                }
                else
                {
                    logger.LogError(ex, "Exception was thrown");

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteJsonAsync(new
                    {
                        error = new[] { ex.InnerException?.Message ?? ex.Message },
                        type = ex.GetType().Name,
                        stackTrace = ex.StackTrace
                    });
                }

                return;
            }

            // We cannot modify response headers after
        }
    }
}
