using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Model.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IClassScheduleRosterStatusDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Roster_StatusesService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using ILocationDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_Service;
using IInstructorDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_Service;
using IDeliveryMethodDomainService = QTD2.Domain.Interfaces.Service.Core.IDeliveryMethodService;
using IILA_PrerequisiteLink_DomainService = QTD2.Domain.Interfaces.Service.Core.IILA_PreRequisite_LinkService;
using ISelfRegistrationDomainService = QTD2.Domain.Interfaces.Service.Core.ISelfRegistrationOptionsService;
using IClassSchedule_EmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using ITaskQualificationDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using ITaskQualificationSettingDomainService = QTD2.Domain.Interfaces.Service.Core.ITQEmpSettingService;
using ITaskQualification_EvaluatorLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualification_Evaluator_LinkService;
using ITaskQualificationStatusDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationStatusService;
using ITaskQualificationEMPSignOffDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_SignOffService;
using ITaskQualificationEMPQuestionDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_QuestionAnswerService;
using ITaskQualificationEMPStepDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_StepsService;
using ITaskQualificationEMPSuggestionsDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_SuggestionService;
using IILACertification_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILACertificationLinkService;
using IILACertificationSubReq_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILACertificationSubRequirementLinkService;
using ICertificationSubRequirementDomainService = QTD2.Domain.Interfaces.Service.Core.ICertificationSubRequirementService;
using IClassTestReleaseEmpSettingDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_TestReleaseEMPSettingsService;
using IOnlineCoursesApplicationService = QTD2.Application.Interfaces.Services.Shared.IOnlineCoursesService;
using IILAEvaluationEMPSettingDomainService = QTD2.Domain.Interfaces.Service.Core.IEvaluationReleaseEMPSettingsService;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Persistence;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.Employee;
using ITaskQualification_Evaluator_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualification_Evaluator_LinkService;

namespace QTD2.Application.Services.Shared
{
    public class DashboardService : IDashboardService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<DashboardService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IClassSchedule_RosterService _classScheduleRosterService;
        private readonly IClassSchedule_Evaluation_RosterService _EvaluationRosterService;
        private readonly IProcedureReview_EmployeeService _procedureReviewService;
        private readonly Domain.Interfaces.Service.Core.IEmployeeService _employeeDomainService;
        private readonly Interfaces.Services.Shared.IEmployeeService _employeeService;
        private readonly IILADomainService _ilaDomainService;
        private readonly IPersonDomainService _personService;
        private readonly Interfaces.Services.Shared.IClassScheduleService _classScheduleService;
        private readonly IClassScheduleDomainService _classScheduleDomainService;
        private readonly ILocationDomainService _locationService;
        private readonly IInstructorDomainService _instService;
        private readonly IDeliveryMethodDomainService _deliverMethService;
        private readonly IClassScheduleRosterStatusDomainService _classScheduleRosterStatusDomainService;
        private readonly Interfaces.Services.Shared.IILAService _ilaService;
        private readonly IILA_PrerequisiteLink_DomainService _ila_preReq_LinkService;
        private readonly ISelfRegistrationDomainService _selfRegService;
        private readonly IClassSchedule_EmployeeDomainService _classScheduleEmpService;
        private readonly IEMPReleaseCheckService _empReleaseCheckService;
        private readonly ITaskQualificationDomainService _taskQualService;
        private readonly ITaskDomainService _taskService;
        private readonly Interfaces.Services.Shared.ITaskService _taskAppService;
        private readonly ITaskQualificationSettingDomainService _taskQualSettingService;
        private readonly ITaskQualification_EvaluatorLinkDomainService _taskQualEvaluator_LinkService;
        private readonly ITaskQualificationStatusDomainService _taskQualStatusService;
        private readonly ITaskQualificationEMPSignOffDomainService _taskQualSignOffService;
        private readonly ITaskQualificationEMPQuestionDomainService _taskQualQuestionService;
        private readonly ITaskQualificationEMPStepDomainService _taskQualStepService;
        private readonly ITaskQualificationEMPSuggestionsDomainService _taskQualSuggestionService;
        private readonly IILACertification_LinkDomainService _ilaCertLinkService;
        private readonly IILACertificationSubReq_LinkDomainService _ilaCertSubReqLinkService;
        private readonly ICertificationSubRequirementDomainService _certSubReqService;
        private readonly IOnlineCoursesService _onlineCourseService;
        private readonly ITaskRequalificationService _taskRequalificationService;
        private readonly IMainUnitOfWork _mainUow;
        private readonly IOnlineCoursesApplicationService _onlineCoursesApplicationService;
        private readonly IILAEvaluationEMPSettingDomainService _evalReleaseService;
        private readonly IClassTestReleaseEmpSettingDomainService _classTestReleaseEmpSettingDomainService;
        private readonly ITaskQualification_Evaluator_LinkDomainService _tq_eval_linkService;
        public DashboardService(
            Interfaces.Services.Shared.IILAService ilaService,
            Interfaces.Services.Shared.IClassScheduleService classScheduleService,
            IPersonDomainService personService,
            Interfaces.Services.Shared.IEmployeeService employeeServiceMain,
            IILADomainService iILAService,
            Domain.Interfaces.Service.Core.IEmployeeService employeeService,
            IClassScheduleRosterStatusDomainService classScheduleRosterStatusDomainService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<DashboardService> localizer,
            UserManager<AppUser> userManager,
            IClassSchedule_Evaluation_RosterService EvaluationRosterService,
            IClassSchedule_RosterService classScheduleRosterService,
            IProcedureReview_EmployeeService procedureReviewService,
            IClassScheduleDomainService classScheduleDomainService,
            ILocationDomainService locationService,
            IInstructorDomainService instService,
            IDeliveryMethodDomainService deliverMethService,
            IILA_PrerequisiteLink_DomainService ila_preReq_LinkService,
            ISelfRegistrationDomainService selfRegService,
            IClassSchedule_EmployeeDomainService classScheduleEmpService,
            IEMPReleaseCheckService empReleaseCheckService,
            ITaskQualificationDomainService taskQualService,
            ITaskDomainService taskService,
            Interfaces.Services.Shared.ITaskService taskAppService,
            ITaskQualificationSettingDomainService taskQualSettingService,
            ITaskQualification_EvaluatorLinkDomainService taskQualEvaluator_LinkService,
            ITaskQualificationStatusDomainService taskQualStatusService,
            ITaskQualificationEMPSignOffDomainService taskQualSignOffService,
            ITaskQualificationEMPQuestionDomainService taskQualQuestionService,
            ITaskQualificationEMPStepDomainService taskQualStepService,
            ITaskQualificationEMPSuggestionsDomainService taskQualSuggestionService,
            IILACertification_LinkDomainService ilaCertLinkService,
            IILACertificationSubReq_LinkDomainService ilaCertSubReqLinkService,
            ICertificationSubRequirementDomainService certSubReqService,
            IOnlineCoursesService onlineCoursesService,
            ITaskRequalificationService taskRequalificationService,
            IMainUnitOfWork mainUow,
            IOnlineCoursesApplicationService onlineCoursesApplicationService,
            IILAEvaluationEMPSettingDomainService evalReleaseService,
            IClassTestReleaseEmpSettingDomainService classTestReleaseEmpSettingDomainService,
             ITaskQualification_Evaluator_LinkDomainService tq_eval_linkService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _classScheduleRosterService = classScheduleRosterService;
            _EvaluationRosterService = EvaluationRosterService;
            _procedureReviewService = procedureReviewService;
            _classScheduleRosterStatusDomainService = classScheduleRosterStatusDomainService;
            _employeeDomainService = employeeService;
            _ilaDomainService = iILAService;
            _employeeService = employeeServiceMain;
            _personService = personService;
            _classScheduleService = classScheduleService;
            _classScheduleDomainService = classScheduleDomainService;
            _locationService = locationService;
            _instService = instService;
            _deliverMethService = deliverMethService;
            _ilaService = ilaService;
            _ila_preReq_LinkService = ila_preReq_LinkService;
            _selfRegService = selfRegService;
            _classScheduleEmpService = classScheduleEmpService;
            _empReleaseCheckService = empReleaseCheckService;
            _taskQualService = taskQualService;
            _taskService = taskService;
            _taskAppService = taskAppService;
            _taskQualSettingService = taskQualSettingService;
            _taskQualEvaluator_LinkService = taskQualEvaluator_LinkService;
            _taskQualStatusService = taskQualStatusService;
            _taskQualSignOffService = taskQualSignOffService;
            _taskQualQuestionService = taskQualQuestionService;
            _taskQualStepService = taskQualStepService;
            _taskQualSuggestionService = taskQualSuggestionService;
            _ilaCertLinkService = ilaCertLinkService;
            _ilaCertSubReqLinkService = ilaCertSubReqLinkService;
            _certSubReqService = certSubReqService;
            _onlineCourseService = onlineCoursesService;
            _taskRequalificationService = taskRequalificationService;
            _mainUow = mainUow;
            _onlineCoursesApplicationService = onlineCoursesApplicationService;
            _evalReleaseService = evalReleaseService;
            _classTestReleaseEmpSettingDomainService = classTestReleaseEmpSettingDomainService;
            _tq_eval_linkService = tq_eval_linkService;
        }

