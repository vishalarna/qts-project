using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using System.Text.RegularExpressions;
using QTD2.Infrastructure.Authorization.Operations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.Reports;

using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Application.Services.Shared
{
    public class ReportGeneratorService : IReportGeneratorService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ReportService> _localizer;
        private readonly Domain.Interfaces.Service.Core.IReportService _reportService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        private readonly IReportExporterFactory _reportExporterFactory;

        private readonly IReportGenerator _reportGenerator;

        public ReportGeneratorService(
            Domain.Interfaces.Service.Core.IReportService reportService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<ReportService> localizer,
            UserManager<AppUser> userManager,
            IReportGenerator reportGenerator,
            IReportExporterFactory reportExporterFactory)
        {
            _reportService = reportService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _reportGenerator = reportGenerator;
            _reportExporterFactory = reportExporterFactory;
        }
        public async Task<string> GenerateReportAsync(Report report)
        {
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, report, ReportOperations.Generate);
            if (result.Succeeded)
            {
                string content = await _reportGenerator.GenerateReport(report);
                return content;
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<string> ExportReportAsync(Infrastructure.Reports.Export.ReportExportType exportType, Report report)
        {
            var content = await GenerateReportAsync(report);

            var exporter = _reportExporterFactory.GetExporter(exportType);
            string internalReportTitle = Regex.Replace(report.InternalReportTitle, "[^a-zA-Z0-9]", "");

            string filename = $"{internalReportTitle} {DateTime.Now.ToString("MMddyyyy")}";
            var file = exporter.ExportReportToFile(filename, content);

            return file;
        }
    }
}
