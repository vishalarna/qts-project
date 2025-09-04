using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using IEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using IProcedureDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedureService;
using ISaftyHazardDomainService = QTD2.Domain.Interfaces.Service.Core.ISaftyHazardService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using IToolDomainService = QTD2.Domain.Interfaces.Service.Core.IToolService;
using IVersion_TaskService = QTD2.Domain.Interfaces.Service.Core.IVersion_TaskService;
using IVersion_TaskStepDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_StepService;
using IVersion_TaskQuestionDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_QuestionService;
using IVersion_EOQuestionDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_EnablingObjective_QuestionService;
using IVersion_Task_Tool_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_Tool_LinkService;
using IVersiont_Task_SH_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_SaftyHazard_LinkService;
using IVersion_Task_Proc_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_Procedure_LinkService;
using IVersion_Task_EO_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_EnablingObjective_LinkService;
using IVersion_ILADomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_ILAService;
using IVersion_Task_ILADomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_ILA_LinkService;
using IVersion_RRDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_RegulatoryRequirementService;
using IVersion_Task_RRDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_RR_LinkService;
using IVersion_TrainingGroupDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_TrainingGroupService;
using IVersion_Task_TrainingGroupDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_TrainingGroupService;
using IVersion_EnablingObjectiveService = QTD2.Domain.Interfaces.Service.Core.IVersion_EnablingObjectiveService;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Application.Services.Shared
{
    public class VersioningService : IVersioningService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IVersion_SaftyHazardService _v_SaftyHazardService;
        private readonly ISaftyHazardDomainService _saftyHazardService;
        private readonly IVersion_EnablingObjectiveService _v_EnablingObjectiveService;
        private readonly IEnablingObjectiveDomainService _enablingObjectiveService;
        private readonly IVersion_ProcedureService _v_ProcedureService;
        private readonly IProcedureDomainService _procedureService;
        private readonly IVersion_ToolService _v_ToolService;
        private readonly IToolDomainService _toolService;
        private readonly IVersion_TaskService _v_TaskService;
        private readonly ITaskDomainService _taskService;
        private readonly IStringLocalizer<VersioningService> _localizer;
        private readonly IVersion_TaskStepDomainService _version_task_StepService;
        private readonly IVersion_TaskQuestionDomainService _version_task_questionService;
        private readonly IVersion_EOQuestionDomainService _version_eo_questionService;
        private readonly IVersion_Task_Tool_LinkDomainService _version_task_toolService;
        private readonly IVersiont_Task_SH_LinkDomainService _version_task_shService;
        private readonly IVersion_Task_Proc_LinkDomainService _version_task_procService;
        private readonly IVersion_Task_EO_LinkDomainService _version_task_eoService;
        private readonly IVersion_ILADomainService _version_ILA_Service;
        private readonly IVersion_Task_ILADomainService _version_task_ilaService;
        private readonly IVersion_RRDomainService _version_rrService;
        private readonly IVersion_Task_RRDomainService _version_task_rrService;
        private readonly IVersion_TrainingGroupDomainService _version_trainingGroupService;
        private readonly IVersion_Task_TrainingGroupDomainService _version_task_trainingGroupService;

        public VersioningService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IVersion_SaftyHazardService v_SaftyHazardService,
            ISaftyHazardDomainService saftyHazardService,
            IVersion_EnablingObjectiveService v_EnablingObjectiveService,
            IEnablingObjectiveDomainService enablingObjectiveService,
            IVersion_ProcedureService v_ProcedureService,
            IProcedureDomainService procedureService,
            IVersion_ToolService v_ToolService,
            IToolDomainService toolService,
            IVersion_TaskService v_TaskService,
            ITaskDomainService taskService,
            IStringLocalizer<VersioningService> localizer,
            IVersion_TaskStepDomainService version_task_StepService,
            IVersion_TaskQuestionDomainService version_task_questionService,
            IVersion_Task_Tool_LinkDomainService version_task_toolService,
            IVersiont_Task_SH_LinkDomainService version_task_shService,
            IVersion_Task_Proc_LinkDomainService version_task_procService,
            IVersion_Task_EO_LinkDomainService version_task_eoService,
            IVersion_ILADomainService version_ILA_Service,
            IVersion_Task_ILADomainService version_task_ilaService,
            IVersion_RRDomainService version_rrService,
            IVersion_TrainingGroupDomainService version_trainingGroupService,
            IVersion_Task_TrainingGroupDomainService version_task_trainingGroupService,
            IVersion_Task_RRDomainService version_task_rrService, IVersion_EOQuestionDomainService version_eo_questionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _v_SaftyHazardService = v_SaftyHazardService;
            _saftyHazardService = saftyHazardService;
            _v_EnablingObjectiveService = v_EnablingObjectiveService;
            _enablingObjectiveService = enablingObjectiveService;
            _v_ProcedureService = v_ProcedureService;
            _procedureService = procedureService;
            _v_ToolService = v_ToolService;
            _toolService = toolService;
            _v_TaskService = v_TaskService;
            _taskService = taskService;
            _localizer = localizer;
            _version_task_StepService = version_task_StepService;
            _version_task_questionService = version_task_questionService;
            _version_task_toolService = version_task_toolService;
            _version_task_shService = version_task_shService;
            _version_task_procService = version_task_procService;
            _version_task_eoService = version_task_eoService;
            _version_ILA_Service = version_ILA_Service;
            _version_task_ilaService = version_task_ilaService;
            _version_rrService = version_rrService;
            _version_trainingGroupService = version_trainingGroupService;
            _version_task_trainingGroupService = version_task_trainingGroupService;
            _version_task_rrService = version_task_rrService;
            _version_eo_questionService = version_eo_questionService;
        }

        public async Task<List<Version_Task>> GetVersionedTasksAsync()
        {
            var vTasks = await _v_TaskService.AllAsync();
            vTasks = vTasks.Where(task => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, Version_TaskOperations.Read).Result.Succeeded).ToList();
            return vTasks.ToList();
        }

        public async Task<Version_ILA> VersionILAAsync(ILA ila,string ChangeNotes,DateTime? EffectiveDate,int state = 1)
        {
            var versionNo = await GetVersionNumberByILAAsync(ila.Id);
            var vILA = ila.CreateSnapshot();
            vILA.ChangeReason = ChangeNotes;
            vILA.EffectiveDate = EffectiveDate;
            vILA.State = state;
            vILA.VersionNumber = versionNo;
            var validationResult = await _version_ILA_Service.AddAsync(vILA);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return vILA;
            }
        }

        public async Task<int> GetVersionNumberByILAAsync(int iLAID)
        {
            var existingVersions = await _version_ILA_Service.FindAsync(r => r.ILAId == iLAID);

            if (existingVersions.Any())
            {
                return existingVersions.Max(v => v.VersionNumber) + 1;
            }
            else
            {
                return 1;
            }
        }


        public async Task<Version_TrainingGroup> VersionTrainingGroupAsync(TrainingGroup tg)
        {
            var vTG = tg.CreateSnapshot();
            var validationResult = await _version_trainingGroupService.AddAsync(vTG);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return vTG;
            }
        }

        public async Task<Version_RegulatoryRequirement> VersionRRAsync(RegulatoryRequirement rr)
        {
            var vRR = rr.CreateSnapshot();
            var validationResult = await _version_rrService.AddAsync(vRR);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return vRR;
            }
        }

        public async System.Threading.Tasks.Task VersionEnablingObjectiveAsync(EnablingObjective eo)
        {
            //eo.UpdateVersion(isSignificant);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Update);
            //if (result.Succeeded)
            //{
            //    var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
            //    if (validationResult.IsValid)
            //    {
            //        var vEO = eo.CreateSnapshot();
            //        result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, vEO, Version_EnablingObjectiveOperations.Create);
            //        if (result.Succeeded)
            //        {
            //            validationResult = await _v_EnablingObjectiveService.AddAsync(vEO);
            //            if (validationResult.IsValid)
            //            {
            //                if (eo.Task_EnablingObjective_Links != null)
            //                {
            //                    foreach (var task in eo.Task_EnablingObjective_Links)
            //                    {
            //                        if (task.Task.Id != task.Id)
            //                        {
            //                            await VersionTaskAsync(task.Task);
            //                        }
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //            }
            //        }
            //        else
            //        {
            //            throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //        }
            //    }
            //    else
            //    {
            //        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //    }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //}

            var vEO = eo.CreateSnapshot();
            //vEO.MajorVersion = 1;
            //vEO.MinorVersion = 1;
            vEO.VersionNumber = "0";
            var validationResult = await _v_EnablingObjectiveService.AddAsync(vEO);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }

        public async Task<Version_Procedure> VersionProcedureAsync(Procedure proc)
        {
            // proc.UpdateVersion(isSignificant);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, proc, ProcedureOperations.Update);
            //if (result.Succeeded)
            //{
            //    var validationResult = await _procedureService.UpdateAsync(proc);
            //    if (validationResult.IsValid)
            //    {
            //        var vProc = proc.CreateSnapshot();
            //        result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, vProc, Version_ProcedureOperations.Create);
            //        if (result.Succeeded)
            //        {
            //            validationResult = await _v_ProcedureService.AddAsync(vProc);
            //            if (validationResult.IsValid)
            //            {
            //                if (proc.Task_Procedure_Links != null)
            //                {
            //                    foreach (var task in proc.Task_Procedure_Links)
            //                    {
            //                        if (task.Task.Id != task.Id)
            //                        {
            //                            await VersionTaskAsync(task.Task);
            //                        }
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //            }
            //        }
            //        else
            //        {
            //            throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //        }
            //    }
            //    else
            //    {
            //        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //    }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //}

            var vProc = proc.CreateSnapshot();
            vProc.MajorVersion = 1;
            vProc.MinorVersion = 1;
            var validationResult = await _v_ProcedureService.AddAsync(vProc);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return vProc;
            }
        }

        public async Task<Version_SaftyHazard> VersionSaftyHazardAsync(SaftyHazard sh)
        {
            //sh.UpdateVersion(isSignificant);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Update);
            //if (result.Succeeded)
            //{
            //    var validationResult = await _saftyHazardService.UpdateAsync(sh);
            //    if (validationResult.IsValid)
            //    {
            //        var vSh = sh.CreateSnapshot();
            //        result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, vSh, Version_SaftyHazardOperations.Create);

            //        if (result.Succeeded)
            //        {
            //            validationResult = await _v_SaftyHazardService.AddAsync(vSh);

            //            if (validationResult.IsValid)
            //            {
            //                foreach (var proc in sh.Procedure_SaftyHazard_Links)
            //                {
            //                    await VersionProcedureAsync(proc.Procedure, isSignificant);
            //                }

            //                foreach (var eo in sh.EnablingObjective_SaftyHazard_Links)
            //                {
            //                    await VersionEnablingObjectiveAsync(eo.EnablingObjective, isSignificant);
            //                }

            //                foreach (var task in sh.Task_SaftyHazard_Links)
            //                {
            //                    if (task.Task.Id != task.Id)
            //                    {
            //                        await VersionTaskAsync(task.Task, isSignificant);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //            }
            //        }
            //        else
            //        {
            //            throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //        }
            //    }
            //    else
            //    {
            //        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //    }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //}

            var vSh = sh.CreateSnapshot();
            vSh.MajorVersion = 1;
            vSh.MinorVersion = 1;
            var validationResult = await _v_SaftyHazardService.AddAsync(vSh);

            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return vSh;
            }
        }

        public async System.Threading.Tasks.Task VersionTaskAsync(Domain.Entities.Core.Task task)
        {
            //task.UpdateVersion(isSignificant);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            if (result.Succeeded)
            {
                var vTask = task.CreateSnapshot();
                //var validationResult = await _taskService.UpdateAsync(task);
                //if (validationResult.IsValid)
                //{
                //result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, vTask, Version_TaskOperations.Create);
                //if (result.Succeeded)
                //{
                //vTask.MajorVersion = 1;
                //vTask.MinorVersion = 1;
                vTask.Standards = "";
                var number = await _v_TaskService.FindQuery(x => x.TaskId == vTask.TaskId).OrderBy(o => o.Id).Select(s => s.VersionNumber).LastOrDefaultAsync();
                if(number == null)
                {
                    number = "1.0";
                }
                else
                {
                    var myNum = Double.Parse(number);
                    myNum += 1.0;
                    number = myNum.ToString() + ".0";
                }
                vTask.VersionNumber = number.ToString();
                var validationResult = await _v_TaskService.AddAsync(vTask);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                //    }
                //    else
                //    {
                //        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                //    }
                //}
                //else
                //{
                //    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                //}
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task VersionTaskStepAsync(Task_Step step)
        {
            var vStep = step.CreateSnapshot();
            var validationResult = await _version_task_StepService.AddAsync(vStep);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }

        public async System.Threading.Tasks.Task VersionTaskQuestionAsync(Task_Question question)
        {
            var vQuestion = question.CreateSnapshot();
            var validationResult = await _version_task_questionService.AddAsync(vQuestion);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }

        public async System.Threading.Tasks.Task VersionTask_ToolLinkAsync(List<Task_Tool> tools)
        {
            foreach (var tool in tools)
            {
                var vTool = tool.CreateSnapshot();
                var validationResult = await _version_task_toolService.AddAsync(vTool);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
        }

        //public async System.Threading.Tasks.Task VersionTaskShAsync(List<Task_SaftyHazard_Link> task_shs)
        //{
        //    foreach(var sh in task_shs)
        //    {
        //        var vSH = sh.CreateSnapshot();
                
        //        var validationResult = await _version_task_shService.AddAsync(vSH);
        //        if (!validationResult.IsValid)
        //        {
        //            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
        //        }
        //    }
        //}

        //public async System.Threading.Tasks.Task VersionTask_ProcedureLinkAsync(List<Task_Procedure_Link> task_procs)
        //{
        //    foreach(var task_proc in task_procs)
        //    {
        //        var vTask_Proc = task_proc.CreateSnapshot();
        //        var validationResult = await _version_task_procService.AddAsync(vTask_Proc);
        //        if (!validationResult.IsValid)
        //        {
        //            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
        //        }
        //    }
        //}

        public async System.Threading.Tasks.Task VersionTask_EOLinkAsync(List<Task_EnablingObjective_Link> task_eos)
        {
           foreach (var task_eo in task_eos)
            {
                var vTask_EO = task_eo.CreateSnapshot();
                var validationResult = await _version_task_eoService.AddAsync(vTask_EO);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
        }

        public async System.Threading.Tasks.Task VersionTask_ILALinkAsync(List<Task_ILA_Link> task_ilas)
        {
            foreach (var task_ila in task_ilas)
            {
                var vTask_EO = task_ila.CreateSnapshot();
                var validationResult = await _version_task_ilaService.AddAsync(vTask_EO);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
        }

        //public async System.Threading.Tasks.Task VersionTask_RR_LinkAsync(List<Task_RR_Link> task_rrs)
        //{
        //    foreach (var task_rr in task_rrs)
        //    {
        //        var vTask_EO = task_rr.CreateSnapshot();
        //        var validationResult = await _version_task_rrService.AddAsync(vTask_EO);
        //        if (!validationResult.IsValid)
        //        {
        //            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
        //        }
        //    }
        //}

        public async System.Threading.Tasks.Task VersionTask_TrainingGroupAsync(List<Task_TrainingGroup> task_tgs)
        {
            foreach (var task_tg in task_tgs)
            {
                var vTask_TG = task_tg.CreateSnapshot();
                var validationResult = await _version_task_trainingGroupService.AddAsync(vTask_TG);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
        }

        public async Task<Version_Tool> VersionToolAsync(Tool tool)
        {
            var vTool = tool.CreateSnapshot();
            vTool.MajorVersion = 1;
            vTool.MinorVersion = 1;
            var validationResult = await _v_ToolService.AddAsync(vTool);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return vTool;
            }
        }

        public async System.Threading.Tasks.Task VersionEnablingObjective_StepAsync(EnablingObjective_Step step)
        {
            //var vStep = step.CreateSnapshot();
            //var validationResult = await _version_task_StepService.AddAsync(vStep);
            //if (!validationResult.IsValid)
            //{
            //    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //}
        }

        public async System.Threading.Tasks.Task VersionEnablingObjective_QuestionAsync(EnablingObjective_Question ques)
        {
            var vQues = ques.CreateSnapshot();
            var validationResult = await _version_eo_questionService.AddAsync(vQues);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }
    }
}
