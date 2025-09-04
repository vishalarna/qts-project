using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QTD2.Data;
using QTD2.Infrastructure.Database.Interfaces;
using MediatR;
using QTD2.Domain.Exceptions;

namespace QTD2.Infrastructure.Database
{
    public class DefaultDbContextBuilder : IDbContextBuilder
    {
        private readonly List<Settings.DbContextConfiguration> _dbContexts;
        private readonly HttpContext _httpContext;
        private readonly IMediator _mediator;

        public DefaultDbContextBuilder(
            IOptions<List<Settings.DbContextConfiguration>> dbContextOptions,
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor = null
            )
        {
            _dbContexts = dbContextOptions.Value;
            _httpContext = httpContextAccessor?.HttpContext;
            _mediator = mediator;
        }

        public QTDContext BuildQtdContext(string databaseName, bool admin = false)
        {
            Settings.DbContextConfiguration selectedContext = new Settings.DbContextConfiguration();
            string connectionString = string.Empty;

            if (admin)
            {
                selectedContext = _dbContexts.Where(r => r.Name == DbContextNames.QTDContext_Admin).First();
                connectionString = selectedContext.ConnectionString.Replace("{DB}", databaseName);
            }
            else
            {
                selectedContext = _dbContexts.Where(r => r.Name == DbContextNames.QTDContext).First();
                connectionString = selectedContext.ConnectionString.Replace("{DB}", databaseName);
            }

            if (selectedContext.Provider == SupportedProviders.Sqlite)
            {
                var optionsBuilder = new DbContextOptionsBuilder<QTDContext>();

                optionsBuilder.UseLazyLoadingProxies(false).UseSqlite(connectionString, b => b.MigrationsAssembly("QTD2.Data.Migrations.Sqlite"));

                var context = new QTDContext(optionsBuilder.Options, _httpContext?.User?.Identity?.Name ?? String.Empty, _mediator);

                return context;
            }
            else if (selectedContext.Provider == SupportedProviders.SqlServer)
            {
                var optionsBuilder = new DbContextOptionsBuilder<QTDContext>();

                optionsBuilder.UseSqlServer(
                    connectionString,
                    p => p.UseCompatibilityLevel(120)
                          .EnableRetryOnFailure(
                              maxRetryCount: 5,
                              maxRetryDelay: TimeSpan.FromSeconds(50),
                              errorNumbersToAdd: null
                          ).CommandTimeout(120)
                );
                optionsBuilder.UseLazyLoadingProxies(false);
                var context = new QTDContext(optionsBuilder.Options, _httpContext?.User?.Identity?.Name ?? String.Empty, _mediator);
                return context;
            }
            else
            {
                throw new QTDServerException("Unknown Provider");
            }
        }

        public string GetConnectionStringFromDatabase(string database)
        {
            Settings.DbContextConfiguration selectedContext = _dbContexts.Where(r => r.Name == DbContextNames.QTDContext).First();
            string connectionString = selectedContext.ConnectionString.Replace("{DB}", database);

            return connectionString;
        }
    }
}
