using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.DutyArea;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.SubdutyArea;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_Collaborator_Link;
using QTD2.Infrastructure.Model.Task_ILA_Link;
using QTD2.Infrastructure.Model.Task_Question;
using QTD2.Infrastructure.Model.Task_Reference_Link;
using QTD2.Infrastructure.Model.Task_RR_Link;
using QTD2.Infrastructure.Model.Task_Step;
using QTD2.Infrastructure.Model.Tool;
using QTD2.Infrastructure.Model.Employee_Task;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IRegulatoryRequirementDomainService = QTD2.Domain.Interfaces.Service.Core.IRegulatoryRequirementService;
using ITask_CollaboratorInvitaitonDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_CollaboratorInvitationService;
using ITask_ReferenceDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_ReferenceService;
using IPosition_TaskDomainService = QTD2.Domain.Interfaces.Service.Core.IPosition_TaskService;
using IILA_TaskObjective_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TaskObjective_LinkService;
using IRR_Task_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IRR_Task_LinkService;
using IEmployeePositionDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeePositionService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using QTD2.Infrastructure.Hashing.Interfaces;
using ITask_ToolsDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_ToolService;
using QTD2.Infrastructure.Model.Task_Suggestion;
using ITask_SuggestionDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_SuggestionService;
using ITask_TrainingGroupDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_TrainingGroupService;
using QTD2.Infrastructure.Model.Task_TrainingGroup;
using ITrainingGroupDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingGroupService;
using IMetaTaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_MetaTask_LinkService;
using IProcedure_Task_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_Task_LinkService;
using ISaftyHazard_Task_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_Task_LinkService;
using ITaskQualificationDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService;
using Task = QTD2.Domain.Entities.Core.Task;
using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Infrastructure.Model.TreeDataVMs;
using System.Security.Cryptography;
using QTD2.Domain.Exceptions;
using System.Threading;
using QTD2.Infrastructure.Model.Position_Task_Link;
using QTD2.Infrastructure.Model.Version_Task;
using IVersion_taskDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_TaskService;

namespace QTD2.Application.Services.Shared
{
    public class TaskService : Interfaces.Services.Shared.ITaskService
    {
        private readonly ITaskService _taskService;
        private readonly ITask_StepService _task_StepService;
        private readonly IDutyAreaService _dutyAreaService;
        private readonly ISubdutyAreaService _subdutyAreaService;
        private readonly IPositionService _positionService;
        private readonly ISaftyHazardService _saftyHazardService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IProcedureService _procedureService;
        private readonly IToolService _toolService;
        private readonly ISaftyHazard_Task_LinkDomainService _SaftyHazard_task_LinkService;
        private readonly ITask_EnablingObjective_LinkService _task_EnablingObjective_LinkService;
        private readonly ITask_QuestionService _taskquestionService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TaskService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITask_ReferenceDomainService _taskRefService;
        private readonly IILADomainService _ilaService;
        private readonly IRegulatoryRequirementDomainService _rrService;
        private readonly ITask_CollaboratorInvitaitonDomainService _taskColabService;
        private readonly IPosition_TaskDomainService _position_taskService;
        private readonly UserManager<AppUser> _userManager;
        private readonly DutyArea _dutyArea;
        private readonly SubdutyArea _subDutyArea;
        private readonly Domain.Entities.Core.Task _task;
        private readonly Domain.Entities.Core.Position_Task _pos_task;
        private readonly Domain.Entities.Core.Task_EnablingObjective_Link _task_EO_Link;
        private readonly Procedure_Task_Link _task_proc_link;
        private readonly SafetyHazard_Task_Link _sh_task_link;
        private readonly IILA_TaskObjective_LinkDomainService _task_ila_linkService;
        private readonly ILA_TaskObjective_Link _ila_task_link;
        private readonly RR_Task_Link _rr_task_link;
        private readonly IRR_Task_LinkDomainService _rr_task_linkService;
        private readonly Task_Step _task_step;
        private readonly IEmployeePositionDomainService _emp_posService;
        private readonly IPersonDomainService _personSrvce;
        private readonly IEmployeeService _emp_Service;
        private readonly EmployeePosition _empPos;
        private readonly Employee _emp;
        private readonly IHasher _hasher;
        private readonly ITask_ToolsDomainService _task_toolService;
        private readonly ITask_SuggestionDomainService _task_suggestionService;
        private readonly IMetaTaskDomainService _metaTask_task_linkService;
        private readonly ITask_TrainingGroupDomainService _task_trainingGroup_service;
        private readonly Task_TrainingGroup _task_trainingGroup;
        private readonly ITrainingGroupDomainService _trainigGroup_Service;
        private readonly Task_CollaboratorInvitation _task_colab;
        private readonly Task_MetaTask_Link _task_metatask_link;
        private readonly IProcedure_Task_LinkDomainService _procedure_taskService;
        private readonly ITaskQualificationDomainService _task_qualService;
        private readonly IVersion_taskDomainService _version_TaskService;

        public TaskService(
            ITaskService taskService,
            ITask_StepService task_StepService,
            IDutyAreaService dutyAreaService,
            ISubdutyAreaService subdutyAreaService,
            IPositionService positionService,
            ISaftyHazardService saftyHazardService,
            IEnablingObjectiveService enablingObjectiveService,
            IProcedureService procedureService,
            IToolService toolService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            ISaftyHazard_Task_LinkDomainService task_SaftyHazard_LinkService,
            ITask_QuestionService taskquestionService,
            ITask_EnablingObjective_LinkService task_EnablingObjective_LinkService,
            IStringLocalizer<TaskService> localizer,
            UserManager<AppUser> userManager,
            ITask_ReferenceDomainService taskRefService,
            IILADomainService ilaService,
            IRegulatoryRequirementDomainService rrService,
            ITask_CollaboratorInvitaitonDomainService taskColabService,
            IILA_TaskObjective_LinkDomainService task_ila_linkService,
            IRR_Task_LinkDomainService task_rr_linkService,
            IEmployeePositionDomainService emp_pos, IPersonDomainService personSrvce,
            IEmployeeService emp_Service,
            IHasher hasher,
            ITask_ToolsDomainService task_toolService,
            ITask_SuggestionDomainService task_suggestionService,
            ITask_TrainingGroupDomainService task_trainingGroup_service,
            ITrainingGroupDomainService trainigGroup_Service,
            IMetaTaskDomainService metaTask_task_linkService, IProcedure_Task_LinkDomainService procedure_taskService, IPosition_TaskDomainService position_taskService, ITaskQualificationDomainService task_qualService, IVersion_taskDomainService version_TaskService)
        {
            _taskService = taskService;
            _task_StepService = task_StepService;
            _dutyAreaService = dutyAreaService;
            _subdutyAreaService = subdutyAreaService;
            _positionService = positionService;
            _saftyHazardService = saftyHazardService;
            _enablingObjectiveService = enablingObjectiveService;
            _procedureService = procedureService;
            _toolService = toolService;
            _SaftyHazard_task_LinkService = task_SaftyHazard_LinkService;
            _task_EnablingObjective_LinkService = task_EnablingObjective_LinkService;
            _taskquestionService = taskquestionService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _dutyArea = new DutyArea();
            _subDutyArea = new SubdutyArea();
            _taskRefService = taskRefService;
            _task = new Domain.Entities.Core.Task();
            _pos_task = new Position_Task();
            _ilaService = ilaService;
            _rrService = rrService;
            _taskColabService = taskColabService;
            _task_EO_Link = new Task_EnablingObjective_Link();
            _task_proc_link = new Procedure_Task_Link();
            _sh_task_link = new SafetyHazard_Task_Link();
            _task_ila_linkService = task_ila_linkService;
            _ila_task_link = new ILA_TaskObjective_Link();
            _rr_task_link = new RR_Task_Link();
            _rr_task_linkService = task_rr_linkService;
            _task_step = new Task_Step();
            _emp_posService = emp_pos;
            _empPos = new EmployeePosition();
            _personSrvce = personSrvce;
            _emp_Service = emp_Service;
            _emp = new Employee();
            _hasher = hasher;
            _task_toolService = task_toolService;
            _task_suggestionService = task_suggestionService;
            _task_trainingGroup_service = task_trainingGroup_service;
            _task_trainingGroup = new Task_TrainingGroup();
            _trainigGroup_Service = trainigGroup_Service;
            _metaTask_task_linkService = metaTask_task_linkService;
            _task_colab = new Task_CollaboratorInvitation();
            _task_metatask_link = new Task_MetaTask_Link();
            _procedure_taskService = procedure_taskService;
            _position_taskService = position_taskService;
            _task_qualService = task_qualService;
            _version_TaskService = version_TaskService;
        }

        public async Task<List<Tool>> GetToolsAsync(int taskId)
        {
            var task = await _taskService.FindQuery(x => x.Id == taskId).FirstOrDefaultAsync();
            if (task == null)
            {
                throw new BadHttpRequestException(message: _localizer["TaskNotFound"]);
            }
            else
            {
                List<Tool> tools = new List<Tool>();
                var taskTools = await _task_toolService.FindQueryWithIncludeAsync(x => x.TaskId == taskId, new string[] { "Tool.ToolCategory" }).ToListAsync();
                foreach (var task_tool in taskTools)
                {
                    var tool = await _toolService.FindQuery(x => x.Id == task_tool.ToolId).FirstOrDefaultAsync();
                    if (tool != null)
                    {
                        tools.Add(tool);
                    }
                }
                return tools;
            }
        }

