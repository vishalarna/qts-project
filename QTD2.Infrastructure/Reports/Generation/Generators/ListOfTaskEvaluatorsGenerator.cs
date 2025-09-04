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
   public class ListOfTaskEvaluatorsGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public ListOfTaskEvaluatorsGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IEmployeeService employeeService,
        ITaskQualificationService taskQualificationService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _employeeService = employeeService;
            _taskQualificationService = taskQualificationService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ListOfTaskEvaluators.cshtml";

            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var includeTrainees = displayColumns.Where(r => r == "Show Assigned Task Qualifications").FirstOrDefault() != null;
            var evaluatorsToFilter = ExtractParameters<List<int>>(report.Filters, "EVALUATORS TO FILTER");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var evaluators = await _employeeService.GetListOfTaskEvalatorsAsync(evaluatorsToFilter);

            List<TaskQualification> taskQuals = null;

            if (Convert.ToBoolean(includeTrainees))
                taskQuals = await _taskQualificationService.GetByEvaluatorAsync(evaluatorsToFilter);


            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new ListOfTaskEvaluators(report.InternalReportTitle, templatePath, displayColumns, companyLogo, evaluators, taskQuals, labelReplacement, defaultTimeZone);
        }
    }
}
