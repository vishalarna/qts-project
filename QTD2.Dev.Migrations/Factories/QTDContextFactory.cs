using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using QTD2.Data;
using QTD2.Infrastructure.Database.Settings;
using QTD2.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Dev.Migrations.Factories
{
    public class QTDContextFactory : IDesignTimeDbContextFactory<QTDContext>
    {
        public QTDContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.{environment}.json", optional: false)
                    .AddEnvironmentVariables()
                .Build();

            var settingsSection = config.GetSection("DbContexts");
            var dbConfigs = new List<DbContextConfiguration>();
            settingsSection.Bind(dbConfigs);

            var conf = dbConfigs.Where(r => r.Name == Infrastructure.Database.DbContextNames.QTDAuthenticationContext).First();

            var optionsBuilder = new DbContextOptionsBuilder<QTDContext>();

            if (conf.Provider == Infrastructure.Database.SupportedProviders.SqlServer)
            {
                optionsBuilder.UseSqlServer(
                   conf.ConnectionString,
                   b => b.MigrationsAssembly("QTD2.Data")
               );
            }
            else if (conf.Provider == Infrastructure.Database.SupportedProviders.Sqlite)
            {
                optionsBuilder.UseSqlite(
                    conf.ConnectionString,
                    b => b.MigrationsAssembly("QTD2.Data.Migrations.Sqlite")
                );
            }
            else
            {
                throw new QTDServerException("Provider not found");
            }

            return new QTDContext(optionsBuilder.Options,null,null);
        }
    }
}
