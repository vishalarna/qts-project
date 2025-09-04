using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Extensions;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Persistence;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.CBT;
using QTD2.Infrastructure.Model.ClassSchedule_Employee;

namespace QTD2.Application.Services.Shared
{
    public class OnlineCoursesService : IOnlineCoursesService
    {
        private readonly IMainUnitOfWork _mainUow;
        private readonly IClassScheduleEmployeeService _classScheduleEmployeeService;

        public OnlineCoursesService(IMainUnitOfWork mainUow, IClassScheduleEmployeeService classScheduleEmployeeService)
        {
            _mainUow = mainUow;
            _classScheduleEmployeeService = classScheduleEmployeeService;
        }

        public async Task<PagedResult<ClassScheduleEmployee_OnlineCourseVM>> GetCompletedCoursesAsync(int employeeId, PagedQuery query)
        {

            var classScheduleEmployees = _mainUow.Repository<ClassSchedule_Employee>()
                .Query("ScormRegistrations", "ClassSchedule.ILA", "ClassSchedule.ClassSchedule_TestReleaseEMPSettings")
                .Where(i => i.EmployeeId == employeeId)
                .Where(i => i.ScormRegistrations.Any(r =>  r.Active && (r.RegistrationCompletion == CBT_ScormRegistrationCompletion.COMPLETED  || r.RegistrationSuccess == CBT_ScormRegistrationSuccess.FAILED)))
                .OrderBy(CreateOrderByExpression(query.OrderBy), query.IsDescending)
                .ApplyPaging(query.Page, query.PageSize)
                .ToArray();

            var totalItems = classScheduleEmployees.Count();
            var pagedOnlineCoursesClassScheduleEmployees = classScheduleEmployees.Select(emp => new ClassScheduleEmployee_OnlineCourseVM(
                            emp.ClassScheduleId,
                            emp.ClassSchedule != null && emp.ClassSchedule.ILA != null ? emp.ClassSchedule.ILA.Number : null,
                            emp.ClassSchedule != null && emp.ClassSchedule.ILA != null ? emp.ClassSchedule.ILA.Name : null,
                            emp.PlannedDate,
                            emp.ClassSchedule != null && emp.ClassSchedule.ClassSchedule_TestReleaseEMPSettings != null ? emp.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.UsePreTestAndTest : false,
                            emp.ClassSchedule != null && emp.ClassSchedule.ClassSchedule_TestReleaseEMPSettings != null ? emp.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTestRequired : false,
                            emp.ClassSchedule != null && emp.ClassSchedule.ILA != null && emp.ClassSchedule.ILA.Provider != null ? emp.ClassSchedule.ILA.Provider.Name : null,
                            emp.ClassSchedule != null && emp.ClassSchedule.Instructor != null ? emp.ClassSchedule.Instructor.InstructorName : null,
                            emp.ClassSchedule != null && emp.ClassSchedule.Location != null ? emp.ClassSchedule.Location.LocName : null,
                            emp.ClassSchedule != null && emp.ClassSchedule.ILA != null ? emp.ClassSchedule.ILA.TotalTrainingHours : (double?)null,
                            emp.ClassSchedule != null && emp.ClassSchedule.ILA.CBTs.FirstOrDefault() != null ?
                                emp.ClassSchedule.ILA.CBTs.FirstOrDefault().CBTLearningContractInstructions : null,
                            emp.CompletionDate,
                            emp.ScormRegistrations.Where(r => r.Active).Select(sc => new CBT_ScormRegistrationVM(
                                sc.LaunchLink,
                                sc.RegistrationSuccess,
                                sc.Score.HasValue ? sc.Score.Value : (double?)null,
                                sc.Grade
                            )).ToList()
            ));

            return PagedResult<ClassScheduleEmployee_OnlineCourseVM>.CreateSuccess(pagedOnlineCoursesClassScheduleEmployees, totalItems, query.PageSize);
        }

