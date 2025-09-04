using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IEmployee_TaskLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployee_TaskService;
using ITask_PositionDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_PositionService;
using IPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using ITaskQualificationStatusDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationStatusService;
using ITQEmpSettingDomainService = QTD2.Domain.Interfaces.Service.Core.ITQEmpSettingService;
using ITaskQualEvalLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualification_Evaluator_LinkService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using ITaskReQualificationEmp_StepsDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_StepsService;
using ITask_StepDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_StepService;
using IMetaTaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_MetaTask_LinkService;
using QTD2.Infrastructure.Model.TaskReQualificationEmp;

namespace QTD2.Application.Services.Shared
{
    public class TaskReQualificationEmp_StepService : ITaskReQualificationEmp_StepService
    {
        private readonly IStringLocalizer<Domain.Entities.Core.TaskReQualificationEmp_Steps> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITaskService _taskService;
        private readonly IEmployeeDomainService _empService;
        private readonly IEmployee_TaskLinkDomainService _empTaskLinkService;
        private readonly ITask_PositionDomainService _task_positionService;
        private readonly IPositionDomainService _positionService;
        private readonly ITaskQualificationStatusDomainService _tqStatusService;
        private readonly ITQEmpSettingDomainService _tqEmpSettingService;
        private readonly ITaskQualEvalLinkDomainService _tq_evalService;
        private readonly IPersonDomainService _personService;
        private readonly ITaskReQualificationEmp_StepsDomainService _empStepsDomainService;
        private readonly ITask_StepDomainService _taskStepService;
        private readonly IMetaTaskDomainService _metaTask_task_linkService;

        public TaskReQualificationEmp_StepService(
           IStringLocalizer<Domain.Entities.Core.TaskReQualificationEmp_Steps> localizer,
           IHttpContextAccessor httpContextAccessor,
           IAuthorizationService authorizationService,
           UserManager<AppUser> userManager,
           ITaskService taskService,
           IEmployeeDomainService empService,
           IEmployee_TaskLinkDomainService empTaskLinkService,
           ITask_PositionDomainService task_positionService,
           IPositionDomainService positionService,
           ITaskService task_AppService,
           ITaskQualificationStatusDomainService tqStatusService,
           ITQEmpSettingDomainService tqEmpSettingService,
           ITaskQualEvalLinkDomainService tq_evalService, IPersonDomainService personService, ITaskReQualificationEmp_StepsDomainService empStepsDomainService, ITask_StepDomainService taskStepService, IMetaTaskDomainService metaTask_task_linkService)
        {
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _taskService = taskService;
            _empService = empService;
            _empTaskLinkService = empTaskLinkService;
            _task_positionService = task_positionService;
            _positionService = positionService;
            _tqStatusService = tqStatusService;
            _tqEmpSettingService = tqEmpSettingService;
            _tq_evalService = tq_evalService;
            _personService = personService;
            _empStepsDomainService = empStepsDomainService;
            _taskStepService = taskStepService;
            _metaTask_task_linkService = metaTask_task_linkService;
        }

        public async Task<TaskReQualificationEmpStepVM> GetStepsData(int qualificationId, int taskId, int employeeId)
        {
            //Get Current Evaluator 
            var qualWithSteps = new TaskReQualificationEmpStepVM();
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();
                var qualificationEmp = await _empStepsDomainService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == qualificationId && x.TraineeId == employeeId && x.EvaluatorId == employee.Id, new string[] { "TaskQualification.Task.Task_Steps" }).ToListAsync();

