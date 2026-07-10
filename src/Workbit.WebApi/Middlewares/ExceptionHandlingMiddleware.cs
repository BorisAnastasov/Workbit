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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, title) = GetHttpStatusCodeAndErrors(exception);

            if (statusCode == HttpStatusCode.InternalServerError)
                logger.LogError(exception, "Unhandled exception occurred");
            else
                logger.LogWarning(exception, "Handled exception: {Title}", title);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            object response = exception switch
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
                    message = exception.Message,
                    detail = environment.IsDevelopment() ? exception.StackTrace : null
                }
            };

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, serializerOptions);

            await context.Response.WriteAsync(json);
        }

        private static (HttpStatusCode httpStatusCode, string) GetHttpStatusCodeAndErrors(Exception exception) =>
            exception switch
            {
                NotFoundException => (HttpStatusCode.NotFound, "Not Found"),
                ValidationException => (HttpStatusCode.BadRequest, "Validation Error"),
                ConflictException => (HttpStatusCode.Conflict, "Conflict"),
                UnauthorizedException => (HttpStatusCode.Unauthorized, "Unauthorized"),
                ForbiddenAccessException => (HttpStatusCode.Forbidden, "Forbidden"),
                DomainException => (HttpStatusCode.BadRequest, "Bad Request"),
                _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
            };
    }
}
