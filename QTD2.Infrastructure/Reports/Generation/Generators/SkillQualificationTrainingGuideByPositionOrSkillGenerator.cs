using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class SkillQualificationTrainingGuideByPositionOrSkillGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;
        public SkillQualificationTrainingGuideByPositionOrSkillGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IEnablingObjectiveService enablingObjectiveService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          IEmployeeService employeeService, ITaskService taskService
          )
        {
            _generalSettingService = generalSettingService;
            _enablingObjectiveService = enablingObjectiveService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _employeeService = employeeService;
            _taskService = taskService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "SkillQualificationTrainingGuideByPositionOrSkill.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";

            var positionsFilter = report.Filters.FirstOrDefault(x => x.Name == "Position" && !String.IsNullOrEmpty(x.Value));
            var positionId = positionsFilter != null ? ExtractParameters<int>(report.Filters, "POSITION") : new int();

            var sqFilter = report.Filters.FirstOrDefault(x => x.Name == "Select Skill Qualification" && !String.IsNullOrEmpty(x.Value));
            var sqIds = sqFilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT SKILL QUALIFICATION") : new List<int>();

            if (positionsFilter == null && sqFilter == null) { throw new QTDServerException("Either POSITION or SKILL QUALIFICATION should not be empty", false); }
            var status = ExtractParameters<string>(report.Filters, "STATUS");
            var employeeFilter = report.Filters.FirstOrDefault(x => x.Name == "Include Employee Name and Certificate No" && !String.IsNullOrEmpty(x.Value));
            var employeeId = employeeFilter != null ? ExtractParameters<int>(report.Filters, "INCLUDE EMPLOYEE NAME AND CERTIFICATE NO") : new int();

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByPositionOrSkillAsync(positionId, sqIds, status);
            var distinctEoIds = enablingObjectives.Select(m => m.Id).Distinct().ToList();
            var allEnablingObjectivesData = await _enablingObjectiveService.GetEnablingObjectivesAllDataByEoIdAsync(distinctEoIds);

            Employee employee = null;
            if (employeeId != 0)
            {
                employee = (await _employeeService.GetWithCertifications(new List<int>() { employeeId })).FirstOrDefault();
            }
            foreach (var eo in enablingObjectives)
            {
                var eoData = allEnablingObjectivesData.FirstOrDefault(e => e.Id == eo.Id);
                if (eoData != null)
                {
                    eo.Position_SQs = eoData.Position_SQs;
                    eo.EnablingObjective_Tools = eoData.EnablingObjective_Tools;
                    eo.Procedure_EnablingObjective_Links = eoData.Procedure_EnablingObjective_Links;
                    eo.SafetyHazard_EO_Links = eoData.SafetyHazard_EO_Links;
                    eo.EnablingObjective_Suggestions = eoData.EnablingObjective_Suggestions;
                    eo.EnablingObjective_Questions = eoData.EnablingObjective_Questions;
                    eo.RegRequirement_EO_Links = eoData.RegRequirement_EO_Links;
                    eo.Task_EnablingObjective_Links = eoData.Task_EnablingObjective_Links;
                    eo.EnablingObjective_Steps = eoData.EnablingObjective_Steps;

                    if (eo.Task_EnablingObjective_Links != null)
                    {
                        var distinctTaskIds = eo.Task_EnablingObjective_Links.Select(x => x.TaskId).Distinct().ToList();
                        var tasks = await _taskService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(distinctTaskIds);

                        foreach (var eotask in eo.Task_EnablingObjective_Links)
                        {
                            eotask.Task = tasks.FirstOrDefault(m => m.Id == eotask.TaskId);
                        }
                    }
                }
            }
            if (positionId != 0)
            {
                foreach (var eo in enablingObjectives)
                {
                    eo.Position_SQs = eo.Position_SQs.Where(m => m.PositionId == positionId).ToList();
                }
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new SkillQualificationReportsModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, labelReplacement, defaultTimeZone, enablingObjectives, employee);
        }
    }
}
