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
    public class TrainingProgramCompletionHistoryGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly IIDPScheduleService _idpScheduleService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IILAService _iLAService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IEmployeeService _employeeService;

        public TrainingProgramCompletionHistoryGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
         IPositionService positionService,
         IIDPScheduleService idpScheduleService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
         IILAService iLAService,
         IClassScheduleService classScheduleService, IEmployeeService employeeService
         )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _idpScheduleService = idpScheduleService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _iLAService = iLAService;
            _classScheduleService = classScheduleService;
            _employeeService = employeeService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TrainingProgramCompletionHistory.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var trainingProgramId = ExtractParameters<List<int>>(report.Filters, "SELECT TRAINING PROGRAM");
            var dateRangefilter = report.Filters.FirstOrDefault(x => x.Name == "Date Range" && !String.IsNullOrEmpty(x.Value));
            var dateRange = dateRangefilter != null ? ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE") : new List<DateTime>();
            var includeInActiveIla = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE ILAS");
            var activeInactiveEmployees = ExtractParameters<string>(report.Filters, "ALL/ACTIVE/INACTIVE EMPLOYEES");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var idpSchedules = await _idpScheduleService.GetForTrainingProgramCompletionReportAsync(trainingProgramId, dateRange, includeInActiveIla,activeInactiveEmployees);
            var distinctClassScheduleIds = idpSchedules.Select(schedule => schedule.ClassScheduleId).Distinct().ToList();
            var classSchedules = await _classScheduleService.GetClassSchedulesForTrainingProgramAsync(distinctClassScheduleIds);
            var distinctEmpIds = idpSchedules.Select(i => i.IDP.EmployeeId).Distinct().ToList();
            var employees = await _employeeService.GetEmployeesWithPersonPositionCertificationsAsync(distinctEmpIds);

            foreach (var idpSchedule in idpSchedules)
            {
                var matchingClassSchedule = classSchedules.FirstOrDefault(cs => cs.Id == idpSchedule.ClassScheduleId);
                var matchingEmployee = employees.FirstOrDefault(e => e.Id == idpSchedule.IDP.EmployeeId);
                if (matchingClassSchedule != null)
                {
                    idpSchedule.ClassSchedule = matchingClassSchedule;
                }
                if(matchingEmployee != null)
                {
                    idpSchedule.IDP.Employee = matchingEmployee;
                }
            }
            var positions = await _positionService.TrainingProgramCompletionHistoryAsync(trainingProgramId, dateRange, includeInActiveIla);
            var distinctIlaIds = positions.SelectMany(position => position.TrainingPrograms).SelectMany(tp => tp.TrainingProgram_ILA_Links).Where(link => link.ILA != null).Select(link => link.ILA.Id).Distinct().ToList();
            var ilas = await _iLAService.GetILAsWithCertificationInformationAsync(distinctIlaIds);

            var updatedPositions = positions.Select(position =>
            {
                foreach (var trainingProgram in position.TrainingPrograms)
                {
                    trainingProgram.TrainingProgram_ILA_Links = trainingProgram.TrainingProgram_ILA_Links
                        .Select(link =>
                        {
                            var updatedIla = ilas.FirstOrDefault(ila => ila.Id == link.ILA.Id);
                            if (updatedIla != null)
                            {
                                link.ILA = updatedIla;
                            }
                            return link;
                        })
                        .ToList();
                }
                return position;
            }).ToList();

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TrainingProgramCompletionHistory(report.InternalReportTitle, templatePath, displayColumns, companyLogo, dateRange, positions, idpSchedules, labelReplacement, defaultTimeZone);
        }

    }
}
