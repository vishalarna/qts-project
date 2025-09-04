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
    public class StudentEvaluationFormModelGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClassSchedule_StudentEvaluations_LinkService _classSchedule_StudentEvaluations_LinkService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public StudentEvaluationFormModelGenerator(
		IClientUserSettings_GeneralSettingService generalSettingService,
        IClassSchedule_StudentEvaluations_LinkService classSchedule_StudentEvaluations_LinkService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
		{
			_generalSettingService = generalSettingService;
            _classSchedule_StudentEvaluations_LinkService = classSchedule_StudentEvaluations_LinkService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {

            string templatePath = "EvaluationFormTemplate.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var classScheduleIds = ExtractParameters<List<int>>(report.Filters, "SELECT CLASSES");
            var formIDs = ExtractParameters<List<int>>(report.Filters, "SELECT FORM");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var classScheduleStudentEvaluationsLink = await _classSchedule_StudentEvaluations_LinkService.GetLinksByClassScheduleAndEvaluationIdsAsync(classScheduleIds, formIDs);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }
            return new Models.StudentEvaluationForm(report.InternalReportTitle, templatePath, displayColumns, companyLogo, classScheduleStudentEvaluationsLink, labelReplacement, defaultTimeZone, defaultDateFormat);
        }
    }
}
