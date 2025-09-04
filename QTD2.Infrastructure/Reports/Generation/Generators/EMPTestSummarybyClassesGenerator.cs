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
	public class EMPTestSummarybyClassesGenerator : ReportModelGenerator
	{
		private readonly IClassScheduleService _classScheduleService;
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IEmployeeService _employeeService;
        private readonly IILAService _iLAService;
        private readonly ITestService _testService;
        private readonly IClassSchedule_Roster_ResponseService _classSchedule_Roster_ResponseService;

        public EMPTestSummarybyClassesGenerator(IClassScheduleService classScheduleService, IClientUserSettings_GeneralSettingService generalSettingService, IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService, IEmployeeService employeeService, ITestService testService, IClassSchedule_Roster_ResponseService classSchedule_Roster_ResponseService, IILAService iLAService)
		{
			_classScheduleService = classScheduleService;
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _employeeService = employeeService;
            _testService = testService;
            _classSchedule_Roster_ResponseService = classSchedule_Roster_ResponseService;
            _iLAService = iLAService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EMPTestSummarybyClasses.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            List<int> classScheduleIds = ExtractParameters<List<int>>(report.Filters, "SELECT CLASS");
            List<int> testIds = ExtractParameters<List<int>>(report.Filters, "SELECT TEST TYPE");
            var employeeFilter = report.Filters.FirstOrDefault(x => x.Name == "Select Employees" && !String.IsNullOrEmpty(x.Value));
            var employeeIds = employeeFilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEES") : new List<int>();
            var includeTestAnswerDetails = ExtractParameters<bool>(report.Filters, "INCLUDE TEST ANSWER DETAILS");
            var showOnlyFailedGrades = ExtractParameters<bool>(report.Filters, "SHOW ONLY FAILED GRADES");
            var classSchedules = await _classScheduleService.GetClassScheduleForEMPTestSummarybyClasses(classScheduleIds, testIds, employeeIds, showOnlyFailedGrades);
            var distinctILAIds = classSchedules.Select(cs=>cs.ILAID.Value).Distinct().ToArray();
            var ilas = await _iLAService.GetByListOfIdsAsync(distinctILAIds);
            classSchedules.ForEach(cs => cs.ILA = ilas.FirstOrDefault(i => i.Id == cs.ILAID));
            var empIds = classSchedules.SelectMany(cs => cs.ClassSchedule_Rosters).Select(r => r.EmpId).Distinct().ToList();
            var employees = await _employeeService.GetEmployeesPersonDetailsByEmpIds(empIds);
            classSchedules.ForEach(cs => cs.ClassSchedule_Rosters.ToList().ForEach(cr => cr.Employee = employees.FirstOrDefault(emp => emp.Id == cr.EmpId)));
            var distinctTestIds = classSchedules.SelectMany(cs => cs.ClassSchedule_Rosters).Select(cr => cr.TestId).Distinct().ToList();
            var tests = await _testService.GetForPretestAndFinalTestComparison(distinctTestIds);
            classSchedules.ForEach(cs => cs.ClassSchedule_Rosters.ToList().ForEach(cr => tests.FirstOrDefault(t => t.Id == cr.TestId)));
            var distinctRosterIds = classSchedules.SelectMany(cs => cs.ClassSchedule_Rosters).Select(cr => cr.Id).Distinct().ToList();
            var rosterResponses = await _classSchedule_Roster_ResponseService.GetWithSelectionsByClassScheduleRosterIdsAsync(distinctRosterIds);
            classSchedules.ForEach(cs => cs.ClassSchedule_Rosters.ToList().ForEach(cr => cr.Responses = rosterResponses.Where(rr => rr.ClassScheduleRosterId == cr.Id).ToList()));
            return new EMPTestSummarybyClasses(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement,classSchedules, includeTestAnswerDetails);
        }
    }
}