        public async System.Threading.Tasks.Task UpdateToolsAsync(int taskId, TaskOptions options)
        {
            await DeleteAllToolLinks(taskId);
            var task = await _taskService.FindQuery(x => x.Id == taskId).FirstOrDefaultAsync();
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            List<Tool> tools = new List<Tool>();
            foreach (var toolId in options.ToolIds)
            {
                var tool = await _toolService.GetAsync(toolId);

                var toolsResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read);

                if (taskResult.Succeeded && toolsResult.Succeeded)
                {
                    var task_tool = task.AddTool(tool, task.Id);
                    task_tool.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    task_tool.CreatedDate = DateTime.Now;
                    var validationResult = await _taskService.UpdateAsync(task);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        tools.Add(tool);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task DeleteAllToolLinks(int taskId)
        {
            var task_tools = await _task_toolService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            foreach (var tool in task_tools)
            {
                var validationResult = await _task_toolService.DeleteAsync(tool);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
        }


        public async Task<List<Tool>> AddToolAsync(int taskId, ToolAddOptions options)
        {
            var task = await GetAsync(taskId);
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            List<Tool> tools = new List<Tool>();
            foreach (var toolId in options.ToolIds)
            {

                var tool = await _toolService.GetAsync(toolId);

                var toolsResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read);

                if (taskResult.Succeeded && toolsResult.Succeeded)
                {
                    var task_tool = task.AddTool(tool, task.Id);
                    task_tool.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    task_tool.CreatedDate = DateTime.Now;
                    var validationResult = await _taskService.UpdateAsync(task);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        tools.Add(tool);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return tools;
        }

        public async Task<List<Task_Tool>> GetTaskToolLinksAsync(int taskId)
        {
            var task_tools = await _task_toolService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            return task_tools;
        }

        public async Task<Domain.Entities.Core.Task> CreateAsync(TaskCreateOptions options)
        {
            var sda = await _subdutyAreaService.GetWithIncludeAsync(options.SubDutyAreaId, new string[] { nameof(_subDutyArea.Tasks) });
            int taskNumber = (await _taskService.AllQueryWithIncludeAndDeleted(new string[] { nameof(_task.SubdutyArea) }).Where(x => x.SubdutyAreaId == options.SubDutyAreaId).ToListAsync()).LastOrDefault()?.Number ?? 0;
            var checkTask = await _taskService.FindQuery(x => x.Number == options.Number && x.SubdutyAreaId == options.SubDutyAreaId).FirstOrDefaultAsync();
            if (checkTask == null)
            {
                var task = new Domain.Entities.Core.Task(
                sda.Id,
                options.Description,
                options.Number,
                options.Criteria,
                options.Critical,
                options.References,
                options.RequiredTime,
                options.Abbreviation,
                options.TaskCriteriaUpload,
                options.Image,
                options.IsMeta,
                options.IsReliability,
                options.Conditions,
                DateOnly.FromDateTime(options.EffectiveDate));

                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Create);
                if (result.Succeeded)
                {
                    task.RequalificationDueDate = DateOnly.FromDateTime(options.EffectiveDate).AddMonths(6);
                    task = sda.AddTask(task);
                    task.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    task.CreatedDate = DateTime.Now;
                    var validationResult = await _taskService.AddAsync(task);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return task;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Task Number In Use"]);
            }

        }

        public async Task<Domain.Entities.Core.Task> CopyTask(int taskId, TaskCopyOptions options)
        {
            var sda = await _subdutyAreaService.FindQueryWithIncludeAsync(x => x.Id == options.SubDutyAreaId, new string[] { nameof(_subDutyArea.Tasks) }, true).FirstOrDefaultAsync();
            int taskNumber = (await _taskService.AllQueryWithIncludeAndDeleted(new string[] { nameof(_task.SubdutyArea) }).Where(x => x.SubdutyAreaId == options.SubDutyAreaId).ToListAsync()).Max(x => (int?)x.Number) ?? 0;
            options.Number = taskNumber + 1;

            var user = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var existingTask = await _taskService.GetForCopyAsync(taskId);

            var task = existingTask.Copy(user, sda.Id, options.Description, options.EffectiveDate, options.Number, options.IsReliability, options.PositionIds);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Create);
            if (result.Succeeded)
            {
                var validationResult = await _taskService.AddAsync(task);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {                   
                    return task;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<DutyArea> CreateDutyAreaAsync(DutyAreaCreateOptions options)
        {
            //int dANumber = (await GetDutyAreasAsync()).LastOrDefault()?.Number ?? 0;
            //options.Number = dANumber + 1;
            var dutyArea = await _dutyAreaService.FindQuery(x => x.Letter == options.Letter && x.Number == options.Number).FirstOrDefaultAsync();

            if (dutyArea != null)
            {
                throw new BadHttpRequestException(_localizer["DutyAreaWithSameNumberAlreadyExists"].Value);

            }
            dutyArea = new DutyArea(options.Title, options.Description, options.Letter, options.Number, options.EffectiveDate, options.ReasonForRevision);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, dutyArea, DutyAreaOperations.Create);
            if (result.Succeeded)
            {
                dutyArea.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                dutyArea.CreatedDate = DateTime.Now;
                var validationResult = await _dutyAreaService.AddAsync(dutyArea);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return dutyArea;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<DutyArea> UpdateDutyAreaAsync(int id, DutyAreaUpdateOptions options)
        {
            //var dutyArea = await GetDutyAreaAsync(id);
            var dutyArea = await _dutyAreaService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, dutyArea, DutyAreaOperations.Update);
            if (result.Succeeded)
            {
                //dutyArea.Number = options.Number;
                dutyArea.Title = options.Title;
                dutyArea.Description = options.Description;
                dutyArea.EffectiveDate = options.EffectiveDate;
                dutyArea.ReasonForRevision = options.ReasonForRevision;
                dutyArea.Letter = options.Letter;
                dutyArea.Number = options.Number;
                dutyArea.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                dutyArea.ModifiedDate = DateTime.Now;
                var numValidation = await _dutyAreaService.FindQuery(x => x.Id != dutyArea.Id && x.Number == dutyArea.Number && x.Letter == dutyArea.Letter).FirstOrDefaultAsync();
                if (numValidation == null)
                {
                    var validationResult = await _dutyAreaService.UpdateAsync(dutyArea);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return dutyArea;
                    }
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["Duty Area number in use"]);
                }

            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["Duty Area Number Already Exists"]);
            }
        }

        public async Task<Task_Step> CreateStepAsync(int taskId, Task_StepCreateOptions options)
        {
            var task = await GetAsync(taskId);
            var step = new Task_Step(task.Id, options.Description, options.Number, options.ParentStepId);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, step, Task_StepOperations.Create);

            if (result.Succeeded)
            {
                step = task.AddStep(step);
                step.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                step.CreatedDate = DateTime.Now;
                var validationResult = await _taskService.UpdateAsync(task);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

                return step;
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<int> GetTaskStepNumber(int id)
        {
            var num = await _task_StepService.AllQueryWithIncludeAndDeleted(new string[] { nameof(_task_step.Task) }).Where(x => x.TaskId == id).Select(x => x.Id).ToListAsync();
            return num.Count + 1;
        }

        public async Task<List<SubdutyArea>> getAllSubDutyAreas()
        {
            var sda = await _subdutyAreaService.AllQuery().ToListAsync();
            return sda;
        }

        public async Task<SubdutyArea> CreateSubDutyAreaAsync(int dutyAreaId, SubdutyAreaCreateOptions options)
        {
            var da = await _dutyAreaService.GetAsync(dutyAreaId);
            var sda = await _subdutyAreaService.FindQuery(r => r.DutyAreaId == dutyAreaId && r.SubNumber == options.SubNumber).FirstOrDefaultAsync();
            if (sda != null)
            {
                throw new BadHttpRequestException(_localizer["SubdDutyAreaExists"]);
            }

            int subNumber = (await _subdutyAreaService.FindAsync(r => r.DutyAreaId == da.Id)).LastOrDefault()?.SubNumber ?? 0;
            options.SubNumber = subNumber + 1;
            var subdutyArea = new SubdutyArea(da.Id, options.Description, options.SubNumber, options.Title, options.EffectiveDate, options.ReasonForRevision);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, subdutyArea, SubdutyAreaOperations.Create);
            if (result.Succeeded)
            {
                //subdutyArea = da.AddSubduty(subdutyArea);

                subdutyArea.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                subdutyArea.CreatedDate = DateTime.Now;
                var validationResult = await _subdutyAreaService.AddAsync(subdutyArea);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return subdutyArea;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int taskId)
        {
            var task = await _taskService.FindQuery(x => x.Id == taskId).FirstOrDefaultAsync();

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Delete);

            if (result.Succeeded)
            {
                task.Delete();
                var validationResult = await _taskService.UpdateAsync(task);
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

        public async System.Threading.Tasks.Task DeactivateAsync(int taskId)
        {
            var task = await _taskService.FindQuery(x => x.Id == taskId).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);

            if (result.Succeeded)
            {
                task.Deactivate();
                var validationResult = await _taskService.UpdateAsync(task);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    //await checkSDAActive(task);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task checkSDAActive(QTD2.Domain.Entities.Core.Task task)
        {
            var sda = await _subdutyAreaService.FindQueryWithIncludeAsync(x => x.Id == task.SubdutyAreaId, new string[] { nameof(_subDutyArea.Tasks) }).FirstOrDefaultAsync();
            if (sda != null && sda.Tasks.All(a => !a.Active))
            {
                sda.Deactivate();
                await _subdutyAreaService.UpdateAsync(sda);
                await CheckDAInactive(sda);
            }
        }

        public async System.Threading.Tasks.Task CheckDAInactive(SubdutyArea sda)
        {
            var da = await _dutyAreaService.FindQueryWithIncludeAsync(x => x.Id == sda.DutyAreaId, new string[] { nameof(_dutyArea.SubdutyAreas) }).FirstOrDefaultAsync();
            if (da != null && da.SubdutyAreas.All(x => !x.Active))
            {
                da.Deactivate();
                await _dutyAreaService.UpdateAsync(da);
            }
        }

        public async System.Threading.Tasks.Task ActivateAsync(int taskId)
        {
            var task = await GetAsync(taskId);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);

            if (result.Succeeded)
            {
                task.Activate();
                var validationResult = await _taskService.UpdateAsync(task);
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

        public async Task<List<Domain.Entities.Core.Task>> GetAsync()
        {
            var tasks = await _taskService.AllAsync();
            tasks = tasks.Where(task => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read).Result.Succeeded);
            return tasks?.ToList();
        }

        public async Task<TaskWithAllLinkedDataVM> GetAllLinkDataOfTaskAsync(int id)
        {
            TaskWithAllLinkedDataVM taskWithData = new TaskWithAllLinkedDataVM();

            taskWithData.TaskPositionWithCount = await GetLinkedPositionsAsync(id);
            taskWithData.ILAWithCountOptions = await GetLinkedILAWithCount(id);
            taskWithData.EnablingObjectiveWithCountOptions = await GetLinkedEOWithCount(id);
            taskWithData.ProceduresWithLinkCounts = await GetLinkedProcedureWithCount(id);
            taskWithData.SafetyHazardWithLinkCounts = await GetLinkedSHWithCount(id);
            taskWithData.RegulatoryRequirementWithLinkCounts = await GetLinkedRRWithCount(id);
            taskWithData.TrainingGroups = await GetLinkedTrainingGroups(id);
            taskWithData.Tools = await GetToolsDataByTaskIdAsync(id);
            taskWithData.Task_Steps = await GetTask_StepsDescriptionAsync(id);
            taskWithData.Task_Suggestions = await GetTask_SuggestionsAsync(id);
            return taskWithData;
        }

        public async Task<Domain.Entities.Core.Task> GetAsync(int taskId)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { "SubdutyArea.DutyArea", "Task_MetaTask_Links" }).FirstOrDefaultAsync();
            if (task != null)
            {
                //task.SubdutyArea.DutyArea = await _dutyAreaService.FindQuery(x => x.Id == task.SubdutyArea.Id).FirstOrDefaultAsync();
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                if (result.Succeeded)
                {
                    return task;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["TaskNotFound"]);
            }
        }

        public async Task<Domain.Entities.Core.Task> GetAllTaskDataWithLinks(int id)
        {
            var task = await _taskService.AllQueryWithInclude(new string[] { "Task_Tools", "Task_Steps", "TaskQualifications.TQEmpSetting" }).Where(x => x.Id == id).FirstOrDefaultAsync();
            return task;
        }

        public async Task<DutyArea> GetDutyAreaAsync(int dutyAreaId)
        {
            var dutyArea = await _dutyAreaService.GetAsync(dutyAreaId);
            if (dutyArea != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, dutyArea, DutyAreaOperations.Read);
                if (result.Succeeded)
                {
                    return dutyArea;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["DutyAreaNotFound"]);
            }
        }

        public async Task<List<DutyArea>> GetDutyAreasAsync()
        {
            var dutyAreas = await _dutyAreaService.AllAsync();
            dutyAreas = dutyAreas.Where(da => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, da, DutyAreaOperations.Read).Result.Succeeded);
            dutyAreas = dutyAreas.OrderBy(da =>
            {
                return char.IsLetter(da.Letter[0]) ? 0 : 1;
            })
            .ThenBy(da =>
            {
                return char.IsLetter(da.Letter[0]) ? da.Letter : da.Letter[0].ToString();
            })
            .ThenBy(da => da.Number)
            .ToList();
            return dutyAreas.ToList();
        }

        public async Task<List<DutyArea>> GetDutyAreasOrderedByAsync(string orderBy)
        {
            var dutyAreas = await _dutyAreaService.AllQuery().ToListAsync();
            switch (orderBy.Trim().ToLower())
            {
                case "num":
                    dutyAreas = dutyAreas.OrderBy(o => o.Letter).ThenBy(t => t.Number).ThenBy(t => t.Title).ToList();
                    break;
            }

            dutyAreas = dutyAreas.Where(da => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, da, DutyAreaOperations.Read).Result.Succeeded).ToList();
            return dutyAreas;
        }

        public async Task<List<DutyArea>> GetDutyAreasWithSubDutyAreaAsync()
        {
            var dutyAreas = (await _dutyAreaService.GetAllOrderByNumber()).GroupBy(g => g.Letter).SelectMany(s => s).ToList();
            var subDutyAreas = await _subdutyAreaService.GetAllOrderByNumber();
            var tasks = await _taskService.GetAllTasksOrderByNumber();
            //var subDutyArea = await _subdutyAreaService.AllQueryWithInclude(new string[] { nameof(_subDutyArea.Tasks) }).ToListAsync();
            //foreach (var da in dutyAreas)
            //{
            //    var list = subDutyArea.Where(x => x.DutyAreaId == da.Id);

            //    foreach (var data in list)
            //        da.SubdutyAreas.Add(data);

            //}

            for (int i = 0; i < dutyAreas.Count; i++)
            {
                dutyAreas[i].SubdutyAreas = subDutyAreas.Where(w => w.DutyAreaId == dutyAreas[i].Id).ToList();
                for (int j = 0; j < dutyAreas[i].SubdutyAreas.Count; j++)
                {
                    dutyAreas[i].SubdutyAreas.ToList()[j].Tasks = tasks.Where(w => w.SubdutyAreaId == dutyAreas[i].SubdutyAreas.ToList()[j].Id).ToList();
                }
            }

            dutyAreas = dutyAreas.Where(da => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, da, DutyAreaOperations.Read).Result.Succeeded).ToList();
            return dutyAreas?.ToList();
        }

        public async Task<List<DutyAreaTreeVM>> GetMinimizedTreeDataForTaskTree()
        {
            List<DutyAreaTreeVM> returnVM = new List<DutyAreaTreeVM>();
            var dutyAreas = (await _dutyAreaService.GetAllOrderByNumber()).GroupBy(g => g.Letter).SelectMany(s => s).ToList();
            var subDutyAreas = await _subdutyAreaService.GetAllOrderByNumber();
            var tasks = (await _taskService.GetMinimizedTaskForTree()).OrderBy(o => o.Number);
            //var subDutyArea = await _subdutyAreaService.AllQueryWithInclude(new string[] { nameof(_subDutyArea.Tasks) }).ToListAsync();
            //foreach (var da in dutyAreas)
            //{
            //    var list = subDutyArea.Where(x => x.DutyAreaId == da.Id);

            //    foreach (var data in list)
            //        da.SubdutyAreas.Add(data);

            //}

            foreach (var da in dutyAreas)
            {
                DutyAreaTreeVM daData = new DutyAreaTreeVM
                {
                    Active = da.Active,
                    Id = da.Id,
                    Letter = da.Letter,
                    Number = da.Number,
                    Title = da.Title,
                    SubdutyAreas = subDutyAreas.Where(w => w.DutyAreaId == da.Id).Select(s => new SubDutyAreaTreeVM
                    {
                        Id = s.Id,
                        Active = s.Active,
                        SubNumber = s.SubNumber,
                        Title = s.Title,
                        Tasks = tasks.Where(w => w.SubdutyAreaId == s.Id).Select(t => new TaskTreeDataVM
                        {
                            Active = t.Active,
                            Description = t.Description,
                            Id = t.Id,
                            IsMeta = t.IsMeta,
                            IsReliability = t.IsReliability,
                            Number = t.Number,
                            Position_Tasks =  t.Position_Tasks
                        }).ToList(),
                    }).ToList(),
                };
                //da.SubdutyAreas = subDutyAreas.Where(w => w.DutyAreaId == da.Id).ToList();
                //foreach (var sda in da.SubdutyAreas)
                //{
                //    //dutyAreas[i].SubdutyAreas.ToList()[j].Tasks = tasks.Where(w => w.SubdutyAreaId == dutyAreas[i].SubdutyAreas.ToList()[j].Id).ToList();
                //    daData.SubdutyAreas.Add(new SubDutyAreaTreeVM
                //    {
                //        Active = sda.Active,
                //        Id = sda.Id,
                //        SubNumber = sda.SubNumber,
                //        Title = sda.Title,

                //    });
                //    //sda.Tasks = tasks.Where(w => w.SubdutyAreaId == sda.Id).ToList();


                //}

                returnVM.Add(daData);
            }

            dutyAreas = dutyAreas.Where(da => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, da, DutyAreaOperations.Read).Result.Succeeded).ToList();
            return returnVM;
        }

