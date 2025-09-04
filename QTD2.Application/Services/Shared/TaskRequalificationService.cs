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
using QTD2.Infrastructure.Model.Task_Requalification;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IEmployee_TaskLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployee_TaskService;
using ITask_PositionDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_PositionService;
using IPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Services.Core;
using ITask_EnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_EnablingObjective_LinkService;
using IEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using IEnablingObjectiveCategoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_CategoryService;
using ITask_TrainngGroupLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_TrainingGroupService;
using IEmployee_PositionDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeePositionService;
using ITaskQualificationService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Task;
using ITaskQualification_Evaluator_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualification_Evaluator_LinkService;
using QTD2.Infrastructure.Hashing.Interfaces;
using ITaskQualificationStatusDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationStatusService;
using ITQEmpSettingDomainService = QTD2.Domain.Interfaces.Service.Core.ITQEmpSettingService;
using IDutyAreaDomainService = QTD2.Domain.Interfaces.Service.Core.IDutyAreaService;
using QTD2.Infrastructure.Model.FilterOptions;
using Microsoft.Extensions.Options;
using ITaskQualEvalLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualification_Evaluator_LinkService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using ITaskReQualificationEmp_SignOffDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_SignOffService;
using QTD2.Application.Utils;
using IPositionTask_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IPosition_TaskService;
using ISubDutyAreasDomainService = QTD2.Domain.Interfaces.Service.Core.ISubdutyAreaService;
using ITask_MetaTask_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_MetaTask_LinkService;
using DocumentFormat.OpenXml.Office2010.Excel;
using QTD2.Domain.Helpers;

namespace QTD2.Application.Services.Shared
{
    public class TaskRequalificationService : ITaskRequalificationService
    {
        private readonly IStringLocalizer<Domain.Entities.Core.Task> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITaskDomainService _taskService;
        private readonly IEmployeeDomainService _empService;
        private readonly IEmployee_TaskLinkDomainService _empTaskLinkService;
        private readonly IPositionDomainService _positionService;
        private readonly Position_Task _position_task;
        private readonly ITaskService _task_AppService;
        private readonly Employee_Task _emp_Task;
        private readonly Employee _emp;
        private readonly ITask_EnablingObjectiveDomainService _task_EOService;
        private readonly IEnablingObjectiveDomainService _eoService;
        private readonly EnablingObjective _eo;
        private readonly Task_EnablingObjective_Link _task_eo;
        private readonly IEnablingObjectiveCategoryDomainService _eo_categoryService;
        private readonly EnablingObjective_Category _eo_category;
        private readonly ITask_TrainngGroupLinkDomainService _task_TrainingGroupService;
        private readonly Task_TrainingGroup _task_TrainingGroup;
        private readonly QTD2.Domain.Entities.Core.Task _task;
        private readonly IEmployee_PositionDomainService _emp_positionService;
        private readonly EmployeePosition _emp_position;
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly TaskQualification _taskQualification;
        private readonly ITaskQualification_Evaluator_LinkDomainService _tq_eval_linkService;
        private readonly TaskQualification_Evaluator_Link _tq_eval_link;
        private readonly IHasher _hasher;
        private readonly ITaskQualificationStatusDomainService _tqStatusService;
        private readonly ITQEmpSettingDomainService _tqEmpSettingService;
        private readonly IDutyAreaDomainService _daService;
        private readonly DutyArea _dutyArea;
        private readonly ITaskQualEvalLinkDomainService _tq_evalService;
        private readonly IPersonDomainService _personService;
        private readonly ITaskReQualificationEmp_SignOffDomainService _empSignOffService;
        private readonly IPositionTask_LinkDomainService _position_TaskService;
        private readonly ISubDutyAreasDomainService _sdaService;
        private readonly ITask_MetaTask_LinkDomainService _metaTask_TaskService;
        private readonly QTD2.Domain.Interfaces.Service.Core.ISkillQualificationService _skillQualificationService;
        private readonly QTD2.Domain.Interfaces.Service.Core.ISkillQualificationEmpSettingService _skillQualificationEmpSettingService;
        private readonly QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_MetaEO_LinkService _enablingObjective_MetaEO_LinkService;
        private readonly QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_SubCategoryService _enablingObjective_SubCategoryService;
        private readonly QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_TopicService _enablingObjective_TopicService;
        private readonly QTD2.Domain.Interfaces.Service.Core.ISkillQualification_Evaluator_LinkService _skillQualification_Evaluator_LinkService;
        private readonly QTD2.Domain.Interfaces.Service.Core.ISkillQualificationStatusService _skillQualificationStatusService;

        public TaskRequalificationService(
            IStringLocalizer<Domain.Entities.Core.Task> localizer,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            UserManager<AppUser> userManager,
            ITaskDomainService taskService,
            IEmployeeDomainService empService,
            IEmployee_TaskLinkDomainService empTaskLinkService,
            IPositionDomainService positionService,
            ITaskService task_AppService,
            ITask_EnablingObjectiveDomainService task_EOService,
            IEnablingObjectiveDomainService eoService,
            IEnablingObjectiveCategoryDomainService eo_categoryService,
            ITask_TrainngGroupLinkDomainService task_TrainingGroupService,
            IEmployee_PositionDomainService emp_positionService,
            ITaskQualificationService taskQualificationService,
            ITaskQualification_Evaluator_LinkDomainService tq_eval_linkService,
            IHasher hasher,
            ITaskQualificationStatusDomainService tqStatusService,
            ITQEmpSettingDomainService tqEmpSettingService,
            IDutyAreaDomainService daService,
            ITaskQualEvalLinkDomainService tq_evalService, IPersonDomainService personService, ITaskReQualificationEmp_SignOffDomainService empSignOffService,
            IPositionTask_LinkDomainService position_TaskService, ISubDutyAreasDomainService sdaService, ITask_MetaTask_LinkDomainService metaTask_TaskService,
            QTD2.Domain.Interfaces.Service.Core.ISkillQualificationService skillQualificationService,  
            QTD2.Domain.Interfaces.Service.Core.ISkillQualificationEmpSettingService skillQualificationEmpSettingService, 
            QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_MetaEO_LinkService enablingObjective_MetaEO_LinkService,
            QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_SubCategoryService enablingObjective_SubCategoryService,
            QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_TopicService enablingObjective_TopicService,
            QTD2.Domain.Interfaces.Service.Core.ISkillQualification_Evaluator_LinkService skillQualification_Evaluator_LinkService,
            QTD2.Domain.Interfaces.Service.Core.ISkillQualificationStatusService skillQualificationStatusService)
        {
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _taskService = taskService;
            _empService = empService;
            _empTaskLinkService = empTaskLinkService;
            _positionService = positionService;
            _position_task = new Position_Task();
            _task_AppService = task_AppService;
            _emp_Task = new Employee_Task();
            _emp = new Employee();
            _task_EOService = task_EOService;
            _eoService = eoService;
            _eo = new EnablingObjective();
            _task = new Domain.Entities.Core.Task();
            _task_eo = new Task_EnablingObjective_Link();
            _eo_categoryService = eo_categoryService;
            _eo_category = new EnablingObjective_Category();
            _task_TrainingGroupService = task_TrainingGroupService;
            _task_TrainingGroup = new Task_TrainingGroup();
            _emp_positionService = emp_positionService;
            _emp_position = new EmployeePosition();
            _taskQualificationService = taskQualificationService;
            _taskQualification = new TaskQualification();
            _tq_eval_linkService = tq_eval_linkService;
            _tq_eval_link = new TaskQualification_Evaluator_Link();
            _hasher = hasher;
            _tqStatusService = tqStatusService;
            _tqEmpSettingService = tqEmpSettingService;
            _daService = daService;
            _dutyArea = new DutyArea();
            _tq_evalService = tq_evalService;
            _personService = personService;
            _empSignOffService = empSignOffService;
            _position_TaskService = position_TaskService;
            _sdaService = sdaService;
            _metaTask_TaskService = metaTask_TaskService;
            _skillQualificationService = skillQualificationService;
            _skillQualificationEmpSettingService = skillQualificationEmpSettingService;
            _enablingObjective_MetaEO_LinkService = enablingObjective_MetaEO_LinkService;
            _enablingObjective_SubCategoryService = enablingObjective_SubCategoryService;
            _enablingObjective_TopicService = enablingObjective_TopicService;
            _skillQualification_Evaluator_LinkService  = skillQualification_Evaluator_LinkService;
            _skillQualificationStatusService = skillQualificationStatusService;
        }

