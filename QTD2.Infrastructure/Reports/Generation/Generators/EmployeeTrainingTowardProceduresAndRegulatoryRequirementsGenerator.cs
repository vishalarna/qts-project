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
	public class EmployeeTrainingTowardProceduresAndRegulatoryRequirementsGenerator : ReportModelGenerator
	{
		private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
		private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IProcedureService _procedureService;
        private readonly IRegulatoryRequirementService _regulatoryRequirementService;
        private readonly IEmployeeService _employeeService;

        public EmployeeTrainingTowardProceduresAndRegulatoryRequirementsGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            IProcedureService procedureService,
            IRegulatoryRequirementService regulatoryRequirementService,
            IEmployeeService employeeService
        )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _procedureService = procedureService;
            _regulatoryRequirementService = regulatoryRequirementService;
            _employeeService = employeeService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EmployeeTrainingTowardProceduresAndRegulatoryRequirements.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var companyLogo = "";
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var procedures = new List<QTD2.Domain.Entities.Core.Procedure>();
            var regulatoryRequirements = new List<QTD2.Domain.Entities.Core.RegulatoryRequirement>();
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat ?? "MM/dd/yyyy";
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var employeeIds = ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEE");
            var procedurefilter = report.Filters.FirstOrDefault(x => x.Name.ToUpper() == "SELECT PROCEDURES" && !String.IsNullOrEmpty(x.Value));
            var procedureIds = procedurefilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT PROCEDURES") : new List<int>();
            var regulatoryRequirementsfilter = report.Filters.FirstOrDefault(x => x.Name.ToUpper() == "SELECT REGULATORY REQUIREMENTS" && !String.IsNullOrEmpty(x.Value));
            var regulatoryRequirementIds = regulatoryRequirementsfilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT REGULATORY REQUIREMENTS") : new List<int>();
            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE");

            if (procedureIds.Count > 0)
            {
                 procedures = await _procedureService.GetProceduresForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(procedureIds);
            }
            if (regulatoryRequirementIds.Count > 0)
            {
                regulatoryRequirements = await _regulatoryRequirementService.GetRegulatoryRequirementsForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(regulatoryRequirementIds);
            }
            
            var employees = await _employeeService.GetEmployeesForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(employeeIds);

			foreach (var employee in employees)
			{
				employee.ClassSchedule_Employee = employee.ClassSchedule_Employee.Where(cse => cse.CompletionDate != null && dateRange[0] <= cse.CompletionDate.Value.Date && cse.CompletionDate.Value.Date <= dateRange[1]).ToList();
			}

			return new EmployeeTrainingTowardProceduresAndRegulatoryRequirements(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, procedures, regulatoryRequirements, employees, defaultDateFormat, dateRange);
        }
    }
}
