using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Data.Initialization;
using QTD2.Infrastructure.Database.Interfaces;

namespace QTD2.Infrastructure.Database
{
    public class DefaultDatabaseManager : IDatabaseManager
    {
        private readonly IDbContextBuilder _dbContextBuilder;

        public DefaultDatabaseManager(
            IDbContextBuilder dbContextBuilder)
        {
            _dbContextBuilder = dbContextBuilder;
        }

        public async Task<string> MigrateDatabaseAsync(string databaseName)
        {
            var context = _dbContextBuilder.BuildQtdContext(databaseName, true);
            context.Database.Migrate();
            return databaseName;
        }
    }
}
