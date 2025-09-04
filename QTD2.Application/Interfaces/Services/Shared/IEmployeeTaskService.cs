using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.Timesheet;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEmployeeTaskService
    {
        public Task<List<Employee_Task>> GetAsync(int taskId, int version);

        public Task<Employee_Task> GetAsync(int taskId, int version, int employee);

        public Task<List<Employee_Task>> CreateAsync(EmployeeTaskCreateOptions options);

        public Task<Timesheet> CreateTimesheetAsync(TimesheetCreateOptions options);

        public System.Threading.Tasks.Task ArchiveEmployeeTaskAsync(int taskId, int? employeeId, int? positionId);
    }
}
