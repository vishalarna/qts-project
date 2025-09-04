using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Model.TrainingProgram;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
   public class EmployeeTrainingNeedsAssessmentGenerators: ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEmployeeService _employeeService;
        private readonly ITrainingProgramService _trainingProgramService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public EmployeeTrainingNeedsAssessmentGenerators(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IEmployeeService employeeService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService, ITrainingProgramService trainingProgramService
        )
        {
            _generalSettingService = generalSettingService;
            _employeeService = employeeService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _trainingProgramService = trainingProgramService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EmployeeTrainingNeedsAssessments.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();

            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var employee = ExtractParameters<List<int>>(report.Filters, "EMPLOYEE");
            var trainingProgramId = ExtractParameters<String>(report.Filters, "SELECT TRAINING PROGRAM");
            var includeInactiveILAs = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE ILAS");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var employees = await _employeeService.GetEmployeeTrainingNeedsAssessmentAsync(employee);
            var distinctPositionIds = employees.SelectMany(m => m.EmployeePositions).Select(r => r.Position.Id).Distinct().ToList();
            var trainingPrograms = await _trainingProgramService.GetTrainingProgramsByIdAndPositionIdsAsync(distinctPositionIds, trainingProgramId);
            foreach(var emp in employees)
            {
                foreach(var ep in emp.EmployeePositions)
                {
                    ep.Position.TrainingPrograms = trainingPrograms.Where(x => x.PositionId == ep.PositionId).ToList();

                    if (!includeInactiveILAs)
                    {
                        foreach (var tp in ep.Position.TrainingPrograms)
                        {
                            tp.TrainingProgram_ILA_Links = tp.TrainingProgram_ILA_Links.Where(link => link.ILA != null && link.ILA.Active).ToList();
                        }
                    }
                }
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat ?? "MM/dd/yyyy";
            }
            return new EmployeeTrainingNeedsAssessment(report.InternalReportTitle, templatePath, displayColumns, companyLogo, employees, labelReplacement, defaultTimeZone, defaultDateFormat);
        }
    }
}

