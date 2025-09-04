using QTD2.Domain.Entities.Core;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Interfaces
{
    public interface IReportModelFactory
    {
        Task<IReportModel> GenerateModelAsync(Report report);
    }
}
