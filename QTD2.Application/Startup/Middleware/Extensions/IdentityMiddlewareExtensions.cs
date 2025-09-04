using Microsoft.AspNetCore.Builder;
using QTD2.Application.Middleware;
using QTD2.Infrastructure.Identity.Interfaces;

namespace QTD2.Application.Startup.Middleware.Extensions
{
    public static class IdentityMiddlewareExtensions
    {
        public static IApplicationBuilder UseIdentityMiddleware(this IApplicationBuilder builder, IClaimsBuilder identityBuilder)
        {
            return builder.UseMiddleware<IdentityMiddleware>(identityBuilder);
        }
    }
}
