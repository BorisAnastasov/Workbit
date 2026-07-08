using Microsoft.AspNetCore.Diagnostics;

namespace Workbit.WebApi.Extensions;

public static class ExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionHandlerMiddleware>();
}

