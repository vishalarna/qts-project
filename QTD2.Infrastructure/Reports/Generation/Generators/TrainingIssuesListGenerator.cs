using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class TrainingIssuesListGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ITrainingIssueService _trainingIssueService;

        public TrainingIssuesListGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IPositionService positionService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
        ITrainingIssueService trainingIssueService
        )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _trainingIssueService = trainingIssueService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TrainingIssuesList.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var dataelementIndexIds = ExtractParameters<List<int>>(report.Filters, "SELECT TRAINING COMPONENT");
            var dataelements = getDataElementDisplayTypes(dataelementIndexIds);
            var severityLevelIds = ExtractParameters<List<int>>(report.Filters, "SEVERITY LEVEL");
            var status = ExtractParameters<string>(report.Filters, "OPEN/CLOSED STATUS");
            var trainingIssues = await _trainingIssueService.GetTrainingIssuesByStatusAndSeverityIdAsync(dataelements, severityLevelIds, status);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TrainingIssuesListModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, trainingIssues);
        }

        private List<string> getDataElementDisplayTypes(List<int> dataelementIndexIds)
        {
            List<string> DataElements = new List<string>();

            foreach (var index in dataelementIndexIds)
            {
                switch (index)
                {
                    case 1:
                        DataElements.Add("EnablingObjective");
                        break;
                    case 2:
                        DataElements.Add("MetaEnablingObjective");
                        break;
                    case 3:
                        DataElements.Add("MetaTask");
                        break;
                    case 4:
                        DataElements.Add("Procedure");
                        break;
                    case 5:
                        DataElements.Add("RegulatoryRequirement");
                        break;
                    case 6:
                        DataElements.Add("SafetyHazard");
                        break;
                    case 7:
                        DataElements.Add("Task");
                        break;
                    case 8:
                        DataElements.Add("Tool");
                        break;
                    case 9:
                        DataElements.Add("TrainingProgram");
                        break;
                    case 10:
                        DataElements.Add("ILAsCourses");
                        break;
                    case 11:
                        DataElements.Add("MetaILAsCourses");
                        break;
                    case 12:
                        DataElements.Add("ComputerBasedTraining");
                        break;
                    case 13:
                        DataElements.Add("TestItem");
                        break;
                    case 14:
                        DataElements.Add("Pretest");
                        break;
                    case 15:
                        DataElements.Add("Test");
                        break;
                }
            }

            return DataElements;
        }

    }
}
