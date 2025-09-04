using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.HttpClients;
using QTD2.Data;
using QTD2.Infrastructure.Database;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using QTD2.Domain.Entities.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;
using QTD2.Infrastructure.Helpers.DatabaseDIHelper;
namespace QTD2.Migrator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                if (args.Length == 0)
                {
                    logger.LogError("No command specified. Use 'RunAuth' or 'RunQtd'");
                    return;
                }

                var command = args[0].ToLowerInvariant();

                switch (command)
                {
                    case "runauth":
                        await RunAuthMigration(services, logger);
                        break;

                    case "runqtd":
                        await RunQtdMigrations(services, logger);
                        break;

                    case "runall":
                        logger.LogInformation("Running both Auth and QTD migrations...");
                        await RunAuthMigration(services, logger);
                        await RunQtdMigrations(services, logger);
                        logger.LogInformation("All migrations completed.");
                        break;

                    default:
                        logger.LogError($"Unknown command: {command}. Use 'RunAuth' or 'RunQtd'");
                        break;
                }
            }

            await host.RunAsync();
        }

        private static async Task RunAuthMigration(IServiceProvider services, ILogger logger)
        {
            try
            {
                var authContext = services.GetRequiredService<QTDAuthenticationContext>();
                logger.LogInformation("Starting QTDAuthDatabaseSetup migration...");
                await authContext.Database.MigrateAsync();
                logger.LogInformation("QTDAuthDatabaseSetup migration complete.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "QTDAuthDatabaseSetup migration failed.");
            }
        }

        private static async Task RunQtdMigrations(IServiceProvider services, ILogger logger)
        {
            try
            {
                var dbManager = services.GetRequiredService<IDatabaseManager>();
                var instanceFetcher = services.GetRequiredService<IInstanceFetcher>();
                var instances = await instanceFetcher.GetAllActiveInstancesAsync();

                foreach (var instance in instances)
                {
                    try
                    {
                        logger.LogInformation($"QTD2Database Set up for {instance.DatabaseName} beginning");
                        await dbManager.MigrateDatabaseAsync(instance.DatabaseName);
                        logger.LogInformation($"QTD2Database Set up for {instance.DatabaseName} completed");
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e, $"QTD2Database Set up for {instance.DatabaseName} failed");
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "QTD2DatabaseSetup failed.");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(config =>
                {
                    config.AddCommandLine(args);
                    config.AddEnvironmentVariables();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json")
                          .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true)
                          .AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;

                    services.AddSharedHttpClients(configuration);
                    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
                    services.AddHttpContextAccessor();
                    services.AddQTDInstanceFetcher(configuration);
                    services.AddAuthServices(configuration);
                    services.AddDatabaseServices(configuration);
                    services.AddAuthenticationDatabase(configuration);
                    services.RegisterSettings(configuration);
                    services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<QTDAuthenticationContext>().AddDefaultTokenProviders();
                    services.AddLocalization();
                });
    }

}