        public async Task<PagedResult<ClassScheduleEmployee_OnlineCourseVM>> GetPendingCoursesAsync(int employeeId, PagedQuery query)
        {

            var classScheduleEmployees = await _classScheduleEmployeeService.GetPendingOnlineCoursesForEmployeeAsync(employeeId);
            var filteredClassScheduleEmployees = await FilterClassScheduleEmployeesAsync(classScheduleEmployees.ToArray());
            var totalItems = filteredClassScheduleEmployees.Count;
            var queryableClassScheduleEmployees = filteredClassScheduleEmployees.AsQueryable();

            var pagedClassScheduleEmployees = queryableClassScheduleEmployees
                .OrderBy(CreateOrderByExpression(query.OrderBy), query.IsDescending)
                .ApplyPaging(query.Page, query.PageSize);

            var pagedOnlineCoursesClassScheduleEmployees = pagedClassScheduleEmployees.Select(emp => new ClassScheduleEmployee_OnlineCourseVM(
                emp.ClassScheduleId,
                emp.ClassSchedule != null && emp.ClassSchedule.ILA != null ? emp.ClassSchedule.ILA.Number : null,
                emp.ClassSchedule != null && emp.ClassSchedule.ILA != null ? emp.ClassSchedule.ILA.Name : null,
                emp.PlannedDate,
                emp.ClassSchedule != null && emp.ClassSchedule.ClassSchedule_TestReleaseEMPSettings != null
                    ? emp.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.UsePreTestAndTest
                    : false,
                emp.ClassSchedule != null && emp.ClassSchedule.ClassSchedule_TestReleaseEMPSettings != null
                    ? emp.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTestRequired
                    : false,
                emp.ClassSchedule != null && emp.ClassSchedule.ILA != null && emp.ClassSchedule.ILA.Provider != null
                    ? emp.ClassSchedule.ILA.Provider.Name
                    : null,
                emp.ClassSchedule != null && emp.ClassSchedule.Instructor != null
                    ? emp.ClassSchedule.Instructor.InstructorName
                    : null,
                emp.ClassSchedule != null && emp.ClassSchedule.Location != null
                    ? emp.ClassSchedule.Location.LocName
                    : null,
                emp.ClassSchedule != null && emp.ClassSchedule.ILA != null
                    ? emp.ClassSchedule.ILA.TotalTrainingHours
                    : (double?)null,
                emp.ClassSchedule != null && emp.ClassSchedule.ILA != null && emp.ClassSchedule.ILA.CBTs.FirstOrDefault() != null
                    ? emp.ClassSchedule.ILA.CBTs.FirstOrDefault().CBTLearningContractInstructions
                    : null,
                emp.CompletionDate,
                emp.ScormRegistrations.Where(r => r.Active).Select(sc => new CBT_ScormRegistrationVM(
                    sc.LaunchLink,
                    sc.RegistrationSuccess,
                    sc.Score.HasValue ? sc.Score.Value : (double?)null,
                    sc.Grade
                )).ToList()
            ));

            return PagedResult<ClassScheduleEmployee_OnlineCourseVM>.CreateSuccess(pagedOnlineCoursesClassScheduleEmployees, totalItems, query.PageSize);
        }

