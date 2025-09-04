using Microsoft.Extensions.DependencyInjection;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using QTD2.Infrastructure.Database.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QTD2.Domain.Interfaces.Service.Authentication;
using QTD2.Domain.Services.Authentication;
using QTD2.Infrastructure.JWT;

namespace QTD2.Infrastructure.Helpers.DatabaseDIHelper
{
    public static class DatabaseDIHelper
    {
        public static IServiceCollection AddQTDInstanceFetcher(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new QTD2.Infrastructure.QTDSettings.QTDSettings();
            configuration.GetSection("QTDSettings").Bind(settings);

            if (settings.UseRemote)
            {
                services.AddScoped<IInstanceFetcher, RemoteInstanceFetcherService>();
            }
            else
            {
                services.AddScoped<IInstanceFetcher, LocalInstanceFetcherService>();
            }

            return services;
        }

        public static void AddSharedHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<Infrastructure.HttpClients.ScormEngineService>();
            services.AddHttpClient<Infrastructure.HttpClients.QtdAuthenticationService>();
        }

        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Infrastructure.Identity.Interfaces.IClaimsBuilderFactory, Infrastructure.Identity.QTDClaimsBuilderFactory>();
            services.AddTransient<Infrastructure.JWT.IJWTBuilder, Infrastructure.JWT.JWTBuilder>();
        }

        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<List<DbContextConfiguration>>(options => configuration.GetSection("DbContexts").Bind(options));

            services.AddTransient<IDbContextBuilder, DefaultDbContextBuilder>();
            services.AddTransient<IDatabaseManager, DefaultDatabaseManager>();
            services.AddTransient<IDatabaseResolver, DefaultDatabaseResolver>();
        }

        public static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTSettings>(options => configuration.GetSection("Jwt").Bind(options));
            services.Configure<Infrastructure.QTD2Auth.Settings.QTDAuthSettings>(options => configuration.GetSection("QtdAuthSettings").Bind(options));
        }


            public static void AddAuthenticationDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = services.BuildServiceProvider();
            var authConfig = provider.GetService<IOptions<List<DbContextConfiguration>>>().Value.Where(r => r.Name == DbContextNames.QTDAuthenticationContext).First();

            switch (authConfig.Provider)
            {
                case SupportedProviders.Sqlite:
                    services.AddDbContext<Data.QTDAuthenticationContext>(options => options.UseSqlite(authConfig.ConnectionString, b => b.MigrationsAssembly("QTD2.Data.Migrations.Sqlite")));
                    break;
                case SupportedProviders.SqlServer:
                    services.AddDbContext<Data.QTDAuthenticationContext>(options => options.UseSqlServer(authConfig.ConnectionString, b => { b.UseCompatibilityLevel(120); b.MigrationsAssembly("QTD2.Data"); }));
                    break;
                default:
                    throw new System.Exception("Unknown provider");
            }
        }

    }
}
