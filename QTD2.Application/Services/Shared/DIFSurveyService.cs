using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.DIFSurvey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDIFSurveyDomainService = QTD2.Domain.Interfaces.Service.Core.IDIFSurveyService;
using IDIFSurvey_EmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IDIFSurvey_EmployeeService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using IDifSurveyTaskDomainService = QTD2.Domain.Interfaces.Service.Core.IDIFSurvey_TaskService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IClientSettings_LicenseDomainService = QTD2.Domain.Interfaces.Service.Core.IClientSettings_LicenseService;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class DIFSurveyService : IDIFSurveyService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<DIFSurveyService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDIFSurveyDomainService _difSurveyDomainService;
        private readonly IDIFSurvey_EmployeeDomainService _difSurvey_EmployeeDomainService;
        private readonly IClientSettingsService _clientLicenseApplicationService;
        private readonly ITaskDomainService _taskDomainService;
        private readonly IDifSurveyTaskDomainService _difSurveyTaskDomainService;
        private readonly IEmployeeDomainService _employeeDomainService;
        private readonly IClientSettings_LicenseDomainService _clientSettings_LicenseDomainService;

        public DIFSurveyService(IAuthorizationService authorizationService,
        IStringLocalizer<DIFSurveyService> localizer,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        QTD2.Domain.Interfaces.Service.Core.IDIFSurveyService dIFSurveyDomainService,
        QTD2.Domain.Interfaces.Service.Core.IDIFSurvey_EmployeeService difSurvey_EmployeeDomainService,
        IClientSettingsService clientLicenseApplicationService,
        ITaskDomainService taskDomainService,
        IDifSurveyTaskDomainService difSurveyTaskDomainService,
        IEmployeeDomainService employeeDomainService,
         IClientSettings_LicenseDomainService clientSettings_LicenseDomainService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _difSurveyDomainService = dIFSurveyDomainService;
            _difSurvey_EmployeeDomainService = difSurvey_EmployeeDomainService;
            _clientLicenseApplicationService = clientLicenseApplicationService;
            _taskDomainService = taskDomainService;
            _difSurveyTaskDomainService = difSurveyTaskDomainService;
            _employeeDomainService = employeeDomainService;
            _clientSettings_LicenseDomainService = clientSettings_LicenseDomainService;
        }

        public async Task<DifSurveyOverview_VM> GetAllAsync()
        {
            var difSurveys = await _difSurveyDomainService.AllWithIncludeAsync(new string[] { "Employees", "DevStatus", "Position" });
            if (difSurveys != null)
            {
                var publishedDifs = difSurveys.Where(r => r.DevStatusId == 2).Count();
                var draftDifs = difSurveys.Where(r => r.DevStatusId == 1).Count();
                var employeePendingDif = difSurveys.SelectMany(x => x.Employees).Where(m => m.StatusId == 1 || m.StatusId == 2).Count();
                var difs = difSurveys.Select(r => new DIFSurveyOverview_DIFSurvey_VM(r.Id, r.Title, r.Position?.PositionTitle, r.StartDate, r.DueDate, r.DevStatus?.Status, r.Employees.Where(r => r.StatusId == 2 || r.StatusId == 3).Count() > 0 ? false : true, r.Employees.Where(r => r.Complete == true).Count() > 0 ? true : false, r.Active,r.SurveyStatus)
                ).ToList();
                var difSurveyOverview_VM = new DifSurveyOverview_VM(publishedDifs, draftDifs, employeePendingDif, difs);
                return difSurveyOverview_VM;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async Task<DIFSurveyVM> GetAsync(int id)
        {
            var difSurvey = await _difSurveyDomainService.GetAsync(id);
            var difSurveyTasks = await _difSurveyTaskDomainService.GetDifSurveyTasksBySurveyIdAsync(id);
            var difSurveyEmployees = await _difSurvey_EmployeeDomainService.GetDifSurveyEmployeesBySurveyIdAsync(id);
            var difSurveyVm = MapDifSurveyToDifSurveyVM(difSurvey);
            difSurveyVm.Tasks = difSurveyTasks.Select(x => MapDifSurveyTaskToDifSurveyTaskVM(x)).ToList();
            difSurveyVm.Employees = difSurveyEmployees.Select(x => MapDifSurveyEmployeeToDifSurvey_EmployeeVM(x)).ToList();
            return difSurveyVm;
        }

        public async Task<DIFSurvey> CreateAsync(DIFSurvey_CreateOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            else
            {
                var difSurvey = new QTD2.Domain.Entities.Core.DIFSurvey(options.Title, options.PositionId, options.StartDate, options.DueDate, options.Instructions, 1);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, difSurvey, AuthorizationOperations.Create);
                if (result.Succeeded)
                {
                    var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                    difSurvey.Create(userName.Id);
                    var validationResult = await _difSurveyDomainService.AddAsync(difSurvey);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return difSurvey;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }

            }
        }

        public async Task<DIFSurvey> EditDIFSurveyAsync(int id, string editType)
        {
            var difSurvey = await _difSurveyDomainService.GetAsync(id);
            switch (editType.ToLower())
            {
                case "inactive":
                default:
                    difSurvey.Active = false;
                    break;
                case "active":
                    difSurvey.Active = true;
                    break;
                case "delete":
                    difSurvey.Deleted = true;
                    break;
            }
            var validationResult = await _difSurveyDomainService.UpdateAsync(difSurvey);

            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return difSurvey;
            }
        }

        public async Task<DIFSurveyVM> UpdateAsync(int id, DIFSurvey_UpdateOptions options)
        {
            var difSurvey = await _difSurveyDomainService.GetAsync(id);
            if (difSurvey == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, difSurvey, DIFSurveyOperations.Update);
            if (!options.IsPublish)
            {
                if (!result.Succeeded)
                {
                    throw new UnauthorizedAccessException();
                }

                if (!String.IsNullOrEmpty(options.Title))
                {
                    difSurvey.SetTitle(options.Title);
                }
                if (options.PositionId > 0)
                {
                    difSurvey.SetPositionId(options.PositionId);
                }
                if (!String.IsNullOrEmpty(options.DueDate.ToString()))
                {
                    difSurvey.SetDueDate(options.DueDate);
                }
                if (!String.IsNullOrEmpty(options.StartDate.ToString()))
                {
                    difSurvey.SetStartDate(options.StartDate);
                }
                difSurvey.SetInstructions(options.Instructions);
            }
            else
            {
                var currentLicense = await _clientSettings_LicenseDomainService.GetCurrentLicense();
                difSurvey.Publish(currentLicense?.LicenseType, options.IsReleaseToEMP);
            }

            var validationResult = await _difSurveyDomainService.UpdateAsync(difSurvey);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return await GetAsync(difSurvey.Id);
            }
        }
        public async Task<List<DIFSurveyEmployeeVM>> GetCompletedSurveyByEmpId(int employeeId)
        {
            var difSurveyEmployeee = await _difSurvey_EmployeeDomainService.GetCompleteDifSurveyByEmployeeId(employeeId);
            if (difSurveyEmployeee != null)
            {
                var difSurveyEmployeeVMs = difSurveyEmployeee.Select(x => new DIFSurveyEmployeeVM
                {
                    DifSurveyId = x.DIFSurveyId,
                    Title = x.DIFSurvey.Title,
                    CompletionDate = x.CompletedDate,
                    DueDate = x.DIFSurvey.DueDate,
                    StatusId = x.StatusId,
                    Status = x.Status.Status
                }).ToList(); ;
                return difSurveyEmployeeVMs;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }
        public async Task<List<DIFSurveyEmployeeVM>> GetPendingSurveyByEmpId(int employeeId)
        {
            var difSurveyEmployeee = await _difSurvey_EmployeeDomainService.GetPendingDifSurveyByEmployeeId(employeeId);
            var currentDate = DateTime.Now.Date;
            if (difSurveyEmployeee != null)
            {
                var difSurveyEmployeeVMs = difSurveyEmployeee.Where(x=>x.DIFSurvey.SurveyStatus=="Open").Select(x => new DIFSurveyEmployeeVM
                {
                    DifSurveyId = x.DIFSurveyId,
                    Title = x.DIFSurvey.Title,
                    CompletionDate = x.CompletedDate,
                    DueDate = x.DIFSurvey.DueDate,
                    StatusId = x.StatusId,
                    Status = x.Status.Status
                }).ToList(); 
                return difSurveyEmployeeVMs;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async Task<DIFSurvey> GetTaskRatingAsync(int id)
        {
            var difSurvey = await _difSurveyDomainService.GetWithIncludeAsync(id, new[] { "Position", "Tasks.Task", "Tasks.Task.SubdutyArea", "Tasks.Task.SubdutyArea.DutyArea", "Tasks.TrainingStatus_Override", "Tasks.DIFSurvey_Task_TrainingFrequency", "Tasks.TrainingStatus_Calculated" });
            if (difSurvey != null)
            {
                return difSurvey;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }

        }

        public async Task<List<DIFSurveyTaskVM>> LinkTasksToDifSurveyAsync(DIFSurveyTaskLinkOptions options)
        {
            var difSurvey = await _difSurveyDomainService.GetWithIncludeAsync(options.DifSurveyId, new string[] { "Tasks" });
            if (difSurvey == null)
            {
                throw new ArgumentNullException();
            }
            var dif_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, difSurvey, DIFSurveyOperations.Update);
            foreach (var taskId in options.TaskIds)
            {
                var task = await _taskDomainService.GetAsync(taskId);

                if (task == null)
                {
                    throw new QTDServerException(_localizer["TaskNotFound"]);
                }


                var task_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);

                if (dif_result.Succeeded && task_Result.Succeeded)
                {
                    var difSurvey_task_link = difSurvey.LinkTask(task);
                    difSurvey_task_link.Create((await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            var validationResult = await _difSurveyDomainService.UpdateAsync(difSurvey);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return (await _difSurveyTaskDomainService.GetDifSurveyTasksBySurveyIdAsync(difSurvey.Id)).Select(x => MapDifSurveyTaskToDifSurveyTaskVM(x)).ToList();
            }
        }
        public async System.Threading.Tasks.Task UnlinkTaskFromDifSurveyAsync(int difSurveyTaskId)
        {
            var difSurveyTask = await _difSurveyTaskDomainService.GetAsync(difSurveyTaskId);
            if (difSurveyTask == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, difSurveyTask, AuthorizationOperations.Update);
            if (result.Succeeded)
            {
                difSurveyTask.Delete();
                var validationResult = await _difSurveyTaskDomainService.UpdateAsync(difSurveyTask);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<DIFSurvey_Task> UpdateDifTaskResultsAsync(int dsTaskId, DIFResult_UpdateOptions options)
        {
            var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            var employee = await _employeeDomainService.GetEmployeeByUsernameAsync(userName.Email);
            var difSurveyTask = await _difSurveyTaskDomainService.GetAsync(dsTaskId);
            if (difSurveyTask == null)
            {
                throw new ArgumentNullException();
            }
            if (options.DIFSurvey_Task_TrainingFrequencyId != null)
            {
                difSurveyTask.DIFSurvey_Task_TrainingFrequencyId = options.DIFSurvey_Task_TrainingFrequencyId;
            }
            if (options.TrainingStatus_OverrideId != null)
            {
                difSurveyTask.TrainingStatus_OverrideId = options.TrainingStatus_OverrideId;
            }
            if (!String.IsNullOrEmpty(options.Comments))
            {
                difSurveyTask.Comments = options.Comments;
                difSurveyTask.CommentingEmployeeId = employee.Id;
            }
            var validationResult = await _difSurveyTaskDomainService.UpdateAsync(difSurveyTask);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return await _difSurveyTaskDomainService.GetAsync(difSurveyTask.Id);
            }

        }

        public async Task<List<DIFSurvey_EmployeeVM>> LinkEmployeesToDifSurveyAsync(DIFSurveyEmployeeLinkUnlinkOptions options)
        {
            var difSurvey = await _difSurveyDomainService.GetWithIncludeAsync(options.DifSurveyId, new string[] { "Employees" });
            if (difSurvey == null)
            {
                throw new ArgumentNullException();
            }
            var dif_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, difSurvey, DIFSurveyOperations.Update);
            foreach (var empId in options.EmployeeIds)
            {
                var employee = await _employeeDomainService.GetAsync(empId);

                if (employee == null)
                {
                    throw new QTDServerException(_localizer["EmployeeNotFound"]);
                }


                var emp_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read);

                if (dif_result.Succeeded && emp_Result.Succeeded)
                {
                    var difSurvey_emp_link = difSurvey.LinkEmployee(employee);
                    difSurvey_emp_link.Create((await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            var validationResult = await _difSurveyDomainService.UpdateAsync(difSurvey);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return (await _difSurvey_EmployeeDomainService.GetDifSurveyEmployeesBySurveyIdAsync(difSurvey.Id)).Select(x => MapDifSurveyEmployeeToDifSurvey_EmployeeVM(x)).ToList();
            }

        }
        public async System.Threading.Tasks.Task UnlinkEmployeesFromDifSurveyAsync(DIFSurveyEmployeeLinkUnlinkOptions options)
        {
            var difSurvey = await _difSurveyDomainService.GetWithIncludeAsync(options.DifSurveyId, new string[] { "Employees" });
            if (difSurvey == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, difSurvey, DIFSurveyOperations.Update);
            if (result.Succeeded)
            {
                foreach (var empId in options.EmployeeIds)
                {
                    var employee = difSurvey.Employees.FirstOrDefault(x => x.EmployeeId == empId);

                    if (employee == null)
                    {
                        throw new QTDServerException(_localizer["EmployeeNotFound"]);
                    }

                    var emp_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Delete);

                    if (emp_Result.Succeeded)
                    {
                        employee.Delete();
                        employee.Modify((await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
                var validationResult = await _difSurveyDomainService.UpdateAsync(difSurvey);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
        public async Task<DIFSurveyVM> GetEnrollmentsBySurveyIdAsync(int id)
        {
            var difSurvey = await _difSurveyDomainService.GetAsync(id);
            var difSurveyEmployees = await _difSurvey_EmployeeDomainService.GetDifSurveyEmployeesBySurveyIdAsync(id);
            var difSurveyVm = MapDifSurveyToDifSurveyVM(difSurvey);
            difSurveyVm.Employees = difSurveyEmployees.Select(x => MapDifSurveyEmployeeToDifSurvey_EmployeeVM(x)).ToList();
            return difSurveyVm;
        }

        public DIFSurveyVM MapDifSurveyToDifSurveyVM(DIFSurvey difSurvey)
        {
            var difSurveyVm = new DIFSurveyVM(difSurvey.Id, difSurvey.Title, difSurvey.PositionId, difSurvey.StartDate, difSurvey.DueDate, difSurvey.Instructions, difSurvey.ReleasedToEMP.GetValueOrDefault(), difSurvey.DevStatusId.GetValueOrDefault(), difSurvey.Active,difSurvey.SurveyStatus);
            return difSurveyVm;
        }

        public DIFSurveyTaskVM MapDifSurveyTaskToDifSurveyTaskVM(DIFSurvey_Task difSurveyTask)
        {
            var difSurveyTaskVm = new DIFSurveyTaskVM(difSurveyTask.Id, difSurveyTask.TaskId, difSurveyTask.DifSurveyId, difSurveyTask.Task?.FullNumber, difSurveyTask.Task?.Description);
            return difSurveyTaskVm;
        }
        public DIFSurvey_EmployeeVM MapDifSurveyEmployeeToDifSurvey_EmployeeVM(DIFSurvey_Employee difSurveyEmployee)
        {
            var positions = String.Join(", ", difSurveyEmployee?.Employee.EmployeePositions.Where(x => x.Active && (x.Position?.Active ?? false)).Select(x => x.Position?.PositionTitle));
            var organizations = String.Join(", ", difSurveyEmployee?.Employee.EmployeeOrganizations.Where(x => x.Active && (x.Organization?.Active ?? false)).Select(x => x.Organization?.Name));
            var person = difSurveyEmployee.Employee?.Person;
            var status = difSurveyEmployee.Status?.Status;
            var difSurveyTEmployeeVm = new DIFSurvey_EmployeeVM(difSurveyEmployee.Id, difSurveyEmployee.EmployeeId, person?.FirstName, person?.LastName, person?.Username, person?.Image, difSurveyEmployee.DIFSurveyId, positions, organizations, difSurveyEmployee.ReleaseDate, difSurveyEmployee.CompletedDate, difSurveyEmployee.StatusId, status);
            return difSurveyTEmployeeVm;
        }

        public async Task<DifSurveyViewResponseVm> GetEmployeeResponsesByEmpId(int employeeId, int difSurveyId)
        {
            var responseList = new List<DIFSurveyResponseVM>();
            var difSurvey = await _difSurveyDomainService.GetSurveyByEmpIdAsync(employeeId, difSurveyId);
            if (difSurvey != null)
            {   
                var difSurveyEmployee = difSurvey.Employees.Where(r => r.EmployeeId == employeeId).FirstOrDefault();
                foreach (var task in difSurvey.Tasks)
                {
                    var response = task.Responses.Where(r => r.DIFSurvey_EmployeeId == difSurveyEmployee.Id).FirstOrDefault();
                    var difSurveyViewResponseVm = new DIFSurveyResponseVM(task.Id, task.Task.FullNumber, task.Task.Description);
                    if(response != null)
                    {
                        difSurveyViewResponseVm.UpdateResponse(response.Difficulty ?? null, response.Importance ?? null, response.Frequency ?? null, response?.NA, response.Comments ?? null);
                    }
                    responseList.Add(difSurveyViewResponseVm);
                }
                var result = new DifSurveyViewResponseVm(difSurveyEmployee.CompletedDate, difSurvey.Title, difSurvey.StartDate, difSurvey.DueDate, responseList,difSurvey.Instructions, difSurveyEmployee.Comments);
                return result;
            }
            else
            {
                throw new ArgumentNullException();
            }

        }
        public async System.Threading.Tasks.Task CreateUpdateEmployeeResponsesAsync(int difSurveyId, int difEmployeeId, DIFSurveyEmployeeResponseOptions options, bool isCompleted)
        {
            var dif_employee = (await _difSurvey_EmployeeDomainService.FindWithIncludeAsync(x => x.DIFSurveyId == difSurveyId && x.EmployeeId == difEmployeeId, new string[] { "Responses" })).FirstOrDefault();
            if (dif_employee == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, dif_employee, AuthorizationOperations.Update);
            if (result.Succeeded)
            {
                foreach (var response in options.Responses)
                {
                    var emp_response = dif_employee.Responses.FirstOrDefault(x => x.DIFSurvey_TaskId == response.DIFSurveyTaskId);
                    if (emp_response == null)
                    {
                        emp_response = new DIFSurvey_Employee_Response(difEmployeeId, response.DIFSurveyTaskId);
                        emp_response.Create((await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id);
                        dif_employee.Responses.Add(emp_response);
                    }
                    else
                    {
                        emp_response.Modify((await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id);
                    }
                    emp_response.UpdateResponse(response.Difficulty, response.Importance, response.Frequency, response.NA, response.Comments);
                }
                if (isCompleted)
                {
                    dif_employee.Completed();
                }
                else
                {
                    dif_employee.Update();
                }
                dif_employee.Comments = options.Comments;
                var validationResult = await _difSurvey_EmployeeDomainService.UpdateAsync(dif_employee);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}