        public async Task<IList<ClassSchedule_Employee>> FilterClassScheduleEmployeesAsync(ClassSchedule_Employee[] classScheduleEmployees)
        {
            var classScheduleEmployeesList = new List<ClassSchedule_Employee>();

            var classScheduleIlaIds = classScheduleEmployees
                .Where(i => i.ClassSchedule.ILAID.HasValue)
                .Select(i => i.ClassSchedule.ILAID.Value);

            var cbtList = _mainUow.Repository<CBT>()
                .Query()
                .Where(i => classScheduleIlaIds.Contains(i.ILAId))
                .ToArray();

            foreach (var classScheduleEmployee in classScheduleEmployees)
            {
                if (classScheduleEmployee.CompletionDate is not null)
                {
                    continue;
                }

                var cbt = cbtList.Where(i => i.ILAId == classScheduleEmployee.ClassSchedule.ILAID).FirstOrDefault(i => i.Active);

                if (cbt is null)
                {
                    continue;
                }

                var dueDate = cbt.GetDueDate(classScheduleEmployee.ClassSchedule.EndDateTime);
                var now = DateTime.UtcNow;

                //if (cbt.DueDateInterval == CBTDueDateInterval.Days)
                //    dueDate = csEmp.ClassSchedule.EndDateTime.AddDays(cbt.DueDateAmount);

                //if (cbt.DueDateInterval == CBTDueDateInterval.Months)
                //    dueDate = csEmp.ClassSchedule.EndDateTime.AddMonths(cbt.DueDateAmount);

                if (dueDate <= now)
                {
                    continue;
                }

                classScheduleEmployee.PlannedDate = dueDate;

                if (cbt.Availablity == CBTAvailablity.OnClassEndDateTime &&
                    classScheduleEmployee.ClassSchedule.EndDateTime < now)
                {
                    classScheduleEmployeesList.Add(classScheduleEmployee);
                }

                if (cbt.Availablity == CBTAvailablity.OnClassStartDateTime &&
                    classScheduleEmployee.ClassSchedule.StartDateTime < now)
                {
                    classScheduleEmployeesList.Add(classScheduleEmployee);
                }

                if (cbt.Availablity == CBTAvailablity.AfterPretestComplete)
                {
                    var pretestClassScheduleRoster = await _mainUow.Repository<ClassSchedule_Roster>()
                        .GetAsync(i =>
                            i.EmpId == classScheduleEmployee.EmployeeId &&
                            i.ClassScheduleId == classScheduleEmployee.ClassScheduleId &&
                            i.TestTypeId == 1);

                    var settings = await _mainUow.Repository<ClassSchedule_TestReleaseEMPSetting>().GetAsync(i => i.ClassScheduleId == classScheduleEmployee.ClassScheduleId);

                    if (settings == null)
                    {
                        classScheduleEmployeesList.Add(classScheduleEmployee);
                    }
                    else if (!settings.PreTestId.HasValue || !settings.PreTestRequired)
                    {
                        classScheduleEmployeesList.Add(classScheduleEmployee);
                    }
                    else if (pretestClassScheduleRoster != null && pretestClassScheduleRoster.CompletedDate.HasValue)
                    {
                        classScheduleEmployeesList.Add(classScheduleEmployee);
                    }

                }

                if (cbt.Availablity == CBTAvailablity.AfterLearningContract)
                {
                    classScheduleEmployee.ClassSchedule.ILA.ClassSchedules = null;
                    classScheduleEmployee.Employee.ClassSchedule_Employee = null;
                    classScheduleEmployeesList.Add(classScheduleEmployee);
                }
            }

            return classScheduleEmployeesList;
        }

        private static Expression<Func<ClassSchedule_Employee, object>> CreateOrderByExpression(string propertyName)
        {
            if (propertyName.Equals("ClassSchedule.ILA.Number", StringComparison.OrdinalIgnoreCase))
            {
                return i => i.ClassSchedule.ILA.Number;
            }
            if (propertyName.Equals("ClassSchedule.ILA.Name", StringComparison.OrdinalIgnoreCase))
            {
                return i => i.ClassSchedule.ILA.Name;
            }
            if (propertyName.Equals("ScormRegistrations", StringComparison.OrdinalIgnoreCase))
            {
                return i => i.ScormRegistrations.Where(r => r.Active).FirstOrDefault().Score;
            }
            if (propertyName.Equals("PlannedDate", StringComparison.OrdinalIgnoreCase))
            {
                return i => i.PlannedDate;
            }
            if (propertyName.Equals("ScormRegistrations.Score", StringComparison.OrdinalIgnoreCase))
            {
                return i => i.ScormRegistrations.Where(r => r.Active).FirstOrDefault().Score;
            }
            if (propertyName.Equals("ScormRegistrations.Grade", StringComparison.OrdinalIgnoreCase))
            {
                return i => i.ScormRegistrations.Where(r => r.Active).FirstOrDefault().Grade;
            }
            if (propertyName.Equals("CompletionDate", StringComparison.OrdinalIgnoreCase))
            {
                return i => i.CompletionDate;
            }

            return i => i.CreatedDate;
        }
    }
}
