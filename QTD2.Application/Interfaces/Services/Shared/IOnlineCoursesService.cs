using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.ClassSchedule_Employee;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IOnlineCoursesService
    {
        Task<PagedResult<ClassScheduleEmployee_OnlineCourseVM>> GetCompletedCoursesAsync(int employeeId, PagedQuery query);
        Task<PagedResult<ClassScheduleEmployee_OnlineCourseVM>> GetPendingCoursesAsync(int employeeId, PagedQuery query);
        Task<IList<ClassSchedule_Employee>> FilterClassScheduleEmployeesAsync(ClassSchedule_Employee[] classScheduleEmployees);
    }
}
