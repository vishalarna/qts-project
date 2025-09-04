using QTD2.Data;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Database.Interfaces
{
    public interface IDatabaseResolver
    {
        Task<QTDContext> BuildQtdContextAsync();
        void SetConnectionString(string database);
    }
}
