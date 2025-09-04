using System.Threading.Tasks;

namespace QTD2.Infrastructure.Database.Interfaces
{
    public interface IDatabaseManager
    {
        Task<string> MigrateDatabaseAsync(string databaseName);
    }
}
