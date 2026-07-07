using System.Net;
using System.Text.Json;
using Workbit.Application.Exceptions;

namespace Workbit.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (statusCode, title) = ex switch
            {
                NotFoundException => (HttpStatusCode.NotFound, "Not Found"),
                ValidationException => (HttpStatusCode.BadRequest, "Validation Error"),
                ConflictException => (HttpStatusCode.Conflict, "Conflict"),
                UnauthorizedException => (HttpStatusCode.Unauthorized, "Unauthorized"),
                ForbiddenAccessException => (HttpStatusCode.Forbidden, "Forbidden"),
                DomainException => (HttpStatusCode.BadRequest, "Bad Request"),
                _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
            };

            if (statusCode == HttpStatusCode.InternalServerError)
                logger.LogError(ex, "Unhandled exception occurred");
            else
                logger.LogWarning(ex, "Handled exception: {Title}", title);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            object response = ex switch
            {
                ValidationException validationEx => new
                {
                    title,
                    status = (int)statusCode,
                    errors = validationEx.Errors
                },
                _ => new
                {
                    title,
                    status = (int)statusCode,
                    message = ex.Message,
                    detail = environment.IsDevelopment() ? ex.StackTrace : null
                }
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}