        public async Task<List<EnablingObjective>> GetLinkedEnablingObjectivesAsync(int taskId)
        {
            var task = (await _task_EnablingObjective_LinkService.FindWithIncludeAsync(x=>x.TaskId==taskId, new string[] {"EnablingObjective.EnablingObjective_Category.EnablingObjective_SubCategories.EnablingObjective_Topics" })).ToList();
            return task.Select(x => x.EnablingObjective).ToList();
        }

        public async Task<List<EnablingObjectiveWithCountOptions>> GetLinkedEOWithMetaTaskAsync(int metaId)
        {
            var linkedEos = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == metaId).Select(x => x.TaskId).ToListAsync();
            List<EnablingObjectiveWithCountOptions> rrList = new List<EnablingObjectiveWithCountOptions>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetLinkedEOWithCount(eoId);
                rrList.AddRange(data);
            }
            return rrList.GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public async Task<List<EnablingObjectiveWithCountOptions>> GetLinkedEOWithCount(int id)
        {
            List<Domain.Entities.Core.EnablingObjective> enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesLinkedToTaskAsync(id);

            List<EnablingObjectiveWithCountOptions> eOWithCount = new List<EnablingObjectiveWithCountOptions>();
            foreach (var eo in enablingObjectives)
            {
                var data = await _task_EnablingObjective_LinkService.GetCount(x => x.EnablingObjectiveId == eo.Id);
                eOWithCount.Add(new EnablingObjectiveWithCountOptions(eo.FullNumber, eo.Description, eo.Id, data, eo.Active));
            }

            return eOWithCount;
        }

        public async Task<List<TaskPositionWithCount>> GetLinkedPositionToMetaTaskWithCountAsync(int metaId)
        {
            var linkedEos = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == metaId).Select(x => x.TaskId).ToListAsync();
            List<TaskPositionWithCount> ilaList = new List<TaskPositionWithCount>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetLinkedPositionsAsync(eoId);
                ilaList.AddRange(data);
            }
            return ilaList.GroupBy(g => g.position.Id).Select(s => s.First()).ToList();
        }

        public async Task<List<TaskPositionWithCount>> GetLinkedPositionsAsync(int taskId)
        {
            List<Position> linkedPositions = new List<Position>();
            var task_position = await _position_taskService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            foreach (var linkPos in task_position)
            {
                var pos = await _positionService.FindQuery(x => x.Id == linkPos.PositionId).Select(s => new Position { FileName = "", PositionsFileUpload = null, EffectiveDate = s.EffectiveDate, Active = s.Active, CreatedBy = s.CreatedBy, CreatedDate = s.CreatedDate, Deleted = s.Deleted, HyperLink = s.HyperLink, IsPublished = s.IsPublished, Id = s.Id, ModifiedBy = s.ModifiedBy, ModifiedDate = s.ModifiedDate, PositionAbbreviation = s.PositionAbbreviation, PositionDescription = s.PositionDescription, PositionNumber = s.PositionNumber, PositionTitle = s.PositionTitle }).FirstOrDefaultAsync();
                if (pos != null)
                {
                    linkedPositions.Add(pos);
                }
            }


            var posWithCount = new List<TaskPositionWithCount>();
            int count = 0;
            foreach (var position in linkedPositions)
            {
                count = await _position_taskService.GetCount(x => x.PositionId == position.Id);
                posWithCount.Add(new TaskPositionWithCount(position, count));
            }
            return posWithCount;
        }

        public async Task<List<SaftyHazard>> GetLinkedSaftyHazardsAsync(int taskId)
        {
            var task = await GetAsync(taskId);
            var saftyHazards = task.SafetyHazard_Task_Links.Select(x => x.SaftyHazard).ToList();
            saftyHazards = saftyHazards.Where(sh => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Read).Result.Succeeded).ToList();
            return saftyHazards?.ToList();
        }

        public async Task<List<Procedure>> GetLinkedProceduresAsync(int taskId)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { "Procedure_Task_Links.Procedure.Procedure_IssuingAuthority" }).FirstOrDefaultAsync();
            var prcedures = task.Procedure_Task_Links.Select(x => x.Procedure).ToList();
            prcedures = prcedures.Where(proc => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, proc, ProcedureOperations.Read).Result.Succeeded).ToList();
            return prcedures?.ToList();
        }

        public async System.Threading.Tasks.Task UpdateTask_StepNumber(int[] numbers, int[] ids)
        {
            for (var id = 0; id < ids.Length; id++)
            {
                var task_step = await _task_StepService.FindQuery(x => x.Id == ids[id]).FirstOrDefaultAsync();
                task_step.Number = numbers[id];
                await _task_StepService.UpdateAsync(task_step);
            }
        }

        public async System.Threading.Tasks.Task UpdateQuestionNumbers(Task_QuestionNumberOptions options)
        {
            for (var id = 0; id < options.QuestionIds.Length; id++)
            {
                var task_question = await _taskquestionService.FindQuery(x => x.Id == options.QuestionIds[id]).FirstOrDefaultAsync();
                task_question.QuestionNumber = options.Numbers[id];
                await _taskquestionService.UpdateAsync(task_question);
            }
        }

        public async System.Threading.Tasks.Task UpdateSuggestionNumbers(Task_SuggestionNumberOptions options)
        {
            for (var id = 0; id < options.SuggestionIds.Length; id++)
            {
                var task_suggestion = await _task_suggestionService.FindQuery(x => x.Id == options.SuggestionIds[id]).FirstOrDefaultAsync();
                task_suggestion.Number = options.Numbers[id];
                await _task_suggestionService.UpdateAsync(task_suggestion);
            }
        }

        public async Task<List<Task_Step>> GetTask_StepsAsync(int taskId)
        {
            var task = await _taskService.AllQueryWithInclude(new string[] { nameof(_task.Task_Steps) }).Where(x => x.Id == taskId).FirstOrDefaultAsync();
            var steps = task.Task_Steps.ToList(); // _task_StepService.FindAsync(x => x.TaskId == task.Id);

            steps = steps.Where(s => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, s, Task_StepOperations.Read).Result.Succeeded)?.ToList();
            return steps.OrderBy(x => x.Number).ToList();
        }

        public async Task<List<EnablingObjective>> LinkEnablingObjectiveAsync(int taskId, TaskOptions options)
        {
            var task = await _taskService.AllQueryWithInclude(new string[] { nameof(_task.Task_EnablingObjective_Links) }).Where(x => x.Id == taskId).FirstOrDefaultAsync();
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            List<EnablingObjective> eoList = new List<EnablingObjective>();
            foreach (var id in options.EnablingObjectiveIds)
            {
                var eo = (await _enablingObjectiveService.FindAsync(x => x.Id == id)).FirstOrDefault();

                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Read);

                if (taskResult.Succeeded && eoResult.Succeeded)
                {
                    var t_eo = task.LinkEnablingObjectives(eo, false);

                    t_eo.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    t_eo.CreatedDate = DateTime.Now;

                    var validationResult = await _taskService.UpdateAsync(task);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        eoList.Add(eo);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return eoList;
        }

        public async Task<List<Task_EnablingObjective_Link>> GetTaskEOLinks(int taskId)
        {
            var task_eos = await _task_EnablingObjective_LinkService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            return task_eos;
        }

        public async Task<List<Position>> LinkWithoutUnlinkPositions(int taskId, TaskOptions options)
        {
            var task = await _taskService.GetWithIncludeAsync(taskId, new string[] { nameof(_task.Task_Positions) });
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            List<Position> positionList = new List<Position>();
            //task.UnlinkPositions();
            await _taskService.UpdateAsync(task);

            foreach (var id in options.PositionIds)
            {
                var position = (await _positionService.FindAsync(x => x.Id == id)).FirstOrDefault();

                var posResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Read);

                if (taskResult.Succeeded && posResult.Succeeded)
                {
                    var task_pos = task.LinkPosition(position);
                    task_pos.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    task_pos.CreatedDate = DateTime.Now;
                    var validationResult = await _taskService.UpdateAsync(task);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        positionList.Add(position);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return positionList;
        }

        public async System.Threading.Tasks.Task<PositionLinkResultVM> LinkPositionAsync(int taskId, TaskOptions options)
        {
            var result = new PositionLinkResultVM();

            var positionTasks = await _position_taskService.GetPositionTaskByTaskIdAsync(taskId);
            var currentLinkedPositionIds = positionTasks.Select(pt => pt.PositionId).ToList();
            var newPositionIds = options.PositionIds.Except(currentLinkedPositionIds).ToList();
            var newPosition_Tasks = new List<Position_Task>();
            foreach(var positionId in newPositionIds)
            {
                newPosition_Tasks.Add(new Position_Task
                {
                    PositionId = positionId,
                    TaskId = taskId
                });
                result.LinkedIds.Add(positionId);
            }
            if (newPosition_Tasks.Any())
                await _position_taskService.AddRangeAsync(newPosition_Tasks);

            var removedPositionIds = currentLinkedPositionIds.Where(cp => !options.PositionIds.Contains(cp)).ToList();
            var removedPosition_Tasks = positionTasks.Where(pt => removedPositionIds.Contains(pt.PositionId)).ToList();
            if (removedPosition_Tasks.Any())
            {
                removedPosition_Tasks.ForEach(rpt => rpt.Delete());
                await _position_taskService.BulkUpdateAsync(removedPosition_Tasks);
                result.UnlinkedIds.AddRange(removedPositionIds);
            }

            return result;
        }

        public async System.Threading.Tasks.Task LinkProcedureAsync(int taskId, TaskOptions options)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_task.Procedure_Task_Links) }).FirstOrDefaultAsync();
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            List<Procedure> procedures = new List<Procedure>();
            foreach (var id in options.ProcedureIds)
            {
                var procedure = (await _procedureService.FindAsync(x => x.Id == id)).FirstOrDefault();

                var proceduresResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Read);

                if (taskResult.Succeeded && proceduresResult.Succeeded)
                {
                    var task_proc = task.LinkProcedure(procedure, false);

                    task_proc.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    task_proc.CreatedDate = DateTime.Now;
                    var validationResult = await _taskService.UpdateAsync(task);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        procedures.Add(procedure);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            //return procedures;
        }

        public async Task<List<Procedure_Task_Link>> GetTaskProcLinks(int taskId)
        {
            var task_procs = await _procedure_taskService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            return task_procs;
        }

        public async System.Threading.Tasks.Task LinkSaftyHazardsAsync(int taskId, TaskOptions options)
        {
            var task = await _taskService.AllQueryWithInclude(new string[] { nameof(_task.SafetyHazard_Task_Links) }).Where(x => x.Id == taskId).FirstOrDefaultAsync();
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            var shList = new List<SaftyHazard>();
            foreach (var shId in options.SafetyHazardIds)
            {
                var saftyHazards = await _saftyHazardService.FindQuery(x => x.Id == shId).FirstOrDefaultAsync();

                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazards, SaftyHazardOperations.Read);

                if (taskResult.Succeeded && shResult.Succeeded)
                {
                    var task_sh = task.LinkSaftyHazard(saftyHazards);

                    task_sh.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    task_sh.CreatedDate = DateTime.Now;
                    var validationResult = await _taskService.UpdateAsync(task);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        shList.Add(saftyHazards);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            //return shList;
        }

        public async Task<List<SafetyHazard_Task_Link>> GetTaskSHLinksAsync(int taskId)
        {
            var task_shs = await _SaftyHazard_task_LinkService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            return task_shs;
        }

        public async Task<Task_Question> AddQuestionAsync(int taskId, QuestionCreateOptions options)
        {
            var task = await GetAsync(taskId);
            var question = new Task_Question(task.Id, options.Question, options.Answer, options.QuestionNumber);
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            var questionResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, Task_QuestionOperations.Create);

            if (taskResult.Succeeded && questionResult.Succeeded)
            {
                var task_question = task.AddQuestion(question);

                task_question.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                task_question.CreatedDate = DateTime.Now;
                var validationResult = await _taskService.UpdateAsync(task);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return question;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task RemoveQuestionAsync(int taskId, int questionId)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_task.Task_Questions) }).FirstOrDefaultAsync();
            var question = await _taskquestionService.FindQuery(x => x.Id == questionId).FirstOrDefaultAsync();
            question.Delete();
            await _taskquestionService.UpdateAsync(question);
            //var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            //var questionResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, Task_QuestionOperations.Delete);

            //if (taskResult.Succeeded && questionResult.Succeeded)
            //{
            //    task.RemoveQuestion(question);
            //    var validationResult = await _taskService.UpdateAsync(task);
            //    if (!validationResult.IsValid)
            //    {
            //        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //    }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //}
        }

        public async Task<int> GetQuestionNumber(int id)
        {
            var num = await _taskquestionService.AllQueryWithIncludeAndDeleted(new string[] { nameof(_task_step.Task) }).Where(x => x.TaskId == id).Select(x => x.Id).ToListAsync();
            return num.Count + 1;
        }

        public async System.Threading.Tasks.Task UpdateQuestionAsync(int id, int quesId, QuestionCreateOptions options)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { nameof(_task.Task_Questions) }).FirstOrDefaultAsync();
            var question = task.Task_Questions.Where(x => x.Id == quesId).FirstOrDefault();
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            var questionResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, Task_QuestionOperations.Update);
            if (taskResult.Succeeded && questionResult.Succeeded)
            {
                question.Question = options.Question;
                question.Answer = options.Answer;
                question.ModifiedDate = DateTime.Now;
                question.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                var validationResult = await _taskquestionService.UpdateAsync(question);
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

        public async System.Threading.Tasks.Task RemoveStepAsync(int taskId, int stepId)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_task.Task_Steps) }).FirstOrDefaultAsync();
            var step = task.Task_Steps.Where(x => x.Id == stepId)?.FirstOrDefault();

            step.Delete();
            var validationResult = await _task_StepService.UpdateAsync(step);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, Task_StepOperations.Update);

            //if (result.Succeeded)
            //{
            //    task.RemoveStep(step);

            //    var validationResult = await _taskService.UpdateAsync(task);
            //    if (!validationResult.IsValid)
            //    {
            //        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //    }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //}
        }

        public async System.Threading.Tasks.Task RemoveTools(int taskId, TaskOptions options)
        {
            var task = await _taskService.AllQueryWithInclude(new string[] { nameof(_task.Task_Tools) }).Where(x => x.Id == taskId).FirstOrDefaultAsync();
            foreach (var toolId in options.ToolIds)
            {
                var tool = await _toolService.FindQuery(x => x.Id == toolId).FirstOrDefaultAsync();

                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
                var toolsResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read);

                if (taskResult.Succeeded && toolsResult.Succeeded)
                {
                    task.RemoveTool(tool);

                    var validationResult = await _taskService.UpdateAsync(task);
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

        public async System.Threading.Tasks.Task RemoveToolAsync(int taskId, int toolId)
        {
            var task = await GetAsync(taskId);
            var tool = await _toolService.GetAsync(toolId);

            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            var toolsResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read);

            if (taskResult.Succeeded && toolsResult.Succeeded)
            {
                task.RemoveTool(tool);

                var validationResult = await _taskService.UpdateAsync(task);
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

        public async System.Threading.Tasks.Task UnlinkEnablingObjectiveAsync(int taskId, TaskOptions options)
        {
            var task = await _taskService.AllQueryWithInclude(new string[] { nameof(_task.Task_EnablingObjective_Links) }).Where(x => x.Id == taskId).FirstOrDefaultAsync();
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);

            foreach (var id in options.EnablingObjectiveIds)
            {
                var enablingObjectives = (await _enablingObjectiveService.FindAsync(x => x.Id == id)).FirstOrDefault();

                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, enablingObjectives, EnablingObjectiveOperations.Update);

                if (taskResult.Succeeded && eoResult.Succeeded)
                {
                    task.UnlinkEnablingObjective(enablingObjectives);

                    var validationResult = await _taskService.UpdateAsync(task);
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

        public async Task<List<TaskWithCountOptions>> GetTasksEOIsLinkedToAsync(int id)
        {
            //var tasks = await _task_EnablingObjective_LinkService.AllQueryWithInclude(new string[] { nameof(_task_EO_Link.Task) }).Where(x => x.EnablingObjectiveId == id).Select(x => x.Task).ToListAsync();
            //foreach(var task in tasks)
            //{
            //    var da = await _subdutyAreaService.GetAsync(task.SubdutyAreaId);
            //    var num = da.Id + "." + task.SubdutyAreaId + "." + task.Id;
            //    task.Number = num;

            //}
            //return tasks;

            var links = await _task_EnablingObjective_LinkService.FindWithIncludeAsync(x => x.EnablingObjectiveId == id, new string[] { nameof(_task_EO_Link.Task) });



            List<Domain.Entities.Core.Task> taskList = new List<Domain.Entities.Core.Task>();
            taskList.AddRange(links.Select(x => x.Task));

            // return taskList;
            List<TaskWithCountOptions> taskWithCount = new List<TaskWithCountOptions>();
            foreach (var task in taskList)
            {
                var data = await _task_EnablingObjective_LinkService.GetCount(x => x.TaskId == task.Id);
                var taskNumber = await _taskService.GetWithIncludeAsync(task.Id, new string[] { nameof(_task.SubdutyArea) });

                var num = taskNumber.SubdutyArea.DutyAreaId.ToString() + '.' + taskNumber.SubdutyArea.SubNumber.ToString() + '.' + task.Number.ToString();

                //var num = taskNumber.SubdutyArea.DutyAreaId + taskNumber.SubdutyArea.Id + task.Number;
                taskWithCount.Add(new TaskWithCountOptions(num, task.Description, task.Id, data, task.Active, 0));
            }

            return taskWithCount;
        }

        public async System.Threading.Tasks.Task UnlinkPositionAsync(int taskId, TaskOptions options)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_task.Position_Tasks) }).FirstOrDefaultAsync();
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);

            foreach (var id in options.PositionIds)
            {
                var position = (await _positionService.FindAsync(x => x.Id == id)).FirstOrDefault();

                var posResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Read);

                if (taskResult.Succeeded && posResult.Succeeded)
                {
                    task.UnlinkPosition(position);
                    var validationResult = await _taskService.UpdateAsync(task);
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

        public async System.Threading.Tasks.Task UnlinkProcedureAsync(int taskId, TaskOptions options)
        {
            var task = await _taskService.AllQueryWithInclude(new string[] { nameof(_task.Procedure_Task_Links) }).Where(x => x.Id == taskId).FirstOrDefaultAsync();
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);

            foreach (var id in options.ProcedureIds)
            {
                var procedure = (await _procedureService.FindAsync(x => x.Id == id)).FirstOrDefault();

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Update);

                if (taskResult.Succeeded && procResult.Succeeded)
                {
                    task.UnlinkProcedure(procedure);

                    var validationResult = await _taskService.UpdateAsync(task);
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

        public async Task<List<ProceduresWithLinkCount>> GetLinkedProceduresToMetaTaskAsync(int metaId)
        {
            var linkedEos = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == metaId).Select(x => x.TaskId).ToListAsync();
            List<ProceduresWithLinkCount> procList = new List<ProceduresWithLinkCount>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetLinkedProcedureWithCount(eoId);
                procList.AddRange(data);
            }
            return procList.GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public async Task<List<ProceduresWithLinkCount>> GetLinkedProcedureWithCount(int id)
        {
            var links = await _procedure_taskService.FindWithIncludeAsync(x => x.TaskId == id, new string[] { nameof(_task_proc_link.Procedure) });
            List<Domain.Entities.Core.Procedure> procList = new List<Domain.Entities.Core.Procedure>();
            procList.AddRange(links.Select(x => x.Procedure));

            List<ProceduresWithLinkCount> procWithCount = new List<ProceduresWithLinkCount>();
            foreach (var proc in procList)
            {
                var data = await _procedure_taskService.GetCount(x => x.ProcedureId == proc.Id);
                procWithCount.Add(new ProceduresWithLinkCount(proc.Id, data, proc.Number, proc.Title, proc.Active));
            }

            return procWithCount;
        }

        public async Task<List<TaskWithCountOptions>> GetTasksProcIsLinkedTo(int id)
        {
            //var tasks = await _task_Procedure_LinkService.AllQueryWithInclude(new string[] { nameof(_task_proc_link.Task)}).Where(x => x.ProcedureId == id).Select(x => x.Task).ToListAsync();
            //return tasks;

            var links = await _procedure_taskService.FindWithIncludeAsync(x => x.ProcedureId == id, new string[] { nameof(_task_proc_link.Task) });



            List<Domain.Entities.Core.Task> taskList = new List<Domain.Entities.Core.Task>();
            taskList.AddRange(links.Select(x => x.Task));

            // return taskList;
            List<TaskWithCountOptions> taskWithCount = new List<TaskWithCountOptions>();
            foreach (var task in taskList)
            {
                var data = await _procedure_taskService.GetCount(x => x.TaskId == task.Id);
                var taskNumber = await _taskService.GetWithIncludeAsync(task.Id, new string[] { nameof(_task.SubdutyArea) });

                var num = taskNumber.SubdutyArea.DutyAreaId.ToString() + '.' + taskNumber.SubdutyArea.SubNumber.ToString() + '.' + task.Number.ToString();

                //var num = taskNumber.SubdutyArea.DutyAreaId + taskNumber.SubdutyArea.Id + task.Number;
                taskWithCount.Add(new TaskWithCountOptions(num, task.Description, task.Id, data, task.Active, 0));
            }

            return taskWithCount;
        }

        public async System.Threading.Tasks.Task UnlinkSaftyHazardAsync(int taskId, TaskOptions options)
        {
            var task = await _taskService.AllQueryWithInclude(new string[] { nameof(_task.SafetyHazard_Task_Links) }).Where(x => x.Id == taskId).FirstOrDefaultAsync();
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);

            foreach (var shId in options.SafetyHazardIds)
            {
                var saftyHazard = await _saftyHazardService.FindQuery(x => x.Id == shId).FirstOrDefaultAsync();

                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard, SaftyHazardOperations.Update);

                if (taskResult.Succeeded && shResult.Succeeded)
                {
                    task.UnlinkSaftyHazard(saftyHazard);
                    var validationResult = await _taskService.UpdateAsync(task);
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

        public async Task<List<SafetyHazardWithLinkCount>> GetLinkedSHWithMetaTaskAsync(int metaId)
        {
            var linkedEos = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == metaId).Select(x => x.TaskId).ToListAsync();
            List<SafetyHazardWithLinkCount> shList = new List<SafetyHazardWithLinkCount>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetLinkedSHWithCount(eoId);
                shList.AddRange(data);
            }
            return shList.GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public async Task<List<SafetyHazardWithLinkCount>> GetLinkedSHWithCount(int id)
        {
            List<Domain.Entities.Core.SaftyHazard> shList = new List<Domain.Entities.Core.SaftyHazard>();
            var links = await _SaftyHazard_task_LinkService.FindQueryWithIncludeAsync(x => x.TaskId == id, new string[] { "SaftyHazard.SaftyHazard_Category" }).ToListAsync();
            foreach (var link in links)
            {
                var sh = await _saftyHazardService.FindQuery(x => x.Id == link.SaftyHazardId).Select(s => new SaftyHazard { Active = s.Active, Id = s.Id, CreatedBy = s.CreatedBy, CreatedDate = s.CreatedDate, Deleted = s.Deleted, EffectiveDate = s.EffectiveDate, FileName = s.FileName, HyperLinks = s.HyperLinks, ModifiedBy = s.ModifiedBy, ModifiedDate = s.ModifiedDate, Number = s.Number, RevisionNumber = s.RevisionNumber, SaftyHazardCategoryId = s.SaftyHazardCategoryId, Text = s.Text, Title = s.Title,SaftyHazard_Category = s.SaftyHazard_Category }).FirstOrDefaultAsync();
                shList.Add(sh);
            }

            List<SafetyHazardWithLinkCount> shWithCount = new List<SafetyHazardWithLinkCount>();
            foreach (var sh in shList)
            {
                var data = await _SaftyHazard_task_LinkService.GetCount(x => x.SaftyHazardId == sh.Id);
                shWithCount.Add(new SafetyHazardWithLinkCount(sh.Id, sh.Title, sh.Number, data, sh.Active, sh.SaftyHazard_Category.Title));
            }

            return shWithCount;
        }

        public async Task<List<TaskWithCountOptions>> GetTaskSHIsLinkedTo(int id)
        {
            //var SHs = await _task_SaftyHazard_LinkService.AllQueryWithInclude(new string[] { nameof(_task_sh_link.Task) }).Where(x => x.SaftyHazardId == id).Select(x => x.Task).ToListAsync();
            //return SHs;
            var links = await _SaftyHazard_task_LinkService.FindWithIncludeAsync(x => x.SaftyHazardId == id, new string[] { nameof(_sh_task_link.Task) });



            List<Domain.Entities.Core.Task> taskList = new List<Domain.Entities.Core.Task>();
            taskList.AddRange(links.Select(x => x.Task));

            // return taskList;
            List<TaskWithCountOptions> taskWithCount = new List<TaskWithCountOptions>();
            foreach (var task in taskList)
            {
                var data = await _SaftyHazard_task_LinkService.GetCount(x => x.TaskId == task.Id);
                var taskNumber = await _taskService.GetWithIncludeAsync(task.Id, new string[] { nameof(_task.SubdutyArea) });

                var num = taskNumber.SubdutyArea.DutyAreaId.ToString() + '.' + taskNumber.SubdutyArea.SubNumber.ToString() + '.' + task.Number.ToString();

                //var num = taskNumber.SubdutyArea.DutyAreaId + taskNumber.SubdutyArea.Id + task.Number;
                taskWithCount.Add(new TaskWithCountOptions(num, task.Description, task.Id, data, task.Active, 0));
            }

            return taskWithCount;
        }

        public async Task<Domain.Entities.Core.Task> UpdateAsync(int taskId, TaskUpdateOptions options)
        {
            var task = await GetAsync(taskId);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);

            if (result.Succeeded)
            {
                bool shouldChangeNumber = task.Number != options.Number;
                bool shouldChangeSubDutyArea = task.SubdutyAreaId != options.SubdutyAreaId;

                if (shouldChangeNumber)
                {
                    var isNumberAlreadyPresent = await _taskService.FindAsync(x => x.Number == options.Number && x.SubdutyAreaId == options.SubdutyAreaId);

                    if (isNumberAlreadyPresent.Count() > 0)
                    {
                        throw new QTDServerException("Duplicate Task Number");
                    }
                }

                if (shouldChangeSubDutyArea)
                {
                    var newTaskNum = (await _taskService.FindQueryWithDeleted(x => x.SubdutyAreaId == options.SubdutyAreaId).ToListAsync());
                    task.Number = newTaskNum.Count() > 0 ? newTaskNum.Max(x => x.Number) + 1 : 1;
                    task.SubdutyAreaId = options.SubdutyAreaId;
                }
                if (shouldChangeNumber)
                {
                    task.Number = options.Number;
                }

                task.RequalificationDueDate = DateOnly.FromDateTime(options.EffectiveDate).AddMonths(6);   
                task.Description = options.Description;
                task.IsReliability = options.IsReliability;
                task.EffectiveDate = DateOnly.FromDateTime(options.EffectiveDate);


                task.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                task.ModifiedDate = DateTime.Now;
                var validationResult = await _taskService.UpdateAsync(task);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return task;
                }

            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task EditSpecificField(int id, SpecificUpdateOptions option)
        {
            var task = await _taskService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (task == null)
            {
                throw new BadHttpRequestException(message: _localizer["TaskNotFound"]);
            }
            else
            {
                switch (option.Field.ToLower().Trim())
                {
                    case "criteria":
                        task.Criteria = option.Value;
                        break;
                    case "references":
                        task.References = option.Value;
                        break;
                    case "conditions":
                        task.Conditions = option.Value;
                        break;
                }
                await _taskService.UpdateAsync(task);
            }
        }

        public async Task<Task_Step> UpdateStepAsync(int taskId, int stepId, Task_StepUpdateOptions options)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_task.Task_Steps) }).FirstOrDefaultAsync();
            var step = task.Task_Steps.Where(x => x.Id == stepId).FirstOrDefault();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, Task_StepOperations.Update);

            if (result.Succeeded)
            {
                // Todo Update Logic
                step.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                step.ModifiedDate = DateTime.Now;
                step.Description = options.Description;
                var validationResult = await _task_StepService.UpdateAsync(step);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return step;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<Task_Question>> GetTask_QuestionsAsync(int taskId)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_task.Task_Questions) }).FirstOrDefaultAsync();
            var taskQuestion = task?.Task_Questions.OrderBy(x => x.QuestionNumber).ToList();
            taskQuestion = taskQuestion.Where(q => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, q, Task_QuestionOperations.Read).Result.Succeeded).ToList();
            return taskQuestion;
        }

        public async System.Threading.Tasks.Task ActivateStepAsync(int taskId, int stepId)
        {
            var task = await GetAsync(taskId);
            var step = task.Task_Steps.Where(x => x.Id == stepId)?.FirstOrDefault();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, Task_StepOperations.Update);

            if (result.Succeeded)
            {
                step.Activate();
                var validationResult = await _task_StepService.UpdateAsync(step);
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

        public async System.Threading.Tasks.Task DeactivateStepAsync(int taskId, int stepId)
        {
            var task = await GetAsync(taskId);
            var step = task.Task_Steps.Where(x => x.Id == stepId)?.FirstOrDefault();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, Task_StepOperations.Update);

            if (result.Succeeded)
            {
                step.Deactivate();
                var validationResult = await _task_StepService.UpdateAsync(step);
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

        public Task<List<SubdutyArea>> GetSubDutyAreas(int dutyAreaId)
        {
            var sda = _subdutyAreaService.FindQuery(x => x.DutyAreaId == dutyAreaId).ToList();
            if (sda != null)
            {
                sda = sda.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, SubdutyAreaOperations.Read).Result.Succeeded).ToList();
                return System.Threading.Tasks.Task.FromResult(sda);
            }
            else
            {
                throw new QTDServerException(_localizer["SubdutuAreaNotFound"]);
            }
        }


        public async Task<SubdutyArea> GetSubDutyArea(int id)
        {
            var sda = await _subdutyAreaService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { nameof(_subDutyArea.DutyArea) }).FirstOrDefaultAsync();
            if (sda != null)
            {
                return sda;
            }
            else
            {
               throw new QTDServerException(_localizer["SubdutuAreaNotFound"]);
            }
        }

        public async Task<Domain.Entities.Core.Task> LinkTaskReference(int taskId, Task_Reference_LinkOptions options)
        {
            var task = await _taskService.GetWithIncludeAsync(taskId, new string[] { nameof(_task.Task_Reference_Links) });
            var taskRef = await _taskRefService.GetAsync(options.TaskReferenceId);
            if (taskRef == null)
            {
               throw new QTDServerException(_localizer["TaskReferenceNotFound"]);
            }
            else
            {
                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                var taskRefResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskRef, Task_ReferenceOperations.Update);
                if (taskResult.Succeeded && taskRefResult.Succeeded)
                {
                    task.LinkTaskReference(taskRef);
                    await _taskService.UpdateAsync(task);
                    return task;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkTaskReference(int taskId, int taskRefId)
        {
            var task = await _taskService.GetWithIncludeAsync(taskId, new string[] { nameof(_task.Task_Reference_Links) });
            var taskRef = await _taskRefService.GetAsync(taskRefId);
            if (taskRef == null)
            {
                throw new QTDServerException(_localizer["TaskReferenceNotFound"]);
            }
            else
            {
                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                var taskRefResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskRef, Task_ReferenceOperations.Update);
                if (taskResult.Succeeded && taskRefResult.Succeeded)
                {
                    task.UnlinkTaskReference(taskRef);
                    await _taskService.UpdateAsync(task);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<List<ILA_TaskObjective_Link>> GetTaskILALinks(int taskId)
        {
            var task_ilas = await _task_ila_linkService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            return task_ilas;
        }
        public async System.Threading.Tasks.Task LinkILA(int taskId, TaskOptions options)
        {
            var task = await _taskService.AllQueryWithInclude(new string[] { nameof(_task.ILA_TaskObjective_Links) }).Where(x => x.Id == taskId).FirstOrDefaultAsync();
            foreach (var ilaId in options.ILAIds)
            {
                var ila = await _ilaService.FindQuery(x => x.Id == ilaId).FirstOrDefaultAsync();
                if (ila == null)
                {
                    throw new QTDServerException(_localizer["ILANotFound"]);
                }
                else
                {
                    var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                    var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);
                    if (taskResult.Succeeded && ilaResult.Succeeded)
                    {
                        task.LinkILA(ila);
                        await _taskService.UpdateAsync(task);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkILA(int taskId, TaskOptions options)
        {
            var task = await _taskService.AllQueryWithInclude(new string[] { nameof(_task.ILA_TaskObjective_Links) }).Where(x => x.Id == taskId).FirstOrDefaultAsync();
            foreach (var ilaId in options.ILAIds)
            {
                var ila = await _ilaService.FindQuery(x => x.Id == ilaId).FirstOrDefaultAsync();
                if (ila == null)
                {
                    throw new QTDServerException(_localizer["ILANotFound"]);
                }
                else
                {
                    var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                    var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);
                    if (taskResult.Succeeded && ilaResult.Succeeded)
                    {
                        task.UnlinkILA(ila);
                        await _taskService.UpdateAsync(task);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task LinkRR(int taskId, TaskOptions options)
        {
            var task = await _taskService.GetWithIncludeAsync(taskId, new string[] { nameof(_task.RR_Task_Links) });
            //task.UnlinkRRs();
            //await _taskService.UpdateAsync(task);
            foreach (var rrId in options.RegulatoryRequirementIds)
            {
                var rr = await _rrService.GetAsync(rrId);
                if (rr == null)
                {
                    throw new QTDServerException(_localizer["RegulatoryRequirementNotFound"]);
                }
                else
                {
                    var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                    var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Update);
                    if (taskResult.Succeeded && rrResult.Succeeded)
                    {
                        task.LinkRR(rr);
                        await _taskService.UpdateAsync(task);

                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }
            //return task;
        }
        public async Task<List<TaskWithCountOptions>> GetTaskLinkedToRR(int id)
        {
            //var tasks = await _task_rr_linkService.FindQueryWithIncludeAsync(x => x.RegulatoryRequirementId == id, new string[] { nameof(_task_rr_link.Task) }).Select(x => x.Task).ToListAsync();
            //return tasks;
            var links = await _rr_task_linkService.FindWithIncludeAsync(x => x.RegRequirementId == id, new string[] { nameof(_rr_task_link.Task) });



            List<Domain.Entities.Core.Task> taskList = new List<Domain.Entities.Core.Task>();
            taskList.AddRange(links.Select(x => x.Task));

            // return taskList;
            List<TaskWithCountOptions> taskWithCount = new List<TaskWithCountOptions>();
            foreach (var task in taskList)
            {
                var data = await _rr_task_linkService.GetCount(x => x.TaskId == task.Id);
                var taskNumber = await _taskService.GetWithIncludeAsync(task.Id, new string[] { nameof(_task.SubdutyArea) });

                var num = taskNumber.SubdutyArea.DutyAreaId.ToString() + '.' + taskNumber.SubdutyArea.SubNumber.ToString() + '.' + task.Number.ToString();

                //var num = taskNumber.SubdutyArea.DutyAreaId + taskNumber.SubdutyArea.Id + task.Number;
                taskWithCount.Add(new TaskWithCountOptions(num, task.Description, task.Id, data, task.Active, 0));
            }

            return taskWithCount;
        }


        public async Task<List<TaskWithCountOptions>> GetTaskLinkedToPosition(int id)
        {
            //var tasks = await _task_position_service.FindQueryWithIncludeAsync(x => x.PositionId == id, new string[] { nameof(_task_pos.Task) }).Select(x => x.Task).ToListAsync();
            //return tasks;
            var links = await _position_taskService.FindWithIncludeAsync(x => x.PositionId == id, new string[] { nameof(_pos_task.Task) });



            List<Domain.Entities.Core.Task> taskList = new List<Domain.Entities.Core.Task>();
            taskList.AddRange(links.Select(x => x.Task));

            // return taskList;
            List<TaskWithCountOptions> taskWithCount = new List<TaskWithCountOptions>();
            foreach (var task in taskList)
            {
                var data = await _position_taskService.GetCount(x => x.TaskId == task.Id);
                var taskNumber = await _taskService.GetWithIncludeAsync(task.Id, new string[] { nameof(_task.SubdutyArea) });

                var num = taskNumber.SubdutyArea.DutyAreaId.ToString() + '.' + taskNumber.SubdutyArea.SubNumber.ToString() + '.' + task.Number.ToString();

                //var num = taskNumber.SubdutyArea.DutyAreaId + taskNumber.SubdutyArea.Id + task.Number;
                taskWithCount.Add(new TaskWithCountOptions(num, task.Description, task.Id, data, task.Active, 0));
            }

            return taskWithCount;
        }

        public async Task<List<TaskRRWithCount>> GetLinkedRRWithMetaTaskAsync(int metaId)
        {
            var linkedEos = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == metaId).Select(x => x.TaskId).ToListAsync();
            List<TaskRRWithCount> rrList = new List<TaskRRWithCount>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetLinkedRRWithCount(eoId);
                rrList.AddRange(data);
            }
            return rrList.GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }


        public async Task<List<TaskRRWithCount>> GetLinkedRRWithCount(int id)
        {
            var links = await _rr_task_linkService.FindWithIncludeAsync(x => x.TaskId == id, new string[] { nameof(_rr_task_link.RegulatoryRequirement), "RegulatoryRequirement.RR_IssuingAuthority" });
            List<Domain.Entities.Core.RegulatoryRequirement> rrList = new List<Domain.Entities.Core.RegulatoryRequirement>();
            rrList.AddRange(links.Select(x => x.RegulatoryRequirement).OrderBy(x => x.Number));

            List<TaskRRWithCount> rrWithCount = new List<TaskRRWithCount>();
            int count = 0;
            foreach (var item in rrList)
            {
                count = await _rr_task_linkService.GetCount(x => x.RegRequirementId == item.Id);
                rrWithCount.Add(new TaskRRWithCount(item.Number, item.Title, item.Id, count, item.Active,item.RR_IssuingAuthority.Title));
            }

            return rrWithCount;
        }

        public async Task<List<ILAWithCountOptions>> GetLinkedILAToMetaTaskWithCountAsync(int metaId)
        {
            var linkedEos = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == metaId).Select(x => x.TaskId).ToListAsync();
            List<ILAWithCountOptions> ilaList = new List<ILAWithCountOptions>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetLinkedILAWithCount(eoId);
                ilaList.AddRange(data);
            }
            return ilaList.GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }


        public async Task<List<ILAWithCountOptions>> GetLinkedILAWithCount(int id)
        {
            var links = await _task_ila_linkService.FindWithIncludeAsync(x => x.TaskId == id, new string[] { nameof(_ila_task_link.ILA) });
            List<Domain.Entities.Core.ILA> ilaList = new List<Domain.Entities.Core.ILA>();
            ilaList.AddRange(links.Select(x => x.ILA));

            List<ILAWithCountOptions> ilaWithCount = new List<ILAWithCountOptions>();
            foreach (var ila in ilaList)
            {
                var data = await _task_ila_linkService.GetCount(x => x.ILAId == ila.Id);
                ilaWithCount.Add(new ILAWithCountOptions(ila.Number, ila.Name, ila.Id, data, ila.Active));
            }

            return ilaWithCount;
        }

        public async Task<List<TaskWithCountOptions>> GetTasksILAIsLinkedWith(int id)
        {
            //var tasks = await _task_ila_linkService.AllQueryWithInclude(new string[] { nameof(_task_ila_link.Task) }).Where(x => x.ILAId == id).Select(x => x.Task).ToListAsync();
            //return tasks;
            var links = await _task_ila_linkService.FindWithIncludeAsync(x => x.ILAId == id, new string[] { nameof(_ila_task_link.Task) });



            List<Domain.Entities.Core.Task> taskList = new List<Domain.Entities.Core.Task>();
            taskList.AddRange(links.Select(x => x.Task));

            // return taskList;
            List<TaskWithCountOptions> taskWithCount = new List<TaskWithCountOptions>();
            foreach (var task in taskList)
            {
                var data = await _task_ila_linkService.GetCount(x => x.TaskId == task.Id);
                var taskNumber = await _taskService.GetWithIncludeAsync(task.Id, new string[] { nameof(_task.SubdutyArea) });

                var num = taskNumber.SubdutyArea.DutyAreaId.ToString() + '.' + taskNumber.SubdutyArea.SubNumber.ToString() + '.' + task.Number.ToString();

                //var num = taskNumber.SubdutyArea.DutyAreaId + taskNumber.SubdutyArea.Id + task.Number;
                taskWithCount.Add(new TaskWithCountOptions(num, task.Description, task.Id, data, task.Active, 0));
            }

            return taskWithCount;
        }

        public async Task<List<RR_Task_Link>> GetTaskRRLinks(int taskId)
        {
            var task_rrs = await _rr_task_linkService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            return task_rrs;
        }

        public async System.Threading.Tasks.Task UnlinkRR(int taskId, TaskOptions options)
        {
            var task = await _taskService.GetWithIncludeAsync(taskId, new string[] { nameof(_task.RR_Task_Links) });
            foreach (var rrId in options.RegulatoryRequirementIds)
            {
                var rr = await _rrService.GetAsync(rrId);
                if (rr == null)
                {
                    throw new QTDServerException(_localizer["RegulatoryRequirementNotFound"]);
                }
                else
                {
                    var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                    var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Update);
                    if (taskResult.Succeeded && rrResult.Succeeded)
                    {
                        task.UnlinkRR(rr);
                        await _taskService.UpdateAsync(task);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }
        }

        public async Task<Domain.Entities.Core.Task> LinkTaskColab(int taskId, Task_Collaborator_LinkOptions options)
        {
            var task = await _taskService.GetWithIncludeAsync(taskId, new string[] { nameof(_task.Task_Collaborator_Links) });
            var taskColab = await _taskColabService.GetAsync(options.TaskCollabInviteId);
            if (taskColab == null)
            {
                throw new QTDServerException(_localizer["TaskCollaboratorInvitationNotFound"]);
            }
            else
            {
                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                var taskColabResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskColab, Task_CollaboratorInvitationOperations.Update);
                if (taskResult.Succeeded && taskColabResult.Succeeded)
                {
                    task.LinkTaskCollab(taskColab, false);
                    await _taskService.UpdateAsync(task);
                    return task;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkTaskColab(int taskId, int taskColabId)
        {
            var task = await _taskService.GetWithIncludeAsync(taskId, new string[] { nameof(_task.Task_Collaborator_Links) });
            var taskColab = await _taskColabService.GetAsync(taskColabId);
            if (taskColab == null)
            {
                throw new QTDServerException(_localizer["TaskCollaboratorInvitationNotFound"]);
            }
            else
            {
                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                var taskColabResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskColab, Task_CollaboratorInvitationOperations.Update);
                if (taskResult.Succeeded && taskColabResult.Succeeded)
                {
                    task.UnlinkTaskCollab(taskColab);
                    await _taskService.UpdateAsync(task);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<Domain.Entities.Core.Task> LinkMetaTask(int metaTaskId, TaskOptions options)
        {
            var metaTask = await _taskService.FindQueryWithIncludeAsync(x => x.Id == metaTaskId, new string[] { nameof(_task.Task_MetaTask_Links) }).FirstOrDefaultAsync();
            var prevIds = metaTask.Task_MetaTask_Links.Select(x => x.TaskId).ToList();
            metaTask.UnlinkMetaTasks();
            await _taskService.UpdateAsync(metaTask);
            options.TaskIds = options.TaskIds.Concat(prevIds).ToArray();
            foreach (var taskId in options.TaskIds.Distinct())
            {
                var task = await _taskService.FindQuery(x => x.Id == taskId).FirstOrDefaultAsync();
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
                if (result.Succeeded)
                {
                    metaTask.LinkMetaTask(task);
                    await _taskService.UpdateAsync(metaTask);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            return metaTask;
        }

        public async System.Threading.Tasks.Task UnlinkMetaTask(int taskId, TaskOptions options)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_task.Task_MetaTask_Links) }).FirstOrDefaultAsync();
            foreach (var metaId in options.TaskIds)
            {
                var meta = await _taskService.FindQuery(x => x.Id == metaId).FirstOrDefaultAsync();
                var metaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, meta, TaskOperations.Delete);
                if (metaResult.Succeeded)
                {
                    task.UnlinkMetaTask(meta);
                    await _taskService.UpdateAsync(task);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<List<Domain.Entities.Core.Task>> getUsingSDAId(int id)
        {
            var tasks = await _taskService.FindAsync(x => x.SubdutyAreaId == id);
            return tasks.ToList();
        }

        public async Task<int> GetDutyAreaNumberAsync(string letter)
        {
            var dutyArea = await _dutyAreaService.FindQueryWithDeleted(x => x.Letter == letter).OrderByDescending(x => x.Number).FirstOrDefaultAsync();

            if (dutyArea == null)
            {
                return 1;
            }

            return dutyArea.Number + 1;
        }

        public async System.Threading.Tasks.Task DeleteDutyAreaAsync(int id)
        {
            var obj = await GetDutyAreaAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DutyAreaOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _dutyAreaService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task DeactivateDutyAreaAsync(int id)
        {
            var obj = await GetDutyAreaAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DutyAreaOperations.Update);

            if (result.Succeeded)
            {
                obj.Deactivate();
                var validationResult = await _dutyAreaService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    await MakeSDAsInactive(obj);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task MakeSDAsInactive(DutyArea da)
        {
            var sdas = await _subdutyAreaService.FindQuery(x => x.DutyAreaId == da.Id).ToListAsync();
            foreach (var sda in sdas)
            {
                sda.Deactivate();
                await _subdutyAreaService.UpdateAsync(sda);
            }
        }

        public async System.Threading.Tasks.Task ActivateDutyAreaAsync(int id)
        {
            var obj = await GetDutyAreaAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DutyAreaOperations.Update);

            if (result.Succeeded)
            {
                obj.Activate();
                var validationResult = await _dutyAreaService.UpdateAsync(obj);
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

        public async Task<SubdutyArea> UpdateSubDutyAreaAsync(int daId, SubdutyAreaUpdateOptions options)
        {

            var subdutyArea = await _subdutyAreaService.GetAsync(options.SdaId);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, subdutyArea, SubdutyAreaOperations.Update);
            if (result.Succeeded)
            {
                subdutyArea.Title = options.Title;
                subdutyArea.Description = options.Description;
                subdutyArea.EffectiveDate = options.EffectiveDate;
                subdutyArea.ReasonForRevision = options.ReasonForRevision;
                subdutyArea.SubNumber = options.SubNumber;
                subdutyArea.DutyAreaId = daId;
                subdutyArea.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                subdutyArea.ModifiedDate = DateTime.Now;
                var numValidation = await _subdutyAreaService.FindQuery(x => x.Id != subdutyArea.Id && x.SubNumber == subdutyArea.SubNumber && x.DutyAreaId == subdutyArea.DutyAreaId).FirstOrDefaultAsync();
                if (numValidation == null)
                {
                    var validationResult = await _subdutyAreaService.UpdateAsync(subdutyArea);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return subdutyArea;
                    }
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["Sub Duty Area number in use"]);
                }

            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task DeleteSubDutyAreaAsync(int id)
        {
            var obj = await _subdutyAreaService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, SubdutyAreaOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _subdutyAreaService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task DeactivateSubDutyAreaAsync(int id)
        {
            var obj = await _subdutyAreaService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, SubdutyAreaOperations.Update);

            if (result.Succeeded)
            {
                obj.Deactivate();
                var validationResult = await _subdutyAreaService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task ActivateSubDutyAreaAsync(int id)
        {
            var obj = await _subdutyAreaService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, SubdutyAreaOperations.Update);

            if (result.Succeeded)
            {
                obj.Activate();
                var validationResult = await _subdutyAreaService.UpdateAsync(obj);
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

        public async Task<TaskStatsCount> GetTaskLinkedStats(int taskId)
        {
            var stats = new TaskStatsCount();
            stats.EnablingObjectives = await _task_EnablingObjective_LinkService.GetCount(x => x.TaskId == taskId);
            stats.Positions = await _position_taskService.GetCount(x => x.TaskId == taskId);
            stats.ILA = await _task_ila_linkService.GetCount(x => x.TaskId == taskId);
            stats.Procedures = await _procedure_taskService.GetCount(x => x.TaskId == taskId);
            stats.SafetyHazards = await _SaftyHazard_task_LinkService.GetCount(x => x.TaskId == taskId);
            stats.Regulations = await _rr_task_linkService.GetCount(x => x.TaskId == taskId);
            stats.TrainingGroups = await _task_trainingGroup_service.GetCount(x => x.TaskId == taskId);

            return stats;
        }

        public async Task<List<TaskWithNumberVM>> GetTasksWithSDAIdAsync(int sdaId)
        {
            List<TaskWithNumberVM> tvm = new List<TaskWithNumberVM>();
            var taskList = await _taskService.FindQuery(x => x.SubdutyAreaId == sdaId).ToListAsync();
            foreach (var task in taskList)
            {
                var numVM = await GetTaskNumberWithLetter(sdaId, task.Id);
                var taskWithNum = new TaskWithNumberVM
                {
                    DANumber = numVM.DANumber,
                    SDANumber = numVM.SDANumber,
                    Task = task,
                    Letter = numVM.Letter,
                };
                tvm.Add(taskWithNum);
            }
            return tvm;
        }

        public async Task<List<SubdutyArea>> GetSDAWithNumberBydaId(int daId)
        {
            var sdas = await _subdutyAreaService.FindQuery(x => x.DutyAreaId == daId).ToListAsync();
            var da = await _dutyAreaService.FindQuery(x => x.Id == daId).FirstOrDefaultAsync();
            if (da == null)
            {
                throw new BadHttpRequestException(message: _localizer["Duty Area Not Found"]);
            }
            else
            {
                for (int i = 0; i < sdas.Count; i++)
                {
                    sdas[i].Title = da.Letter + da.Number.ToString() + "." + sdas[i].SubNumber.ToString() + " - " + sdas[i].Title;
                }

                return sdas;
            }
        }

        public async Task<TaskStatsCount> GetLinkedMetaStatsAsync(int taskId)
        {
            var stats = new TaskStatsCount
            {
                EnablingObjectives = 0,
                Positions = 0,
                ILA = 0,
                Procedures = 0,
                SafetyHazards = 0,
                Regulations = 0,
                TrainingGroups = 0,
            };
            var linkedTasks = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == taskId).Select(s => s.TaskId).ToListAsync();

            stats.EnablingObjectives = (await GetLinkedEOWithMetaTaskAsync(taskId)).Count;
            stats.Positions = (await GetLinkedPositionToMetaTaskWithCountAsync(taskId)).Count;
            stats.ILA = (await GetLinkedILAToMetaTaskWithCountAsync(taskId)).Count;
            stats.Procedures = (await GetLinkedProceduresToMetaTaskAsync(taskId)).Count;
            stats.SafetyHazards = (await GetLinkedSHWithMetaTaskAsync(taskId)).Count;
            stats.Regulations = (await GetLinkedRRWithMetaTaskAsync(taskId)).Count;
            stats.TrainingGroups = (await GetMetaTaskTrainingGroups(taskId)).Count;

            return stats;
        }

        public async Task<TaskStatsCount> GetTaskNotLinkedStats()
        {
            var taskIds = await _taskService.AllQuery().Where(w => w.Active == true).Select(x => x.Id).ToListAsync();
            var taskEOIds = await _task_EnablingObjective_LinkService.AllQuery().Select(x => x.TaskId).Distinct().ToListAsync();
            var taskProcIds = await _procedure_taskService.AllQuery().Select(x => x.TaskId).Distinct().ToListAsync();
            var taskSHIds = await _SaftyHazard_task_LinkService.AllQuery().Select(x => x.TaskId).Distinct().ToListAsync();
            var taskRRIds = await _rr_task_linkService.AllQuery().Select(x => x.TaskId).Distinct().ToListAsync();
            var taskILAIds = await _task_ila_linkService.AllQuery().Select(x => x.TaskId).Distinct().ToListAsync();
            var taskPosIds = await _position_taskService.AllQuery().Select(x => x.TaskId).Distinct().ToListAsync();


            var stats = new TaskStatsCount()
            {
                ActiveTask = await _taskService.GetCount(x => x.Active == true),
                InActiveTask = await _taskService.GetCount(x => x.Active == false),
                EnablingObjectives = taskIds.Except(taskEOIds).Count(),
                Procedures = taskIds.Except(taskProcIds).Count(),
                ILA = taskIds.Except(taskILAIds).Count(),
                Positions = taskIds.Except(taskPosIds).Count(),
                SafetyHazards = taskIds.Except(taskSHIds).Count(),
                Regulations = taskIds.Except(taskRRIds).Count(),
            };

            return stats;
        }

        public async Task<List<EmployeeTaskVM>> GetLinkedEmployeeToMetaTaskWithCountAsync(int metaId)
        {
            var linkedEos = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == metaId).Select(x => x.TaskId).ToListAsync();
            List<EmployeeTaskVM> ilaList = new List<EmployeeTaskVM>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetTaskPositionEmployeesAsync(eoId);
                ilaList.AddRange(data);
            }
            return ilaList.GroupBy(g => g.EmployeeId).Select(s => s.First()).ToList();
        }

        public async Task<List<EmployeeTaskVM>> GetTaskPositionEmployeesAsync(int taskId)
        {
            var posIds = await _position_taskService.FindQuery(x => x.TaskId == taskId).Select(x => x.PositionId).ToListAsync();
            var empPos = _emp_posService.FindQueryWithIncludeAsync(x => posIds.Contains(x.PositionId), new string[] { "Employee.Person" }, false);

            var employees = await empPos.Select(x => new EmployeeTaskVM
            {
                Active = x.Employee.Active,
                EmpEmail = x.Employee.Person.Username,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee.Person.FirstName + " " + x.Employee.Person.LastName,
                EmpNumber = x.Employee.EmployeeNumber ?? "",
                LastQualification = x.StartDate.ToDateTime(TimeOnly.MinValue),
                PositionId = x.PositionId,
                PositionName = x.Position.PositionTitle,
                QualificationStatus = x.Trainee == false ? "Qualified - " + (x.QualificationDate == null ? "N/A" : x.QualificationDate.GetValueOrDefault()) : "Not Qualified - Trainee",
            }).ToListAsync();

            var employeeIds = employees.Select(x => x.EmployeeId);

            var tqs = await _task_qualService.FindQuery(x => employeeIds.Contains(x.EmpId)).OrderBy(o => o.TaskQualificationDate).ToListAsync();

            foreach (var emp in employees)
            {
                var tq = tqs.OrderByDescending(x => x.TaskQualificationDate).FirstOrDefault(s => s.EmpId == emp.EmployeeId);
                if (tq != null)
                {
                    emp.LastQualification = tq.TaskQualificationDate;
                }
                else
                {
                    emp.LastQualification = null;
                }
            }
            return employees.GroupBy(g => g.EmployeeId).Select(s => s.First()).OrderBy(x => x.EmployeeName).ToList();

        }

        public async Task<List<string>> GetLinkedIds(string option)
        {
            List<int> linkedIds = new List<int>();

            switch (option.ToLower().Trim())
            {
                case "ilas":
                    linkedIds = await _task_ila_linkService.AllQuery().Select(x => x.TaskId).ToListAsync();
                    break;
                case "regulations":
                    linkedIds = await _rr_task_linkService.AllQuery().Select(x => x.TaskId).ToListAsync();
                    break;
                case "procedures":
                    linkedIds = await _procedure_taskService.AllQuery().Select(x => x.TaskId).ToListAsync();
                    break;
                case "safety hazards":
                    linkedIds = await _SaftyHazard_task_LinkService.AllQuery().Select(x => x.TaskId).ToListAsync();
                    break;
                case "enabling objectives":
                    linkedIds = await _task_EnablingObjective_LinkService.AllQuery().Select(x => x.TaskId).ToListAsync();
                    break;
                case "positions":
                    linkedIds = await _position_taskService.AllQuery().Select(x => x.TaskId).ToListAsync();
                    break;
            }
            linkedIds = linkedIds.Distinct().ToList();

            var hashedIds = new List<string>();
            linkedIds.ForEach(item =>
            {
                hashedIds.Add(_hasher.Encode(item.ToString()));
            });

            return hashedIds;
        }


        public async Task<Task_Suggestion> CreateTaskSuggestionAsync(int taskId, Task_SuggestionOptions options)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_task.Task_Suggestions) }).FirstOrDefaultAsync();
            if (task == null)
            {
                throw new BadHttpRequestException(message: _localizer["TaskNotFound"]);
            }
            else
            {
                var number = await GetSuggestionNumberAsync(taskId);
                var task_suggestion = new Task_Suggestion(taskId, options.Description, number);
                task.AddSuggestion(task_suggestion);
                await _taskService.UpdateAsync(task);
                return task_suggestion;
            }
        }

        public async Task<List<Task_Suggestion>> GetAllSuggestionsAsync(int taskId)
        {
            var task = (await _taskService.FindWithIncludeAsync(x => x.Id == taskId, new string[] { "Task_Suggestions" })).FirstOrDefault();
            if (task == null)
            {
                throw new BadHttpRequestException(message: _localizer["TaskNotFound"]);
            }
            else
            {
                return task.Task_Suggestions.OrderBy(x => x.Number).ToList();
            }
        }

        public async Task<int> GetSuggestionNumberAsync(int taskId)
        {
            var task = await _taskService.AllQueryWithIncludeAndDeleted(new string[] { nameof(_task.Task_Suggestions) }).Where(x => x.Id == taskId).FirstOrDefaultAsync();
            if (task == null)
            {
                throw new BadHttpRequestException(message: _localizer["TaskNotFound"]);
            }
            {
                var number = task.Task_Suggestions.OrderBy(x => x.Number).Select(x => x.Number).LastOrDefault();
                return number + 1;
            }
        }

        public async System.Threading.Tasks.Task UpdateSuggestionAsync(int taskId, int suggestionId, Task_SuggestionOptions options)
        {
            var suggestion = await _task_suggestionService.FindQuery(x => x.Id == suggestionId).FirstOrDefaultAsync();
            var suggestionValidation = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, suggestion, Task_SuggestionOperations.Update);
            if (suggestionValidation.Succeeded)
            {
                suggestion.Description = options.Description;
                var validationResult = await _task_suggestionService.UpdateAsync(suggestion);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: String.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }

        }

        public async System.Threading.Tasks.Task DeleteSuggestionAsync(int taskId, int suggestionId)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_task.Task_Suggestions) }).FirstOrDefaultAsync();
            var suggestion = task.Task_Suggestions.Where(x => x.Id == suggestionId).FirstOrDefault();
            var taskValidation = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            var suggestionValidation = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, suggestion, Task_SuggestionOperations.Delete);
            if (taskValidation.Succeeded && suggestionValidation.Succeeded)
            {
                //task.RemoveSuggestion(suggestion);
                suggestion.Delete();
                await _task_suggestionService.UpdateAsync(suggestion);
                var validationResult = await _taskService.UpdateAsync(task);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: String.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<TrainingGroup>> GetLinkedTrainingGroups(int id)
        {
            var trainingGroup = await _task_trainingGroup_service.FindQueryWithIncludeAsync(x => x.TaskId == id, new string[] { nameof(_task_trainingGroup.TrainingGroup) }).Select(x => x.TrainingGroup).ToListAsync();
            return trainingGroup;
        }

        public async System.Threading.Tasks.Task LinkTrainingGroupsAsync(int id, Task_TrainingGroupOptions options)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { nameof(_task.Task_TrainingGroups) }).FirstOrDefaultAsync();

            foreach (var trId in options.TrainingGroupIds)
            {
                var trainingGroup = await _trainigGroup_Service.FindQuery(x => x.Id == trId).FirstOrDefaultAsync();
                task.LinkTrainingGroup(trainingGroup);
                await _taskService.UpdateAsync(task);
            }
        }

        public async Task<List<TaskMetaLinkVM>> GetLinkedMetaTasks(int metaTaskId)
        {
            var metaTaskLink = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == metaTaskId).ToListAsync();
            var linkedTaskIds = metaTaskLink.Select(x => x.TaskId).ToList();
            var tasks = await _taskService.FindQuery(x => linkedTaskIds.Contains(x.Id)).OrderBy(x => x.Id).ToListAsync();
            var taskNumbers = new Dictionary<int, string>();

            foreach (var item in tasks)
            {
                var number = await GetTaskNumberWithLetter(item.SubdutyAreaId, item.Id);
                taskNumbers.Add(item.Id, number.Letter + " " + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber);
            }

            var vm = (from t in tasks
                      join m in metaTaskLink on t.Id equals m.TaskId
                      select new TaskMetaLinkVM
                      {
                          Id = t.Id,
                          Description = t.Description,
                          IsRR = t.IsReliability,
                          Number = taskNumbers[t.Id],
                          MetaTaskId = m.Id,
                          Active = t.Active
                      }).OrderBy(x => x.Id).ToList();


            return vm;
        }

        public async System.Threading.Tasks.Task UnlinkTrainingGroupsAsync(int id, Task_TrainingGroupOptions options)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { nameof(_task.Task_TrainingGroups) }).FirstOrDefaultAsync();

            foreach (var trId in options.TrainingGroupIds)
            {
                var trainingGroup = await _trainigGroup_Service.FindQuery(x => x.Id == trId).FirstOrDefaultAsync();
                task.UnlinkTrainingGroup(trainingGroup);
                await _taskService.UpdateAsync(task);
            }
        }

        public async Task<TaskNumberVM> GetTaskNumberWithLetter(int id, int? taskId = null)
        {
            var sda = await _subdutyAreaService.AllQueryWithIncludeAndDeleted(new string[] { nameof(_subDutyArea.Tasks) }).Where(x => x.Id == id).FirstOrDefaultAsync();
            sda.Tasks = sda.Tasks.OrderBy(o => o.Number).ToList();
            var taskNumber = taskId.HasValue ? sda.Tasks.FirstOrDefault(x => x.Id == taskId)?.Number ?? 0 : sda.Tasks.LastOrDefault()?.Number ?? 0;
            var DA = await _dutyAreaService.FindQueryWithDeleted(x => x.Id == sda.DutyAreaId).FirstOrDefaultAsync();

            var numberVM = new TaskNumberVM();
            numberVM.TaskNumber = taskNumber;
            numberVM.SDANumber = sda.SubNumber;
            numberVM.DANumber = DA.Number;
            numberVM.Letter = DA.Letter;
            return numberVM;
        }

        public async Task<List<TaskWithNumberVM>> GetPendingTasks()
        {
            var taskIds = await _taskColabService.AllQuery().Select(x => x.InvitedForTaskId).Distinct().ToListAsync();
            List<TaskWithNumberVM> taskWithNumberVMs = new List<TaskWithNumberVM>();
            foreach (var id in taskIds)
            {
                var taskWithNum = new TaskWithNumberVM();
                var task = await _taskService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
                if (task != null)
                {
                    taskWithNum.Task = task;
                    var numbers = await GetTaskNumberWithLetter(task.SubdutyAreaId, id);
                    taskWithNum.SDANumber = numbers.SDANumber;
                    taskWithNum.DANumber = numbers.DANumber;
                    taskWithNum.Letter = numbers.Letter;
                    taskWithNumberVMs.Add(taskWithNum);
                }
            }

            return taskWithNumberVMs;
        }

        public async Task<bool> DAHasTaskWithLinks(int daId)
        {
            var da = await _dutyAreaService.FindQueryWithIncludeAsync(x => x.Id == daId, new string[] { nameof(_dutyArea.SubdutyAreas) }).FirstOrDefaultAsync();
            foreach (var sda in da.SubdutyAreas)
            {
                if (await SDAHasTaskWithLinks(sda.Id))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> SDAHasTaskWithLinks(int sdaId)
        {
            var sda = await _subdutyAreaService.FindQueryWithIncludeAsync(x => x.Id == sdaId, new string[] { nameof(_subDutyArea.Tasks) }).FirstOrDefaultAsync();
            foreach (var task in sda.Tasks)
            {
                if (task.Active)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> TaskHasLinks(int taskId)
        {
            var task = (await _taskService.FindWithIncludeAsync(x => x.Id == taskId, new string[] { "TaskQualifications" })).FirstOrDefault();
            if (task != null && task.TaskQualifications.Any(x=>x.IsComplete))
            {
                return true;
            }
            return false;
        }

        public async Task<List<MetaTask_OJTVM>> GetMetaTaskStepsAsync(int taskId)
        {
            List<MetaTask_OJTVM> metaStepsList = new List<MetaTask_OJTVM>();
            var metaSteps = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_task.Task_Steps) }, true).SelectMany(s => s.Task_Steps).ToListAsync();
            foreach (var metaStep in metaSteps)
            {
                metaStepsList.Add(new MetaTask_OJTVM { Description = metaStep.Description, Number = metaStep.Number, Id = metaStep.Id, TaskId = metaStep.TaskId, ParentStepId = metaStep.ParentStepId, isCreated = true });
            }

            var linkedTaskSteps = await _metaTask_task_linkService.FindQueryWithIncludeAsync(x => x.Meta_TaskId == taskId, new string[] { "Task.Task_Steps" }, true).OrderBy(o => o.Id).SelectMany(s => s.Task.Task_Steps).ToListAsync();
            if (linkedTaskSteps != null)
            {
                foreach (var linkedStep in linkedTaskSteps)
                {
                    metaStepsList.Add(new MetaTask_OJTVM { Description = linkedStep.Description, Number = linkedStep.Number, Id = linkedStep.Id, TaskId = linkedStep.TaskId, ParentStepId = linkedStep.ParentStepId, isCreated = false });
                }
            }
            return metaStepsList;
        }

        public async Task<List<Tool>> GetMetaToolsDataAsync(int taskId)
        {
            List<Tool> metaLinkedTools = new List<Tool>();
            var taskIds = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == taskId).Select(s => s.TaskId).ToListAsync();
            foreach (var tId in taskIds)
            {
                var tools = await _task_toolService.FindQueryWithIncludeAsync(x => x.TaskId == tId, new string[] { "Tool" }, true).Select(s => s.Tool).ToListAsync();
                metaLinkedTools.AddRange(tools);
            }
            metaLinkedTools = metaLinkedTools.GroupBy(g => g.Id).Select(s => s.First()).ToList();
            return metaLinkedTools;
        }

        public async Task<List<MetaTask_QuestionsVM>> GetMetaTaskQuestionDataAsync(int taskId)
        {
            List<MetaTask_QuestionsVM> metaQuestions = new List<MetaTask_QuestionsVM>();
            var createQuestions = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { "Task_Questions" }, true).Select(s => s.Task_Questions).FirstOrDefaultAsync();
            foreach (var createdQuestion in createQuestions)
            {
                metaQuestions.Add(new MetaTask_QuestionsVM { Answer = createdQuestion.Answer, Id = createdQuestion.Id, Question = createdQuestion.Question, QuestionNumber = createdQuestion.QuestionNumber, TaskId = createdQuestion.TaskId, isCreated = true });
            }

            var taskQuestons = await _metaTask_task_linkService.FindQueryWithIncludeAsync(x => x.Meta_TaskId == taskId, new string[] { "Task.Task_Questions" }, true).SelectMany(s => s.Task.Task_Questions).ToListAsync();
            if (taskQuestons != null)
            {
                foreach (var taskQuestion in taskQuestons)
                {
                    metaQuestions.Add(new MetaTask_QuestionsVM { Answer = taskQuestion.Answer, Id = taskQuestion.Id, Question = taskQuestion.Question, QuestionNumber = taskQuestion.QuestionNumber, TaskId = taskQuestion.TaskId, isCreated = false });
                }
            }

            return metaQuestions;
        }

        public async Task<List<MetaTask_SuggestionsVM>> GetMetaTaskSuggestionsAsync(int taskId)
        {
            List<MetaTask_SuggestionsVM> metaSuggestions = new List<MetaTask_SuggestionsVM>();

            var createSuggestions = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { "Task_Suggestions" }, true).Select(s => s.Task_Suggestions).FirstOrDefaultAsync();
            foreach (var createdSuggestion in createSuggestions)
            {
                metaSuggestions.Add(new MetaTask_SuggestionsVM { Description = createdSuggestion.Description, Id = createdSuggestion.Id, Number = createdSuggestion.Number, TaskId = createdSuggestion.TaskId, isCreated = true });
            }

            var taskSuggestions = await _metaTask_task_linkService.FindQueryWithIncludeAsync(x => x.Meta_TaskId == taskId, new string[] { "Task.Task_Suggestions" }, true).SelectMany(s => s.Task.Task_Suggestions).ToListAsync();
            if (taskSuggestions != null)
            {
                foreach (var taskSuggestion in taskSuggestions)
                {
                    metaSuggestions.Add(new MetaTask_SuggestionsVM { Description = taskSuggestion.Description, Id = taskSuggestion.Id, Number = taskSuggestion.Number, TaskId = taskSuggestion.TaskId, isCreated = false });
                }
            }

            return metaSuggestions;
        }

        public async Task<List<TrainingGroup>> GetMetaTaskTrainingGroups(int taskId)
        {
            //var trainingGroups = (await _metaTask_task_linkService.FindQueryWithIncludeAsync(x => x.Meta_TaskId == taskId, new string[] { "Task.Task_TrainingGroups" },true).Select(s => s.Task.Task_TrainingGroups).FirstOrDefaultAsync()).Select(s => s.TrainingGroup).ToList();
            List<TrainingGroup> trainingGroups = new List<TrainingGroup>();
            var taskIds = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == taskId).Select(s => s.TaskId).ToListAsync();
            foreach (var tId in taskIds)
            {
                var tgs = await _task_trainingGroup_service.FindQueryWithIncludeAsync(x => x.TaskId == tId, new string[] { "TrainingGroup" }).Select(s => s.TrainingGroup).ToListAsync();
                trainingGroups.AddRange(tgs);
            }
            trainingGroups = trainingGroups.GroupBy(g => g.Id).Select(s => s.First()).ToList();
            return trainingGroups;
        }

        public async Task<MetaTaskOJTVM> GetCondCritRefForMetaAsync(int id)
        {
            var ojtVM = new MetaTaskOJTVM();
            var meta = await _taskService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (meta == null)
            {
                throw new BadHttpRequestException(message: _localizer["MetaTaskNotFoundException"]);
            }
            else
            {
                if (!string.IsNullOrEmpty(meta.Conditions))
                {
                    ojtVM.MetaConditions.Add(meta.Conditions);
                }
                if (!string.IsNullOrEmpty(meta.Criteria))
                {
                    ojtVM.MetaCriteria.Add(meta.Criteria);
                }
                if (!string.IsNullOrEmpty(meta.References))
                {
                    ojtVM.MetaReferences.Add(meta.References);
                }
                var taskIds = await _metaTask_task_linkService.FindQuery(x => x.Meta_TaskId == meta.Id).Select(s => s.TaskId).ToListAsync();
                foreach (var taskId in taskIds)
                {
                    var task = await _taskService.FindQuery(x => x.Id == taskId).FirstOrDefaultAsync();
                    if (task != null)
                    {
                        if (!string.IsNullOrEmpty(task.Conditions))
                        {
                            ojtVM.MetaConditions.Add(task.Conditions);
                        }
                        if (!string.IsNullOrEmpty(task.Criteria))
                        {
                            ojtVM.MetaCriteria.Add(task.Criteria);
                        }
                        if (!string.IsNullOrEmpty(task.References))
                        {
                            ojtVM.MetaReferences.Add(task.References);
                        }
                    }
                }

                return ojtVM;
            }
        }

        public async Task<TaskRequalVM> GetRequalInfoAsync(int id)
        {
            var requalInfo = new TaskRequalVM();
            var task = await _taskService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (task == null)
            {
                throw new BadHttpRequestException(message: _localizer["TaskNotFoundException"]);
            }
            else
            {
                requalInfo.RequalDueDate = task.RequalificationDueDate;
                requalInfo.RequalRequired = task.RequalificationRequired ?? false;
                return requalInfo;
            }
        }

        public async Task<bool> CanTaskBeDeactivatedAsync(int id)
        {
            var task = await _taskService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (task != null)
            {

                var taskQuals = await _task_qualService.FindQuery(x => x.TaskId == task.Id && x.IsReleasedToEMP == true && x.TaskQualificationStatus.Name== "Pending").AnyAsync();
                return taskQuals;

            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Task Not Found"]);
            }
        }


        public async Task<List<Task>> GetTaskActiveInactive(string option)
        {
            var rrList = new List<Task>();

            switch (option.ToLower().Trim())
            {
                case "active":
                    rrList = await _taskService.FindQuery(x => x.Active == true).Select(s => new Task
                    {
                        Id = s.Id,
                        Number = s.Number,
                        Description = s.Description,
                    }).OrderBy(o => o.Number).ToListAsync();
                    break;
                case "inactive":
                    rrList = await _taskService.FindQuery(x => x.Active != true).Select(s => new Task
                    {
                        Id = s.Id,
                        Number = s.Number,
                        Description = s.Description,
                    }).OrderBy(o => o.Number).ToListAsync();
                    break;


            }

            return rrList;

        }

        public async Task<List<TaskWithPositionCompactVM>> GetTasksWithDutySubDutyAreaAsync()
        {
            var tasks = (await _taskService.GetTasksWithPositionTasksAsync()).Select(x=> new TaskWithPositionCompactVM ( 
                x.Id,x.FullNumber,x.Description,x.IsReliability,x.Position_Tasks.Select(x=>_hasher.Encode(x.PositionId.ToString())).Distinct().ToList())
            );
            return tasks.ToList();
        }

        public async Task<List<DutyAreaTreeVM>> GetTaskTreeDataByPositionsAsync(List<int> posIds)
        {
            List<DutyAreaTreeVM> returnVM = new List<DutyAreaTreeVM>();
            var dutyAreas = (await _dutyAreaService.GetAllOrderByNumber()).GroupBy(g => g.Letter).SelectMany(s => s).ToList();
            var subDutyAreas = await _subdutyAreaService.GetAllOrderByNumber();
            var positiontasks = (await _position_taskService.GetPositionTasksByPositionIds(posIds));
            var tasks = positiontasks.Select(m => m.Task).Distinct().ToList().OrderBy(x => x.Number);

            foreach (var da in dutyAreas)
            {
                DutyAreaTreeVM daData = new DutyAreaTreeVM
                {
                    Active = da.Active,
                    Id = da.Id,
                    Letter = da.Letter,
                    Number = da.Number,
                    Title = da.Title,
                    SubdutyAreas = subDutyAreas.Where(w => w.DutyAreaId == da.Id).Select(s => new SubDutyAreaTreeVM
                    {
                        Id = s.Id,
                        Active = s.Active,
                        SubNumber = s.SubNumber,
                        Title = s.Title,
                        Tasks = tasks.Where(w => w.SubdutyAreaId == s.Id).Select(t => new TaskTreeDataVM
                        {
                            Active = t.Active,
                            Description = t.Description,
                            Id = t.Id,
                            IsMeta = t.IsMeta,
                            IsReliability = t.IsReliability,
                            Number = t.Number,
                        }).ToList(),
                    }).ToList(),
                };

                returnVM.Add(daData);
            }

            foreach(var rV in returnVM)
            {
                rV.SubdutyAreas = rV.SubdutyAreas.Where(m => m.Tasks.Any()).ToList();
            }
            returnVM = returnVM.Where(o => o.SubdutyAreas.Any()).ToList();

            return returnVM;
        }

        public async Task<List<ToolDataVM>> GetToolsDataByTaskIdAsync(int taskId)
        {
            var taskTools = await _task_toolService.FindWithIncludeAsync(x => x.TaskId == taskId,new[] { "Tool" });

            return taskTools.Select(task_tool => new ToolDataVM(task_tool.Tool.Name)).ToList();
        }

        public async Task<List<Task_StepDescriptionVM>> GetTask_StepsDescriptionAsync(int taskId)
        {
            var taskSteps = await _task_StepService.FindAsync(x => x.TaskId == taskId);
            return taskSteps.Where(r => r.Active).OrderBy(r => r.Number).Select(step => new Task_StepDescriptionVM(step.Description)).ToList();
        }

        public async Task<List<Task_SuggestionOptions>> GetTask_SuggestionsAsync(int taskId)
        {
            var taskSuggestions = await _task_suggestionService.FindAsync(x => x.TaskId == taskId);
            return taskSuggestions.Select(step => new Task_SuggestionOptions(step.TaskId, step.Description)).ToList();
        }

        public async System.Threading.Tasks.Task UpdateTaskAndVersionTaskAsync(VersionTaskModel options)
        {
            var task = await _taskService.GetAsync(options.TaskId);
            if (task == null)
            {
                return;
            }
            task.RequalificationDueDate = options.RequalificationDueDate;
            task.RequalificationRequired = options.RequalificationRequired;
            await _taskService.UpdateAsync(task);
            var version_Task = await _version_TaskService.GetRecentVersionTaskAsync(options.TaskId);
            if (version_Task != null)
            {
                version_Task.RequalificationDueDate = options.RequalificationDueDate;
                version_Task.RequalificationRequired = options.RequalificationRequired;
                await _version_TaskService.UpdateAsync(version_Task);
            }
        }
    }
}
