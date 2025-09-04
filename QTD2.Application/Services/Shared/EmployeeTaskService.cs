using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.Timesheet;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using Task = QTD2.Domain.Entities.Core.Task;

namespace QTD2.Application.Services.Shared
{
    public class EmployeeTaskService : IEmployeeTaskService
    {
        private readonly IEmployee_TaskService _employeeTaskService;
        private readonly ITaskDomainService _taskService;
        private readonly IPositionDomainService _positionService;
        private readonly ITimesheetService _timesheetService;
        private readonly IEmployeeDomainService _employeeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<EmployeeTaskService> _localizer;
        private readonly Task _task;

        public EmployeeTaskService(
            IEmployee_TaskService employeeTaskService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            ITimesheetService timesheetService,
            ITaskDomainService taskService,
            IPositionDomainService positionService,
            IEmployeeDomainService employeeService,
            IStringLocalizer<EmployeeTaskService> localizer)
        {
            _employeeTaskService = employeeTaskService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _timesheetService = timesheetService;
            _taskService = taskService;
            _employeeService = employeeService;
            _positionService = positionService;
            _localizer = localizer;
            _task = new Task();
        }

        public async System.Threading.Tasks.Task ArchiveEmployeeTaskAsync(int taskNumber, int? employee, int? positionId)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskNumber, new string[] { nameof(_task.Employee_Tasks) }).FirstOrDefaultAsync();
            List<Employee_Task> employee_Tasks = new List<Employee_Task>();
            var emp = task.Employee_Tasks.FirstOrDefault(x => x.TaskId == taskNumber);
            if(emp != null)
            {
                var currEMP = await _employeeService.GetEmployeesWithCompactPersons(emp.EmployeeId);
                if(currEMP != null)
                {
                    var emp_task = await _employeeTaskService.GetAsync(emp.Id);
                    employee_Tasks.Add(emp_task);
                    if (currEMP != null)
                    {
                        //emp = await _employeeService.GetAsync(currEMP.Value);
                        employee_Tasks.Add(task.CreateEmployeeTask(currEMP));
                    }
                    else if (positionId.HasValue)
                    {
                        var position = await _positionService.GetAsync(positionId.Value);
                        employee_Tasks.AddRange(task.CreateEmployeeTask(position));
                    }

                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee_Tasks.FirstOrDefault(), Employee_TaskOperations.Delete);

                    if (result.Succeeded)
                    {
                        emp_task.Archive();
                        var validationResult = await _employeeTaskService.UpdateAsync(emp_task);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }
        }

        public async Task<List<Employee_Task>> CreateAsync(EmployeeTaskCreateOptions options)
        {
            var task = await _taskService.GetAsync(options.TaskId);
            List<Employee_Task> employee_Tasks = new List<Employee_Task>();
            var pos = task.Task_Positions.FirstOrDefault(x => x.TaskId == options.TaskId).Position;
            employee_Tasks.AddRange(task.CreateEmployeeTask(pos));

            if (options.PositionId.HasValue)
            {
                var position = await _positionService.GetAsync(options.PositionId.Value);
                employee_Tasks.AddRange(task.CreateEmployeeTask(position));
            }
            else if (options.EmployeeId.HasValue)
            {
                var emp = await _employeeService.GetAsync(options.EmployeeId.Value);
                employee_Tasks.Add(task.CreateEmployeeTask(emp));
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee_Tasks.FirstOrDefault(), Employee_TaskOperations.Create);

            if (result.Succeeded)
            {
                var validationResult = await _taskService.UpdateAsync(task);
                if (validationResult.IsValid)
                {
                    return employee_Tasks;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<Timesheet> CreateTimesheetAsync(TimesheetCreateOptions options)
        {
            var timesheet = new Timesheet(options.EmployeeTaskId, options.Date, options.MethodId, options.Note);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, timesheet, Employee_TaskOperations.Create);
            if (result.Succeeded)
            {
                var validationResult = await _timesheetService.AddAsync(timesheet);
                if (validationResult.IsValid)
                {
                    return timesheet;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<Employee_Task>> GetAsync(int taskNumber, int version)
        {
            var emp_tasks = (await _employeeTaskService.FindAsync(x => x.TaskId == taskNumber && x.MajorVersion == version)).ToList();
            emp_tasks = emp_tasks.Where(item => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, item, Employee_TaskOperations.Read).Result.Succeeded).ToList();

            return emp_tasks;
        }

        public async Task<Employee_Task> GetAsync(int taskNumber, int version, int employee)
        {
            var empTask = (await _employeeTaskService.FindAsync(x => x.TaskId == taskNumber && x.MajorVersion == version && x.EmployeeId == employee)).FirstOrDefault();
            if (empTask != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empTask, Employee_TaskOperations.Read);
                if (result.Succeeded)
                {
                    return empTask;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return empTask;
        }
    }
}
