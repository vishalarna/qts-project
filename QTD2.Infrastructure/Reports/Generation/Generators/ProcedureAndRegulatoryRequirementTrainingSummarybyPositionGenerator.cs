using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
	public class ProcedureAndRegulatoryRequirementTrainingSummarybyPositionGenerator : ReportModelGenerator
	{
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IPositionService _positionService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IProcedureService _procedureService;
        private readonly IRegulatoryRequirementService _regulatoryRequirementService;
        private readonly IClassScheduleEmployeeService _classScheduleEmployeeService;

        public ProcedureAndRegulatoryRequirementTrainingSummarybyPositionGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            IPositionService positionService,
            IClassScheduleService classScheduleService,
            IProcedureService procedureService,
            IRegulatoryRequirementService regulatoryRequirementService,
            IClassScheduleEmployeeService classScheduleEmployeeService
        )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _positionService = positionService;
            _classScheduleService = classScheduleService;
            _regulatoryRequirementService = regulatoryRequirementService;
            _procedureService = procedureService;
            _classScheduleEmployeeService = classScheduleEmployeeService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ProcedureAndRegulatoryRequirementTrainingSummarybyPosition.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var companyLogo = "";
            var defaultTimeZone = "";
            var procedures = new List<QTD2.Domain.Entities.Core.Procedure>();
            var regulatoryRequirements = new List<QTD2.Domain.Entities.Core.RegulatoryRequirement>();
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var positionIds = ExtractParameters<List<int>>(report.Filters, "SELECT POSITION");
            string allActiveInactiveOnlyEmployees = ExtractParameters<string>(report.Filters, "ACTIVE ONLY/INACTIVE ONLY/ALL EMPLOYEES");
            bool currentPositionsOnly = ExtractParameters<bool>(report.Filters, "CURRENT POSITION(S) ONLY");
            bool includeTrainees = ExtractParameters<bool>(report.Filters, "INCLUDE TRAINEES");
            var procedurefilter = report.Filters.FirstOrDefault(x => x.Name.ToUpper() == "SELECT UP TO 10 PROCEDURES" && !String.IsNullOrEmpty(x.Value));
            var procedureIds = procedurefilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT UP TO 10 PROCEDURES") : new List<int>();
            var regulatoryRequirementsfilter = report.Filters.FirstOrDefault(x => x.Name.ToUpper() == "SELECT UP TO 10 REGULATORY REQUIREMENTS" && !String.IsNullOrEmpty(x.Value));
            var regulatoryRequirementIds = regulatoryRequirementsfilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT UP TO 10 REGULATORY REQUIREMENTS") : new List<int>();
            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE");

            if (procedureIds != null && procedureIds.Count > 10)
            {
                throw new QTDServerException("You can select up to 10 Procedures only.", false);
            }
            if (regulatoryRequirementIds != null && regulatoryRequirementIds.Count > 10)
            {
                throw new QTDServerException("You can select up to 10 Regulatory Requirements only.", false);
            }

            if (procedureIds.Count > 0)
            {
                procedures = await _procedureService.GetProceduresForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(procedureIds);
            }
            if (regulatoryRequirementIds.Count > 0)
            {
                regulatoryRequirements = await _regulatoryRequirementService.GetRegulatoryRequirementsForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(regulatoryRequirementIds);
            }

            var positions = await _positionService.GetPositionsForProcedureAndRegulatoryRequirementTrainingSummarybyPosition(positionIds);
            var distinctEmployeeIds = positions.SelectMany(position => position.EmployeePositions).Select(employeePosition => employeePosition.Employee.Id).Distinct().ToList();
            var classScheduleEmployees = await _classScheduleEmployeeService.GetClassSchdulesEmployeeByEmployeeIdAsync(distinctEmployeeIds);
            var distinctClassScheduleIds = classScheduleEmployees.Select(cse => cse.ClassScheduleId).Distinct().ToList();
            var classSchedules = await _classScheduleService.GetProcedureAndRegulatoryRequirementByClassScheduleIdAsync(distinctClassScheduleIds);

            var filteredClassSchedules = classSchedules.Where(cs =>
                cs.ILA.ILA_Procedure_Links.Any(link => procedures.Any(p => p.Id == link.Procedure.Id)) ||
                cs.ILA.ILA_RegRequirement_Links.Any(link => regulatoryRequirements.Any(rr => rr.Id == link.RegulatoryRequirement.Id))
            ).ToList();

            foreach (var position in positions)
            {
                foreach (var employeePosition in position.EmployeePositions)
                {
                    var employee = employeePosition.Employee;

                    foreach (var classScheduleEmployee in employee.ClassSchedule_Employee)
                    {
                        var matchingClassSchedule = filteredClassSchedules.FirstOrDefault(cs => cs.Id == classScheduleEmployee.ClassScheduleId);

                        if (matchingClassSchedule != null)
                        {
                            classScheduleEmployee.ClassSchedule = matchingClassSchedule;
                        }
                    }
                }

                if (allActiveInactiveOnlyEmployees == "Active Only")
                {
                    position.EmployeePositions = position.EmployeePositions.Where(ep => ep.Employee.Active).ToList();
                }
                else if (allActiveInactiveOnlyEmployees == "Inactive Only")
                {
                    position.EmployeePositions = position.EmployeePositions.Where(ep => !ep.Employee.Active).ToList();
                }

                if (currentPositionsOnly)
                {
                    position.EmployeePositions = position.EmployeePositions.Where(ep => ep.Active).ToList();
                }

                if (!includeTrainees)
                {
                    position.EmployeePositions = position.EmployeePositions.Where(ep => ep.Trainee == false).ToList();
                }
                position.EmployeePositions = position.EmployeePositions
                    .Where(cse => DateOnly.FromDateTime(dateRange[0]) <= cse.StartDate && (!cse.EndDate.HasValue || cse.EndDate.Value <= DateOnly.FromDateTime(dateRange[1])))
                    .ToList();
            }

            return new ProcedureAndRegulatoryRequirementTrainingSummarybyPosition(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, positions, dateRange, procedures, regulatoryRequirements);
        }
    }
}