        private async Task<int> GetEmployeeId()
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();
            if (person == null)
            {
                return 0;
                //throw new BadHttpRequestException(message: _localizer["PersonNotFoundException"]);
            }
            var employee = await _employeeService.GetByPersonIdAsync(person.Id);
            if (employee == null)
            {
                return 0;
                //throw new BadHttpRequestException(message: _localizer["EmployeeNotFoundException"]);
            }
            return employee.Id;
        }

        public async Task<Result<EmployeeDashboardStatsDto>> GetDashboardStatistics()
        {
            var now = DateTime.UtcNow;
            var userId = await GetEmployeeId();

            var classScheduleStatuses = await _mainUow.Repository<ClassSchedule_Roster_Statuses>()
                .GetListAsync(i => i.Name == "In Progress" || i.Name == "Not Started");

            var tqStatuses = await _mainUow.Repository<TaskQualificationStatus>()
                .GetListAsync(i => i.Name == "Pending" || i.Name == "On Time");

            var inProgressStatusId = classScheduleStatuses.FirstOrDefault(i => i.Name == "In Progress")?.Id;
            var notStartedStatusId = classScheduleStatuses.FirstOrDefault(i => i.Name == "Not Started")?.Id;
            var pendingTqStatusId = tqStatuses.FirstOrDefault(i => i.Name == "Pending")?.Id;
            var onTimeTqStatusId = tqStatuses.FirstOrDefault(i => i.Name == "On Time")?.Id;

            InProgressNotStartedCountModel testCounts = await GetTestDashboardCountsAsync(userId, inProgressStatusId, notStartedStatusId);
            InProgressNotStartedCountModel evaluationCounts = await GetStudentEvaluationDashboardCountsAsync(userId);
            InProgressNotStartedCountModel onlineCoursesCounts = await GetOnlineCoursesDashboardCountsAsync(userId);
            InProgressNotStartedCountModel procReviewCounts = await GetProcedureReviewDashboardCountsAsync(userId);

            var taskQualificationInProgressCount = await _mainUow.Repository<TaskQualification>()
                .CountAsync(i => i.EmpId == userId && i.TQStatusId == pendingTqStatusId);

            var taskQualificationNotStartedCount = await _mainUow.Repository<TaskQualification>()
                .CountAsync(i => i.EmpId == userId && i.TQStatusId == onTimeTqStatusId);

            var statsDto = new EmployeeDashboardStatsDto
            {
                TestInProgressCount = testCounts.InProgressCount,
                TestNotStartedCount = testCounts.NotStartedCount,
                StudentEvaluationInProgressCount = evaluationCounts.InProgressCount,
                StudentEvaluationNotStartedCount = evaluationCounts.NotStartedCount,
                OnlineCourseInProgressCount = onlineCoursesCounts.InProgressCount,
                OnlineCourseNotStartedCount = onlineCoursesCounts.NotStartedCount,
                ProcedureReviewsInProgressCount = procReviewCounts.InProgressCount,
                ProcedureReviewsNotStartedCount = procReviewCounts.NotStartedCount,
                TaskQualificationInProgressCount = taskQualificationInProgressCount,
                TaskQualificationNotStartedCount = taskQualificationNotStartedCount,
            };

            return Result<EmployeeDashboardStatsDto>.CreateSuccess(statsDto);
        }

        public async Task<InProgressNotStartedCountModel> GetTestDashboardCountsAsync(int empId, int? inProgressStatusId, int? notStartedStatusId)
        {
            InProgressNotStartedCountModel count = new InProgressNotStartedCountModel();
            var includeProperties = new[] { "ClassSchedule", "TestType" };

            var classScheduleRosters = (await _mainUow.Repository<ClassSchedule_Roster>().GetListAsync(i => i.EmpId == empId && !i.ClassSchedule.Deleted && (i.ClassSchedule.ClassSchedule_Employee.Where(m => m.EmployeeId == i.EmpId).All(e => e.IsEnrolled && e.Active)) && i.ClassSchedule.Active, includeProperties)).Where(x => x.IsReleased == true);
            foreach (var classScheduleRoster in classScheduleRosters)
            {
                DateTime? dueDate = null;
                var setting = (await _classTestReleaseEmpSettingDomainService.FindAsync(x => x.ClassScheduleId == classScheduleRoster.ClassScheduleId)).FirstOrDefault();
                if (setting != null && classScheduleRoster.TestType != null)
                {
                    if (classScheduleRoster.TestType.Description.ToUpper() == "PRETEST")
                        dueDate = classScheduleRoster.ClassSchedule.EndDateTime;

                    else
                        dueDate = setting.GetDueDate(classScheduleRoster.ClassSchedule.EndDateTime);

                    if (classScheduleRoster.StatusId == inProgressStatusId && dueDate > DateTime.UtcNow)
                    {
                        count.InProgressCount++;
                    }
                    else if (classScheduleRoster.StatusId == notStartedStatusId && dueDate > DateTime.UtcNow)
                    {
                        count.NotStartedCount++;
                    }

                }
            }
            return count;
        }

        public async Task<InProgressNotStartedCountModel> GetStudentEvaluationDashboardCountsAsync(int empId)
        {
            InProgressNotStartedCountModel count = new InProgressNotStartedCountModel();
            var includeProperties = new[] { "StudentEvaluationInfo", "ClassScheduleInfo.ILA.Provider" };
            var evaluationsRosters = (await _EvaluationRosterService.GetEvaluationsByEmpIdAsync(empId)).ToList();
            foreach (var evaluationsRoster in evaluationsRosters)
            {
                var evalSettings = (await _evalReleaseService.FindAsync(x => x.ILAId == evaluationsRoster.ClassScheduleInfo.ILA.Id)).FirstOrDefault();

                var dueDate = (evalSettings != null ? evalSettings.GetDueDate(evaluationsRoster.ClassScheduleInfo.EndDateTime) : evaluationsRoster.ClassScheduleInfo.EndDateTime.AddDays(10));

                if (evaluationsRoster.getStatus() == "In Progress" && dueDate > DateTime.UtcNow)
                {
                    count.InProgressCount++;
                }
                else if (evaluationsRoster.getStatus() == "Pending" && dueDate > DateTime.UtcNow)
                {
                    count.NotStartedCount++;
                }
            }
            return count;
        }
        public async Task<InProgressNotStartedCountModel> GetOnlineCoursesDashboardCountsAsync(int empId)
        {
            InProgressNotStartedCountModel count = new InProgressNotStartedCountModel();
            var includeProperties = new[] { "ScormRegistrations", "ClassSchedule.ILA", "Employee" };

            var classScheduleEmployees = (await _mainUow.Repository<ClassSchedule_Employee>()
                .GetListAsync(i => i.EmployeeId == empId, includeProperties))
                .Where(i => !i.CompletionDate.HasValue && i.IsEnrolled && i.Active)
                .ToArray();
                
            foreach (var classScheduleEmployee in classScheduleEmployees)
            {
                classScheduleEmployee.ScormRegistrations = classScheduleEmployee.ScormRegistrations.Where(r => r.Active).ToList();
            }
            var filteredClassScheduleEmployees = await _onlineCoursesApplicationService.FilterClassScheduleEmployeesAsync(classScheduleEmployees);
            count.InProgressCount = filteredClassScheduleEmployees.Where(x => x.ScormRegistrations.Any(r => !string.IsNullOrEmpty(r.LaunchLink))).Count();
            count.NotStartedCount = filteredClassScheduleEmployees.Where(x => x.ScormRegistrations.All(r => string.IsNullOrEmpty(r.LaunchLink))).Count();
            return count;
        }

        public async Task<InProgressNotStartedCountModel> GetProcedureReviewDashboardCountsAsync(int empId)
        {
            InProgressNotStartedCountModel count = new InProgressNotStartedCountModel();
            var includeProperties = new[] { "ProcedureReview" };

            var currentDate = DateTime.UtcNow;
            var procedureReviews = (await _mainUow.Repository<ProcedureReview_Employee>()
                .GetListAsync(i => i.EmployeeId == empId, includeProperties))
                .Where(x => x.Active && x.ProcedureReview.IsPublished && x.ProcedureReview.Active && x.ProcedureReview.StartDateTime <= DateTime.UtcNow && DateTime.UtcNow <= x.ProcedureReview.EndDateTime)
                .ToList();
            count.InProgressCount = procedureReviews.Where(x => x.IsStarted && !x.IsCompleted).Count();
            count.NotStartedCount = procedureReviews.Where(x => !x.IsStarted).Count();

            return count;
        }


        public async Task<object> GetDueTrainingsData(string date)
        {
            var toReturnList = new List<DueTrainingDataVM>();
            var userId = await GetEmployeeId();

            var completeStatusId = await _classScheduleRosterStatusDomainService.GetDashboardStatusAsync("Completed");
            var incompleteStatusId = await _classScheduleRosterStatusDomainService.GetDashboardStatusAsync("In Progress");
            var notStartedStatusId = await _classScheduleRosterStatusDomainService.GetDashboardStatusAsync("Not Started");
            var classScheduleRoster = await _classScheduleRosterService.GetClassScheduleRosterByUserId(userId);
            var evaluationRosters = await _EvaluationRosterService.GetEvaluationsByEmpIdAsync(userId);
            var proceduresReviews = await _procedureReviewService.GetEmpProcedureReviewByIdAsync(userId);
            var classScheduleEmployees = await _classScheduleEmpService.GetPendingOnlineCoursesForEmployeeAsync(userId);
            var filteredClassScheduleEmployees = await _onlineCoursesApplicationService.FilterClassScheduleEmployeesAsync(classScheduleEmployees.ToArray());
            var taskQualList = await _taskQualService.GetTaskQualificationsListByEmpId(userId, DateTime.Parse(date).Date);
            classScheduleRoster = classScheduleRoster?.Where(x => x.ClassSchedule != null && x.ClassSchedule.StartDateTime.Date <= DateTime.Today && x.ClassSchedule.EndDateTime.Date > DateTime.Today).ToList();

            if (proceduresReviews != null)
            {
                foreach (var item in proceduresReviews)
                {
                    if (item.ProcedureReview?.EffectiveDueDate != null && item.ProcedureReview.EffectiveDueDate >= DateTime.UtcNow)
                    {
                        toReturnList.Add(new DueTrainingDataVM()
                        {
                            Type = "Procedure Review",
                            Title = item.ProcedureReview?.ProcedureReviewTitle,
                            Status = item.IsStarted && !item.IsCompleted ? "In Progress" : !item.IsStarted ? "Not Started" : item.IsCompleted ? "Completed" : "Invalid",
                            Id = item.ProcedureReview?.Id,
                            ParentId = item.ProcedureReview.ProcedureId,
                            CanStart = item.ProcedureReview.EffectiveDueDate >= DateTime.UtcNow,
                            DueDate = item.ProcedureReview.EffectiveDueDate
                        });
                    }
                }
            }
            if (evaluationRosters != null)
            {
                foreach (var item in evaluationRosters)
                {
                    bool canStart = item.ClassScheduleInfo?.ILA?.EvaluationReleaseEMPSetting?.GetDueDate(item.ClassScheduleInfo.EndDateTime) >= DateTime.UtcNow.Date;
                    string status = item.IsStarted && !item.IsCompleted ? "In Progress" : !item.IsStarted ? "Not Started" : item.IsCompleted ? "Completed" : "Invalid";

                    if (!canStart && status != "Completed")
                    {
                        continue;
                    }

                    toReturnList.Add(new DueTrainingDataVM()
                    {
                        Type = "Evaluation",
                        Title = item.StudentEvaluationInfo?.Title ?? String.Empty,
                        Status = status,
                        Id = item.StudentEvaluationInfo?.Id ?? 0,
                        ParentId = item?.ClassScheduleId,
                        CanStart = canStart,
                        DueDate = item.ClassScheduleInfo.EndDateTime
                    });
                }
            }
            if (classScheduleRoster != null)
            {
                foreach (var item in classScheduleRoster)
                {
                    toReturnList.Add(new DueTrainingDataVM()
                    {
                        Type = "Test",
                        Title = item.Test?.TestTitle ?? String.Empty,
                        Status = item.StatusId == notStartedStatusId ? "Not Started" : item.StatusId == incompleteStatusId ? "In Progress" : "Completed",
                        Id = item.TestId,
                        ParentId = item.ClassScheduleId,
                        CanStart = item.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.GetDueDate(item.ClassSchedule.EndDateTime) >= DateTime.UtcNow.Date,
                        DueDate = item.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.GetDueDate(item.ClassSchedule.EndDateTime)
                    });
                }
            }
            if(filteredClassScheduleEmployees != null)
            {
                foreach (var item in filteredClassScheduleEmployees)
                {
                    toReturnList.Add(new DueTrainingDataVM()
                    {
                        Type = "Online Course",
                        Title = item.ClassSchedule?.ILA?.Name ?? String.Empty,
                        Status = item.ScormRegistrations.Any(x=>x.LaunchLink != null) ? "In Progress" :"Not Started",
                        Id = item.Id,
                        ParentId = item.ClassScheduleId,
                        CanStart = item.ClassSchedule?.ILA?.CBTs.Where(x=>x.Active)?.FirstOrDefault().GetDueDate(item.ClassSchedule.EndDateTime) >= DateTime.UtcNow.Date,
                        DueDate = item.ClassSchedule?.ILA?.CBTs.Where(x => x.Active)?.FirstOrDefault().GetDueDate(item.ClassSchedule.EndDateTime)
                    });
                }
            }
            foreach (var tq in taskQualList.Where(m=>m.TQEmpSetting?.ReleaseDate <= DateTime.UtcNow))
            {
                var taskTitle = await _taskService.FindQuery(x => x.Id == tq.TaskId).Select(s => new Domain.Entities.Core.Task { Id = s.Id, Description = s.Description }).FirstOrDefaultAsync();
                if (taskTitle != null)
                {
                    toReturnList.Add(new DueTrainingDataVM()
                    {
                        Type = "TQ",
                        Title = taskTitle.Description,
                        Status = tq?.TaskQualificationStatus?.Name ?? "N/A",
                        Id = tq.Id,
                        ParentId = tq.TaskId,
                        CanStart = tq.DueDate > DateTime.UtcNow,
                        DueDate = tq.DueDate
                    });
                }
            }
            return toReturnList.OrderByDescending(x => x.DueDate).Select(x => new
            {x.Type,x.Title,x.Status,x.Id,x.ParentId,x.CanStart,DueDate = x.DueDateFormatted});
        }

        public async Task<bool> CheckCourseAvailabilityForSelfRegestration()
        {
            var empId = await GetEmployeeId();
            var emp = await _employeeDomainService.FindQuery(x => x.Id == empId).Select(s => new Employee { Id = s.Id, PersonId = s.PersonId }).FirstOrDefaultAsync();
            if (emp == null)
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
            else
            {
                emp.Person = await _personService.FindQuery(x => x.Id == emp.PersonId).FirstOrDefaultAsync();
                var ilas = await _ilaDomainService.FindQuery(x => x.Active == true).Select(s => new ILA { Id = s.Id, Active = s.Active }).ToListAsync();
                foreach (var ila in ilas)
                {
                    var selfRegOption = await _selfRegService.FindQuery(x => x.ILAId == ila.Id).FirstOrDefaultAsync();
                    if (selfRegOption != null)
                    {
                        if (selfRegOption.MakeAvailableForSelfReg)
                        {
                            var seats = ila.ClassSize ?? 30;
                            var classSchedules = await _classScheduleDomainService.FindQuery(x => x.ILAID == ila.Id).ToListAsync();
                            foreach (var classSchedule in classSchedules)
                            {
                                var registeredStudents = await _classScheduleEmpService.GetCount(x => x.EmployeeId != empId && x.IsEnrolled == true && x.ClassScheduleId == classSchedule.Id);
                                if (seats - registeredStudents > 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
            //var result = await _classScheduleService.GetSelfRegAvailableCoursesAsync();
            //if (result.Count > 0)
            //{
            //    return true;
            //}
            //return false;
        }

        public async Task<RequiredEMPSettingVM> GetEMPSettings(int ilaID)
        {
            var userId = await GetEmployeeId();
            var ilaDetails = await _ilaDomainService.GetWithIncludeAsync(ilaID, new string[] { "TestReleaseEMPSettings.FinalTest", "EvaluationReleaseEMPSetting", "TQILAEmpSettings" });

            return new RequiredEMPSettingVM
            {
                PreTestRequired = ilaDetails.TestReleaseEMPSettings != null && ilaDetails.TestReleaseEMPSettings.PreTestRequired,
                CBTRequiredForCourse = ilaDetails.CBTRequiredForCourse,
                WrittenTest = ilaDetails.TestReleaseEMPSettings != null && ilaDetails.TestReleaseEMPSettings.FinalTest != null,
                TaskQualification = ilaDetails.TQILAEmpSettings?.FirstOrDefault()?.TQRequired ?? false,
                StudentEvaluation = ilaDetails.EvaluationReleaseEMPSetting != null && !ilaDetails.EvaluationReleaseEMPSetting.EvaluationRequired,
                OralTest = false,
                SimulatorScenerio = false
            };
        }
        public async Task<RequiredEMPSettingVM> GetEMPSettingsByClass(int classScheduleId)
        {
            var userId = await GetEmployeeId();
            var classsDetails = await _classScheduleDomainService.GetWithIncludeAsync(classScheduleId, new string[] { "ClassSchedule_TestReleaseEMPSettings.FinalTest", "ILA.EvaluationReleaseEMPSetting", "ClassSchedule_TQEMPSettings" });

            return new RequiredEMPSettingVM
            {
                PreTestRequired = classsDetails.ClassSchedule_TestReleaseEMPSettings != null && classsDetails.ClassSchedule_TestReleaseEMPSettings.PreTestRequired,
                CBTRequiredForCourse = classsDetails.ILA.CBTRequiredForCourse,
                WrittenTest = classsDetails.ClassSchedule_TestReleaseEMPSettings != null && classsDetails.ClassSchedule_TestReleaseEMPSettings.FinalTest != null,
                TaskQualification = classsDetails.ClassSchedule_TQEMPSettings?.TQRequired ?? false,
                StudentEvaluation = classsDetails.ILA.EvaluationReleaseEMPSetting != null && !classsDetails.ILA.EvaluationReleaseEMPSetting.EvaluationRequired,
                OralTest = false,
                SimulatorScenerio = false
            };
        }
        public async Task<object> GetTrainingScheduleFinalInProgress(DateTime startDate, DateTime endDate)
        {
            var weekStartDate = startDate;
            var weekEndDate = endDate;
            var toReturnList = new List<TrainingsVM>();
            var toReturnProcedureReviewList = new List<object>();
            var toReturnTQList = new List<object>();
            var userId = await GetEmployeeId();

            var employee = await _employeeDomainService.GetWithIncludeAsync(userId, new string[] { "Person" });
            var completeStatusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Completed").FirstOrDefault().Id;
            var incompleteStatusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "In Progress").FirstOrDefault().Id;
            var notStartedStatusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Not Started").FirstOrDefault().Id;
            /// I AM HERE 
            var classScheduleRoster = await _classScheduleRosterService.FindQueryWithIncludeAsync(x => x.EmpId == userId && x.IsReleased == true, new string[] { "Test", "ClassSchedule.ClassSchedule_TestReleaseEMPSettings", "TestType", "ClassSchedule.Location" }, true).ToListAsync();
            var evaluationRosters = await _EvaluationRosterService.FindQueryWithIncludeAsync(x => x.EmployeeId == userId && x.IsReleased == true, new string[] { "ClassScheduleInfo.ILA.EvaluationReleaseEMPSetting", "StudentEvaluationInfo", "ClassScheduleInfo.Location" }, true).ToListAsync();
            var proceduresReviews = await _procedureReviewService.FindQueryWithIncludeAsync(x => x.EmployeeId == userId, new string[] { "ProcedureReview" }, true).ToListAsync();
            var taskQualList = await _taskQualService.FindQueryWithIncludeAsync(x => x.EmpId == userId && x.TaskQualificationStatus.Name == "Pending" && x.IsReleasedToEMP && x.TQEmpSetting != null && x.TQEmpSetting.ReleaseDate != null && DateTime.Compare(x.TQEmpSetting.ReleaseDate.Value.Date, DateTime.UtcNow.Date) <= 0 && DateTime.Compare(x.DueDate.Value.Date, weekEndDate.Date) <= 0 && DateTime.Compare(x.DueDate.Value.Date, weekStartDate.Date) >= 0, new string[] { "TQEmpSetting", "TaskQualificationStatus" }).ToListAsync();
            proceduresReviews = proceduresReviews?.Where(x => DateTime.Compare(x.ProcedureReview.EndDateTime.Date, weekStartDate.Date) >= 0 && DateTime.Compare(x.ProcedureReview.EndDateTime.Date, weekEndDate.Date) <= 0 && x.IsStarted && !x.IsCompleted).ToList();
            evaluationRosters = evaluationRosters?.Where(x =>
            {
                var setting = x.ClassScheduleInfo?.ILA?.EvaluationReleaseEMPSetting;
                return DateTime.Compare((setting != null ? setting.GetDueDate(x.ClassScheduleInfo.EndDateTime) : x.ClassScheduleInfo.EndDateTime.AddDays(10)).Date, weekEndDate.Date) <= 0 && x.IsStarted && !x.IsCompleted;
            })?.ToList();
            classScheduleRoster = classScheduleRoster?.Where(x =>
            {
                var setting = x.ClassSchedule?.ClassSchedule_TestReleaseEMPSettings;
                return DateTime.Compare((setting != null ? setting.GetDueDate(x.ClassSchedule.EndDateTime) : x.ClassSchedule.EndDateTime.AddDays(10)).Date, weekStartDate.Date) >= 0 && DateTime.Compare((setting != null ? setting.GetDueDate(x.ClassSchedule.EndDateTime) : x.ClassSchedule.EndDateTime.AddDays(10)).Date, weekEndDate.Date) <= 0 && x.StatusId == incompleteStatusId;
            }).ToList();
            if (proceduresReviews != null)
            {
                foreach (var item in proceduresReviews)
                {
                    if (!item.IsCompleted)
                    {
                        toReturnProcedureReviewList.Add(new
                        {
                            type = "Procedure Review",
                            Title = item.ProcedureReview?.ProcedureReviewTitle,
                            status = item.IsStarted && !item.IsCompleted ? "In Progress" : !item.IsStarted ? "Not Started" : item.IsCompleted ? "Completed" : "Invalid",
                            Id = item.ProcedureReview?.Id,
                            procedureId = item.ProcedureReview?.ProcedureId,
                            dueDate = item.ProcedureReview.EndDateTime.Date,
                            startDateTime = item.ProcedureReview.StartDateTime,
                            endDateTime = item.ProcedureReview.EndDateTime,
                            isCollapsable = false,
                            Location = "N/A"
                        });
                    }
                }
            }
            if (evaluationRosters != null)
            {
                foreach (var item in evaluationRosters)
                {
                    if (toReturnList.Any(x => x.ParentId == item.ClassScheduleId))
                    {
                        var returnItem = toReturnList.FirstOrDefault(x => x.ilaId == item.ClassScheduleInfo.ILAID);
                        if (!item.IsCompleted)
                        {
                            var setting = item.ClassScheduleInfo?.ILA?.EvaluationReleaseEMPSetting;
                            returnItem.Trainings.Add(new TrainingItems
                            {
                                type = "Student Evaluation",
                                Id = item.StudentEvaluationInfo?.Id ?? 0,
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassScheduleInfo.EndDateTime) : item.ClassScheduleInfo.EndDateTime.AddDays(10)).Date,
                                Title = item.StudentEvaluationInfo.Title,
                                TestType = "NA",
                                ParentId = item.ClassScheduleId.Value,
                                Status = item.IsStarted && !item.IsCompleted ? "In Progress" : !item.IsStarted ? "Not Started" : item.IsCompleted ? "Completed" : "Invalid",
                            });
                        }

                    }
                    else
                    {
                        var ILAItemMOdels = new TrainingsVM
                        {
                            type = "ILA",
                            IsCollapsable = true,
                            Location = item.ClassScheduleInfo.Location?.LocAddress,
                            StartDateTime = item.ClassScheduleInfo.StartDateTime,
                            EndDateTime = item.ClassScheduleInfo.EndDateTime,
                            ilaId = item.ClassScheduleInfo.ILAID ?? 0,
                            ParentId = item.ClassScheduleId.Value,
                            IlaTitle = item.ClassScheduleInfo.ILA.Name,
                        };
                        ILAItemMOdels.Trainings = new List<TrainingItems>();
                        if (!item.IsCompleted)
                        {
                            var setting = item.ClassScheduleInfo?.ILA?.EvaluationReleaseEMPSetting;
                            ILAItemMOdels.Trainings.Add(new TrainingItems
                            {
                                type = "Student Evaluation",
                                Id = item.StudentEvaluationInfo?.Id ?? 0,
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassScheduleInfo.EndDateTime) : item.ClassScheduleInfo.EndDateTime.AddDays(10)).Date,
                                TestType = "NA",
                                ParentId = item.ClassScheduleId.Value,
                                Title = item.StudentEvaluationInfo.Title,
                                Status = item.IsStarted && !item.IsCompleted ? "In Progress" : !item.IsStarted ? "Not Started" : item.IsCompleted ? "Completed" : "Invalid",
                            });
                        }
                        toReturnList.Add(ILAItemMOdels);
                    }

                }
            }
            if (classScheduleRoster != null)
            {
                foreach (var item in classScheduleRoster)
                {
                    if (toReturnList.Any(x => x.ParentId == item.ClassScheduleId))
                    {
                        var returnItem = toReturnList.FirstOrDefault(x => x.ilaId == item.ClassSchedule.ILAID);
                        if (item.StatusId != completeStatusId)
                        {
                            var setting = item.ClassSchedule?.ClassSchedule_TestReleaseEMPSettings;
                            returnItem.Trainings.Add(new TrainingItems
                            {
                                type = "Test",
                                Id = item.TestId,
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassSchedule.EndDateTime) : item.ClassSchedule.EndDateTime.AddDays(10)).Date,
                                Title = item.Test.TestTitle,
                                TestType = item.TestType.Description,
                                ParentId = item.ClassScheduleId.Value,
                                Status = item.StatusId == notStartedStatusId ? "Not Started" : item.StatusId == incompleteStatusId ? "In Progress" : "Completed",
                            });
                        }

                    }
                    else
                    {
                        var ILAItemMOdels = new TrainingsVM
                        {
                            type = "ILA",
                            IsCollapsable = true,
                            Location = item.ClassSchedule.Location.LocName,
                            ilaId = item.ClassSchedule.ILAID ?? 0,
                            IlaTitle = item.ClassSchedule.ILA.Name,
                            StartDateTime = item.ClassSchedule.StartDateTime,
                            ParentId = item.ClassScheduleId.Value,
                            EndDateTime = item.ClassSchedule.EndDateTime
                        };
                        ILAItemMOdels.Trainings = new List<TrainingItems>();
                        if (item.StatusId != completeStatusId)
                        {
                            var setting = item.ClassSchedule?.ClassSchedule_TestReleaseEMPSettings;
                            ILAItemMOdels.Trainings.Add(new TrainingItems
                            {
                                type = "Test",
                                Id = item.TestId,
                                Title = item.Test.TestTitle,
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassSchedule.EndDateTime) : item.ClassSchedule.EndDateTime.AddDays(10)).Date,
                                ParentId = item.ClassScheduleId.Value,
                                TestType = item.TestType.Description,
                                Status = item.StatusId == notStartedStatusId ? "Not Started" : item.StatusId == incompleteStatusId ? "In Progress" : "Completed",
                            });
                        }
                        toReturnList.Add(ILAItemMOdels);
                    }

                }
            }
            if (taskQualList != null)
            {
                foreach (var taskQual in taskQualList)
                {
                    var task = await _taskService.FindQuery(x => x.Id == taskQual.TaskId).FirstOrDefaultAsync();
                    var taskNumber = await _taskAppService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
                    var setting = await _taskQualSettingService.FindQuery(x => x.TaskQualificationId == taskQual.Id).FirstOrDefaultAsync();
                    toReturnTQList.Add(new
                    {
                        ParentId = taskQual.TaskId,
                        Id = taskQual.Id,
                        Type = "Task Qualification",
                        Title = taskNumber.Letter + taskNumber.DANumber + "." + taskNumber.SDANumber + "." + task.Number + " " + task.Description,
                        Status = taskQual.TaskQualificationStatus.Name ?? "N/A",
                        StartDateTime = setting.ReleaseDate ?? null,
                        EndDateTime = taskQual.DueDate ?? null,
                    });
                }
            }
            var toReturnObject = new
            {
                ProcedureReviewList = toReturnProcedureReviewList,
                TaskQualificationList = toReturnTQList,
                IlaList = toReturnList,
                FirstName = employee.Person.FirstName,
                MiddleName = employee.Person.MiddleName,
                LastName = employee.Person?.LastName,
                picture = employee.Person?.Image
            };

            return toReturnObject;

        }

        public async Task<object> GetTrainingScheduleToday(DateTime date)
        {
            var Today = date;
            var toReturnList = new List<TrainingsVM>();
            var toReturnProcedureReviewList = new List<object>();
            var toReturnTQList = new List<object>();
            var userId = await GetEmployeeId();

            var employee = await _employeeDomainService.GetWithIncludeAsync(userId, new string[] { "Person" });
            var completeStatusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Completed").FirstOrDefault().Id;
            var incompleteStatusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "In Progress").FirstOrDefault().Id;
            var notStartedStatusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Not Started").FirstOrDefault().Id;

            var classScheduleRoster = await _classScheduleRosterService.FindQueryWithIncludeAsync(x => x.EmpId == userId && x.IsReleased == true, new string[] { "Test", "ClassSchedule.ClassSchedule_TestReleaseEMPSettings", "TestType", "ClassSchedule.Location", "ClassSchedule.ILA" }, true).ToListAsync();
            var evaluationRosters = await _EvaluationRosterService.FindQueryWithIncludeAsync(x => x.EmployeeId == userId && x.IsReleased == true, new string[] { "ClassScheduleInfo.ILA.EvaluationReleaseEMPSetting", "StudentEvaluationInfo", "ClassScheduleInfo.Location" }, true).ToListAsync();
            var proceduresReviews = await _procedureReviewService.FindQueryWithIncludeAsync(x => x.EmployeeId == userId, new string[] { "ProcedureReview" }, true).ToListAsync();
            var taskQualList = await _taskQualService.FindQueryWithIncludeAsync(x => x.EmpId == userId && x.TaskQualificationStatus.Name != "Completed" && x.IsReleasedToEMP && x.TQEmpSetting != null && x.TQEmpSetting.ReleaseDate != null && DateTime.Compare(x.TQEmpSetting.ReleaseDate.Value.Date, Today.Date) <= 0 && DateTime.Compare(x.DueDate.Value.Date, Today.Date) == 0, new string[] { "TQEmpSetting", "TaskQualificationStatus" }).ToListAsync();
            proceduresReviews = proceduresReviews?.Where(x => DateTime.Compare(x.ProcedureReview.StartDateTime.Date, Today.Date) == 0 && !x.IsStarted).ToList();
            evaluationRosters = evaluationRosters?.Where(x => DateTime.Compare(x.ClassScheduleInfo.StartDateTime.Date, Today.Date) == 0 && !x.IsStarted)?.ToList();
            classScheduleRoster = classScheduleRoster?.Where(x =>
            {
                var setting = x.ClassSchedule?.ClassSchedule_TestReleaseEMPSettings;
                return DateTime.Compare((setting != null ? setting.GetDueDate(x.ClassSchedule.EndDateTime) : x.ClassSchedule.EndDateTime.AddDays(10)).Date, Today.Date) == 0 && x.IsReleased == true;
            }
            ).ToList();
            if (proceduresReviews != null)
            {
                foreach (var item in proceduresReviews)
                {
                    if (!item.IsCompleted)
                    {
                        toReturnProcedureReviewList.Add(new
                        {
                            type = "Procedure Review",
                            Title = item.ProcedureReview?.ProcedureReviewTitle,
                            status = item.IsStarted && !item.IsCompleted ? "In Progress" : !item.IsStarted ? "Not Started" : item.IsCompleted ? "Completed" : "Invalid",
                            Id = item.ProcedureReview?.Id,
                            procedureId = item.ProcedureReview?.ProcedureId,
                            dueDate = item.ProcedureReview.EndDateTime.Date,
                            startDateTime = item.ProcedureReview.StartDateTime,
                            endDateTime = item.ProcedureReview.EndDateTime,
                            isCollapsable = false,
                            Location = "N/A"
                        });
                    }
                }
            }
            if (evaluationRosters != null)
            {
                foreach (var item in evaluationRosters)
                {
                    if (toReturnList.Any(x => x.ParentId == item.ClassScheduleId))
                    {
                        var returnItem = toReturnList.FirstOrDefault(x => x.ilaId == item.ClassScheduleInfo.ILAID);
                        if (!item.IsCompleted)
                        {
                            var setting = item.ClassScheduleInfo?.ILA?.EvaluationReleaseEMPSetting;
                            returnItem.Trainings.Add(new TrainingItems
                            {
                                type = "Student Evaluation",
                                Id = item.StudentEvaluationInfo?.Id ?? 0,
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassScheduleInfo.EndDateTime) : item.ClassScheduleInfo.EndDateTime.AddDays(10)).Date,
                                Title = item.StudentEvaluationInfo.Title,
                                TestType = "NA",
                                ParentId = item.ClassScheduleId.Value,
                                Status = item.IsStarted && !item.IsCompleted ? "In Progress" : !item.IsStarted ? "Not Started" : item.IsCompleted ? "Completed" : "Invalid",
                            });
                        }

                    }
                    else
                    {
                        var ILAItemMOdels = new TrainingsVM
                        {
                            type = "ILA",
                            IsCollapsable = true,
                            Location = item.ClassScheduleInfo.Location?.LocAddress,
                            StartDateTime = item.ClassScheduleInfo.StartDateTime,
                            EndDateTime = item.ClassScheduleInfo.EndDateTime,
                            ilaId = item.ClassScheduleInfo.ILAID ?? 0,
                            ParentId = item.ClassScheduleId.Value,
                            IlaTitle = item.ClassScheduleInfo.ILA.Name,
                        };
                        ILAItemMOdels.Trainings = new List<TrainingItems>();
                        if (!item.IsCompleted)
                        {
                            var setting = item.ClassScheduleInfo?.ILA?.EvaluationReleaseEMPSetting;
                            ILAItemMOdels.Trainings.Add(new TrainingItems
                            {
                                type = "Student Evaluation",
                                Id = item.StudentEvaluationInfo?.Id ?? 0,
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassScheduleInfo.EndDateTime) : item.ClassScheduleInfo.EndDateTime.AddDays(10)).Date,
                                TestType = "NA",
                                ParentId = item.ClassScheduleId.Value,
                                Title = item.StudentEvaluationInfo.Title,
                                Status = item.IsStarted && !item.IsCompleted ? "In Progress" : !item.IsStarted ? "Not Started" : item.IsCompleted ? "Completed" : "Invalid",
                            });
                        }
                        toReturnList.Add(ILAItemMOdels);
                    }

                }
            }
            if (classScheduleRoster != null)
            {
                foreach (var item in classScheduleRoster)
                {
                    if (toReturnList.Any(x => x.ParentId == item.ClassScheduleId))
                    {
                        var returnItem = toReturnList.FirstOrDefault(x => x.ilaId == item.ClassSchedule.ILAID);
                        if (item.StatusId != completeStatusId)
                        {
                            var setting = item.ClassSchedule?.ClassSchedule_TestReleaseEMPSettings;
                            returnItem.Trainings.Add(new TrainingItems
                            {
                                type = "Test",
                                Id = item.TestId,
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassSchedule.EndDateTime) : item.ClassSchedule.EndDateTime.AddDays(10)).Date,
                                Title = item.Test.TestTitle,
                                TestType = item.TestType.Description,
                                ParentId = item.ClassScheduleId.Value,
                                Status = item.StatusId == notStartedStatusId ? "Not Started" : item.StatusId == incompleteStatusId ? "In Progress" : "Completed",
                            });
                        }

                    }
                    else
                    {
                        var ILAItemMOdels = new TrainingsVM
                        {
                            type = "ILA",
                            IsCollapsable = true,
                            Location = item.ClassSchedule.Location.LocName,
                            ilaId = item.ClassSchedule.ILAID ?? 0,
                            IlaTitle = item.ClassSchedule.ILA.Name,
                            StartDateTime = item.ClassSchedule.StartDateTime,
                            ParentId = item.ClassScheduleId.Value,
                            EndDateTime = item.ClassSchedule.EndDateTime
                        };
                        ILAItemMOdels.Trainings = new List<TrainingItems>();
                        if (item.StatusId != completeStatusId)
                        {
                            var setting = item.ClassSchedule?.ClassSchedule_TestReleaseEMPSettings;
                            ILAItemMOdels.Trainings.Add(new TrainingItems
                            {
                                type = "Test",
                                Id = item.TestId,
                                Title = item.Test.TestTitle,
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassSchedule.EndDateTime) : item.ClassSchedule.EndDateTime.AddDays(10)).Date,
                                ParentId = item.ClassScheduleId.Value,
                                TestType = item.TestType.Description,
                                Status = item.StatusId == notStartedStatusId ? "Not Started" : item.StatusId == incompleteStatusId ? "In Progress" : "Completed",
                            });
                        }
                        toReturnList.Add(ILAItemMOdels);
                    }

                }
            }
            if (taskQualList != null)
            {
                foreach (var taskQual in taskQualList)
                {
                    var task = await _taskService.FindQuery(x => x.Id == taskQual.TaskId).FirstOrDefaultAsync();
                    var taskNumber = await _taskAppService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
                    var setting = await _taskQualSettingService.FindQuery(x => x.TaskQualificationId == taskQual.Id).FirstOrDefaultAsync();
                    toReturnTQList.Add(new
                    {
                        ParentId = taskQual.TaskId,
                        Id = taskQual.Id,
                        Type = "Task Qualification",
                        Title = taskNumber.Letter + taskNumber.DANumber + "." + taskNumber.SDANumber + "." + task.Number + " " + task.Description,
                        Status = taskQual.TaskQualificationStatus.Name ?? "N/A",
                        StartDateTime = setting.ReleaseDate ?? null,
                        EndDateTime = taskQual.DueDate ?? null,
                    });
                }
            }
            var toReturnObject = new
            {
                ProcedureReviewList = toReturnProcedureReviewList,
                TaskQualificationList = toReturnTQList,
                IlaList = toReturnList,
                FirstName = employee.Person.FirstName,
                MiddleName = employee.Person.MiddleName,
                LastName = employee.Person?.LastName,
                picture = employee.Person?.Image
            };

            return toReturnObject;

        }


        public async Task<object> GetTrainingScheduleFinal(GetDueTrainingOptions options)
        {

            var toReturnList = new List<TrainingsVM>();
            var toReturnProcedureReviewList = new List<object>();
            var toReturnTQList = new List<object>();
            var userId = await GetEmployeeId();

            var employee = await _employeeDomainService.GetWithIncludeAsync(userId, new string[] { "Person" });
            var completeStatusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Completed").FirstOrDefault().Id;
            var incompleteStatusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "In Progress").FirstOrDefault().Id;
            var notStartedStatusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Not Started").FirstOrDefault().Id;

            var classScheduleRoster = await _classScheduleRosterService.FindQueryWithIncludeAsync(x => x.EmpId == userId && x.IsReleased == true, new string[] { "Test", "ClassSchedule.ClassSchedule_TestReleaseEMPSettings", "TestType", "ClassSchedule.Location" }, true).ToListAsync();
            var evaluationRosters = await _EvaluationRosterService.FindQueryWithIncludeAsync(x => x.EmployeeId == userId && x.IsReleased == true, new string[] { "ClassScheduleInfo.ILA.EvaluationReleaseEMPSetting", "StudentEvaluationInfo", "ClassScheduleInfo.Location" }, true).ToListAsync();
            var proceduresReviews = await _procedureReviewService.FindQueryWithIncludeAsync(x => x.EmployeeId == userId, new string[] { "ProcedureReview" }, true).ToListAsync();
            var taskQualList = await _taskQualService.FindQueryWithIncludeAsync(x => x.EmpId == userId && x.TaskQualificationStatus.Name != "Completed" && x.IsReleasedToEMP && x.TQEmpSetting != null && x.TQEmpSetting.ReleaseDate != null && DateTime.Compare(x.TQEmpSetting.ReleaseDate.Value.Date, DateTime.UtcNow.Date) <= 0 && DateTime.Compare(x.DueDate.Value.Date, options.StartDate.Date) >= 0 && DateTime.Compare(x.DueDate.Value.Date, options.EndDate.Date) <= 0, new string[] { "TQEmpSetting", "TaskQualificationStatus" }).ToListAsync();
            proceduresReviews = proceduresReviews?.Where(x => DateTime.Compare(x.ProcedureReview.EndDateTime.Date, options.StartDate.Date) >= 0 && DateTime.Compare(x.ProcedureReview.EndDateTime.Date, options.EndDate.Date) <= 0).ToList();
            evaluationRosters = evaluationRosters?.Where(x =>
            {
                var setting = x.ClassScheduleInfo?.ILA?.EvaluationReleaseEMPSetting;
                return DateTime.Compare((setting != null ? setting.GetDueDate(x.ClassScheduleInfo.EndDateTime) : x.ClassScheduleInfo.EndDateTime.AddDays(10)).Date, options.StartDate.Date) >= 0 && DateTime.Compare((setting != null ? setting.GetDueDate(x.ClassScheduleInfo.EndDateTime) : x.ClassScheduleInfo.EndDateTime.AddDays(10)).Date, options.EndDate.Date) <= 0;
            })?.ToList();
            classScheduleRoster = classScheduleRoster?.Where(x =>
            {
                var setting = x.ClassSchedule?.ClassSchedule_TestReleaseEMPSettings;
                return DateTime.Compare((setting != null ? setting.GetDueDate(x.ClassSchedule.EndDateTime) : x.ClassSchedule.EndDateTime.AddDays(10)).Date, options.StartDate.Date) >= 0 && DateTime.Compare((setting != null ? setting.GetDueDate(x.ClassSchedule.EndDateTime) : x.ClassSchedule.EndDateTime.AddDays(10)).Date, options.EndDate.Date) <= 0;
            }).ToList();
            if (proceduresReviews != null)
            {
                foreach (var item in proceduresReviews)
                {
                    if (!item.IsCompleted)
                    {
                        toReturnProcedureReviewList.Add(new
                        {
                            type = "Procedure Review",
                            Title = item.ProcedureReview?.ProcedureReviewTitle,
                            status = item.IsStarted && !item.IsCompleted ? "In Progress" : !item.IsStarted ? "Not Started" : item.IsCompleted ? "Completed" : "Invalid",
                            Id = item.ProcedureReview?.Id,
                            procedureId = item.ProcedureReview?.ProcedureId,
                            dueDate = item.ProcedureReview.EndDateTime.Date,
                            startDateTime = item.ProcedureReview.StartDateTime,
                            endDateTime = item.ProcedureReview.EndDateTime,
                            isCollapsable = false,
                            Location = "N/A"
                        });
                    }
                }
            }
            if (evaluationRosters != null)
            {
                foreach (var item in evaluationRosters)
                {
                    if (toReturnList.Any(x => x.ParentId == item.ClassScheduleId))
                    {
                        var returnItem = toReturnList.FirstOrDefault(x => x.ilaId == item.ClassScheduleInfo.ILAID);
                        if (!item.IsCompleted)
                        {
                            var setting = item.ClassScheduleInfo?.ILA?.EvaluationReleaseEMPSetting;
                            returnItem.Trainings.Add(new TrainingItems
                            {
                                type = "Student Evaluation",
                                Id = item.StudentEvaluationInfo?.Id ?? 0,
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassScheduleInfo.EndDateTime) : item.ClassScheduleInfo.EndDateTime.AddDays(10)).Date,
                                Title = item.StudentEvaluationInfo.Title,
                                TestType = "NA",
                                ParentId = item.ClassScheduleId.Value,
                                Status = item.IsStarted && !item.IsCompleted ? "In Progress" : !item.IsStarted ? "Not Started" : item.IsCompleted ? "Completed" : "Invalid",
                            });
                        }

                    }
                    else
                    {
                        var ILAItemMOdels = new TrainingsVM
                        {
                            type = "ILA",
                            IsCollapsable = true,
                            Location = item.ClassScheduleInfo?.Location?.LocAddress ?? "N/A",
                            StartDateTime = item.ClassScheduleInfo.StartDateTime,
                            EndDateTime = item.ClassScheduleInfo.EndDateTime,
                            ilaId = item.ClassScheduleInfo.ILAID ?? 0,
                            ParentId = item.ClassScheduleId.Value,
                            IlaTitle = item.ClassScheduleInfo.ILA.Name,
                        };
                        ILAItemMOdels.Trainings = new List<TrainingItems>();
                        if (!item.IsCompleted)
                        {
                            var setting = item.ClassScheduleInfo?.ILA?.EvaluationReleaseEMPSetting;
                            ILAItemMOdels.Trainings.Add(new TrainingItems
                            {
                                type = "Student Evaluation",
                                Id = item.StudentEvaluationInfo?.Id ?? 0,
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassScheduleInfo.EndDateTime) : item.ClassScheduleInfo.EndDateTime.AddDays(10)).Date,
                                TestType = "NA",
                                ParentId = item.ClassScheduleId.Value,
                                Title = item.StudentEvaluationInfo.Title,
                                Status = item.IsStarted && !item.IsCompleted ? "In Progress" : !item.IsStarted ? "Not Started" : item.IsCompleted ? "Completed" : "Invalid",
                            });
                        }
                        toReturnList.Add(ILAItemMOdels);
                    }

                }
            }
            if (classScheduleRoster != null)
            {
                foreach (var item in classScheduleRoster)
                {
                    if (toReturnList.Any(x => x.ParentId == item.ClassScheduleId))
                    {
                        var returnItem = toReturnList.FirstOrDefault(x => x.ilaId == item.ClassSchedule.ILAID);
                        if (item.StatusId != completeStatusId)
                        {
                            var setting = item.ClassSchedule?.ClassSchedule_TestReleaseEMPSettings;
                            returnItem.Trainings.Add(new TrainingItems
                            {
                                type = "Test",
                                Id = item.TestId,
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassSchedule.EndDateTime) : item.ClassSchedule.EndDateTime.AddDays(10)).Date,
                                Title = item.Test.TestTitle,
                                TestType = item.TestType.Description,
                                ParentId = item.ClassScheduleId.Value,
                                Status = item.StatusId == notStartedStatusId ? "Not Started" : item.StatusId == incompleteStatusId ? "In Progress" : "Completed",
                            });
                        }

                    }
                    else
                    {
                        var ILAItemMOdels = new TrainingsVM
                        {
                            type = "ILA",
                            IsCollapsable = true,
                            Location = item?.ClassSchedule?.Location?.LocAddress ?? "N/A",
                            ilaId = item?.ClassSchedule.ILAID ?? 0,
                            IlaTitle = item?.ClassSchedule?.ILA?.Name ?? "N/A",
                            StartDateTime = item.ClassSchedule.StartDateTime,
                            ParentId = item.ClassScheduleId.Value,
                            EndDateTime = item.ClassSchedule.EndDateTime
                        };
                        ILAItemMOdels.Trainings = new List<TrainingItems>();
                        if (item.StatusId != completeStatusId)
                        {
                            var setting = item.ClassSchedule?.ClassSchedule_TestReleaseEMPSettings;
                            ILAItemMOdels.Trainings.Add(new TrainingItems
                            {
                                type = "Test",
                                Id = item.TestId,
                                Title = item?.Test?.TestTitle ?? "N/A",
                                DueDate = (setting != null ? setting.GetDueDate(item.ClassSchedule.EndDateTime) : item.ClassSchedule.EndDateTime.AddDays(10)).Date,
                                ParentId = item.ClassScheduleId.Value,
                                TestType = item.TestType.Description,
                                Status = item.StatusId == notStartedStatusId ? "Not Started" : item.StatusId == incompleteStatusId ? "In Progress" : "Completed",
                            });
                        }
                        toReturnList.Add(ILAItemMOdels);
                    }

                }
            }
            if (taskQualList != null)
            {
                foreach (var taskQual in taskQualList)
                {
                    var task = await _taskService.FindQuery(x => x.Id == taskQual.TaskId).FirstOrDefaultAsync();
                    var taskNumber = await _taskAppService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
                    var setting = await _taskQualSettingService.FindQuery(x => x.TaskQualificationId == taskQual.Id).FirstOrDefaultAsync();
                    toReturnTQList.Add(new
                    {
                        ParentId = taskQual.TaskId,
                        Id = taskQual.Id,
                        Type = "Task Qualification",
                        Title = taskNumber.Letter + taskNumber.DANumber + "." + taskNumber.SDANumber + "." + task.Number + " " + task.Description,
                        Status = taskQual?.TaskQualificationStatus?.Name ?? "N/A",
                        StartDateTime = setting.ReleaseDate ?? null,
                        EndDateTime = taskQual.DueDate ?? null,
                    });
                }
            }
            var toReturnObject = new
            {
                ProcedureReviewList = toReturnProcedureReviewList,
                TaskQualificationList = toReturnTQList,
                IlaList = toReturnList,
                FirstName = employee.Person.FirstName,
                MiddleName = employee.Person.MiddleName,
                LastName = employee.Person?.LastName,
                picture = employee.Person?.Image
            };

            return toReturnObject;

        }
        public async Task<ClassInfoVM> GetClassInfoAsync(int id)
        {
            var cs = await _classScheduleDomainService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cs == null)
            {
                throw new BadHttpRequestException(message: _localizer["ClassNotFoundException"]);
            }
            else
            {
                ClassInfoVM csInfo = new ClassInfoVM();
                var ila = await _ilaDomainService.FindQuery(x => x.Id == cs.ILAID).FirstOrDefaultAsync();
                if (ila != null)
                {
                    var loc = await _locationService.FindQuery(x => x.Id == cs.LocationId).FirstOrDefaultAsync();
                    var inst = await _instService.FindQuery(x => x.Id == cs.InstructorId).FirstOrDefaultAsync();
                    var de = await _deliverMethService.FindQuery(x => x.Id == ila.DeliveryMethodId).FirstOrDefaultAsync();
                    var reqData = await GetEMPSettingsByClass(id);

                    csInfo.ILANumber = ila.Number ?? "N/A";
                    csInfo.ILAName = ila.Name;
                    csInfo.CourseInstruction = cs.SpecialInstructions;
                    csInfo.WebinarLink = cs.WebinarLink;
                    csInfo.StartDate = cs.StartDateTime;
                    csInfo.EndDate = cs.EndDateTime;
                    csInfo.Instructor = inst?.InstructorName;
                    csInfo.PreTestRequired = (reqData?.PreTestRequired).GetValueOrDefault();
                    csInfo.CBTRequired = (reqData?.CBTRequiredForCourse).GetValueOrDefault();
                    csInfo.FinalTestRequired = (reqData?.WrittenTest).GetValueOrDefault();
                    csInfo.StudentEvaluationRequired = (reqData?.StudentEvaluation).GetValueOrDefault();
                    csInfo.TaskQualificationRequired = (reqData?.TaskQualification).GetValueOrDefault();
                    csInfo.Location = loc?.LocAddress;
                    if (de == null)
                    {
                        csInfo.DeliveryMethod = "N/A";
                    }
                    else
                    {
                        csInfo.DeliveryMethod = de.Name;
                    }
                    return csInfo;
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["ILANotFoundException"]);
                }
            }
        }

        public async Task<object> GetCourseInfoByILAId(int ilaId)
        {
            var ilaInfo = await _ilaDomainService.GetWithIncludeAsync(ilaId, new string[] { "DeliveryMethod", "ILA_NercStandard_Links.NercStandard", "ILA_NercStandard_Links.NercStandardMember" });
            ilaInfo.ILA_PreRequisite_Links = await _ila_preReq_LinkService.FindQueryWithIncludeAsync(x => x.ILAId == ilaId, new string[] { "PreRequisite" }).ToListAsync();
            var linkedObjectives = await _ilaService.GetAllLinkedObjectivesAsync(ilaId);
            var isNercStandards = ilaInfo?.DeliveryMethod?.IsNerc ?? true;
            var data = ilaInfo?.ILA_NercStandard_Links;
            float standardRelatedHours = 0;
            float operatingTopics = 0;
            float simulationHours = 0;
            float emergencyHours = 0;
            double pmjTotalTrainingHours = ilaInfo.TotalTrainingHours ?? 0.0d;
            var certificationLink = await _ilaCertLinkService.FindQuery(x => x.ILAId == ilaId).OrderBy(x => x.Id).LastOrDefaultAsync();
            var subReqList = new List<object>();
            if (certificationLink != null)
            {
                var subReqLinks = await _ilaCertSubReqLinkService.FindQuery(x => x.ILACertificationLinkId == certificationLink.Id).ToListAsync();
                if (subReqLinks.Count < 1)
                {
                    subReqList.Add(new { Name = "CEH Hours", Hours = certificationLink.CEHHours });
                }
                foreach (var subReqLink in subReqLinks)
                {
                    var subReqData = await _certSubReqService.FindQuery(x => x.Id == subReqLink.CertificationSubRequirementId).FirstOrDefaultAsync();
                    if (subReqData != null)
                    {
                        subReqList.Add(new { Name = subReqData.ReqName, Hours = subReqData.ReqHour });
                    }
                }
            }

            if (isNercStandards)
            {
                if (data != null)
                {
                    standardRelatedHours = data.FirstOrDefault(x => x.NercStandardMember.Name == "Standard related hours")?.CreditHoursByStd ?? 0;
                    operatingTopics = data.FirstOrDefault(x => x.NercStandardMember.Name == "Operating Topic/CEHs")?.CreditHoursByStd ?? 0;
                    simulationHours = data.FirstOrDefault(x => x.NercStandardMember.Name == "Simulation Hours")?.CreditHoursByStd ?? 0;
                    emergencyHours = data.FirstOrDefault(x => x.NercStandardMember.Name == "PER-005 Emergency Op Hours")?.CreditHoursByStd ?? 0;
                }
            }
            else
            {
                pmjTotalTrainingHours = data.FirstOrDefault(x => x.NercStandardMember.Name == "PJM Total Training Hours")?.CreditHoursByStd ?? 0;

            }
            return new
            {
                IlaId = ilaId,
                IlaName = ilaInfo.Name,
                IlaDescription = ilaInfo.Description,
                IlaNumber = ilaInfo.Number,
                TrainingPlan = ilaInfo.TrainingPlan,
                NERCHours = 0,
                IsNercStandards = isNercStandards,
                OPCEHsHours = operatingTopics,
                StandardHours = standardRelatedHours,
                SimulationsHours = simulationHours,
                EmergencyOPHours = emergencyHours,
                PmjTotalTrainingHours = pmjTotalTrainingHours,
                PrerequisitesList = ilaInfo.ILA_PreRequisite_Links.Select(x => x.PreRequisite).ToList(),
                LearningObjectivesList = linkedObjectives,
                SubRequirements = subReqList,
            };
        }

        public async Task<Result<EmployeeDashboardStatsDto>> GetDashboardStatisticsByIdAsync(int employeeId)
        {
            var classScheduleStatuses = await _mainUow.Repository<ClassSchedule_Roster_Statuses>()
                .GetListAsync(i => i.Name == "In Progress" || i.Name == "Not Started");

            var tqStatuses = await _mainUow.Repository<TaskQualificationStatus>()
                .GetListAsync(i => i.Name == "Pending" || i.Name == "On Time");

            var inProgressStatusId = classScheduleStatuses.FirstOrDefault(i => i.Name == "In Progress")?.Id;
            var notStartedStatusId = classScheduleStatuses.FirstOrDefault(i => i.Name == "Not Started")?.Id;
            var pendingTqStatusId = tqStatuses.FirstOrDefault(i => i.Name == "Pending")?.Id;
            var onTimeTqStatusId = tqStatuses.FirstOrDefault(i => i.Name == "On Time")?.Id;
           
            InProgressNotStartedCountModel testCounts = await GetTestDashboardCountsAsync(employeeId, inProgressStatusId, notStartedStatusId);
            InProgressNotStartedCountModel evaluationCounts = await GetStudentEvaluationDashboardCountsAsync(employeeId);
            InProgressNotStartedCountModel onlineCoursesCounts = await GetOnlineCoursesDashboardCountsAsync(employeeId);
            InProgressNotStartedCountModel procReviewCounts = await GetProcedureReviewDashboardCountsAsync(employeeId);
            InProgressNotStartedCountModel taskQualCounts = await GetTaskQualificationDashboardCountsAsync(employeeId);
            InProgressNotStartedCountModel taskQualEvalCounts = await GetTaskQualificationEvaluatorDashboardCountsAsync(employeeId);

            var statsDto = new EmployeeDashboardStatsDto
            {
                TestInProgressCount = testCounts.InProgressCount,
                TestNotStartedCount = testCounts.NotStartedCount,
                StudentEvaluationInProgressCount = evaluationCounts.InProgressCount,
                StudentEvaluationNotStartedCount = evaluationCounts.NotStartedCount,
                OnlineCourseInProgressCount = onlineCoursesCounts.InProgressCount,
                OnlineCourseNotStartedCount = onlineCoursesCounts.NotStartedCount,
                ProcedureReviewsInProgressCount = procReviewCounts.InProgressCount,
                ProcedureReviewsNotStartedCount = procReviewCounts.NotStartedCount,
                TaskQualificationInProgressCount = taskQualCounts.InProgressCount + taskQualEvalCounts.InProgressCount,
                TaskQualificationNotStartedCount = taskQualCounts.NotStartedCount +taskQualEvalCounts.NotStartedCount,
            };

            return Result<EmployeeDashboardStatsDto>.CreateSuccess(statsDto);
        }


        public async Task<bool> CheckCourseAvailabilityForSelfRegestration(int employeeId)
        {
            if (employeeId != null)
            {
                var ilas = await _ilaDomainService.GetILAAsync();
                foreach (var ila in ilas)
                {
                    var selfRegOption = await _selfRegService.GetSelfRegistrationAsync(ila.Id);
                    if (selfRegOption != null)
                    {
                        if (selfRegOption.MakeAvailableForSelfReg)
                        {
                            var seats = ila.ClassSize ?? 30;
                            var classSchedules = await _classScheduleDomainService.GetClassSchedulesByIdAsync(ila.Id);
                            foreach (var classSchedule in classSchedules)
                            {
                                var registeredStudents = await _classScheduleEmpService.GetEmployeeByClassScheduleByIdAsync(employeeId, classSchedule.Id);
                                if (seats - registeredStudents > 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
        }

        public async Task<string> GetCurrentEmployeeName(int employeeId)
        {
            if (employeeId != 0)
            {
                var personId = (await _employeeDomainService.FindAsync(x => x.Id == employeeId)).Select(S => S.PersonId).FirstOrDefault();
                if (personId != 0)
                {
                    var personName = (await _personService.FindAsync(x => x.Id == personId)).Select(s => s.FirstName).FirstOrDefault();
                    return personName ?? "N/A";
                }
                else
                {
                    return "N/A";
                }
            }
            else
            {
                return "N/A";
            }
        }

        public async Task<InProgressNotStartedCountModel> GetTaskQualificationDashboardCountsAsync(int empId)
        {
            InProgressNotStartedCountModel count = new InProgressNotStartedCountModel();

            var taskQualifications = await _taskQualService.GetTaskQualificationsDashboardCountByEmpId(empId);
            foreach(var tq in taskQualifications)
            {
                if(tq.TaskReQualificationEmp_SignOff.Count>0 && tq.TQEmpSetting?.ReleaseDate <= DateTime.UtcNow)
                {
                    count.InProgressCount++;
                }
                if (tq.TaskReQualificationEmp_SignOff.Count == 0 && tq.TQEmpSetting?.ReleaseDate <= DateTime.UtcNow)
                {
                    count.NotStartedCount++;
                }
            }
            return count;
        }

        public async Task<InProgressNotStartedCountModel> GetTaskQualificationEvaluatorDashboardCountsAsync(int empId)
        {
            InProgressNotStartedCountModel count = new InProgressNotStartedCountModel();

            var taskQualification_Evaluator_Links = await _tq_eval_linkService.GetPendingTaskQualificationsByEvaluator(empId);
            foreach (var evaluatorTQLink in taskQualification_Evaluator_Links)
            {
                if (evaluatorTQLink.TaskQualification.TaskReQualificationEmp_SignOff.Count > 0 && evaluatorTQLink.TaskQualification.TQEmpSetting?.ReleaseDate <= DateTime.UtcNow)
                {
                    count.InProgressCount++;
                }
                if (evaluatorTQLink.TaskQualification.TaskReQualificationEmp_SignOff.Count == 0 && evaluatorTQLink.TaskQualification.TQEmpSetting?.ReleaseDate <= DateTime.UtcNow)
                {
                    count.NotStartedCount++;
                }
            }
            return count;
        }
    }

}