        public async Task<TaskQualification> UpdateAsync(int id, TaskQualificationCreateOptions options)
        {
            var taskQualification = await _taskQualificationService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (taskQualification == null)
            {
                throw new BadHttpRequestException(message: _localizer["TaskQualificationNotFoundException"]);
            }
            else
            {
                if (options.EvaluationId == 0)
                {
                    options.EvaluationId = null;
                }
                taskQualification.Comments = options.Comments;
                taskQualification.CriteriaMet = options.CriteriaMet;
                taskQualification.EvaluationId = options.EvaluationId;
                taskQualification.TaskId = options.TaskId;
                taskQualification.EmpId = options.EmpId;
                taskQualification.TaskQualificationDate = options.TaskQualificationDate?.ToUniversalTime();
                taskQualification.TaskQualificationEvaluator = options.TaskQualificationEvaluator;
                taskQualification.DueDate = options.DueDate;
                taskQualification.TQStatusId = getStatusId(options);
                taskQualification.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                taskQualification.ModifiedDate = DateTime.Now;
                //taskQualification.EvaluatorId = options.EvaluatorId;

                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskQualification, TaskQualificationOperations.Update);
                if (authResult.Succeeded)
                {
                    var taskQualification_Evaluator_Links = await _tq_evalService.GetTaskEvaluatorLinksByTQIdAsync(taskQualification.Id);
                    var currentEvaluatorIds = taskQualification_Evaluator_Links.Select(tel => tel.EvaluatorId).ToList();

                    // Add net-new Evaluators' links
                    var newEvaluatorIds = options.EvaluatorIds.Where(ei => !currentEvaluatorIds.Contains(ei)).ToList();
                    var newTaskQualification_Evaluator_Links = new List<TaskQualification_Evaluator_Link>();
                    foreach (var newEvaluatorId in newEvaluatorIds)
                    {
                        var newTaskQualification_Evaluator_Link = new TaskQualification_Evaluator_Link
                        {
                            TaskQualificationId = taskQualification.Id,
                            EvaluatorId = newEvaluatorId
                        };
                        newTaskQualification_Evaluator_Links.Add(newTaskQualification_Evaluator_Link);
                    }
                    await _tq_evalService.AddRangeAsync(newTaskQualification_Evaluator_Links);
                    
                    // Soft-delete discontinued Evaluators' links
                    var removedEvaluatorIds = currentEvaluatorIds.Where(cei => !options.EvaluatorIds.Contains(cei));
                    var removedTaskQualification_Evaluator_Links = taskQualification_Evaluator_Links.Where(tel => removedEvaluatorIds.Contains(tel.EvaluatorId)).ToList();
                    removedTaskQualification_Evaluator_Links.ForEach(tel => tel.Delete());
                    await _tq_evalService.BulkUpdateAsync(removedTaskQualification_Evaluator_Links);

                    var validationResult = await _taskQualificationService.UpdateAsync(taskQualification);
                    if (validationResult.IsValid)
                    {
                        return taskQualification;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
        }

        public async Task<List<Employee>> GetEmpDataForPositionsAsync(EMPFilterOptions options)
        {
            var employees = await _empService.FindQueryWithIncludeAsync(x => x.EmployeePositions.Any(y => options.PositionIds.Contains(y.PositionId)) && x.Active == true, new string[] { nameof(_emp.EmployeePositions), nameof(_emp.Person), nameof(_emp.EmployeeOrganizations) }).ToListAsync();
            return employees.OrderBy(o => o.Person.LastName).ToList();
        }

        public async System.Threading.Tasks.Task UpdateReleasedTQ(ReleasedTQAndSQUpdateOptions options)
        {
            switch (options.Action.Trim().ToLower())
            {
                case "date":
                    await UpdateReleasedTQAndSQDates(options);
                    break;
                case "reassign":
                    await ReassignReleasedTQ(options);
                    break;
                case "recall":
                    await RecallReleasedTQ(options);
                    break;
            }
        }

        public async System.Threading.Tasks.Task ReassignReleasedTQ(ReleasedTQAndSQUpdateOptions options)
        {
            foreach (var tqId in options.TQIds)
            {
                var tq = await _taskQualificationService.FindQueryWithIncludeAsync(x => x.Id == tqId, new string[] { nameof(_taskQualification.TaskQualification_Evaluator_Links), nameof(_taskQualification.TQEmpSetting) }).FirstOrDefaultAsync();
                if (tq == null)
                {
                    throw new BadHttpRequestException(message: _localizer["TaskQualificationNotFoundException"]);
                }
                else
                {
                    tq.DueDate = options.DueDate;
                    tq.TQEmpSetting.ReleaseDate = options.ReleaseDate?.ToUniversalTime();
                    var linkedIds = tq.TaskQualification_Evaluator_Links.Select(s => s.EvaluatorId).ToList();
                    tq.TaskQualification_Evaluator_Links.Clear();
                    if (options.RemoveSignOffs)
                    {
                        switch (options.CheckStarted)
                        {
                            case 1:
                                tq.ModifiedDate = DateTime.Now;
                                tq.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                                foreach (var evalId in options.EvaluatorIds)
                                {
                                    if (tq.TQEmpSetting.ReleaseInSpecificOrder)
                                    {
                                        var eval = await _empService.FindQuery(x => x.Id == evalId).FirstOrDefaultAsync();
                                        tq.LinkEvaluator(eval);
                                    }
                                    else
                                    {
                                        var evalLink = new TaskQualification_Evaluator_Link();
                                        evalLink.EvaluatorId = evalId;
                                        evalLink.TaskQualificationId = tq.Id;
                                        evalLink.Number = 0;
                                        tq.TaskQualification_Evaluator_Links.Add(evalLink);
                                    }
                                }

                                await _taskQualificationService.UpdateAsync(tq);
                                break;
                            case 2:

                                foreach (var evalId in options.EvaluatorIds)
                                {

                                    var previousSignOffs = await _empSignOffService.FindQuery(x => x.TaskQualificationId == tqId && x.EvaluatorId == evalId).FirstOrDefaultAsync();
                                    if (previousSignOffs != null)
                                    {
                                        previousSignOffs.Delete();
                                        await _empSignOffService.UpdateAsync(previousSignOffs);
                                    }
                                }

                                tq.ModifiedDate = DateTime.Now;
                                tq.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                                foreach (var evalId in options.EvaluatorIds)
                                {
                                    if (tq.TQEmpSetting.ReleaseInSpecificOrder)
                                    {
                                        var eval = await _empService.FindQuery(x => x.Id == evalId).FirstOrDefaultAsync();
                                        tq.LinkEvaluator(eval);
                                    }
                                    else
                                    {
                                        var evalLink = new TaskQualification_Evaluator_Link();
                                        evalLink.EvaluatorId = evalId;
                                        evalLink.TaskQualificationId = tq.Id;
                                        evalLink.Number = 0;
                                        tq.TaskQualification_Evaluator_Links.Add(evalLink);
                                    }
                                }

                                await _taskQualificationService.UpdateAsync(tq);
                                break;
                            default:
                                throw new BadHttpRequestException(message: _localizer["Invalid Reassign Option Selected"]);
                        }
                    }
                    else
                    {
                        tq.ModifiedDate = DateTime.Now;
                        tq.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                        foreach (var evalId in options.EvaluatorIds)
                        {
                            if (tq.TQEmpSetting.ReleaseInSpecificOrder)
                            {
                                var eval = await _empService.FindQuery(x => x.Id == evalId).FirstOrDefaultAsync();
                                tq.LinkEvaluator(eval);
                            }
                            else
                            {
                                var evalLink = new TaskQualification_Evaluator_Link();
                                evalLink.EvaluatorId = evalId;
                                evalLink.TaskQualificationId = tq.Id;
                                evalLink.Number = 0;
                                tq.TaskQualification_Evaluator_Links.Add(evalLink);
                            }
                        }

                        await _taskQualificationService.UpdateAsync(tq);
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task RecallReleasedTQ(ReleasedTQAndSQUpdateOptions options)
        {
            foreach (var tqId in options.TQIds)
            {
                var tq = await _taskQualificationService.FindQuery(x => x.Id == tqId).FirstOrDefaultAsync();
                if (tq != null)
                {
                    tq.Recall();
                    tq.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    tq.ModifiedDate = DateTime.Now;
                    var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tq, TaskQualificationOperations.Update);
                    if (authResult.Succeeded)
                    {
                        var validationResult = await _taskQualificationService.UpdateAsync(tq);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(',', validationResult.Errors)]);
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                    }
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["TaskQualificationNotFoundException"]);
                }
            }
        }

        public async System.Threading.Tasks.Task UpdateReleasedTQAndSQDates(ReleasedTQAndSQUpdateOptions options)
        {
            foreach (var tqId in options.TQIds)
            {
                var tq = await _taskQualificationService.FindQuery(x => x.Id == tqId).FirstOrDefaultAsync();
                if (tq != null)
                {
                    var tqSettings = await _tqEmpSettingService.FindQuery(x => x.TaskQualificationId == tq.Id).FirstOrDefaultAsync();
                    if (tqSettings != null)
                    {
                        tqSettings.ReleaseDate = options.ReleaseDate?.ToUniversalTime();
                        tqSettings.ModifiedDate = DateTime.Now;
                        tqSettings.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                        var settingAuth = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tqSettings, TQEmpSettingOperations.Update);
                        if (settingAuth.Succeeded)
                        {
                            var settingsValidation = await _tqEmpSettingService.UpdateAsync(tqSettings);
                            if (!settingsValidation.IsValid)
                            {
                                throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", settingsValidation.Errors)]);
                            }
                        }
                        else
                        {
                            throw new UnauthorizedAccessException(message: _localizer["TQEmpSettingsNotFoundException"]);
                        }
                    }
                    else
                    {
                        throw new BadHttpRequestException(message: _localizer["TaskQualificationSettingsNotFoundException"]);
                    }
                    tq.DueDate = options.DueDate;
                    var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tq, TaskQualificationOperations.Update);
                    if (authResult.Succeeded)
                    {
                        var validationResult = await _taskQualificationService.UpdateAsync(tq);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(',', validationResult.Errors)]);
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                    }
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["TaskQualificationNotFoundException"]);
                }
            }

            foreach (var sqId in options.SQIds)
            {
                var sq = await _skillQualificationService.GetSkillQualificationAsync(sqId);
                if (sq != null)
                {
                    var sqSettings = await _skillQualificationEmpSettingService.GetSQSettingBySkillQualificationIdAsync(sq.Id);
                    if (sqSettings != null)
                    {
                        sqSettings.ReleaseDate = options.ReleaseDate?.ToUniversalTime();
                        sqSettings.ModifiedDate = DateTime.Now;
                        sqSettings.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;

                        var settingAuth = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sqSettings, SkillQualificationEmpSettingOperations.Update);
                        if (settingAuth.Succeeded)
                        {
                            var settingsValidation = await _skillQualificationEmpSettingService.UpdateAsync(sqSettings);
                            if (!settingsValidation.IsValid)
                            {
                                throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", settingsValidation.Errors)]);
                            }
                        }
                        else
                        {
                            throw new UnauthorizedAccessException(message: _localizer["SQEmpSettingsNotFoundException"]);
                        }
                    }
                    else
                    {
                        throw new BadHttpRequestException(message: _localizer["SkillQualificationSettingsNotFoundException"]);
                    }

                    sq.DueDate = options.DueDate;
                    var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sq, SkillQualificationOperations.Update);
                    if (authResult.Succeeded)
                    {
                        var validationResult = await _skillQualificationService.UpdateAsync(sq);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(',', validationResult.Errors)]);
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                    }
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["SkillQualificationNotFoundException"]);
                }
            }
        }

        public async Task<List<DutyArea>> GetTaskTreeDataWithPositionIdsAsync(EMPFilterOptions option)
        {
            var das = (await _daService.FindQuery(x => x.Active == true).ToListAsync()).OrderBy(o => o.Number).GroupBy(g => g.Letter).SelectMany(s => s).ToList();
            foreach (var da in das)
            {
                da.SubdutyAreas = await _sdaService.FindQuery(x => x.DutyAreaId == da.Id && x.Active == true).OrderBy(o => o.SubNumber).ToListAsync();
                foreach (var sda in da.SubdutyAreas)
                {
                    sda.Tasks.Clear();
                    var tasks = new List<Domain.Entities.Core.Task>();
                    switch (option.Type.Trim().ToLower())
                    {
                        case "all":
                            var totaltasks = await _taskService.FindQuery(x => x.SubdutyAreaId == sda.Id && x.Active == true).Distinct().ToListAsync();
                            for (int i = 0; i < totaltasks.Count; i++)
                            {
                                if (totaltasks[i].IsMeta)
                                {
                                    var links = await _metaTask_TaskService.FindQuery(x => x.Meta_TaskId == totaltasks[i].Id).ToListAsync();
                                    for (int j = 0; j < links.Count; j++)
                                    {
                                        var posLinks = await _position_TaskService.FindQuery(x => x.TaskId == links[j].TaskId).ToListAsync();
                                        foreach (var posLink in posLinks)
                                        {
                                            if (!totaltasks[i].Position_Tasks.Contains(posLink))
                                            {
                                                totaltasks[i].Position_Tasks.Add(posLink);
                                            }
                                        }
                                    }
                                    totaltasks[i].Position_Tasks = totaltasks[i].Position_Tasks.GroupBy(g => g.Id).Select(s => s.First()).ToList();
                                    tasks.Add(totaltasks[i]);
                                }
                                else
                                {
                                    totaltasks[i].Position_Tasks = await _position_TaskService.FindQuery(x => x.TaskId == totaltasks[i].Id).ToListAsync();
                                    tasks.Add(totaltasks[i]);
                                }
                            }
                            break;
                        default:
                            var totaltasksWithFilter = await _taskService.FindQuery(x => x.SubdutyAreaId == sda.Id && x.Active == true).Distinct().ToListAsync();
                            //tasks = await _taskService.FindQuery(x => x.SubdutyAreaId == sda.Id).Distinct().ToListAsync();
                            for (int i = 0; i < totaltasksWithFilter.Count; i++)
                            {
                                if (totaltasksWithFilter[i].IsMeta)
                                {
                                    var links = await _metaTask_TaskService.FindQuery(x => x.Meta_TaskId == totaltasksWithFilter[i].Id).ToListAsync();
                                    for (int j = 0; j < links.Count; j++)
                                    {
                                        var posLinks = await _position_TaskService.FindQuery(x => x.TaskId == links[j].TaskId && option.PositionIds.Contains(x.PositionId)).ToListAsync();
                                        foreach (var posLink in posLinks)
                                        {
                                            if (!totaltasksWithFilter[i].Position_Tasks.Contains(posLink))
                                            {
                                                totaltasksWithFilter[i].Position_Tasks.Add(posLink);
                                            }
                                        }
                                    }
                                    if (links.Count > 0)
                                    {
                                        tasks.Add(totaltasksWithFilter[i]);
                                    }
                                }
                                else
                                {
                                    totaltasksWithFilter[i].Position_Tasks = await _position_TaskService.FindQuery(x => x.TaskId == totaltasksWithFilter[i].Id && option.PositionIds.Contains(x.PositionId)).ToListAsync();
                                    if (totaltasksWithFilter[i].Position_Tasks.Count > 0)
                                    {
                                        tasks.Add(totaltasksWithFilter[i]);
                                    }
                                }
                            }
                            break;
                    }

                    foreach (var task in tasks)
                    {
                        sda.Tasks.Add(task);
                    }
                    sda.Tasks = sda.Tasks.OrderBy(o => o.Number).ToList();
                }
            }

            return das.Where(r => r.SubdutyAreas.SelectMany(s => s.Tasks).Any()).ToList();
        }

        public async Task<List<EnablingObjective>> GetEoTreeWithPositionIds(EMPFilterOptions option)
        {
            var eos = new List<EnablingObjective>();

            switch (option.Type.Trim().ToLower())
            {
                case "all":
                    eos.AddRange(await _eoService.GetEOAsync());
                    break;

                default:
                    eos.AddRange((await _eoService.GetEOAsync()).Where(ef => ef.Position_SQs.Any(ep => option.PositionIds.Contains(ep.PositionId))));
                    break;
            }

            return eos.OrderBy(o => o.Number).ToList();
        }


        public async Task<List<TQReleasedToEMPVM>> GetTQReleasedToEMP()
        {
            List<TQReleasedToEMPVM> result = new List<TQReleasedToEMPVM>();
            List<int> pendingTqStatuses = new List<int>()
            {
                3, 4, 5, 9
            };

            var taskQualifications = await _taskQualificationService
                .FindQueryWithIncludeAsync(x => x.IsReleasedToEMP && !x.IsRecalled && pendingTqStatuses.Contains(x.TQStatusId.GetValueOrDefault()), 
                    new string[]{
                        nameof(_taskQualification.TQEmpSetting)
                    })
                .ToListAsync();

            foreach (var taskQualification in taskQualifications)
            {
                TQReleasedToEMPVM tqReleased = new TQReleasedToEMPVM();
                tqReleased.Id = taskQualification.Id;
                tqReleased.EvaluatorName = "";
                tqReleased.DueDate = taskQualification.DueDate;

                var taskQualification_Evaluator_Links = await _tq_eval_linkService.FindQuery(x => x.TaskQualificationId == taskQualification.Id).ToListAsync();
                foreach (var taskQualification_Evaluator_Link in taskQualification_Evaluator_Links)
                {
                    var hashedId = _hasher.Encode(taskQualification_Evaluator_Link.EvaluatorId.ToString());
                    tqReleased.EvaluatorIds.Add(hashedId);
                    var eval = await _empService.FindQueryWithIncludeAsync(x => x.Id == taskQualification_Evaluator_Link.EvaluatorId, new string[] { nameof(_emp.Person) }).FirstOrDefaultAsync();
                    if (eval != null)
                    {
                        tqReleased.EvaluatorName = tqReleased.EvaluatorName + eval.Person.FirstName + " " + eval.Person.LastName + ", ";
                    }
                }

                var task = await _taskService.FindQuery(x => x.Id == taskQualification.TaskId).FirstOrDefaultAsync();
                var emp = await _empService.FindQuery(x => x.Id == taskQualification.EmpId && x.Active == true).FirstOrDefaultAsync();
                if (emp != null && task.Active == true)
                {
                    var person = await _personService.FindQuery(x => x.Id == emp.PersonId && x.Active == true).FirstOrDefaultAsync();
                    if (person != null)
                    {
                        tqReleased.EmpFName = person.FirstName;
                        tqReleased.EmpLName = person.LastName;
                        tqReleased.EmpName = person.FirstName + " " + person.LastName;
                        tqReleased.EmpCommaSepName = person.LastName + ", " + person.FirstName;
                        tqReleased.ReleaseDate = taskQualification.TQEmpSetting?.ReleaseDate;
                        tqReleased.WasTQStarted = await _empSignOffService.FindQuery(x => x.TaskQualificationId == tqReleased.Id).AnyAsync();
                        var signOffs = await _empSignOffService.FindQuery(x => x.TaskQualificationId == tqReleased.Id && x.IsCompleted == true).CountAsync();
                        if (taskQualification.TQEmpSetting?.ReleaseToAllSingleSignOff ?? false)
                        {
                            tqReleased.RequiredTaskQuals = "One Sign Off Required - " + signOffs.ToString() + "/1";
                        }
                        else
                        {
                            tqReleased.RequiredTaskQuals = "Multiple Sign Offs Required -" + signOffs.ToString() + "/" + taskQualification.TQEmpSetting?.MultipleSignOffDisplay;
                        }

                        var number = await _task_AppService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
                        tqReleased.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + task.Number;
                        tqReleased.Number = tqReleased.TaskNumber;
                        tqReleased.TaskDescription = task.Description;
                        result.Add(tqReleased);
                    }
                }
            }
            return result.OrderBy(o => o.EmpLName).ToList();
        }

        public async Task<List<TaskQualificationEmpVM>> GetAllQualificationsForEmp(int empId, int taskId)
        {
            List<TaskQualificationEmpVM> result = new List<TaskQualificationEmpVM>();
            
            var taskQualifications = await _taskQualificationService.FindQueryWithIncludeAsync(x => x.EmpId == empId && x.TaskId == taskId, new string[] { 
                nameof(_taskQualification.Task), 
                nameof(_taskQualification.Employee), 
                nameof(_taskQualification.TaskQualificationStatus), 
                nameof(_taskQualification.TaskQualification_Evaluator_Links),
                nameof(_taskQualification.TaskReQualificationEmp_SignOff),
                nameof(_taskQualification.TQEmpSetting)})
            .ToListAsync();

            taskQualifications = taskQualifications
                .OrderByDescending(o => o.TQEmpSetting?.ReleaseDate)
                .ThenByDescending(o => o.TaskQualificationDate)
                .ThenBy(tq => tq.Id) // Done so that this always returns the same record if all other filters are the same
                .ToList();

            foreach (var taskQualification in taskQualifications)
            {
                var trimmedTqComments = taskQualification.Comments?.Trim();
                var validSignOffComments = taskQualification.TaskReQualificationEmp_SignOff.Select(x => x.Comments?.Trim()).Where(c => !string.IsNullOrEmpty(c));
                var combinedComments = new[] { trimmedTqComments }.Concat(validSignOffComments).Where(c => !string.IsNullOrEmpty(c));
                var comments = combinedComments.Any() ? string.Join(", ", combinedComments) : null;

                var person = await _empService.FindQueryWithIncludeAsync(x => x.Id == taskQualification.Employee.Id, new string[] { nameof(_emp.Person) }).Select(s => s.Person).FirstOrDefaultAsync();
                TaskQualificationEmpVM taskQualEmp = new TaskQualificationEmpVM();
                taskQualEmp.CriteriaMet = taskQualification.CriteriaMet;
                taskQualEmp.Id = taskQualification.Id;
                taskQualEmp.EmpId = taskQualification.Employee.Id;
                taskQualEmp.EmpEmail = person.Username;
                taskQualEmp.DueDate = taskQualification.DueDate;
                taskQualEmp.QualificationDate = taskQualification.TaskQualificationDate;
                taskQualEmp.EmpImage = person.Image;
                taskQualEmp.Comments = comments;
                taskQualEmp.EmpName = person.FirstName + " " + person.LastName;
                var number = await _task_AppService.GetTaskNumberWithLetter(taskQualification.Task.SubdutyAreaId, taskQualification.Task.Id);
                taskQualEmp.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
                taskQualEmp.Number = taskQualEmp.TaskNumber;
                taskQualEmp.EmpReleaseDate = taskQualification.TQEmpSetting?.ReleaseDate;
                taskQualEmp.TaskId = taskQualification.Task.Id;
                taskQualEmp.Status = taskQualification.TaskQualificationStatus.Name;
                List<string> evalNames = new List<string>();
                foreach (var evalId in taskQualification.TaskQualification_Evaluator_Links.Select(s => s.EvaluatorId))
                {
                    var eval = await _empService.FindQueryWithIncludeAsync(x => x.Id == evalId, new string[] { nameof(_emp.Person) }).FirstOrDefaultAsync();
                    if (eval != null)
                    {
                        evalNames.Add(eval.Person.FirstName + " " + eval.Person.LastName);
                    }
                }
                taskQualEmp.EvaluatorName = string.Join(", ", evalNames);
                taskQualEmp.IsRecalled = taskQualification.IsRecalled;
                result.Add(taskQualEmp);
            }

            return result;
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var qualification = await _taskQualificationService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (qualification != null)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, qualification, TaskQualificationOperations.Delete);
                if (authResult.Succeeded)
                {
                    qualification.Delete();
                    var validationResult = await _taskQualificationService.UpdateAsync(qualification);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["TaskQualificationNotFoundException"]);
            }
        }

        public async Task<TaskQualificationWithEvalsVM> GetAsync(int qualificationId)
        {
            var qualification = await _taskQualificationService.FindQueryWithIncludeAsync(x => x.Id == qualificationId, new string[] { nameof(_taskQualification.TaskQualification_Evaluator_Links) }).FirstOrDefaultAsync();
            var qualWithEvals = new TaskQualificationWithEvalsVM();
            if (qualification == null)
            {
                throw new BadHttpRequestException(message: _localizer["TaskQualificationNotFoundException"]);
            }
            else
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, qualification, TaskQualificationOperations.Read);
                if (authResult.Succeeded)
                {
                    qualWithEvals.TaskQualification = qualification;
                    foreach (var evalId in qualification.TaskQualification_Evaluator_Links.Select(s => s.EvaluatorId))
                    {
                        var eval = await _empService.FindQuery(x => x.Id == evalId).FirstOrDefaultAsync();
                        if (eval != null)
                        {
                            qualWithEvals.Evaluators.Add(eval);
                        }
                    }
                    return qualWithEvals;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
        }

        public async Task<List<TaskReQualificationTabVM>> FilterByPositionAsync(TaskRequalificationFilterOptions options)
        {
            List<TaskReQualificationTabVM> taskData = new List<TaskReQualificationTabVM>();
            var taskQuals = await _position_TaskService.FindQueryWithIncludeAsync(x => x.Task.Active == true && x.PositionId == options.PosId && (options.MetaOnly == true ? x.Task.IsMeta == true : true) && (options.RrtOnly == true ? x.Task.IsReliability == true : true), new string[] { nameof(_position_task.Task) }).Select(x => x.Task).ToListAsync();
            foreach (var quals in taskQuals)
            {
                var taskVM = new TaskReQualificationTabVM();
                taskVM.RequalificationRequired = quals.RequalificationRequired == true && quals.RequalificationDueDate != null && quals.RequalificationDueDate.Value >= DateOnly.FromDateTime(DateTime.Now);
                taskVM.DueDate = quals.RequalificationDueDate;
                taskVM.TaskDescription = quals.Description;
                var taskPositions = await _position_TaskService.FindQuery(x => x.TaskId == quals.Id).Select(s => s.PositionId).ToListAsync();
                var emps = await _emp_positionService.FindQueryWithIncludeAsync(x => taskPositions.Any(a => x.PositionId == a), new string[] { nameof(_emp_position.Employee) }).Select(s => s.EmployeeId).ToListAsync();
                emps = emps.Distinct().ToList();
                taskVM.EmpLinkCount = emps.Count;
                taskVM.TaskId = quals.Id;
                var number = await _task_AppService.GetTaskNumberWithLetter(quals.SubdutyAreaId, quals.Id);
                taskVM.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + quals.Number;
                var position = await _positionService.FindQuery(x => x.Id == options.PosId).FirstOrDefaultAsync();
                if (position != null)
                {
                    taskVM.Position = position.PositionTitle;
                }
                if (!taskData.Select(x => x.TaskId).Contains(taskVM.TaskId))
                {
                    taskData.Add(taskVM);
                }
            }

            //var tasks = await _task_positionService.FindQueryWithIncludeAsync(x => x.PositionId == options.PosId, new string[] { nameof(_task_position.Task) }, true).Select(x => x.Task).ToListAsync();
            //foreach (var task in tasks)
            //{
            //    var taskVM = new TaskReQualificationTabVM();
            //    taskVM.RequalificationRequired = false;
            //    taskVM.DueDate = null;
            //    taskVM.TaskDescription = task.Description;
            //    taskVM.EmpLinkCount = await _empTaskLinkService.GetCount(x => x.TaskId == task.Id);
            //    taskVM.TaskId = task.Id;
            //    var number = await _task_AppService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
            //    taskVM.TaskNumber = number.DANumber + "." + number.SDANumber + "." + task.Number;
            //    taskData.Add(taskVM);
            //}

            return taskData.OrderBy(o => o.TaskNumber).ToList();
        }

        public async Task<List<TaskReQualificationTabVM>> FilterByTaskAsync(TaskRequalificationFilterOptions options)
        {
            List<TaskReQualificationTabVM> taskData = new List<TaskReQualificationTabVM>();
            List<Domain.Entities.Core.Task> tasks = new List<Domain.Entities.Core.Task>();
            //var tasks = await _taskService.AllQueryWithInclude(new string[] { nameof(_task.SubdutyArea) }).Where(x => (x.Id == options.TaskId || x.SubdutyAreaId == options.SdaId || x.SubdutyArea.DutyAreaId == options.DaId) && (options.MetaOnly ? x.IsMeta : true) && (options.RrtOnly ? x.IsReliability : true)).ToListAsync();
            if (options.TaskId != null)
            {
                tasks = await _taskService.FindQueryWithIncludeAsync(x => x.Active == true && (x.Id == options.TaskId) && (options.MetaOnly ? x.IsMeta : true) && (options.RrtOnly ? x.IsReliability : true), new string[] { "SubdutyArea" }).ToListAsync();
            }
            else if (options.SdaId != null)
            {
                tasks = await _taskService.FindQueryWithIncludeAsync(x => x.Active == true && (x.SubdutyAreaId == options.SdaId) && (options.MetaOnly ? x.IsMeta : true) && (options.RrtOnly ? x.IsReliability : true), new string[] { "SubdutyArea" }).ToListAsync();
            }
            else
            {
                tasks = await _taskService.FindQueryWithIncludeAsync(x => x.Active == true && (x.SubdutyArea.DutyAreaId == options.DaId) && (options.MetaOnly ? x.IsMeta : true) && (options.RrtOnly ? x.IsReliability : true), new string[] { "SubdutyArea" }).ToListAsync();
            }
            tasks = tasks.Distinct().ToList();

            foreach (var link in tasks)
            {
                var taskVM = new TaskReQualificationTabVM();
                taskVM.RequalificationRequired = link.RequalificationRequired == true && link.RequalificationDueDate != null && link.RequalificationDueDate.Value >= DateOnly.FromDateTime(DateTime.Now);
                taskVM.DueDate = link.RequalificationDueDate;
                taskVM.TaskDescription = link.Description;
                var taskPositions = await _position_TaskService.FindQuery(x => x.TaskId == link.Id).Select(s => s.PositionId).ToListAsync();
                var emps = await _emp_positionService.FindQueryWithIncludeAsync(x => taskPositions.Any(a => x.PositionId == a), new string[] { nameof(_emp_position.Employee) }).Select(s => s.EmployeeId).ToListAsync();
                emps = emps.Distinct().ToList();
                taskVM.EmpLinkCount = emps.Count;
                taskVM.TaskId = link.Id;
                var number = await _task_AppService.GetTaskNumberWithLetter(link.SubdutyAreaId, link.Id);
                taskVM.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + link.Number;
                taskData.Add(taskVM);
            }

            return taskData.OrderBy(o => o.TaskNumber).ToList();
        }

        public async Task<TaskReQualificationTabVM> GetTaskWithNumberAsync(int id)
        {
            var task = await _taskService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (task == null)
            {
                throw new BadHttpRequestException(message: _localizer["TaskNotFound"]);
            }
            else
            {
                var taskVM = new TaskReQualificationTabVM();
                taskVM.RequalificationRequired = task.RequalificationRequired == null ? false : (task.RequalificationRequired ?? false && task.RequalificationDueDate >= DateOnly.FromDateTime(DateTime.Now)); ;
                taskVM.DueDate = task.RequalificationDueDate;
                taskVM.TaskDescription = task.Description;
                taskVM.EmpLinkCount = await _empTaskLinkService.GetCount(x => x.TaskId == task.Id);
                taskVM.TaskId = task.Id;
                var number = await _task_AppService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
                taskVM.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + task.Number;
                return taskVM;
            }
        }

        public async Task<List<TaskQualificationWithoutPosDateVM>> GetEmployeesWithoutTQRecordsAsync()
        {
            List<TaskQualificationWithoutPosDateVM> pendingQuals = new List<TaskQualificationWithoutPosDateVM>();
            var empIds = await _empService.FindQuery(x => x.Active == true).Select(x => x.Id).ToListAsync();
            var empQualIds = await _taskQualificationService.FindQueryWithIncludeAsync(x => x.Active == true && x.Employee.Active == true, new string[] { nameof(_taskQualification.Employee) }).Distinct().Select(s => s.Employee.Id).ToListAsync();
            empQualIds = empIds.Except(empQualIds).ToList();
            foreach (var empQualId in empQualIds)
            {
                if (!pendingQuals.Select(x => x.EmpId).Contains(empQualId))
                {
                    var pendingQual = new TaskQualificationWithoutPosDateVM();

                    var person = await _empService.FindQueryWithIncludeAsync(x => x.Id == empQualId && x.Active == true && x.Person.Active == true, new string[] { nameof(_emp.Person) }).Select(s => s.Person).FirstOrDefaultAsync();
                    pendingQual.EmpLastName = person?.LastName ?? "";
                    pendingQual.EmpFirstName = person?.FirstName ?? "";
                    pendingQual.EmpImage = person?.Image ?? "";
                    pendingQual.EmpId = empQualId;
                    pendingQual.EmpEmail = person?.Username ?? "";
                    //pendingQual.PosCount = await _taskQualificationService.FindQuery(x => x.EmpId == empQualId).Select(s => s.TaskId).Distinct().CountAsync();
                    pendingQual.PosCount = 0;
                    pendingQuals.Add(pendingQual);
                }
            }
            return pendingQuals.OrderBy(o => o.EmpLastName).ToList();
        }

        public async Task<List<TaskQualificationWithoutPosDateVM>> GetPendingQualifications()
        {
            List<TaskQualificationWithoutPosDateVM> pendingQuals = new List<TaskQualificationWithoutPosDateVM>();
            var empQuals = await _taskQualificationService.FindQueryWithIncludeAsync(x => x.Employee.Active == true && x.Employee.Person != null && x.Employee.Person.Active == true && x.TaskQualificationDate == null && (DateTime.Compare(x.DueDate.Value.Date, DateTime.Now.Date) >= 0), new string[] { "Employee.Person" }).Select(s => s.Employee).ToListAsync();
            foreach (var empQual in empQuals)
            {
                if (!pendingQuals.Select(x => x.EmpId).Contains(empQual.Id))
                {
                    var pendingQual = new TaskQualificationWithoutPosDateVM();
                    var person = await _empService.FindQueryWithIncludeAsync(x => x.Id == empQual.Id, new string[] { nameof(_emp.Person) }).Select(s => s.Person).FirstOrDefaultAsync();
                    pendingQual.EmpLastName = person.LastName;
                    pendingQual.EmpFirstName = person.FirstName;
                    pendingQual.EmpImage = person.Image;
                    pendingQual.EmpId = empQual.Id;
                    pendingQual.EmpEmail = person.Username;
                    pendingQual.PosCount = await _taskQualificationService.FindQuery(x => x.EmpId == empQual.Id && x.TaskQualificationDate == null && (DateTime.Compare(x.DueDate.Value.Date, DateTime.Now.Date) >= 0)).Select(s => s.TaskId).Distinct().CountAsync();
                    pendingQuals.Add(pendingQual);
                }
            }

            pendingQuals = pendingQuals.GroupBy(g => g.EmpId).Select(s => s.First()).ToList();

            return pendingQuals.OrderBy(o => o.EmpLastName).ToList();
        }

        public async Task<List<TaskQualificationEmpVM>> GetTQLinkedToEMPAsync(int empId)
        {
            List<TaskQualificationEmpVM> result = new List<TaskQualificationEmpVM>();

            var person = await _empService.FindQueryWithIncludeAsync(x => x.Id == empId, new string[] { nameof(_emp.Person) }).Select(s => s.Person).FirstOrDefaultAsync();

            var taskQualifications = await _taskQualificationService
                .FindQueryWithIncludeAsync(x => x.EmpId == empId && !x.IsRecalled, new string[] { 
                    nameof(_taskQualification.Task), 
                    nameof(_taskQualification.TaskQualification_Evaluator_Links), 
                    nameof(_taskQualification.TaskQualificationStatus), 
                    nameof(_taskQualification.TaskReQualificationEmp_SignOff),
                    nameof(_taskQualification.TQEmpSetting)
                })
                .ToListAsync();

            taskQualifications = taskQualifications
                .GroupBy(tq => tq.TaskId)
                .Select(g => g
                    .OrderByDescending(tq => tq.TQEmpSetting?.ReleaseDate)
                    .ThenByDescending(tq => tq.TaskQualificationDate)
                    .ThenBy(tq => tq.Id) // Done so that this always returns the same record if all other filters are the same
                    .First())
                .ToList();

            foreach (var taskQualification in taskQualifications)
            {
                var trimmedTqComments = taskQualification.Comments?.Trim();
                var validSignOffComments = taskQualification.TaskReQualificationEmp_SignOff.Select(x => x.Comments?.Trim()).Where(c => !string.IsNullOrEmpty(c));
                var combinedComments = new[] { trimmedTqComments }.Concat(validSignOffComments).Where(c => !string.IsNullOrEmpty(c));
                var comments = combinedComments.Any() ? string.Join(", ", combinedComments) : null;

                TaskQualificationEmpVM taskQualTask = new TaskQualificationEmpVM();
                taskQualTask.CriteriaMet = taskQualification.CriteriaMet;
                taskQualTask.Id = taskQualification.Id;
                taskQualTask.EmpId = empId;
                taskQualTask.EmpEmail = person.Username;
                taskQualTask.DueDate = taskQualification.DueDate;
                taskQualTask.QualificationDate = taskQualification.TaskQualificationDate;
                taskQualTask.EmpImage = person.Image;
                taskQualTask.Comments = comments;
                taskQualTask.EmpName = person.FirstName + " " + person.LastName;
                taskQualTask.TaskDescription = taskQualification.Task.Description;
                taskQualTask.EmpReleaseDate = taskQualification.TQEmpSetting?.ReleaseDate;
                taskQualTask.Status = taskQualification.TaskQualificationStatus?.Name;
                taskQualTask.ToolTip = taskQualification.TaskQualificationStatus?.Description;
                var number = await _task_AppService.GetTaskNumberWithLetter(taskQualification.Task.SubdutyAreaId, taskQualification.Task.Id);
                taskQualTask.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
                taskQualTask.Number = taskQualTask.TaskNumber;
                taskQualTask.TaskId = taskQualification.Task.Id;
                taskQualTask.IsReliability = taskQualification.Task.IsReliability;
                taskQualTask.Active = taskQualification.Task.Active;
                List<string> posNames = new List<string>();
                List<string> posIds = new List<string>();
                var posLinks = (await _position_TaskService.FindAsync(x => x.TaskId == taskQualTask.TaskId)).ToList();
                foreach (var poslink in posLinks)
                {
                    var posID = _hasher.Encode(poslink.PositionId.ToString());
                    posIds.Add(posID);
                    var position = (await _positionService.FindAsync(x => x.Id == poslink.PositionId)).FirstOrDefault();
                    if (position != null)
                    {
                        posNames.Add((position.PositionAbbreviation == null || position.PositionAbbreviation == "") ? position.PositionTitle : position.PositionAbbreviation);
                    }
                }
                taskQualTask.PosIds = posIds;
                taskQualTask.PosNames = string.Join(", ", posNames);
                var evalIds = taskQualification.TaskQualification_Evaluator_Links.Select(x => x.EvaluatorId);
                List<string> signedEvaluators = new List<string>();
                List<string> pendingEvaluators = new List<string>();
                foreach (var evalId in evalIds)
                {
                    var tqEmpOff = taskQualification.TaskReQualificationEmp_SignOff.FirstOrDefault(x=>x.EvaluatorId==evalId);
                    var eval = await _empService.FindQueryWithIncludeAsync(x => x.Id == evalId, new string[] { nameof(_emp.Person) }).FirstOrDefaultAsync();
                    if (eval != null && eval.TQEqulator)
                    {
                        var evalName = eval.Person.FirstName + ' ' + eval.Person.LastName;
                        if (tqEmpOff != null && tqEmpOff.IsCompleted.GetValueOrDefault() == true) 
                        {
                            signedEvaluators.Add($"* <b>{evalName}</b>");
                        }
                        else
                        {
                            pendingEvaluators.Add(evalName);
                        }
                    }
                }
                var evalNames = signedEvaluators.Concat(pendingEvaluators).ToList();
                taskQualTask.EvaluatorName = string.Join(", ", evalNames);
                result.Add(taskQualTask);
            }

            var empPosIds = await _emp_positionService.FindQuery(x => x.EmployeeId == empId).Select(s => s.PositionId).ToListAsync();
            var posTasks = await _position_TaskService.FindQueryWithIncludeAsync(x => empPosIds.Any(a => a == x.PositionId), new string[] { nameof(_position_task.Task) }).Select(s => s.Task).ToListAsync();
            foreach (var posTask in posTasks)
            {
                if (!result.Any(a => a.TaskId == posTask.Id))
                {
                    TaskQualificationEmpVM taskQualTask = new TaskQualificationEmpVM();
                    var status = await _tqStatusService.FindQuery(x => x.Id == 1).FirstOrDefaultAsync();
                    taskQualTask.Status = status.Name;
                    taskQualTask.ToolTip = status.Description;
                    taskQualTask.EmpEmail = person.Username;
                    taskQualTask.EmpId = empId;
                    taskQualTask.TaskId = posTask.Id;
                    taskQualTask.TaskDescription = posTask.Description;
                    taskQualTask.CriteriaMet = false;
                    taskQualTask.EmpImage = person.Image;
                    taskQualTask.EmpName = person.FirstName + " " + person.LastName;
                    taskQualTask.EmpReleaseDate = null;
                    taskQualTask.Id = null;
                    taskQualTask.IsReliability = posTask.IsReliability;
                    taskQualTask.Active = posTask.Active;
                    var number = await _task_AppService.GetTaskNumberWithLetter(posTask.SubdutyAreaId, posTask.Id);
                    taskQualTask.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
                    taskQualTask.Number = taskQualTask.TaskNumber;
                    List<string> posNames = new List<string>();
                    List<string> posIds = new List<string>();
                    var posLinks = await _position_TaskService.FindQuery(x => x.TaskId == taskQualTask.TaskId).ToListAsync();
                    foreach (var poslink in posLinks)
                    {
                        var posID = _hasher.Encode(poslink.PositionId.ToString());
                        posIds.Add(posID);
                        var position = await _positionService.FindQuery(x => x.Id == poslink.PositionId).FirstOrDefaultAsync();
                        if (position != null)
                        {
                            posNames.Add((position.PositionAbbreviation == null || position.PositionAbbreviation == "") ? position.PositionTitle : position.PositionAbbreviation);
                        }
                    }
                    taskQualTask.PosIds = posIds;
                    taskQualTask.PosNames = string.Join(", ", posNames);
                    result.Add(taskQualTask);
                }
            }

            return result;
        }

        public async Task<List<TaskQualificationEmpVM>> GetEmpLinkedToTaskAsync(int id)
        {
            List<TaskQualificationEmpVM> result = new List<TaskQualificationEmpVM>();

            var taskQualifications = await _taskQualificationService
                .FindQueryWithIncludeAsync(x => x.TaskId == id && !x.IsRecalled, new string[] { 
                    nameof(_taskQualification.Task), 
                    nameof(_taskQualification.Employee), 
                    nameof(_taskQualification.TaskQualification_Evaluator_Links), 
                    nameof(_taskQualification.TaskQualificationStatus),
                    nameof(_taskQualification.TaskReQualificationEmp_SignOff),
                    nameof(_taskQualification.TQEmpSetting)})
                .ToListAsync();

            taskQualifications = taskQualifications
                .GroupBy(tq => tq.EmpId)
                .Select(g => g
                    .OrderByDescending(tq => tq.TQEmpSetting?.ReleaseDate)
                    .ThenByDescending(tq => tq.TaskQualificationDate)
                    .ThenBy(tq => tq.Id) // Done so that this always returns the same record if all other filters are the same
                    .First())
                .ToList();

            foreach (var taskQualification in taskQualifications)
            {
                var trimmedTqComments = taskQualification.Comments?.Trim();
                var validSignOffComments = taskQualification.TaskReQualificationEmp_SignOff.Select(x => x.Comments?.Trim()).Where(c => !string.IsNullOrEmpty(c));
                var combinedComments = new[] { trimmedTqComments }.Concat(validSignOffComments).Where(c => !string.IsNullOrEmpty(c));
                var comments = combinedComments.Any() ? string.Join(", ", combinedComments) : null;

                var person = await _empService.FindQueryWithIncludeAsync(x => x.Id == taskQualification.Employee.Id, new string[] { nameof(_emp.Person) }).Select(s => s.Person).FirstOrDefaultAsync();
                TaskQualificationEmpVM taskQualEmp = new TaskQualificationEmpVM();
                taskQualEmp.CriteriaMet = taskQualification.CriteriaMet;
                taskQualEmp.Id = taskQualification.Id;
                taskQualEmp.EmpId = taskQualification.Employee.Id;
                taskQualEmp.EmpEmail = person.Username;
                taskQualEmp.DueDate = taskQualification.DueDate;
                taskQualEmp.QualificationDate = taskQualification.TaskQualificationDate;
                taskQualEmp.EmpImage = person.Image;
                taskQualEmp.Comments = comments;
                taskQualEmp.EmpName = person.FirstName + " " + person.LastName;
                taskQualEmp.EmpReleaseDate = taskQualification.TQEmpSetting?.ReleaseDate;
                var posIds = await _emp_positionService.FindQuery(x => x.EmployeeId == taskQualification.Employee.Id).Select(s => s.PositionId).ToArrayAsync();
                foreach (var posId in posIds)
                {
                    taskQualEmp.PosIds.Add(_hasher.Encode(posId.ToString()));
                }
                var number = await _task_AppService.GetTaskNumberWithLetter(taskQualification.Task.SubdutyAreaId, taskQualification.Task.Id);
                taskQualEmp.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
                taskQualEmp.Number = taskQualEmp.TaskNumber;
                taskQualEmp.TaskId = taskQualification.Task.Id;
                var evalIds = taskQualification.TaskQualification_Evaluator_Links.Select(x => x.EvaluatorId);
                List<string> signedEvaluators = new List<string>();
                List<string> pendingEvaluators = new List<string>();
                foreach (var evalId in evalIds)
                {
                    var tqEmpOff = taskQualification.TaskReQualificationEmp_SignOff.FirstOrDefault(x => x.EvaluatorId == evalId);
                    var eval = await _empService.FindQueryWithIncludeAsync(x => x.Id == evalId, new string[] { nameof(_emp.Person) }).FirstOrDefaultAsync();
                    if (eval != null)
                    {
                        var evalName = eval.Person.FirstName + ' ' + eval.Person.LastName;
                        if (tqEmpOff != null && tqEmpOff.IsCompleted.GetValueOrDefault() == true)
                        {
                            signedEvaluators.Add($"* <b>{evalName}</b>");
                        }
                        else
                        {
                            pendingEvaluators.Add(evalName);
                        }
                    }
                }
                var evalNames = signedEvaluators.Concat(pendingEvaluators).ToList();
                taskQualEmp.EvaluatorName = string.Join(", ", evalNames);
                taskQualEmp.Status = taskQualification.TaskQualificationStatus.Name;
                taskQualEmp.ToolTip = taskQualification.TaskQualificationStatus.Description;

                result.Add(taskQualEmp);
            }

            var positionTasks = await _position_TaskService.FindQueryWithIncludeAsync(x => x.TaskId == id, new string[] { nameof(_position_task.Task), nameof(_position_task.Position) }).ToListAsync();

            foreach (var positionTask in positionTasks)
            {
                var employeePositions = await _emp_positionService.FindQueryWithIncludeAsync(x => x.PositionId == positionTask.PositionId, new string[] { nameof(_emp_position.Employee), nameof(_emp_position.Position) }).ToListAsync();
                foreach (var employeePosition in employeePositions)
                {
                    var isExist = result.Find(x => x.TaskId == positionTask.TaskId && x.EmpId == employeePosition.EmployeeId);
                    if (isExist == null)
                    {
                        var person = await _empService.FindQueryWithIncludeAsync(x => x.Id == employeePosition.Employee.Id, new string[] { nameof(_emp.Person) }).Select(s => s.Person).FirstOrDefaultAsync();
                        employeePosition.Employee.Person = person;
                        TaskQualificationEmpVM taskQualEmp = new TaskQualificationEmpVM();
                        taskQualEmp.CriteriaMet = false;
                        taskQualEmp.EmpId = employeePosition.Employee.Id;
                        taskQualEmp.Id = null;
                        taskQualEmp.EmpEmail = person.Username;
                        taskQualEmp.EmpImage = person.Image;
                        taskQualEmp.EmpName = person.FirstName + " " + person.LastName;
                        taskQualEmp.EmpReleaseDate = null;
                        var number = await _task_AppService.GetTaskNumberWithLetter(positionTask.Task.SubdutyAreaId, positionTask.Task.Id);
                        taskQualEmp.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
                        taskQualEmp.Number = taskQualEmp.TaskNumber;
                        taskQualEmp.TaskId = positionTask.TaskId;
                        var status = await _tqStatusService.FindQuery(x => x.Id == 1).FirstOrDefaultAsync();
                        taskQualEmp.Status = status.Name;
                        taskQualEmp.ToolTip = status.Description;
                        var posIds = await _emp_positionService.FindQuery(x => x.EmployeeId == employeePosition.Employee.Id).Select(s => s.PositionId).ToArrayAsync();
                        foreach (var posId in posIds)
                        {
                            taskQualEmp.PosIds.Add(_hasher.Encode(posId.ToString()));
                        }
                        result.Add(taskQualEmp);
                    }
                }


            }

            return result.OrderBy(str => str.TaskNumber, new AlphaNumericSortHelper()).ToList();
        }

        public async Task<List<TaskWithNumberVM>> GetRequalTasksForEmp(int id)
        {
            List<TaskWithNumberVM> tasksWithNumbers = new List<TaskWithNumberVM>();
            var tasks = await _taskQualificationService.FindQueryWithIncludeAsync(x => x.EmpId == id && x.TaskQualificationDate == null && (DateTime.Compare(x.DueDate.Value.Date, DateTime.Now.Date) >= 0), new string[] { nameof(_taskQualification.Task) }).Select(x => x.Task).ToListAsync();
            tasks = tasks.Where(task => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskQualificationOperations.Read).Result.Succeeded).ToList();
            tasks = tasks.Distinct().ToList();
            foreach (var task in tasks)
            {

                var withNum = new TaskWithNumberVM();
                var number = await _task_AppService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
                withNum.SDANumber = number.SDANumber;
                withNum.DANumber = number.DANumber;
                withNum.Letter = number.Letter;
                withNum.CompleteNumber = number.DANumber.ToString() + "." + number.SDANumber.ToString() + "." + number.TaskNumber.ToString();
                withNum.Task = task;
                tasksWithNumbers.Add(withNum);

            }
            return tasksWithNumbers.OrderBy(o => o.CompleteNumber).ToList();
        }

        public async Task<List<TaskWithNumberVM>> GetRequalTasksForEval(int id)
        {
            List<TaskWithNumberVM> tasksWithNumbers = new List<TaskWithNumberVM>();

            var tqs = (await _tq_eval_linkService.FindWithIncludeAsync(x => x.EvaluatorId == id && x.Active == true && x.TaskQualification.Active == true && x.TaskQualification.TaskQualificationDate == null && !x.TaskQualification.IsRecalled && ((x.TaskQualification.DueDate ?? DateTime.Now) >= DateTime.Now), new string[] { "TaskQualification.TQEmpSetting",
                "TaskQualification.TaskQualificationStatus" })).Select(s => s.TaskQualification).GroupBy(tq => tq.TaskId).ToList();

            foreach (var tq in tqs)
            {
                var task = (await _taskService.FindWithIncludeAsync(x => x.Id == tq.Key, new string[] { "SubdutyArea.DutyArea" })).FirstOrDefault();
                var withNum = new TaskWithNumberVM();
                foreach (var taskQualification in tq)
                {
                    var person = await _empService.FindQuery(x => x.Id == taskQualification.EmpId).Select(s => s.Person).FirstOrDefaultAsync();
                    if (task != null)
                    {
                        var empWithTaskObj = new TQEmpWithTasksVM
                        {
                            EmpId = taskQualification.EmpId,
                            EmpFName = person?.FirstName,
                            EmpLName = person?.LastName,
                            EmpImage = person?.Image,
                            EmpEmail = person?.Username,
                            TqId = taskQualification.Id,
                            ReleaseDate = taskQualification.TQEmpSetting.ReleaseDate,
                            DueDate = taskQualification.DueDate,
                            Status = taskQualification.TaskQualificationStatus.Name
                        };
                         withNum.TQEmpWithTasksVM.Add(empWithTaskObj);
                        task.TaskQualifications = new List<TaskQualification>();
                    }
                }
                    var number = await _task_AppService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
                    withNum.SDANumber = number.SDANumber;
                    withNum.DANumber = number.DANumber;
                    withNum.Letter = number.Letter;
                    withNum.Task = task;
                    withNum.CompleteNumber = number.DANumber.ToString() + "." + number.SDANumber.ToString() + "." + number.TaskNumber.ToString();
                    tasksWithNumbers.Add(withNum);
            }
            return tasksWithNumbers.OrderBy(t => t.Task.FullNumber, new AlphaNumericSortHelper()).ToList();
        }

        public async System.Threading.Tasks.Task RemoveEvaluatorAsync(int id)
        {
            var emp = await _empService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (emp == null)
            {
                throw new BadHttpRequestException(message: _localizer["EvaluatorNotFoundException"]);
            }
            else
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, emp, EmployeeOperations.Update);
                if (authResult.Succeeded)
                {
                    emp.TQEqulator = false;
                    var validationResult = await _empService.UpdateAsync(emp);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
        }

        public async Task<List<TQEmpWithTasksVM>> GetEmpWithTasksForTQEvaluator(int id) 
        {
            List<TQEmpWithTasksVM> result = new List<TQEmpWithTasksVM>();
            var taskQualifications_byEmployees = (await _tq_eval_linkService.FindWithIncludeAsync(
             x => x.EvaluatorId == id &&
             x.Active &&
             x.TaskQualification.Active &&
             x.TaskQualification.TaskQualificationDate == null &&
             !x.TaskQualification.IsRecalled &&
             ((x.TaskQualification.DueDate ?? DateTime.Now) >= DateTime.Now),
            new string[] {
                "TaskQualification.TQEmpSetting",
                "TaskQualification.TaskQualificationStatus"
            }))
            .Select(s => s.TaskQualification)
            .GroupBy(tq => tq.EmpId)
            .ToList();

            foreach (var taskQualifications_byEmployee in taskQualifications_byEmployees)
            {
                var empWithTaskObj = new TQEmpWithTasksVM();
                var person = await _empService.FindQuery(x => x.Id == taskQualifications_byEmployee.Key).Select(s => s.Person).FirstOrDefaultAsync();
                    
                foreach (var taskQualification in taskQualifications_byEmployee)
                {
                    var task = (await _taskService.FindWithIncludeAsync(x => x.Id == taskQualification.TaskId, new string[] { "SubdutyArea.DutyArea" })).FirstOrDefault();
                    if (task != null)
                    {
                        var number = await _task_AppService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
                        var taskWithNum = new TaskWithNumberVM
                        {
                            DANumber = number.DANumber,
                            SDANumber = number.SDANumber,
                            Letter = number.Letter,
                            Task = task,
                            TQId = taskQualification.Id,
                            DueDate = taskQualification.DueDate,
                            ReleaseDate = taskQualification.TQEmpSetting.ReleaseDate,
                            Status = taskQualification.TaskQualificationStatus.Name
                        };
                        empWithTaskObj.TasksWithNumber.Add(taskWithNum);
                        task.TaskQualifications = new List<TaskQualification>();
                    }
                }
                empWithTaskObj.TasksWithNumber = empWithTaskObj.TasksWithNumber.OrderBy(t => t.Task.FullNumber, new AlphaNumericSortHelper()).ToList();
                empWithTaskObj.EmpId = taskQualifications_byEmployee.Key;
                empWithTaskObj.EmpFName = person.FirstName;
                empWithTaskObj.EmpLName = person.LastName;
                empWithTaskObj.EmpImage = person.Image;
                empWithTaskObj.EmpEmail = person.Username;
                result.Add(empWithTaskObj);
            }

            return result.OrderBy(o => o.EmpFName).ToList();
        }

        public async Task<List<TQEvaluatorWithCountVM>> GetEvaluatorWithCount()
        {
            List<TQEvaluatorWithCountVM> tqCounts = new List<TQEvaluatorWithCountVM>();
            var tqs = await _empService.FindQueryWithIncludeAsync(x => x.TQEqulator && x.Active == true, new string[] { nameof(_emp.Person) }, true).ToListAsync();
            foreach (var tq in tqs)
            {
                var withCount = new TQEvaluatorWithCountVM();
                var positionlinks = await _emp_positionService.FindQuery(x => x.EmployeeId == tq.Id).OrderBy(o => o.Id).ToListAsync();
                List<Position> positions = new List<Position>();
                foreach (var poslink in positionlinks)
                {
                    var pos = await _positionService.FindQuery(x => x.Id == poslink.PositionId && x.Active == true).Select(s => new Position { Id = s.Id, Active = s.Active, PositionAbbreviation = s.PositionAbbreviation, PositionDescription = s.PositionDescription, PositionTitle = s.PositionTitle, PositionNumber = s.PositionNumber }).FirstOrDefaultAsync();
                    if (pos != null)
                    {
                        positions.Add(pos);
                    }
                }
                var tqEvals = await _tq_eval_linkService.FindQueryWithIncludeAsync(x => x.EvaluatorId == tq.Id && x.Active == true && x.TaskQualification.Active == true && !x.TaskQualification.IsRecalled && x.TaskQualification.TaskQualificationDate == null  && ((x.TaskQualification.DueDate ?? DateTime.Now) >= DateTime.Now), new string[] { nameof(_tq_eval_link.TaskQualification) }).ToListAsync();
                withCount.Count = tqEvals.Count();
                withCount.EvaluatorId = tq.Id;
                withCount.EvaluatorFName = tq.Person.FirstName;
                withCount.EvaluatorLName = tq.Person.LastName;
                withCount.Positions = positions;
                withCount.PositionTitle = "";
                List<string> positionTitles = new List<string>();
                foreach (var position in positions)
                {
                    positionTitles.Add((position.PositionAbbreviation == null || position.PositionAbbreviation == "") ? position.PositionTitle : position.PositionAbbreviation);
                }
                withCount.PositionTitle = string.Join(",", positionTitles);
                tqCounts.Add(withCount);
            }
            return tqCounts.OrderBy(o => o.EvaluatorLName).ToList();
        }

        public async Task<List<TaskReQualificationTabVM>> FilterBySQAsync(TaskRequalificationFilterOptions options)
        {
            List<TaskReQualificationTabVM> taskData = new List<TaskReQualificationTabVM>();
            var taskEOLinks = await _task_EOService.AllQueryWithInclude(new string[] { nameof(_task_eo.Task), nameof(_task_eo.EnablingObjective) }).Where(x => x.Task.Active == true && x.EnablingObjective.IsSkillQualification && (options.MetaOnly ? x.Task.IsMeta : true) && (options.RrtOnly ? x.Task.IsReliability : true)).ToListAsync();
            foreach (var link in taskEOLinks)
            {
                if (link.EnablingObjective.Id == options.SqId || link.EnablingObjective.SubCategoryId == options.SqSubCatId || (link.EnablingObjective.CategoryId == options.SqCatId))
                {
                    var taskVM = new TaskReQualificationTabVM();
                    taskVM.RequalificationRequired = (link.Task.RequalificationRequired ?? false) && link.Task.RequalificationDueDate != null && link.Task.RequalificationDueDate.Value >= DateOnly.FromDateTime(DateTime.Now);
                    taskVM.DueDate = link.Task.RequalificationDueDate;
                    taskVM.TaskDescription = link.Task.Description;
                    var taskPositions = await _position_TaskService.FindQuery(x => x.TaskId == link.Task.Id).Select(s => s.PositionId).ToListAsync();
                    var emps = await _emp_positionService.FindQueryWithIncludeAsync(x => taskPositions.Any(a => x.PositionId == a), new string[] { nameof(_emp_position.Employee) }).Select(s => s.EmployeeId).ToListAsync();
                    emps = emps.Distinct().ToList();
                    taskVM.EmpLinkCount = emps.Count;
                    taskVM.TaskId = link.Task.Id;
                    var number = await _task_AppService.GetTaskNumberWithLetter(link.Task.SubdutyAreaId, link.Task.Id);
                    taskVM.TaskNumber = number.DANumber + "." + number.SDANumber + "." + link.Task.Number;
                    taskData.Add(taskVM);
                }
            }

            return taskData.OrderBy(o => o.TaskNumber).ToList();
        }

        public async Task<List<TaskReQualificationTabVM>> FilterByEMPAsync(TaskRequalificationFilterOptions options)
        {
            List<TaskReQualificationTabVM> taskData = new List<TaskReQualificationTabVM>();
            var taskEmpLinks = await _empTaskLinkService.FindQueryWithIncludeAsync(x => x.Task.Active == true && x.EmployeeId == options.EmpId && (options.MetaOnly ? x.Task.IsMeta : true) && (options.RrtOnly ? x.Task.IsReliability : true), new[] { nameof(_emp_Task.Task) }).ToListAsync();
            foreach (var taskEmpLink in taskEmpLinks)
            {
                var taskVM = new TaskReQualificationTabVM();
                taskVM.RequalificationRequired = (taskEmpLink.Task.RequalificationRequired ?? false) && taskEmpLink.Task.RequalificationDueDate != null && taskEmpLink.Task.RequalificationDueDate.Value >= DateOnly.FromDateTime(DateTime.Now);
                taskVM.DueDate = taskEmpLink.Task.RequalificationDueDate;
                taskVM.TaskDescription = taskEmpLink.Task.Description;
                taskVM.EmpLinkCount = await _empTaskLinkService.GetCount(x => x.TaskId == taskEmpLink.Task.Id);
                taskVM.TaskId = taskEmpLink.Task.Id;
                var number = await _task_AppService.GetTaskNumberWithLetter(taskEmpLink.Task.SubdutyAreaId, taskEmpLink.Task.Id);
                taskVM.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + taskEmpLink.Task.Number;
                taskVM.Number = taskVM.TaskNumber;
                taskData.Add(taskVM);
            }
            return taskData.OrderBy(o => o.TaskNumber).ToList();
        }

        public async Task<List<TaskReQualificationTabVM>> FilterByEvalAsync(TaskRequalificationFilterOptions options)
        {
            List<TaskReQualificationTabVM> taskData = new List<TaskReQualificationTabVM>();
            var taskEmpLinks = await _empTaskLinkService.FindQueryWithIncludeAsync(x => x.Task.Active == true && x.EmployeeId == options.EmpId && x.Employee.TQEqulator && (options.MetaOnly ? x.Task.IsMeta : true) && (options.RrtOnly ? x.Task.IsReliability : true), new[] { nameof(_emp_Task.Task), nameof(_emp_Task.Employee) }).ToListAsync();
            foreach (var taskEmpLink in taskEmpLinks)
            {
                var taskVM = new TaskReQualificationTabVM();
                taskVM.RequalificationRequired = (taskEmpLink.Task.RequalificationRequired ?? false) && taskEmpLink.Task.RequalificationDueDate != null && taskEmpLink.Task.RequalificationDueDate.Value >= DateOnly.FromDateTime(DateTime.Now);
                taskVM.DueDate = taskEmpLink.Task.RequalificationDueDate;
                taskVM.TaskDescription = taskEmpLink.Task.Description;
                var taskPositions = await _position_TaskService.FindQuery(x => x.TaskId == taskEmpLink.Task.Id).Select(s => s.PositionId).ToListAsync();
                var emps = await _emp_positionService.FindQueryWithIncludeAsync(x => taskPositions.Any(a => x.PositionId == a), new string[] { nameof(_emp_position.Employee) }).Select(s => s.EmployeeId).ToListAsync();
                emps = emps.Distinct().ToList();
                taskVM.EmpLinkCount = emps.Count;
                taskVM.TaskId = taskEmpLink.Task.Id;
                var number = await _task_AppService.GetTaskNumberWithLetter(taskEmpLink.Task.SubdutyAreaId, taskEmpLink.Task.Id);
                taskVM.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + taskEmpLink.Task.Number;
                taskData.Add(taskVM);
            }
            return taskData.OrderBy(o => o.TaskNumber).ToList();
        }

        public async Task<List<TaskReQualificationTabVM>> FilterByTGAsync(TaskRequalificationFilterOptions options)
        {
            List<TaskReQualificationTabVM> taskData = new List<TaskReQualificationTabVM>();
            var taskTGLinks = await _task_TrainingGroupService.FindQueryWithIncludeAsync(x => x.Task.Active == true && x.TrainingGroupId == options.TaskTGId && (options.MetaOnly ? x.Task.IsMeta : true) && (options.RrtOnly ? x.Task.IsReliability : true), new string[] { nameof(_task_TrainingGroup.Task) }).ToListAsync();
            foreach (var taskTGLink in taskTGLinks)
            {
                var taskVM = new TaskReQualificationTabVM();
                taskVM.RequalificationRequired = (taskTGLink.Task.RequalificationRequired ?? false) && taskTGLink.Task.RequalificationDueDate != null && taskTGLink.Task.RequalificationDueDate.Value >= DateOnly.FromDateTime(DateTime.Now);
                taskVM.DueDate = taskTGLink.Task.RequalificationDueDate;
                taskVM.TaskDescription = taskTGLink.Task.Description;
                var taskPositions = await _position_TaskService.FindQuery(x => x.TaskId == taskTGLink.Task.Id).Select(s => s.PositionId).ToListAsync();
                var emps = await _emp_positionService.FindQueryWithIncludeAsync(x => taskPositions.Any(a => x.PositionId == a), new string[] { nameof(_emp_position.Employee) }).Select(s => s.EmployeeId).ToListAsync();
                emps = emps.Distinct().ToList();
                taskVM.EmpLinkCount = emps.Count;
                taskVM.TaskId = taskTGLink.Task.Id;
                var number = await _task_AppService.GetTaskNumberWithLetter(taskTGLink.Task.SubdutyAreaId, taskTGLink.Task.Id);
                taskVM.TaskNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + taskTGLink.Task.Number;
                taskData.Add(taskVM);
            }
            return taskData.OrderBy(o => o.TaskNumber).ToList();
        }

        public async Task<List<TaskQualificationWithoutPosDateVM>> GetEmpsWithoutPosQualDateAsync()
        {
            List<TaskQualificationWithoutPosDateVM> empWithouDates = new List<TaskQualificationWithoutPosDateVM>();
            var emp_posLinks = await _emp_positionService.FindQueryWithIncludeAsync(x => !x.Trainee && x.QualificationDate == null && x.Active == true && x.Position.Active == true && x.Employee.Active == true, new string[] { nameof(_emp_position.Employee), nameof(_emp_position.Position) }).Select(s => s.Employee).ToListAsync();
            foreach (var link in emp_posLinks)
            {
                var person = await _empService.FindQueryWithIncludeAsync(x => x.Id == link.Id && x.Active == true && x.Person.Active == true, new string[] { nameof(_emp.Person) }).Select(s => s.Person).FirstOrDefaultAsync();
                var empData = new TaskQualificationWithoutPosDateVM();
                empData.EmpImage = person.Image;
                empData.EmpEmail = person.Username;
                empData.EmpFirstName = person.FirstName;
                empData.EmpLastName = person.LastName;
                empData.EmpId = link.Id;
                empData.PosCount = await _taskQualificationService.FindQuery(x => x.EmpId == link.Id && x.TaskQualificationDate == null && DateTime.Compare(x.DueDate.Value.Date, DateTime.Now.Date) >= 0).CountAsync();
                empWithouDates.Add(empData);
            }
            empWithouDates = empWithouDates.GroupBy(g => g.EmpId).Select(s => s.First()).ToList();
            return empWithouDates.OrderBy(o => o.EmpLastName).ToList();
        }

        public async System.Threading.Tasks.Task UnlinkAll(int id)
        {
            var links = await _tq_evalService.GetTaskEvaluatorLinksByTQIdAsync(id);

            foreach (var link in links)
            {
                var validationResult = await _tq_evalService.DeleteAsync(link);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(",", validationResult.Errors));
                }
            }
        }

        public async Task<TaskQualificationStatsVM> GetStatsAsync()
        {
            TaskQualificationStatsVM taskQualStats = new TaskQualificationStatsVM();
            taskQualStats.WithoutPositionQualDateCount = await _emp_positionService.FindQueryWithIncludeAsync(x => !x.Trainee && x.QualificationDate == null && x.Active == true && x.Position.Active == true && x.Employee.Active == true, new string[] { nameof(_emp_position.Employee), nameof(_emp_position.Position) }).Select(s => s.Employee).Distinct().CountAsync();
            var empIdWithQual = await _taskQualificationService.FindQuery(x => x.Active == true).Select(x => x.EmpId).Distinct().ToListAsync();
            //taskQualStats.WithoutTaskQualificationCount = (await _empService.AllQuery().Select(s => s.Id).Distinct().ToListAsync()).Except(empIdWithQual).Count();
            taskQualStats.WithoutTaskQualificationCount = 0;
            //var positions = await _emp_positionService.FindQueryWithIncludeAsync(x => !x.Employee.TQEqulator,new string[] { nameof(_emp_position.Position),nameof(_emp_position.Employee) }).Select(s => s.Position).Distinct().ToListAsync();
            //foreach(var pos in positions)
            //{
            //    var tasks = await _task_positionService.FindQueryWithIncludeAsync(x => x.PositionId == pos.Id, new string[] { nameof(_task_position.Task) }).Select(s => s.Task).Distinct().ToListAsync();
            //    foreach(var task in tasks)
            //    {
            //        var qual = await _taskQualificationService.FindQuery(x => x.TaskId == task.Id).FirstOrDefaultAsync();
            //        if(qual == null)
            //        {
            //            taskQualStats.WithoutTaskQualificationCount += 1;
            //        }
            //    }
            //    if(tasks.Count < 1)
            //    {
            //        taskQualStats.WithoutTaskQualificationCount += 1;
            //    }
            //}
            var totalEmpIds = await _empService.FindQuery(x => x.Active == true).Select(s => s.Id).ToListAsync();
            var empIds = await _emp_positionService.AllQuery().Select(s => s.EmployeeId).Distinct().ToListAsync();
            var empIdsWithTQ = await _taskQualificationService.FindQueryWithIncludeAsync(x => x.Active == true && x.Employee.Active == true, new string[] { nameof(_taskQualification.Employee) }).Select(s => s.EmpId).Distinct().ToListAsync();
            taskQualStats.WithoutTaskQualificationCount = totalEmpIds.Except(empIdsWithTQ).Count();
            //taskQualStats.WithoutTaskQualificationCount += await _empService.FindQuery(x => !empIds.Contains(x.Id) && !x.TQEqulator).CountAsync();
            taskQualStats.TaskQualificationEvaluatorCount = await _empService.FindQuery(x => x.TQEqulator && x.Active == true).Distinct().CountAsync();
            taskQualStats.PendingRequalificationCount = await _taskQualificationService.FindQueryWithIncludeAsync(x => x.TaskQualificationDate == null && x.Active == true && x.Employee.Active == true && x.Employee.Person.Active == true && ((DateTime.Compare(x.DueDate.Value.Date, DateTime.Now.Date) >= 0)), new string[] { "Employee.Person" }).Select(s => s.Employee).Distinct().CountAsync();
            return taskQualStats;
        }

        public async Task<TaskQualification> CreateTaskQualificationAsync(TaskQualificationCreateOptions options)
        {
            var statusId = getStatusId(options);
            if (options.EvaluationId == 0)
            {
                options.EvaluationId = null;
            }
            var taskQual = new TaskQualification
            {
                EvaluationId = options.EvaluationId,
                EmpId = options.EmpId,
                TQStatusId = statusId,
                Comments = options.Comments,
                DueDate = options.DueDate,
                TaskQualificationDate = options.TaskQualificationDate?.ToUniversalTime(),
                TaskQualificationEvaluator = options.TaskQualificationEvaluator,
                TaskId = options.TaskId,
                CriteriaMet = options.CriteriaMet,
                CreatedDate = DateTime.Now,
                CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id,
            };

            var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskQual, TaskQualificationOperations.Create);
            if (authResult.Succeeded)
            {
                var validationResult = await _taskQualificationService.AddAsync(taskQual);
                if (validationResult.IsValid)
                {
                    foreach (var evalId in options.EvaluatorIds)
                    {
                        var eval = await _empService.FindQuery(x => x.Id == evalId).FirstOrDefaultAsync();
                        taskQual.LinkEvaluator(eval);
                    }
                    await _taskQualificationService.UpdateAsync(taskQual);
                    return taskQual;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        //public async Task<List<TaskQualificationRecentVM>> GetRecentTaskQualificationAsync()
        //{
        //    List<TaskQualificationRecentVM> taskQualifications = new List<TaskQualificationRecentVM>();

        //    var taskQuals = await _taskQualificationService.AllQueryWithInclude(new string[] { nameof(_taskQualification.Task), nameof(_taskQualification.Employee), nameof(_taskQualification.TaskQualification_Evaluator_Links) }).ToListAsync();
        //    foreach (var qual in taskQuals)
        //    {
        //        var person = await _empService.FindQueryWithIncludeAsync(x => x.Id == qual.Employee.Id, new string[] { nameof(_emp.Person) }).Select(s => s.Person).FirstOrDefaultAsync();
        //        qual.Employee.Person = person;
        //        TaskQualificationRecentVM taskQualEmp = new TaskQualificationRecentVM();
        //        taskQualEmp.CriteriaMet = qual.CriteriaMet;
        //        taskQualEmp.Id = qual.Id;
        //        taskQualEmp.EmpId = qual.Employee.Id;
        //        taskQualEmp.EmpEmail = qual.Employee.Person.Username;
        //        taskQualEmp.DueDate = qual.DueDate;
        //        taskQualEmp.QualificationDate = qual.TaskQualificationDate;
        //        taskQualEmp.EmpImage = qual.Employee.Person.Image;
        //        taskQualEmp.Comments = qual.Comments;
        //        taskQualEmp.EmpName = qual.Employee.Person.FirstName + " " + qual.Employee.Person.LastName;
        //        taskQualEmp.EmpReleaseDate = null;
        //        var posIds = await _emp_positionService.FindQuery(x => x.EmployeeId == qual.Employee.Id).Select(s => s.PositionId).ToArrayAsync();
        //        foreach (var posId in posIds)
        //        {
        //            taskQualEmp.PosIds.Add(_hasher.Encode(posId.ToString()));
        //        }
        //        var number = await _task_AppService.GetTaskNumberWithLetter(qual.Task.SubdutyAreaId, qual.Task.Id);
        //        taskQualEmp.TaskNumber = number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
        //        taskQualEmp.TaskId = qual.Task.Id;
        //        var evalIds = qual.TaskQualification_Evaluator_Links.Select(x => x.EvaluatorId);
        //        foreach (var evalId in evalIds)
        //        {
        //            var eval = await _empService.FindQueryWithIncludeAsync(x => x.Id == evalId, new string[] { nameof(_emp.Person) }).FirstOrDefaultAsync();
        //            if (eval != null)
        //            {
        //                taskQualEmp.EvaluatorName = taskQualEmp.EvaluatorName + eval.Person.FirstName + " " + eval.Person.LastName;
        //                if (evalIds.Count() > 1)
        //                {
        //                    taskQualEmp.EvaluatorName = taskQualEmp.EvaluatorName + " , ";
        //                }
        //            }
        //        }
        //        if (taskQualEmp.QualificationDate != null && taskQualEmp.DueDate.Value.Date > taskQualEmp.QualificationDate.Value.Date)
        //        {
        //            taskQualEmp.Status = "On Time";
        //        }
        //        else if (taskQualEmp.QualificationDate == null && DateTime.Now.Date <= taskQualEmp.DueDate.Value.Date)
        //        {
        //            taskQualEmp.Status = "Pending";
        //        }
        //        else if (taskQualEmp.QualificationDate != null && taskQualEmp.DueDate.Value.Date < taskQualEmp.QualificationDate.Value.Date)
        //        {
        //            taskQualEmp.Status = "Delayed";
        //        }
        //        else if (taskQualEmp.QualificationDate == null && (DateTime.Now - taskQualEmp.DueDate).Value.TotalDays > 183)
        //        {
        //            taskQualEmp.Status = "Overdue";
        //        }

        //        taskQualifications.Add(taskQualEmp);
        //    }


        //    return taskQualifications;

        //}

        public int getStatusId(TaskQualificationCreateOptions options)
        {
            if (options.TaskQualificationDate != null && options.DueDate.Value.Date >= options.TaskQualificationDate.Value.Date)
            {
                return 2;
            }
            else if (options.TaskQualificationDate == null && DateTime.Now.Date <= options.DueDate.Value.Date)
            {
                return 3;
            }
            else if (options.TaskQualificationDate != null && options.DueDate.Value.Date < options.TaskQualificationDate.Value.Date)
            {
                return 4;
            }
            else if (options.TaskQualificationDate == null && (DateTime.Now - options.DueDate).Value.TotalDays > 183)
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }

        public async System.Threading.Tasks.Task CreateAndReleaseTaskAndSkillQualifications(TQReleaseByTaskAndSkillOptions options)
        {
            int? evalId = options.EvalMethodId == 0 ? null : options.EvalMethodId;

            if (options.EnablingObjectiveId > 0)
            {
                foreach (var empId in options.EmpIds)
                {
                    var skillQual = new SkillQualification
                    {
                        EnablingObjectiveId = options.EnablingObjectiveId,
                        EmployeeId = empId,
                        EvaluationMethodId = evalId,
                        SkillQualificationStatusId = 3, // pending
                        Comments = "",
                        DueDate = options.DueDate,
                        SkillQualificationDate = null,
                        CriteriaMet = false,
                        IsReleasedToEMP = false,
                        CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id,
                        CreatedDate = DateTime.Now,
                    };

                    var validationResult = await _skillQualificationService.AddAsync(skillQual);

                    if (validationResult.IsValid)
                    {
                        foreach (var evalOption in options.EvaluatorOptions)
                        {
                            var sqEvalLink = new SkillQualification_Evaluator_Link
                            {
                                EvaluatorId = evalOption.EvaluatorId,
                                Number = options.OrderMatters ? evalOption.Number : 0,
                                SkillQualificationId = skillQual.Id
                            };
                            skillQual.SkillQualification_Evaluator_Links.Add(sqEvalLink);
                        }

                        var sqSettings = new SkillQualificationEmpSetting
                        {
                            MultipleSignOff = options.MultiReq,
                            ReleaseDate = options.ReleaseDate.ToUniversalTime(),
                            ShowSkillQuestions = options.ShowQuestions,
                            ShowSkillSuggestions = options.ShowSuggestions,
                            ReleaseInSpecificOrder = options.OrderMatters,
                            ReleaseOnReleaseDate = !options.OrderMatters,
                            ReleaseToAllSingleSignOff = options.OneReq,
                            SkillQualificationId = skillQual.Id,
                            CreatedDate = DateTime.Now,
                            CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id,
                        };

                        skillQual.SkillQualificationEmpSetting = sqSettings;

                        skillQual.Release();
                        await _skillQualificationService.UpdateAsync(skillQual);
                    }
                }
            }
            if (options.TaskId > 0)
            {
                foreach (var empId in options.EmpIds)
                {
                    var taskQual = new TaskQualification
                    {
                        EvaluationId = evalId,
                        EmpId = empId,
                        TQStatusId = 3,
                        Comments = "",
                        DueDate = options.DueDate,
                        TaskQualificationDate = null,
                        TaskQualificationEvaluator = "",
                        TaskId = options.TaskId,
                        CriteriaMet = false,
                        CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id,
                        CreatedDate = DateTime.Now,
                    };

                    var validationResult = await _taskQualificationService.AddAsync(taskQual);

                    if (validationResult.IsValid)
                    {
                        foreach (var evalOption in options.EvaluatorOptions)
                        {
                            var tqEvalLink = new TaskQualification_Evaluator_Link
                            {
                                EvaluatorId = evalOption.EvaluatorId,
                                Number = options.OrderMatters ? evalOption.Number : 0,
                                TaskQualificationId = taskQual.Id
                            };
                            taskQual.TaskQualification_Evaluator_Links.Add(tqEvalLink);
                        }

                        var tqSettings = new TQEmpSetting
                        {
                            MultipleSignOff = options.MultiReq,
                            ReleaseDate = options.ReleaseDate.ToUniversalTime(),
                            ShowTaskQuestions = options.ShowQuestions,
                            ShowTaskSuggestions = options.ShowSuggestions,
                            ReleaseInSpecificOrder = options.OrderMatters,
                            ReleaseOnReleaseDate = !options.OrderMatters,
                            ReleaseToAllSingleSignOff = options.OneReq,
                            TaskQualificationId = taskQual.Id,
                            CreatedDate = DateTime.Now,
                            CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id,
                        };

                        taskQual.TQEmpSetting = tqSettings;

                        taskQual.Release();
                        await _taskQualificationService.UpdateAsync(taskQual);
                    }
                }
            }
        }


        public async System.Threading.Tasks.Task ReassignTaskQualification(ReassignTQVM options)
        {
            var evaluator = await _empService.FindQuery(x => x.Id == options.EvaluatorId).FirstOrDefaultAsync();
            foreach (var tqId in options.TQIds)
            {
                var tq = await _taskQualificationService.FindQuery(x => x.Id == tqId).FirstOrDefaultAsync();
                var empId = tq.EmpId;
                var taskId = tq.TaskId;
                var tqsToReassign = await _taskQualificationService.FindQueryWithIncludeAsync(x =>x.Id==tq.Id && x.EmpId == empId && taskId == x.TaskId, new string[] { nameof(_taskQualification.TaskQualification_Evaluator_Links) }).ToListAsync();
                foreach (var tqToReassign in tqsToReassign)
                {
                    tqToReassign.UnlinkEvaluator(evaluator);
                    foreach (var evalId in options.ReassignEvalIds)
                    {
                        var eval = await _empService.FindQuery(x => x.Id == evalId).FirstOrDefaultAsync();
                        tqToReassign.LinkEvaluator(eval);
                    }
                    tqToReassign.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    tqToReassign.ModifiedDate = DateTime.Now;
                    tqToReassign.IsReleasedToEMP = false;
                    await _taskQualificationService.UpdateAsync(tqToReassign);
                }
            }
        }

        public async Task<List<TaskQualificationEmpVM>> GetRecentTQAsync()
        {
            List<TaskQualificationEmpVM> recentQuals = new List<TaskQualificationEmpVM>();
            var tqs = await _taskQualificationService.GetRecentTaskQualificationsAsync();

            var empIds = new HashSet<int>();
            var evalIds = new HashSet<int>();

            foreach (var tq in tqs)
            {
                empIds.Add(tq.EmpId);
                foreach (var eval in tq.TaskQualification_Evaluator_Links)
                {
                    evalIds.Add(eval.EvaluatorId);
                }
            }

            var employees = await _empService.GetEmployeesPersonDetailsByEmpIds(empIds.ToList());
            var evaluators = await _empService.GetEmployeesPersonDetailsByEmpIds(evalIds.ToList());

            var employeeDict = employees.ToDictionary(e => e.Id);
            var evaluatorDict = evaluators.ToDictionary(e => e.Id);
            var distinctTaskIds = tqs.Select(m => m.TaskId).Distinct().ToList();
            var distinctTqIds = tqs.Select(m => m.Id).Distinct().ToList();
            var tasks = await _taskService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(distinctTaskIds);
            var tqEmpSettings = await _tqEmpSettingService.GetTQEmpSettingByTQIds(distinctTqIds);
            var signOffCounts = await _empSignOffService.GetTaskReQualificationEmp_SignOffListByTQIds(distinctTqIds);
            foreach (var tq in tqs)
            {
                var tqSignOffs = signOffCounts.Where(x => x.TaskQualificationId == tq.Id);
                if (employeeDict.TryGetValue(tq.EmpId, out var employee) && employee.Active)
                {
                    var trimmedTqComments = tq.Comments?.Trim();
                    var validSignOffComments = tqSignOffs.Select(x => x.Comments?.Trim()).Where(c => !string.IsNullOrEmpty(c));
                    var combinedComments = new[] { trimmedTqComments }.Concat(validSignOffComments).Where(c => !string.IsNullOrEmpty(c));
                    var comments = combinedComments.Any() ? string.Join(", ", combinedComments) : null;

                    TaskQualificationEmpVM recentTQ = new TaskQualificationEmpVM
                    {
                        Status = tq.TaskQualificationStatus?.Name ?? "N/A",
                        ToolTip = tq.TaskQualificationStatus?.Description ?? "N/A",
                        TaskId = tq.TaskId,
                        EmpId = tq.EmpId,
                        EmpEmail = employee.Person?.Username ?? "N/A",
                        EmpImage = employee.Person.Image,
                        EmpName = $"{employee.Person.LastName}, {employee.Person.FirstName}",
                        QualificationDate = tq.TaskQualificationDate,
                        Comments = comments,
                        CriteriaMet = tq.CriteriaMet,
                        CreatedDate = tq.CreatedDate,
                        DueDate = tq.DueDate,
                        ModifiedDate = tq.ModifiedDate ?? tq.CreatedDate,
                        Id = tq.Id
                    };

                        tq.Task = tasks.FirstOrDefault(x=>x.Id==tq.TaskId);
                        recentTQ.TaskNumber = tq.Task.FullNumber;
                        recentTQ.Number = recentTQ.TaskNumber;

                        List<string> evalNames = new List<string>();
                        foreach (var eval in tq.TaskQualification_Evaluator_Links)
                        {
                            if (evaluatorDict.TryGetValue(eval.EvaluatorId, out var evaluator))
                            {
                                evalNames.Add($"{evaluator.Person.FirstName} {evaluator.Person.LastName}");
                            }
                        }
                        recentTQ.EvaluatorName = string.Join(',', evalNames);

                        var setting = tqEmpSettings.FirstOrDefault(x=>x.TaskQualificationId==tq.Id);

                        if (setting != null)
                        {
                            recentTQ.EmpReleaseDate = setting.ReleaseDate;
                            var signOffsCount = tqSignOffs.Count();
                            recentTQ.RequiredRequals =
                                setting.ReleaseToAllSingleSignOff
                                ? $"One Sign Off Required - {signOffsCount}/1"
                                : $"{setting.MultipleSignOffDisplay} Sign Offs Required - {signOffsCount}/{setting.MultipleSignOffDisplay}";
                        }

                        recentQuals.Add(recentTQ);
                }
            }

            return recentQuals.OrderByDescending(o => o.ModifiedDate).ToList();
        }


        public async Task<List<TaskQualificationPengingEvaluatorVM>> GetEmployeePendingTaskRequalification()
        {
            List<TaskQualificationPengingEvaluatorVM> taskQualVMs = new List<TaskQualificationPengingEvaluatorVM>();

            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).Select(s => new Person { Id = s.Id, Active = s.Active, FirstName = s.FirstName, Username = s.Username, LastName = s.LastName, MiddleName = s.MiddleName, Image = s.Image }).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _empService.FindQuery(x => x.PersonId == person.Id).Select(s => new Employee { Id = s.Id, Active = s.Active, PersonId = s.PersonId, EmployeeNumber = s.EmployeeNumber }).FirstOrDefaultAsync();
                if (employee == null)
                {
                    throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
                }
                employee.Person = person;

                if (employee.Person != null)
                {
                    var tqEvals = await _tq_eval_linkService.GetPendingTaskQualificationsByEvaluator(employee.Id);
                    var taskIds = tqEvals.Select(tqe => tqe.TaskQualification.TaskId).Distinct().ToList();
                    var tasks = await _taskService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(taskIds);
                    foreach (var tqEval in tqEvals)
                    {
                        tqEval.TaskQualification.Task = tasks.FirstOrDefault(t => t.Id == tqEval.TaskQualification.TaskId);
                    }

                    var taskQualifications = tqEvals.Select(x => x.TaskQualification).Where(x => x.Employee?.Person != null && x.Task != null && x.TQEmpSetting != null && x.TQEmpSetting.ReleaseDate < DateTime.UtcNow); 
                    foreach (var taskQual in taskQualifications)
                    {
                        TaskQualificationPengingEvaluatorVM taskQualVM = new TaskQualificationPengingEvaluatorVM();
                        taskQualVM.Id = taskQual.Id;
                        taskQualVM.EmpId = taskQual.EmpId;
                        taskQualVM.TaskId = taskQual.TaskId;
                        taskQualVM.EmpFName = taskQual.Employee.Person.FirstName;
                        taskQualVM.EmpLastName = taskQual.Employee.Person.LastName;
                        taskQualVM.EmpEmail = taskQual.Employee.Person.Username;
                        taskQualVM.EmpNumber = taskQual.Employee.EmployeeNumber;
                        taskQualVM.EmpPositions = String.Join(",", taskQual.Employee.EmployeePositions.Where(x => x.Active && (x.EndDate == null || x.EndDate >= DateOnly.FromDateTime(DateTime.UtcNow))).Select(x => x.Position.PositionTitle).Distinct());
                        var task = taskQual.Task;
                        taskQualVM.TaskFullNumber = task.FullNumber;
                        taskQualVM.TaskNumber = task.Number;
                        taskQualVM.TaskLetter = task.SubdutyArea.DutyArea.Letter;
                        taskQualVM.TaskDescription = task.Description;
                        taskQualVM.DueDate = taskQual.DueDate;
                        var tqEmpSetting = taskQual.TQEmpSetting;
                        taskQualVM.EmpReleaseDate = tqEmpSetting?.ReleaseDate;
                        taskQualVM.SignOffOrderEnabled = tqEmpSetting?.ReleaseInSpecificOrder ?? false;
                        taskQualVM.ReleaseToAllSingleSignOff = tqEmpSetting.ReleaseToAllSingleSignOff;
                        if (tqEmpSetting != null && tqEmpSetting.ReleaseToAllSingleSignOff)
                        {
                            var checkSignOffs = (await _empSignOffService.FindWithIncludeAsync(x => x.TaskQualificationId == taskQual.Id && x.IsCompleted == true, new string[] { "Evaluator.Person" })).FirstOrDefault();
                            var reaminingEvals = checkSignOffs != null ? 1 : 0;
                            taskQualVM.RequiredRequals = "One Sign Off Required - " + reaminingEvals + "/1";
                        }
                        else
                        {
                            var checkSignOffs = (await _empSignOffService.FindWithIncludeAsync(x => x.TaskQualificationId == taskQual.Id && x.IsCompleted == true, new string[] { "Evaluator.Person" })).ToList();
                            var signOffs = checkSignOffs.Select(x => x.EvaluatorId).ToList();
                            var evaluatorsList = await _tq_eval_linkService.FindQuery(x => x.TaskQualificationId == taskQual.Id).Select(x => x.EvaluatorId).ToListAsync();
                            var reaminingEvals = evaluatorsList.Count - signOffs.Count;
                            taskQualVM.RequiredRequals = tqEmpSetting?.MultipleSignOffDisplay + " Sign Offs Required - " + checkSignOffs.Count + "/" + tqEmpSetting?.MultipleSignOffDisplay;
                        }
                        var evaluators = (await _tq_eval_linkService.FindWithIncludeAsync(x => x.TaskQualificationId == taskQual.Id && (!taskQualVM.ReleaseToAllSingleSignOff || x.EvaluatorId == employee.Id), new string[] { "Evaluator.Person" })).ToList();

                        var listwithStatus = new List<EvaluatorNameWithStatus>();

                        foreach (var eval in evaluators)
                        {
                            if (eval.Evaluator?.Person != null)
                            {
                                var evaluatorQAName = eval.Evaluator.Person.FirstName + " " + eval.Evaluator.Person.LastName;
                                var evaluatorStatus = (await _empSignOffService.FindAsync(x => x.EvaluatorId == eval.EvaluatorId && x.TaskQualificationId == taskQual.Id)).FirstOrDefault();
                                if (evaluatorStatus != null)
                                {
                                    if (evaluatorStatus.IsStarted == true)
                                    {
                                        listwithStatus.Add(new EvaluatorNameWithStatus()
                                        {
                                            EvaluatorId = eval.EvaluatorId,
                                            EvaluatorName = evaluatorQAName,
                                            Status = (evaluatorStatus?.IsCompleted ?? false) ? "Completed" : "Pending",

                                        });

                                    }

                                }
                                else
                                {
                                    if (taskQualVM.SignOffOrderEnabled == true)
                                    {
                                        var getSignOffedIds = (await _empSignOffService.FindAsync(x => x.TaskQualificationId == taskQual.Id && x.IsCompleted == true)).Select(s => s.EvaluatorId).ToList();
                                        var evaluatorforLockingObj = (await _tq_eval_linkService.FindWithIncludeAsync(x => x.TaskQualificationId == taskQual.Id && !getSignOffedIds.Contains(x.EvaluatorId), new string[] { "Evaluator.Person" })).OrderBy(p => p.Id).ToList();

                                        var indexObj = evaluatorforLockingObj.FindIndex(x => !getSignOffedIds.Contains(x.Id) && x.EvaluatorId == eval.EvaluatorId);
                                        if (indexObj != 0 && indexObj > 0)
                                        {
                                            var previousIndexObj = indexObj - 1;
                                            var getPreviousEmployeeObj = evaluatorforLockingObj[previousIndexObj];
                                            var previousEmpStatusObj = (await _empSignOffService.FindAsync(x => x.EvaluatorId == getPreviousEmployeeObj.EvaluatorId && x.TaskQualificationId == taskQual.Id)).FirstOrDefault();
                                            if (previousEmpStatusObj != null)
                                            {
                                                if ((previousEmpStatusObj.IsCompleted ?? false) == false)
                                                {
                                                    listwithStatus.Add(new EvaluatorNameWithStatus()
                                                    {
                                                        EvaluatorId = eval.EvaluatorId,
                                                        EvaluatorName = evaluatorQAName,
                                                        Status = "Locked"

                                                    });
                                                }
                                            }
                                            else
                                            {
                                                listwithStatus.Add(new EvaluatorNameWithStatus()
                                                {
                                                    EvaluatorId = eval.EvaluatorId,
                                                    EvaluatorName = evaluatorQAName,
                                                    Status = "Locked"

                                                });
                                            }

                                        }
                                        else
                                        {
                                            listwithStatus.Add(new EvaluatorNameWithStatus()
                                            {
                                                EvaluatorId = eval.EvaluatorId,
                                                EvaluatorName = evaluatorQAName,
                                                Status = "Not Started"

                                            });
                                        }
                                    }
                                    else
                                    {
                                        listwithStatus.Add(new EvaluatorNameWithStatus()
                                        {
                                            EvaluatorId = eval.EvaluatorId,
                                            EvaluatorName = evaluatorQAName,
                                            Status = "Not Started"

                                        });
                                    }

                                }
                            }
                        }

                        taskQualVM.EvaluatorListWithStatus = listwithStatus;
                        taskQualVM.Status = taskQual.TaskQualificationStatus.Name;
                        taskQualVM.CanStart = !(listwithStatus.Where(x => x.EvaluatorId == employee.Id && x.Status == "Locked").Any());
                        taskQualVMs.Add(taskQualVM);
                    }
                }
            }
            return taskQualVMs.OrderBy(str => str.TaskFullNumber, new AlphaNumericSortHelper()).ToList();
        }




        //public async Task<List<TQTasksWithEmployeesVM>> GetEmpWithTasksForTQEvaluatorEmpTaskView()
        //{
        //    List<TQTasksWithEmployeesVM> TaskWithObjs = new List<TQTasksWithEmployeesVM>();

        //    var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

        //    var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

        //    if (person != null)
        //    {
        //        var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();
        //        var tqs = await _tq_eval_linkService.FindQueryWithIncludeAsync(x => x.EvaluatorId == employee.Id && x.TaskQualification.TaskQualificationDate == null && x.TaskQualification.DueDate.Value.Date >= DateTime.Now.Date, new string[] { "TaskQualification.Task" }).Select(s => s.TaskQualification).ToListAsync();
        //        foreach (var tq in tqs)
        //        {
        //            var setting = await _tqEmpSettingService.FindQuery(x => x.TaskQualificationId == tq.Id).FirstOrDefaultAsync();
        //            DateTime? releaseDate = null;
        //            string RequiredRequals = null;
        //            bool signOffOrderEnabled = false;
        //            if (setting != null)
        //            {
        //                releaseDate = setting.ReleaseDate;
        //                if (setting.ReleaseToAllSingleSignOff)
        //                {
        //                    var checkSignOffs = await _empSignOffService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == tq.Id && x.IsCompleted == true, new string[] { "Employee.Person" }).FirstOrDefaultAsync();


        //                    var reaminingEvals = 0;
        //                    if (checkSignOffs != null)
        //                    {
        //                        reaminingEvals = 1;

        //                    }
        //                    RequiredRequals = "One Sign Off Required - " + reaminingEvals + "/1";

        //                }
        //                else
        //                {
        //                    var checkSignOffs = await _empSignOffService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == tq.Id && x.IsCompleted == true, new string[] { "Employee.Person" }).ToListAsync();
        //                    var signOffs = checkSignOffs.Select(x => x.EvaluatorId).ToList();
        //                    //signoffs required
        //                    var evaluatorsList = await _tq_eval_linkService.FindQuery(x => x.TaskQualificationId == tq.Id).Select(x => x.EvaluatorId).ToListAsync();

        //                    var reaminingEvals = evaluatorsList.Count - signOffs.Count;
        //                    RequiredRequals = setting.MultipleSignOff + " Sign Offs Required - " + reaminingEvals + "/" + setting.MultipleSignOff;
        //                    //RequiredRequals = setting.MultipleSignOff + " Sign Offs Required - X/" + setting.MultipleSignOff;
        //                }
        //                if (setting.ReleaseInSpecificOrder == true)
        //                {
        //                    signOffOrderEnabled = true;
        //                }
        //            }
        //            var task = tq.Task;
        //            var employees = await _taskQualificationService.FindQueryWithIncludeAsync(x => x.Id == tq.Id, new string[] { "Employee.Person" }).Select(x => x.Employee).ToListAsync();
        //            //get evaluators
        //            var evaluators = new List<TaskQualification_Evaluator_Link>();
        //            if (setting.ReleaseToAllSingleSignOff)
        //            {
        //                evaluators = await _tq_eval_linkService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == tq.Id && x.EvaluatorId == employee.Id, new string[] { "Evaluator.Person" }).ToListAsync();
        //            }
        //            else
        //            {
        //                evaluators = await _tq_eval_linkService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == tq.Id && x.EvaluatorId != employee.Id, new string[] { "Evaluator.Person" }).ToListAsync();
        //            }


        //            //check Status in signOff
        //            var listwithStatus = new List<EvaluatorNameWithStatus>();
        //            if (evaluators.Count > 0)
        //            {
        //                foreach (var eval in evaluators)
        //                {
        //                    var evaluatorObj = await _empService.FindQueryWithIncludeAsync(x => x.Id == eval.EvaluatorId, new string[] { "Person" }).FirstOrDefaultAsync();
        //                    var evaluatorQAName = evaluatorObj.Person.FirstName + " " + evaluatorObj.Person.LastName;
        //                    var evaluatorStatus = await _empSignOffService.FindQuery(x => x.EvaluatorId == eval.Id && x.TaskQualificationId == tq.Id).FirstOrDefaultAsync();
        //                    if (evaluatorStatus != null)
        //                    {
        //                        if (signOffOrderEnabled == true)
        //                        {
        //                            var evaluatorforLockingObj = await _tq_eval_linkService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == tq.Id && x.EvaluatorId == employee.Id, new string[] { "Evaluator.Person" }).OrderBy(p => p.Id).ToListAsync();
        //                            var indexObj = evaluatorforLockingObj.FindIndex(x => x.EvaluatorId == employee.Id);
        //                            if (indexObj != 0 && indexObj > 0)
        //                            {
        //                                var previousIndexObj = indexObj - 1;
        //                                var getPreviousEmployeeObj = evaluatorforLockingObj[previousIndexObj];
        //                                var previousEmpStatusObj = await _empSignOffService.FindQuery(x => x.EvaluatorId == getPreviousEmployeeObj.EvaluatorId && x.TaskQualificationId == tq.Id).FirstOrDefaultAsync();
        //                                if (previousEmpStatusObj != null)
        //                                {
        //                                    if (previousEmpStatusObj.IsCompleted == false)
        //                                    {
        //                                        listwithStatus.Add(new EvaluatorNameWithStatus()
        //                                        {

        //                                            EvaluatorName = evaluatorQAName,
        //                                            Status = "Locked"

        //                                        });
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    listwithStatus.Add(new EvaluatorNameWithStatus()
        //                                    {

        //                                        EvaluatorName = evaluatorQAName,
        //                                        Status = "Locked"

        //                                    });
        //                                }

        //                            }
        //                        }


        //                        else
        //                        {
        //                            if (evaluatorStatus.IsStarted == true && evaluatorStatus.IsCompleted == false)
        //                            {
        //                                listwithStatus.Add(new EvaluatorNameWithStatus()
        //                                {

        //                                    EvaluatorName = evaluatorQAName,
        //                                    Status = "Pending"

        //                                });

        //                            }
        //                            else if (evaluatorStatus.IsStarted == true && evaluatorStatus.IsCompleted == true)
        //                            {
        //                                listwithStatus.Add(new EvaluatorNameWithStatus()
        //                                {

        //                                    EvaluatorName = evaluatorQAName,
        //                                    Status = "Completed"

        //                                });

        //                            }

        //                        }

        //                    }
        //                    else
        //                    {
        //                        listwithStatus.Add(new EvaluatorNameWithStatus()
        //                        {

        //                            EvaluatorName = evaluatorQAName,
        //                            Status = "Not Started"

        //                        });

        //                    }
        //                }

        //            }

        //            var employeesList = new List<TQTaskWithEmployeesListVM>();
        //            if (employees.Count > 0)
        //            {
        //                foreach (var emp in employees)
        //                {
        //                    var posIds = await _emp_positionService.FindQuery(x => x.EmployeeId == employee.Id).Select(s => s.PositionId).ToArrayAsync();
        //                    var listPos = new List<string>();
        //                    string positions = string.Empty;
        //                    foreach (var posId in posIds)
        //                    {
        //                        var position = _positionService.GetAsync(posId).Result;
        //                        listPos.Add(position.PositionDescription);

        //                    }
        //                    if (listPos.Count > 0)
        //                    {
        //                        positions = string.Join(',', listPos);
        //                    }
        //                    var tqstats = string.Empty;

        //                    if (signOffOrderEnabled == true)
        //                    {
        //                        var evaluatorforLocking = await _tq_eval_linkService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == tq.Id && x.EvaluatorId == employee.Id, new string[] { "Evaluator.Person" }).OrderBy(p => p.Id).ToListAsync();
        //                        var index = evaluatorforLocking.FindIndex(x => x.EvaluatorId == employee.Id);
        //                        if (index != 0 && index > 0)
        //                        {
        //                            var previousIndex = index - 1;
        //                            var getPreviousEmployee = evaluatorforLocking[previousIndex];
        //                            var previousEmpStatus = await _empSignOffService.FindQuery(x => x.EvaluatorId == getPreviousEmployee.EvaluatorId && x.TaskQualificationId == tq.Id).FirstOrDefaultAsync();
        //                            if (previousEmpStatus != null)
        //                            {
        //                                if (previousEmpStatus.IsCompleted == false)
        //                                {
        //                                    tqstats = "Locked";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                tqstats = "Locked";
        //                            }

        //                        }
        //                    }
        //                    else
        //                    {
        //                        var evaluatorSts = await _empSignOffService.FindQuery(x => x.EvaluatorId == employee.Id && x.TaskQualificationId == tq.Id).FirstOrDefaultAsync();
        //                        if (evaluatorSts == null)
        //                        {
        //                            tqstats = "Not Started";
        //                        }
        //                        else
        //                        {
        //                            if (evaluatorSts.IsCompleted == true)
        //                            {
        //                                tqstats = "Completed";
        //                            }
        //                            else if (evaluatorSts.IsStarted == true && evaluatorSts.IsCompleted == false)
        //                            {
        //                                tqstats = "Pending";
        //                            }
        //                        }

        //                    }

        //                    employeesList.Add(new TQTaskWithEmployeesListVM()
        //                    {
        //                        EmpId = emp.Id,
        //                        EmpName = emp.Person.FirstName + " " + emp.Person.LastName,
        //                        EmpImage = emp.Person.Image,
        //                        Positions = positions,
        //                        TQId = tq.Id,
        //                        DueDate = tq.DueDate,
        //                        ReleaseDate = releaseDate,
        //                        RequiredRequals = RequiredRequals,
        //                        SignOffOrderEnabled = signOffOrderEnabled,
        //                        EvaluatorListWithStatus = listwithStatus,
        //                        TQStatus = tqstats


        //                    });


        //                }

        //            }
        //            var number = await _task_AppService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
        //            if (!TaskWithObjs.Select(x => x.Task.Id).Contains(task.Id))
        //            {
        //                //add with task
        //                TaskWithObjs.Add(new TQTasksWithEmployeesVM
        //                {
        //                    //Task
        //                    DANumber = number.DANumber,
        //                    SDANumber = number.SDANumber,
        //                    Letter = number.Letter,
        //                    Task = task,
        //                    TQTaskWithEmployeesList = employeesList

        //                });


        //            }
        //            else
        //            {
        //                //task is present we need to add Employee only
        //                var taskListIndex = TaskWithObjs.Where(x => x.Task.Id == task.Id).FirstOrDefault();
        //                taskListIndex.TQTaskWithEmployeesList.AddRange(employeesList);

        //            }



        //        }
        //    }
        //    return TaskWithObjs;
        //}


        //New Application Service
        public async Task<List<TaskQualificationEmpVM>> GetPendingTaskRequalificationByEmpId(int employeeId)
        {
            List<TaskQualificationEmpVM> recentQuals = new List<TaskQualificationEmpVM>();

            var employee = (await _empService.FindWithIncludeAsync(x => x.Id == employeeId, new[] { "Person" })).FirstOrDefault();

            if (employee == null)
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
            var tqs = await _taskQualificationService.FindQuery(x => x.EmpId == employeeId).ToListAsync();
            foreach (var tq in tqs)
            {
                tq.Task = (await _taskService.FindAsync(x => x.Id == tq.TaskId)).FirstOrDefault();
                tq.Employee = (await _empService.FindAsync(x => x.Id == tq.EmpId)).FirstOrDefault();
                tq.TaskQualification_Evaluator_Links = (await _tq_eval_linkService.FindAsync(x => x.TaskQualificationId == tq.Id)).ToList();
                tq.TaskQualificationStatus = (await _tqStatusService.FindAsync(x => x.Id == tq.TQStatusId)).FirstOrDefault();

                var setting = await _tqEmpSettingService.FindQuery(x => x.TaskQualificationId == tq.Id).FirstOrDefaultAsync();
                var number = await _task_AppService.GetTaskNumberWithLetter(tq.Task.SubdutyAreaId, tq.Task.Id);


                List<string> evalNames = new List<string>();
                foreach (var evalIds in tq.TaskQualification_Evaluator_Links.Select(s => s.EvaluatorId))
                {
                    var eval = await _empService.FindQuery(x => x.Id == evalIds).FirstOrDefaultAsync();
                    var evalPerson = await _personService.FindQuery(x => x.Id == eval.PersonId).FirstOrDefaultAsync();
                    if (evalPerson != null)
                    {
                        eval.Person = evalPerson;
                        evalNames.Add(eval.Person.FirstName + " " + eval.Person.LastName);
                    }
                }
                DateTime? empReleaseDate = null;
                string requiredRequals = "";

                if (setting != null)
                {
                    empReleaseDate = setting.ReleaseDate;
                    requiredRequals = await GetRequiredRequals(setting, tq.Id);
                }

                var recentTQ = new TaskQualificationEmpVM(tq.TaskQualificationStatus.Name, tq.TaskQualificationStatus.Description, tq.TaskId, tq.EmpId, employee.Person.Username, employee.Person.Image, employee.Person.LastName + ", " + employee.Person.FirstName,
                    tq.TaskQualificationDate, tq.Comments, tq.Task.Description, tq.CriteriaMet, tq.CreatedDate, tq.DueDate,
                    tq.ModifiedDate, tq.Id, number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber, number.Letter, string.Join(',', evalNames), empReleaseDate, requiredRequals,tq.Task.IsReliability,tq.Task.Active, tq.IsRecalled);

                recentQuals.Add(recentTQ);

            }


            var sqs = await _skillQualificationService.GetSkillQualificationByEmployeeIdAsync(employeeId);
            foreach (var sq in sqs)
            {
                sq.EnablingObjective = (await _eoService.GetAsync(sq.EnablingObjectiveId));
                sq.Employee = await _empService.GetAsync(sq.EmployeeId);
                sq.SkillQualification_Evaluator_Links = await _skillQualification_Evaluator_LinkService.GetSkillQualificationEvaluatorLinkByIdAsync(sq.Id);
                sq.SkillQualificationStatus = await _skillQualificationStatusService.GetSkillQualificationStatusByIdAsync(sq.SkillQualificationStatusId);

                var setting = await _skillQualificationEmpSettingService.GetSQSettingBySkillQualificationIdAsync(sq.Id);

                List<string> evalNames = new List<string>();
                foreach (var evalIds in sq.SkillQualification_Evaluator_Links.Select(s => s.EvaluatorId))
                {
                    var eval = (await _empService.FindAsync(x => x.Id == evalIds)).FirstOrDefault();
                    var evalPerson = (await _personService.FindAsync(x => x.Id == eval.PersonId)).FirstOrDefault();
                    if (evalPerson != null)
                    {
                        eval.Person = evalPerson;
                        evalNames.Add(eval.Person.FirstName + " " + eval.Person.LastName);
                    }
                }

                DateTime? empReleaseDate = null;
                string requiredRequals = "";
                if (setting != null)
                {
                    empReleaseDate = setting.ReleaseDate;
                    //requiredRequals = await GetRequiredRequals(setting, sq.Id);
                }

                var recentSQ = new TaskQualificationEmpVM(
                    sq.SkillQualificationStatus.Name, sq.SkillQualificationStatus.Description, sq.EnablingObjectiveId, sq.EmployeeId,
                    employee.Person.Username, employee.Person.Image, employee.Person.LastName + ", " + employee.Person.FirstName,
                    sq.SkillQualificationDate, sq.Comments, sq.EnablingObjective.Description, sq.CriteriaMet, sq.CreatedDate, sq.DueDate,
                    sq.ModifiedDate, sq.Id, sq.EnablingObjective.FullNumber, null, string.Join(',', evalNames), empReleaseDate, requiredRequals,
                    sq.EnablingObjective.IsSkillQualification, sq.EnablingObjective.Active, sq.IsRecalled);

                recentQuals.Add(recentSQ);
            }
            return recentQuals.OrderByDescending(o => o.ModifiedDate).ToList();
        }


        public async Task<string> GetRequiredRequals(TQEmpSetting setting, int taskQualificationId)
        {
            string requiredRequals = "";
            if (setting.ReleaseToAllSingleSignOff)
            {
                var checkSignOffs = (await _empSignOffService.GetTaskReQualificationEmp_SignOffByTQId(taskQualificationId)).FirstOrDefault();

                var reaminingEvals = 0;
                if (checkSignOffs != null)
                {
                    reaminingEvals = 1;
                }
                requiredRequals = "One Sign Off Required - " + reaminingEvals + "/1";
            }
            else
            {
                var checkSignOffs = (await _empSignOffService.GetTaskReQualificationEmp_SignOffByTQId(taskQualificationId)).ToList();
                var signOffs = checkSignOffs.Select(x => x.EvaluatorId).ToList();
                var evaluatorsList = await _tq_eval_linkService.FindQuery(x => x.TaskQualificationId == taskQualificationId).Select(x => x.EvaluatorId).ToListAsync();

                var reaminingEvals = evaluatorsList.Count - signOffs.Count;
                requiredRequals = setting.MultipleSignOffDisplay + " Sign Offs Required - " + reaminingEvals + "/" + setting.MultipleSignOffDisplay;
            }
            return requiredRequals;
        }

        public async Task<List<TQEmpWithPosAndTaskVM>> GetPendingTaskQualificationsAsTraineeAsync(int employeeId)
        {
            List<TQEmpWithPosAndTaskVM> taskQualifications = new List<TQEmpWithPosAndTaskVM>();
            var employee = await _empService.GetEmployeeById(employeeId);
            if (employee == null)
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
            var tqs = await _taskQualificationService.GetPendingTaskQualificationsListAsTraineeByEmpId(employeeId);
            foreach (var tq in tqs)
            {
                var setting = await _tqEmpSettingService.GetTQEmpSettingByTQId(tq.Id);
                DateTime? empReleaseDate = null;
                if (setting != null)
                {
                    empReleaseDate = setting.ReleaseDate;
                }

                if (empReleaseDate.HasValue && empReleaseDate.Value > DateTime.UtcNow)
                {
                    continue;
                }
                tq.Task = await _taskService.GetTaskByIdAsync(tq.TaskId);
                var number = await _task_AppService.GetTaskNumberWithLetter(tq.Task.SubdutyAreaId, tq.Task.Id);
                tq.TaskQualification_Evaluator_Links = await _tq_eval_linkService.GetTaskEvaluatorLinksByTQIdAsync(tq.Id);
                var taskQualification = await _tqEmpSettingService.GetTQEmpSettingByTQId(tq.Id);
                var totalRequiredSignOffsCount = taskQualification.ReleaseToAllSingleSignOff == true ? 1 : taskQualification.MultipleSignOffDisplay;

                List<TQEvalSignOffModel> tQEvalSignOffModels = new List<TQEvalSignOffModel>();
                var completedSignOffsCount = 0;
                string evaluators = "";

                foreach (var evalId in tq.TaskQualification_Evaluator_Links.Select(s => s.EvaluatorId))
                {
                    var eval = await _empService.GetWithPersonAsync(evalId);
                    if (eval != null)
                    {

                        evaluators += String.IsNullOrEmpty(evaluators) ? eval.Person.FirstName + " " + eval.Person.LastName : Environment.NewLine + eval.Person.FirstName + " " + eval.Person.LastName;
                        var totalSignOffs = await _empSignOffService.GetTaskReQualificationsEmp_SignOffByTQId(tq.Id, evalId);
                        if (totalSignOffs.Count > 0)
                        {
                            completedSignOffsCount = completedSignOffsCount + 1;
                        }

                        var tQEvalSignOffModel = new TQEvalSignOffModel(evalId, eval.Person.FirstName + " " + eval.Person.LastName, totalSignOffs.FirstOrDefault()?.SignOffDate);
                        tQEvalSignOffModels.Add(tQEvalSignOffModel);
                    }
                }

                var position = string.Join(",", tq.Task.Position_Tasks.Select(r => r.Position.PositionAbbreviation).Distinct());
                
                var recentTQ = new TQEmpWithPosAndTaskVM(
                        tq.Id,
                        tq.TaskId,
                        tq.EmpId,
                        number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber,
                        tq.Task.Description,
                        tq.DueDate,
                        tQEvalSignOffModels,
                        empReleaseDate,
                        position,
                        totalRequiredSignOffsCount,
                        evaluators,null);

                recentTQ.SetCompletedSignOffCount(completedSignOffsCount);
                taskQualifications.Add(recentTQ);
            }
            return taskQualifications.OrderBy(str => str.Number, new AlphaNumericSortHelper()).ToList();
        }

        public async Task<List<TQEmpWithPosAndTaskVM>> GetCompletedTaskQualificationsAsTraineeAsync(int employeeId)
        {
            List<TQEmpWithPosAndTaskVM> taskQualifications = new List<TQEmpWithPosAndTaskVM>();
            var employee = await _empService.GetEmployeeById(employeeId);
            if (employee == null)
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
            var tqs = await _taskQualificationService.GetCompletedTaskQualificationsListAsTraineeByEmpId(employeeId);
            foreach (var tq in tqs)
            {
                tq.Task = await _taskService.GetTaskByIdAsync(tq.TaskId);
                var number = await _task_AppService.GetTaskNumberWithLetter(tq.Task.SubdutyAreaId, tq.Task.Id);
                tq.TaskQualification_Evaluator_Links = await _tq_eval_linkService.GetTaskEvaluatorLinksByTQIdAsync(tq.Id);
                List<TQEvalSignOffModel> tQEvalSignOffModels = new List<TQEvalSignOffModel>();
                var totalSignOffsCount = 0;
                string evaluators = "";
                foreach (var evalId in tq.TaskQualification_Evaluator_Links.Select(s => s.EvaluatorId))
                {
                    var eval = await _empService.GetWithPersonAsync(evalId);
                    if (eval != null)
                    {
                        //test this line
                        if (eval.Person != null)
                        {
                            evaluators += String.IsNullOrEmpty(evaluators) ? eval.Person.FirstName + " " + eval.Person.LastName : Environment.NewLine + eval.Person.FirstName + " " + eval.Person.LastName;

                            var totalSignOffs = await _empSignOffService.GetTaskReQualificationsEmp_SignOffByTQId(tq.Id, evalId);
                            totalSignOffsCount = totalSignOffs.Count();
                            foreach (var signOffDate in totalSignOffs.Select(s => s.SignOffDate))
                            {
                                var tQEvalSignOffModel = new TQEvalSignOffModel(evalId, eval.Person.FirstName + " " + eval.Person.LastName, signOffDate);
                                tQEvalSignOffModels.Add(tQEvalSignOffModel);
                            }
                        }
                    }
                }
                var position = string.Join(",", tq.Task.Position_Tasks.Select(r => r.Position.PositionAbbreviation).Distinct());
                var setting = await _tqEmpSettingService.GetTQEmpSettingByTQId(tq.Id);
                DateTime? empReleaseDate = null;
                if (setting != null)
                {
                    empReleaseDate = setting.ReleaseDate;
                }
                var recentTQ = new TQEmpWithPosAndTaskVM(
                        tq.Id,
                        tq.TaskId,
                        tq.EmpId,
                        number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber,
                        tq.Task.Description,
                        tq.DueDate,
                        tQEvalSignOffModels,
                        empReleaseDate,
                        position,
                        totalSignOffsCount,
                        evaluators,
                        tq.Comments);
                recentTQ.TaskQualificationDate = tq.TaskQualificationDate;
                recentTQ.CriteriaMet = tq.CriteriaMet;
                taskQualifications.Add(recentTQ);
            }
            return taskQualifications;
        }

        public async Task<List<TQEmpWithPosAndTaskVM>> GetCompletedTaskQualificationsAsEvaluatorAsync(int employeeId)
        {
            List<TQEmpWithPosAndTaskVM> taskQualifications = new List<TQEmpWithPosAndTaskVM>();
            var employee = await _empService.GetEmployeeById(employeeId);
            if (employee == null)
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
            var tqs = await _taskQualificationService.GetCompletedTaskQualificationsListAsEvalByEmpId(employeeId);
            foreach (var tq in tqs)
            {
                tq.Task = await _taskService.GetTaskByIdAsync(tq.TaskId);
                tq.Employee = await _empService.GetWithPersonAsync(tq.EmpId);
                var setting = await _tqEmpSettingService.GetTQEmpSettingByTQId(tq.Id);
                var number = await _task_AppService.GetTaskNumberWithLetter(tq.Task.SubdutyAreaId, tq.Task.Id);
                DateTime? empReleaseDate = null;
                if (setting != null)
                {
                    empReleaseDate = setting.ReleaseDate;
                }
                var recentTQ = new TQEmpWithPosAndTaskVM(tq.Id, tq.TaskId, number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber, tq.Task.Description, tq.DueDate, empReleaseDate, tq.EmpId, tq.Employee.Person.FirstName + " " + tq.Employee.Person.LastName,tq.TaskQualificationDate,tq.CriteriaMet);
                taskQualifications.Add(recentTQ);
            }
            return taskQualifications;
        }
        
        public async Task<bool> GetTQEvaluatorBitAsync(int employeeId)
        {
            return (await _empService.GetAsync(employeeId))?.TQEqulator ?? false;
        }
    }
}
