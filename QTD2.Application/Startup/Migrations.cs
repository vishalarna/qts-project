using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace QTD2.Application.Startup
{
    public static class Migrations
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app)
            where T : DbContext
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
            }
        }
    }
}
