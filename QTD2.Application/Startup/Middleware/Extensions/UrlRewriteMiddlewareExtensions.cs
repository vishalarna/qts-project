using Microsoft.AspNetCore.Builder;
using QTD2.Application.Middleware;

namespace QTD2.Application.Startup.Middleware.Extensions
{
    public static class UrlRewriteMiddlewareExtensions
    {
        public static IApplicationBuilder UseUrlRewrite(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UrlRewriteMiddleware>();
        }
    }
}
