using Microsoft.AspNetCore.Builder;
using QTD2.Application.Middleware;

namespace QTD2.Application.Startup.Middleware.Extensions
{
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseCustomErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
