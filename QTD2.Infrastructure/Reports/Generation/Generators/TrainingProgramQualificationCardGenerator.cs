



using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class TrainingProgramQualificationCardGenerator : ReportModelGenerator
    {

        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;

        public TrainingProgramQualificationCardGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IPositionService positionService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          ITaskService taskService,
          IEmployeeService employeeService
          )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _taskService = taskService;
            _employeeService = employeeService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TrainingProgramQualificationCard.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            string employeeFullName = "";

            var positionIds = ExtractParameters<List<int>>(report.Filters, "SELECT POSITION");
            var trainingProgramId = ExtractParameters<string>(report.Filters, "TRAINING PROGRAM");
            var employeeId = ExtractParameters<string>(report.Filters, "INCLUDE EMPLOYEE NAME");
            var includeMetaTasks = ExtractParameters<bool>(report.Filters, "INCLUDE META TASKS");
            var includePseudoTasks = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var positions = await _positionService.GetPositionsByIdAsync(positionIds, trainingProgramId);
            var distinctTaskIdsFromILA = positions.SelectMany(p => p.TrainingPrograms).SelectMany(tp => tp.TrainingProgram_ILA_Links).Where(link => link.ILA != null).SelectMany(link => link.ILA.ILA_TaskObjective_Links).Select(ilaTaskLink => ilaTaskLink.TaskId).Distinct().ToList();
            var tasks = await _taskService.GetTasksByIdAsync(distinctTaskIdsFromILA, includeMetaTasks, includePseudoTasks);
            var employee = await _employeeService.GetWithPersonAsync(int.Parse(employeeId));

            foreach (var position in positions)
            {
                foreach (var tpLink in position.TrainingPrograms.SelectMany(tp => tp.TrainingProgram_ILA_Links))
                {
                    if (tpLink.ILA != null)
                    {
                        foreach (var ilaTaskObjectiveLink in tpLink.ILA.ILA_TaskObjective_Links ?? new List<ILA_TaskObjective_Link>())
                        {
                            var task = tasks.FirstOrDefault(t => t.Id == ilaTaskObjectiveLink.TaskId);
                            if (task != null)
                            {
                                ilaTaskObjectiveLink.Task = task;
                            }
                        }
                    }
                }
            }

            if (!includeMetaTasks)
            {
                foreach (var position in positions)
                {
                    foreach (var tpLink in position.TrainingPrograms.SelectMany(tp => tp.TrainingProgram_ILA_Links))
                    {
                        if (tpLink.ILA != null)
                        {
                            foreach (var ilaLink in tpLink.ILA.ILA_TaskObjective_Links)
                            {
                                if (ilaLink.Task?.IsMeta == true)
                                {
                                    ilaLink.Task = null;
                                }
                            }
                        }
                    }
                }
            }

            if (!includePseudoTasks)
            {
                foreach (var position in positions)
                {
                    foreach (var tpLink in position.TrainingPrograms.SelectMany(tp => tp.TrainingProgram_ILA_Links))
                    {
                        if (tpLink.ILA != null)
                        {
                            foreach (var ilaLink in tpLink.ILA.ILA_TaskObjective_Links ?? new List<ILA_TaskObjective_Link>())
                            {
                                if (ilaLink.Task?.SubdutyArea?.DutyArea?.Letter == "P")
                                {
                                    ilaLink.Task = null;
                                }
                            }
                        }
                    }
                }
            }
            if (employee?.Person != null)
            {
                var person = employee.Person;
                employeeFullName = $"{person.LastName}, {person.FirstName}";
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TrainingProgramQualificationCardModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positions, labelReplacement, defaultTimeZone, employeeFullName);
        }
    }
}
