using QTD2.Data;
using QTD2.Infrastructure.Database.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;

using Microsoft.EntityFrameworkCore;

namespace QTD2.Infrastructure.Database
{
    public class DefaultDatabaseResolver : IDatabaseResolver
    {
        private readonly IInstanceFetcher _instanceFetcher;
        private readonly HttpContext _httpContext;
        private readonly IDbContextBuilder _dbContextBuilder;
        private readonly IServiceProvider _serviceProvider;

        public DefaultDatabaseResolver(
            IInstanceFetcher instanceFetcher, 
            IDbContextBuilder dbContextBuilder,
            IServiceProvider serviceProvider,
            IHttpContextAccessor httpContextAccessor = null)
        {
            _instanceFetcher = instanceFetcher;
            _httpContext = httpContextAccessor?.HttpContext;
            _dbContextBuilder = dbContextBuilder;
            _serviceProvider = serviceProvider;
        }

        public async Task<QTDContext> BuildQtdContextAsync()
        {
            string instanceName = _httpContext?.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();

            var setting = await _instanceFetcher.GetInstanceSettingAsync(instanceName);

            if(setting == null) return _dbContextBuilder.BuildQtdContext("");

            return _dbContextBuilder.BuildQtdContext(setting.DatabaseName);
        }

        public void SetConnectionString(string database)
        {
            var connectionString = _dbContextBuilder.GetConnectionStringFromDatabase(database);
            var c = _serviceProvider.GetService(typeof(QTDContext)) as QTDContext;
            c.Database.GetDbConnection().ConnectionString = connectionString;

        }
    }
}
