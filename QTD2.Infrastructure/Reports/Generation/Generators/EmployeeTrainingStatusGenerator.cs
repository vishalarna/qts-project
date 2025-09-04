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
    public class EmployeeTrainingStatusGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEmployeeService _employeeService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IILAService _iLAService;
        private readonly IClassScheduleEmployee_ILACertificationLink_PartialCreditService _classScheduleEmployee_ILACertificationLink_PartialCreditService;

        public EmployeeTrainingStatusGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IEmployeeService employeeService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
        IILAService iLAService, IClassScheduleEmployee_ILACertificationLink_PartialCreditService classScheduleEmployee_ILACertificationLink_PartialCreditService
        )
        {
            _generalSettingService = generalSettingService;
            _employeeService = employeeService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _iLAService = iLAService;
            _classScheduleEmployee_ILACertificationLink_PartialCreditService = classScheduleEmployee_ILACertificationLink_PartialCreditService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EmployeeTrainingStatus.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            List<Employee> employees = new List<Employee>();
                  
            var employeeIds = ExtractParameters<List<int>>(report.Filters, "Employee");
            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            employees = await _employeeService.GetEmployeeTrainingStatusAsync(employeeIds, dateRange[0], dateRange[1]);
            var uniqueIlaIds = employees.SelectMany(employee => employee.ClassSchedule_Employee).Select(cse => cse.ClassSchedule.ILAID).Where(id => id.HasValue).Select(id => id.Value).Distinct().ToList();

            var ilas = await _iLAService.GetILAsWithCertificationInformationAsync(uniqueIlaIds);
            var ilaDictionary = ilas.ToDictionary(ila => ila.Id);

            var distinctClassEmpIds = employees.SelectMany(m => m.ClassSchedule_Employee).Select(o => o.Id).Distinct().ToList();
            var clsEmpILACertificationPartialCredits = await _classScheduleEmployee_ILACertificationLink_PartialCreditService.GetByClassScheduleEmployeeIdsAsync(distinctClassEmpIds);
            var partialCreditsLookup = clsEmpILACertificationPartialCredits.GroupBy(p => p.ClassScheduleEmployeeId).ToDictionary(g => g.Key, g => g.ToList());

            foreach (var employee in employees)
            {
                foreach (var classScheduleEmployee in employee.ClassSchedule_Employee)
                {
                    if (partialCreditsLookup.TryGetValue(classScheduleEmployee.Id, out var partialCredits))
                    {
                        classScheduleEmployee.ClassScheduleEmployee_ILACertificationLink_PartialCredits = partialCredits;
                    }
                    var classSchedule = classScheduleEmployee.ClassSchedule;

                    if (classSchedule.ILAID.HasValue && ilaDictionary.TryGetValue(classSchedule.ILAID.Value, out var matchingIla))
                    {
                        classSchedule.ILA = matchingIla;
                    }
                }
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new EmployeeTrainingStatusModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, employees, dateRange, labelReplacement, defaultTimeZone);
        }
    }
}
