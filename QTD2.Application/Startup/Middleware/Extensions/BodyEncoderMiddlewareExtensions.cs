using Microsoft.AspNetCore.Builder;
using QTD2.Application.Middleware;

namespace QTD2.Application.Startup.Middleware.Extensions
{
    public static class BodyEncoderMiddlewareExtensions
    {
        public static IApplicationBuilder UseBodyEncoding(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BodyEncoderMiddleware>();
        }

        public static IApplicationBuilder UseBodyDecoding(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BodyDecoderMiddleware>();
        }
    }
}
