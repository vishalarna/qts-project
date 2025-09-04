using QTD2.Infrastructure.Model.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IReportGeneratorService
    {
        public Task<string> GenerateReportAsync(Report report);
        public Task<string> ExportReportAsync(Infrastructure.Reports.Export.ReportExportType exportType, Report report);
    }
}
