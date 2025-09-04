using System.Threading.Tasks;
using QTD2.Data;

namespace QTD2.Infrastructure.Database.Interfaces
{
    public interface IDbContextBuilder
    {
        QTDContext BuildQtdContext(string databaseName = null, bool admin = false);
        string GetConnectionStringFromDatabase(string database);
    }
}
