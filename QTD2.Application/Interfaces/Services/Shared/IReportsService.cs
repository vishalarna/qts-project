using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Infrastructure.Model.Reports;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IReportsService
    {
        public Task<Report> CreateReportAsync(ReportCreateOrUpdateOptions options, bool saveToDb = true);
        public System.Threading.Tasks.Task UpdateReportAsync(int reportId, ReportCreateOrUpdateOptions options);
        public System.Threading.Tasks.Task<Report> UpdateReportLastRunAsync(int reportId, ReportCreateOrUpdateOptions options);
        public Task<List<ReportFilterOption>> GetReportFilterOptionsAsync(string filterName);
        public Task<IEnumerable<Report>> GetAllAsync();
        public Task<Report> GetAsync(int reportId);
        public Task<Report> GetAsync(int reportId, ReportCreateOrUpdateOptions options);
        public System.Threading.Tasks.Task DeleteReportsAsync(ReportDeleteOptions options);
    }
}
