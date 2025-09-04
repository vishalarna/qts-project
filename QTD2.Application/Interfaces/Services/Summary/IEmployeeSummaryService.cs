using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Model.Employee;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEmployeeSummaryService
    {
        public Task<List<EmployeeSummaryDTO>> GetEmployeeLists(string name);
        public Task<EmpDashboardStatistics> GetEmpDashboardStatisticsAsync();
    }
}