                var stepsVM = new Steps();
                var stepsListVM = new List<Steps>();
                if (qualificationEmp.Count == 0)
                {
                    //check steps in tasks
                    var steps = (await _taskService.GetTask_StepsAsync(taskId)).ToList();
                    var task = await _taskService.GetAsync(taskId);
                    if (steps != null && steps.Count > 0)
                    {
                        foreach (var step in steps)
                        {
                            qualWithSteps.StepsList.Add(new Steps()
                            {
                                StepId = step.Id,
                                StepDescription = step.Description,
                                Comments = string.Empty,
                                IsCompleted = null,
                            });
                           
                        }
                    }
                    else
                    {
                        qualWithSteps.StepsList = new List<Steps>();
                    }
                    if (task.IsMeta)
                    {
                        var linkedTaskSteps = await _metaTask_task_linkService.FindQueryWithIncludeAsync(x => x.Meta_TaskId == taskId, new string[] { "Task.Task_Steps" }, true).OrderBy(o => o.Id).SelectMany(s => s.Task.Task_Steps).ToListAsync();
                        if (linkedTaskSteps != null)
                        {
                            foreach (var step in linkedTaskSteps)
                            {
                                qualWithSteps.StepsList.Add(new Steps()
                                {
                                    StepId = step.Id,
                                    StepDescription = step.Description,
                                    Comments = string.Empty,
                                    IsCompleted = null,
                                });

                            }
                        }
                    }

                    qualWithSteps.TaskDescription = task.Description;
                    qualWithSteps.TaskQualificationId = qualificationId;
                    qualWithSteps.TaskId = task.Id;
                }
                else
                {
                    //get a list of suggestions
                    var task = await _taskService.GetAsync(taskId);
                    var steps =(await _taskService.GetTask_StepsAsync(taskId)).ToList();
                    foreach(var step in steps)
                    {
                        var qual = qualificationEmp.FirstOrDefault(x => x.TaskStepId == step.Id);
                        qualWithSteps.StepsList.Add(new Steps()
                        {
                            StepId = step.Id,
                            StepDescription = step.Description,
                            Comments = qual == null ? string.Empty : qual.Comments,
                            IsCompleted = qual == null ? null : qual.IsCompleted,
                        });
                    }
                    if (task.IsMeta)
                    {
                        var linkedTaskSteps = await _metaTask_task_linkService.FindQueryWithIncludeAsync(x => x.Meta_TaskId == task.Id, new string[] { "Task.Task_Steps" }, true).OrderBy(o => o.Id).SelectMany(s => s.Task.Task_Steps).ToListAsync();
                        if (linkedTaskSteps != null)
                        {
                            foreach (var step in linkedTaskSteps)
                            {
                                 var qual = qualificationEmp.FirstOrDefault(x => x.TaskStepId == step.Id);
                                qualWithSteps.StepsList.Add(new Steps()
                                {
                                    StepId = step.Id,
                                    StepDescription = step.Description,
                                    Comments =qual == null ? string.Empty : qual.Comments,
                                    IsCompleted = qual == null ? null : qual.IsCompleted,
                                });
                            }
                        }
                    }
                    qualWithSteps.TaskDescription = task.Description;
                    qualWithSteps.TaskQualificationId = qualificationId;
                    qualWithSteps.TaskId = task.Id;
                }
            }
            return qualWithSteps;
        }


        public async System.Threading.Tasks.Task CreateOrUpdateStepsAsync(TaskReQualificationEmpStepVM options)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();
                if (options.StepsList != null && options.StepsList.Count > 0)
                {
                    foreach (var step in options.StepsList)
                    {
                        var qualificationEmp = await _empStepsDomainService.FindQuery(x => x.TaskQualificationId == options.TaskQualificationId && x.TraineeId == options.TraineeId && x.EvaluatorId == employee.Id && x.TaskStepId == step.StepId).FirstOrDefaultAsync();

                        if (qualificationEmp == null)
                        {
                            //add case
                            var requalification = new TaskReQualificationEmp_Steps(options.TaskQualificationId, step.StepId, step.Comments, employee.Id, DateTime.UtcNow, options.TraineeId, step.IsCompleted??false);
                            var validationResult = await _empStepsDomainService.AddAsync(requalification);

                        }
                        else
                        {
                            qualificationEmp.TaskQualificationId = options.TaskQualificationId;
                            qualificationEmp.TaskStepId = step.StepId;
                            qualificationEmp.Comments = step.Comments;
                            qualificationEmp.TraineeId = options.TraineeId;
                            qualificationEmp.IsCompleted = step.IsCompleted ?? false;
                            qualificationEmp.CommentDate = DateTime.UtcNow;
                            qualificationEmp.EvaluatorId = employee.Id;
                            var validationResult = await _empStepsDomainService.UpdateAsync(qualificationEmp);
                        }
                    }


                }


            }
        }
    }
}
