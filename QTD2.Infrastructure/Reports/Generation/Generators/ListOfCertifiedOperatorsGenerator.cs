using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;


namespace QTD2.Infrastructure.Reports.Generation.Generators
{
   public class ListOfCertifiedOperatorsGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEmployeeService _employeeService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public ListOfCertifiedOperatorsGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IEmployeeService employeeService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _employeeService = employeeService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ListOfCertifiedOperators.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var organizationFilter = report.Filters.FirstOrDefault(x => x.Name == "Filter by Organization" && !String.IsNullOrEmpty(x.Value));
            var organizationIDs = organizationFilter != null ? ExtractParameters<List<int>>(report.Filters, "FILTER BY ORGANIZATION") : new List<int>();

            var activeStatus = ExtractParameters<string>(report.Filters, "ALL COMPANY EMPLOYEES");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var employees = await _employeeService.GetListOfCertifiedOperatorsAsync(organizationIDs, activeStatus);

            foreach(var employee in employees)
            {
                employee.EmployeePositions = employee.EmployeePositions.Where(x => x.Active && (x.Position?.Active ?? false)).ToList();
                if (organizationIDs.Any())
                {
                    employee.EmployeeOrganizations = employee.EmployeeOrganizations.Where(m=>organizationIDs.Contains(m.OrganizationId)).ToList();
                }
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new ListOfCertifiedOperators(report.InternalReportTitle, templatePath, displayColumns, companyLogo, employees.ToList(), labelReplacement, defaultTimeZone);
        }
    }
}
