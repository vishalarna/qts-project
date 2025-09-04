using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;
using ITask_HistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_HistoryService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using ITask_VersionDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_TaskService;
using IVersion_Task_ProcedureLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_Procedure_LinkService;
using IVersion_Task_RRLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_RR_LinkService;
using IVersion_Task_ILALinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_ILA_LinkService;
using IVersion_Task_EOLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_EnablingObjective_LinkService;
using IVersion_Task_SHLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_SaftyHazard_LinkService;
using IVersion_Task_TGLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_TrainingGroupService;
using IVersion_Task_ToolLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_Tool_LinkService;
using IVersion_Task_StepsLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_StepService;
using IVersion_Task_QuestionLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_QuestionService;
using ITask_StepDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_StepService;
using ITask_SuggestionDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_SuggestionService;
using ITask_QuestionDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_QuestionService;
using IVersionTask_SuggDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_SuggestionService;
using IVersionTask_QuestionDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_QuestionService;
using IVersionTask_PositionLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_Position_LinkService;
using IVersionTask_MetaTask_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_MetaTask_LinkService;
using IProcedure_TaskDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_Task_LinkService;
using IILA_TaskObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TaskObjective_LinkService;
using ISafetyHazard_TaskLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_Task_LinkService;
using IRR_Task_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IRR_Task_LinkService;
using IPositionTaskDomainService = QTD2.Domain.Interfaces.Service.Core.IPosition_TaskService;
using ITask_EOLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_EnablingObjective_LinkService;
using ITask_MetaTaskLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_MetaTask_LinkService;
using QTD2.Infrastructure.Model.Version_Task;
using ITask_TGDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_TrainingGroupService;
using ITask_ToolDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_ToolService;

namespace QTD2.Application.Services.Shared
{
    public class Task_HistoryService : ITask_HistoryService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Task_CollaboratorInvitationService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITask_HistoryDomainService _task_hist_service;
        private readonly ITaskDomainService _taskService;
        private readonly ITaskService _task_appService;
        private readonly IPersonDomainService _person_Serivce;
        private readonly ITask_VersionDomainService _task_versionService;
        private readonly Version_Task _taskVersion;
        private readonly IVersion_Task_ProcedureLinkDomainService _version_task_proc_linkService;
        private readonly IVersion_Task_RRLinkDomainService _version_task_rr_linkService;
        private readonly IVersion_Task_ILALinkDomainService _version_task_ila_linkService;
        private readonly IVersion_Task_EOLinkDomainService _version_task_eo_linkService;
        private readonly IVersion_Task_SHLinkDomainService _version_task_sh_linkService;
        private readonly IVersion_Task_TGLinkDomainService _version_task_tg_linkService;
        private readonly IVersion_Task_ToolLinkDomainService _version_task_tool_linkService;
        private readonly IVersion_Task_StepsLinkDomainService _version_task_step_linkService;
        private readonly IVersion_Task_QuestionLinkDomainService _version_task_question_linkService;
        private readonly IVersionTask_PositionLinkDomainService _version_task_position_linkService;
        private readonly Version_Task_Procedure_Link _version_task_procedure;
        private readonly Version_Task_RR_Link _version_task_rr;
        private readonly Version_Task_ILA_Link _version_task_ila;
        private readonly Version_Task_EnablingObjective_Link _version_task_eo;
        private readonly Version_Task_SaftyHazard_Link _version_task_sh;
        private readonly Version_Task_TrainingGroup _version_task_tg;
        private readonly Version_Task_Tool_Link _version_task_tool;
        private readonly Version_Task_Position_Link _version_task_pos;
        private readonly Domain.Entities.Core.Task _task;
        private readonly ITask_StepDomainService _task_stepService;
        private readonly ITask_SuggestionDomainService _task_suggService;
        private readonly ITask_QuestionDomainService _task_quesService;
        private readonly IVersionTask_SuggDomainService _ver_task_suggService;
        private readonly IVersionTask_QuestionDomainService _ver_task_quesService;
        private readonly IVersionTask_MetaTask_LinkDomainService _version_Task_meta_linkService;
        private readonly Version_Task_MetaTask_Link _ver_task_meta;
        private readonly Version_Task_Step _version_task_step;
        private readonly IProcedure_TaskDomainService _proc_taskService;
        private readonly IILA_TaskObjectiveDomainService _ila_taskService;
        private readonly ISafetyHazard_TaskLinkDomainService _sh_taskService;
        private readonly IRR_Task_LinkDomainService _rr_taskService;
        private readonly IPositionTaskDomainService _pos_taskService;
        private readonly ITask_EOLinkDomainService _task_eoService;
        private readonly ITask_MetaTaskLinkDomainService _task_metaService;
        private readonly ITask_TGDomainService _task_tgService;
        private readonly ITask_ToolDomainService _task_toolService;


        public Task_HistoryService(IAuthorizationService authorizationService, IStringLocalizer<Task_CollaboratorInvitationService> localizer, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, ITask_HistoryDomainService task_hist_service, ITaskDomainService taskService, ITaskService task_appService, IPersonDomainService person_Serivce, ITask_VersionDomainService task_versionService, IVersion_Task_ProcedureLinkDomainService version_task_proc_linkService, IVersion_Task_RRLinkDomainService version_task_rr_linkService, IVersion_Task_ILALinkDomainService version_task_ila_linkService, IVersion_Task_EOLinkDomainService version_task_eo_linkService, IVersion_Task_SHLinkDomainService version_task_sh_linkService, IVersion_Task_TGLinkDomainService version_task_tg_linkService, IVersion_Task_ToolLinkDomainService version_task_tool_linkService, IVersion_Task_StepsLinkDomainService version_task_step_linkService, IVersion_Task_QuestionLinkDomainService version_task_question_linkService, ITask_SuggestionDomainService task_suggService, ITask_QuestionDomainService task_quesService, IVersionTask_SuggDomainService ver_task_suggService, IVersionTask_QuestionDomainService ver_task_quesService, ITask_StepDomainService task_stepService, IVersionTask_PositionLinkDomainService version_task_position_linkService, IVersionTask_MetaTask_LinkDomainService version_Task_meta_linkService, IProcedure_TaskDomainService proc_taskService, IILA_TaskObjectiveDomainService ila_taskService, ISafetyHazard_TaskLinkDomainService sh_taskService, IRR_Task_LinkDomainService rr_taskService, IPositionTaskDomainService pos_taskService, ITask_EOLinkDomainService task_eoService, ITask_MetaTaskLinkDomainService task_metaService, ITask_TGDomainService task_tgService, ITask_ToolDomainService task_toolService)
        {
            _authorizationService = authorizationService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _task_hist_service = task_hist_service;
            _taskService = taskService;
            _task_appService = task_appService;
            _person_Serivce = person_Serivce;
            _task_versionService = task_versionService;
            _taskVersion = new Version_Task();
            _version_task_proc_linkService = version_task_proc_linkService;
            _version_task_rr_linkService = version_task_rr_linkService;
            _version_task_ila_linkService = version_task_ila_linkService;
            _version_task_eo_linkService = version_task_eo_linkService;
            _version_task_sh_linkService = version_task_sh_linkService;
            _version_task_tg_linkService = version_task_tg_linkService;
            _version_task_tool_linkService = version_task_tool_linkService;
            _version_task_step_linkService = version_task_step_linkService;
            _version_task_question_linkService = version_task_question_linkService;
            _task = new Domain.Entities.Core.Task();
            _task_suggService = task_suggService;
            _task_quesService = task_quesService;
            _ver_task_suggService = ver_task_suggService;
            _ver_task_quesService = ver_task_quesService;
            _task_stepService = task_stepService;
            _version_task_position_linkService = version_task_position_linkService;
            _version_task_procedure = new Version_Task_Procedure_Link();
            _version_task_rr = new Version_Task_RR_Link();
            _version_task_ila = new Version_Task_ILA_Link();
            _version_task_eo = new Version_Task_EnablingObjective_Link();
            _version_task_sh = new Version_Task_SaftyHazard_Link();
            _version_task_tg = new Version_Task_TrainingGroup();
            _version_task_tool = new Version_Task_Tool_Link();
            _version_task_pos = new Version_Task_Position_Link();
            _ver_task_meta = new Version_Task_MetaTask_Link();
            _version_task_step = new Version_Task_Step();
            _version_Task_meta_linkService = version_Task_meta_linkService;
            _proc_taskService = proc_taskService;
            _ila_taskService = ila_taskService;
            _sh_taskService = sh_taskService;
            _rr_taskService = rr_taskService;
            _pos_taskService = pos_taskService;
            _task_eoService = task_eoService;
            _task_metaService = task_metaService;
            _task_tgService = task_tgService;
            _task_toolService = task_toolService;
        }

        public async Task<List<Task_History>> GetAllHistoryAsync()
        {
            var histories = await _task_hist_service.AllQuery().ToListAsync();
            return histories;
        }

        public async Task<Task_History> GetHistoryAsync(int id)
        {
            var history = await _task_hist_service.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            return history;
        }

        public async Task<List<Version_Task>> GetTaskVersions(int taskId)
        {
            var versions = await _task_versionService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            //versions.ForEach((ver) =>
            //{
            //    ver.CreatedBy = _userManager.Users.Where(w => w.Email == ver.CreatedBy).FirstOrDefault().Email;
            //});
            return versions;
        }

        public async System.Threading.Tasks.Task SaveHistoryAsync(Task_HistoryOptions options)
        {
            foreach (var id in options.TaskIds)
            {
                var hist = new Task_History();
                hist.TaskId = id;
                hist.ChangeEffectiveDate = options.EffectiveDate;
                hist.ChangeNotes = options.ChangeNotes;
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = System.DateTime.Now;
                hist.OldStatus = false;
                hist.NewStatus = true;
                hist.Version_TaskId = options.Version_TaskId;
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, Task_HistoryOperations.Create);
                if (result.Succeeded)
                {
                    var validationResult = await _task_hist_service.AddAsync(hist);
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


        public async Task<List<TaskLatestActivityVM>> GetLatestActivity(bool getTrimmed)
        {
            IEnumerable<Task_History> history = new List<Task_History>();

            if(getTrimmed)
            {
                history = await _task_hist_service.GetLastXHistorysAsync(5);
            }
            else
            {
                history = await _task_hist_service.AllWithIncludeAsync(new string[] { "Task.SubdutyArea.DutyArea" });
            }

            List<TaskLatestActivityVM> latestactivity = history.Select(r => new TaskLatestActivityVM()
            {
                Id = r.Task.Id,
                ActivityDesc = r.ChangeNotes,
                Title = r.Task.SubdutyArea.DutyArea.Letter + " " + r.Task.SubdutyArea.DutyArea.Number + "." + r.Task.SubdutyArea.SubNumber + "." + r.Task.Number + " " + r.Task.Description,
                CreatedBy = r.CreatedBy,
                EffectiveDate = r.ChangeEffectiveDate,
                CreatedDate = r.CreatedDate
            }).ToList();
          
            latestactivity = latestactivity.OrderByDescending(o => o.CreatedDate).ToList();

            if (getTrimmed)
            {
                latestactivity = latestactivity.Take(5).ToList();
            }
            return latestactivity;
        }

        public async System.Threading.Tasks.Task RestoreHistory(int taskId, int historyId)
        {
            var version = await _task_versionService.FindQuery(x => x.TaskId == taskId && x.IsInUse, true
                ).FirstOrDefaultAsync();
            version.IsInUse = false;
            await _task_versionService.UpdateAsync(version);
            //var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId
            //, new string[]
            //{ nameof(_task.Task_Steps), nameof(_task.Task_Questions), nameof(_task.Task_Tools)
            //    , nameof(_task.Task_Suggestions),nameof(_task.Procedure_Task_Links),nameof(_task.Task_EnablingObjective_Links)
            //    ,nameof(_task.RR_Task_Links),nameof(_task.ILA_TaskObjective_Links),nameof(_task.SafetyHazard_Task_Links)
            //    , nameof(_task.Position_Tasks)}).FirstOrDefaultAsync();

            var task = await _taskService.FindQuery(x => x.Id == taskId).FirstOrDefaultAsync();
            var history = await _task_versionService.FindQuery(x => x.Id == historyId).FirstOrDefaultAsync();
            history.IsInUse = true;
            await _task_versionService.UpdateAsync(history);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Update);
            if (result.Succeeded)
            {
                task.Description = history.Description;
                task.Number = int.Parse(history.TaskNumber);
                task.Criteria = history.Criteria;
                task.Critical = history.Critical;
                task.References = history.References;
                task.RequiredTime = history.RequiredTime;
                task.Abreviation = history.Abbreviation;
                task.TaskCriteriaUpload = history.TaskCriteriaUpload;
                task.Image = history.Image;
                task.IsMeta = history.IsMeta;
                task.IsReliability = history.IsReliability;
                task.Conditions = history.Conditions;
                task.RequalificationDueDate = history.RequalificationDueDate;
                task.RequalificationNotes = history.RequalificationNotes;
                task.RequalificationRequired = history.RequalificationRequired;
                task.EffectiveDate = history.EffectiveDate.GetValueOrDefault();
                //task.Set_Id(taskId);
                if (history.TaskActive)
                {
                    task.Activate();
                }
                else
                {
                    task.Deactivate();
                }
                task.CreatedBy = history.CreatedBy;
                task.CreatedDate = history.CreatedDate;
                task.ModifiedBy = history.ModifiedBy;
                task.ModifiedDate = history.ModifiedDate;

                //task.Procedure_Task_Links.Clear();
                //task.Task_EnablingObjective_Links.Clear();
                //task.RR_Task_Links.Clear();
                //task.SafetyHazard_Task_Links.Clear();
                //task.ILA_TaskObjective_Links.Clear();
                //task.Task_TrainingGroups.Clear();
                //task.Task_Tools.Clear();
                //task.Position_Tasks.Clear();
                //task.Task_MetaTask_Links.Clear();
                //task.Task_Steps.Clear();
                //task.Task_Questions.Clear();
                //task.Task_Suggestions.Clear();

                var numCheck = await _taskService.FindQuery(x => x.Id != task.Id && x.Number == task.Number && x.SubdutyAreaId == task.Id).FirstOrDefaultAsync();
                if (numCheck != null)
                {
                    task.Number = (await _task_appService.GetTaskNumberWithLetter(task.SubdutyAreaId)).TaskNumber + 1;
                }

                await _taskService.UpdateAsync(task);

                //// For Steps
                //var verSteps = await _version_task_step_linkService.FindQuery(x => x.VersionTaskId == historyId).ToListAsync();
                //await ResetAllSteps(task.Id);
                //foreach (var step in verSteps)
                //{
                //    var foundStep = await _task_stepService.FindQueryWithDeleted(x => x.Id == step.TaskStepId).FirstOrDefaultAsync();
                //    foundStep.Restore();
                //    await _task_stepService.UpdateAsync(foundStep);
                //}

                //// For Suggestions
                //var verSugg = await _ver_task_suggService.FindQuery(x => x.Version_TaskId == historyId).ToListAsync();
                //await ResetAllSuggestions(task.Id);
                //foreach (var sugg in verSugg)
                //{
                //    var foundsugg = await _task_suggService.FindQueryWithDeleted(x => x.Id == sugg.Task_SuggestionId).FirstOrDefaultAsync();
                //    foundsugg.Restore();
                //    await _task_suggService.UpdateAsync(foundsugg);
                //}

                //// For Questions
                //var verQues = await _ver_task_quesService.FindQuery(x => x.VersionTaskId == historyId).ToListAsync();
                //await ResetAllQuestions(task.Id);
                //foreach (var ques in verQues)
                //{
                //    var foundQue = await _task_quesService.FindQueryWithDeleted(x => x.Id == ques.TaskQuestionId).FirstOrDefaultAsync();
                //    foundQue.Restore();
                //    await _task_quesService.UpdateAsync(foundQue);
                //}


                var histProc = await _version_task_proc_linkService.FindQueryWithIncludeAsync(x => x.Version_TaskId == historyId, new string[] { nameof(_version_task_procedure.Version_Procedure), nameof(_version_task_procedure.Version_Task) }).ToListAsync();
                var shouldLinkPROC = histProc.Select(s => s.Version_Procedure.ProcedureId).ToList();
                var toDeletePROC = await _proc_taskService.FindQuery(x => x.TaskId == taskId && !shouldLinkPROC.Contains(x.ProcedureId)).ToListAsync();
                foreach (var delPROC in toDeletePROC)
                {
                    await _proc_taskService.DeleteAsync(delPROC);
                }
                foreach (var proc in histProc)
                {
                    var proc_Link = new Procedure_Task_Link();
                    proc_Link.TaskId = proc.Version_Task.TaskId;
                    proc_Link.ProcedureId = proc.Version_Procedure.ProcedureId;
                    var proc_link = await _proc_taskService.FindQuery(x => x.TaskId == proc_Link.TaskId && x.ProcedureId == proc_Link.ProcedureId).FirstOrDefaultAsync();
                    if (proc_link == null)
                    {
                        await _proc_taskService.AddAsync(proc_Link);
                    }
                }

                var rrHist = await _version_task_rr_linkService.FindQueryWithIncludeAsync(x => x.Version_TaskId == historyId, new string[] { nameof(_version_task_rr.Version_Task), nameof(_version_task_rr.Version_RegulatoryRequirement) }).ToListAsync();
                var shouldLinkRR = rrHist.Select(s => s.Version_RegulatoryRequirement.RegulatoryRequirementId).ToList();
                var toDeleteRR = await _rr_taskService.FindQuery(x => x.TaskId == taskId && !shouldLinkRR.Contains(x.RegRequirementId)).ToListAsync();
                foreach (var delRR in toDeleteRR)
                {
                    await _rr_taskService.DeleteAsync(delRR);
                }
                foreach (var rr in rrHist)
                {
                    var rr_Link = new RR_Task_Link();
                    rr_Link.TaskId = rr.Version_Task.TaskId;
                    rr_Link.RegRequirementId = rr.Version_RegulatoryRequirement.RegulatoryRequirementId;
                    var rrlink = await _rr_taskService.FindQuery(x => x.RegRequirementId == rr_Link.RegRequirementId && x.TaskId == rr_Link.TaskId).FirstOrDefaultAsync();
                    if (rrlink == null)
                    {
                        await _rr_taskService.AddAsync(rr_Link);
                    }
                }

                var posHist = await _version_task_position_linkService.FindQueryWithIncludeAsync(x => x.Version_TaskId == historyId, new string[] { nameof(_version_task_pos.Version_Position), nameof(_version_task_pos.Version_Task) }).ToListAsync();
                var shouldLinkPOS = posHist.Select(s => s.Version_Position.PositionId).ToList();
                var toDeletePOS = await _pos_taskService.FindQuery(x => x.TaskId == taskId && !shouldLinkPOS.Contains(x.PositionId)).ToListAsync();
                foreach (var delPOS in toDeletePOS)
                {
                    await _pos_taskService.DeleteAsync(delPOS);
                }
                foreach (var pos in posHist)
                {
                    var posLink = new Position_Task();
                    posLink.PositionId = pos.Version_Position.PositionId;
                    posLink.TaskId = pos.Version_Task.TaskId;
                    var link = await _pos_taskService.FindQuery(x => x.PositionId == posLink.PositionId && x.TaskId == posLink.TaskId).FirstOrDefaultAsync();
                    if (link == null)
                    {
                        await _pos_taskService.AddAsync(posLink);
                    }
                }

                var ilaHist = await _version_task_ila_linkService.FindQueryWithIncludeAsync(x => x.Version_TaskId == historyId, new string[] { nameof(_version_task_ila.Version_Task), nameof(_version_task_ila.Version_ILA) }).ToListAsync();
                var shouldLinkILA = ilaHist.Select(s => s.Version_ILA.ILAId).ToList();
                var toDeleteILA = await _ila_taskService.FindQuery(x => x.TaskId == taskId && !shouldLinkILA.Contains(x.ILAId)).ToListAsync();
                foreach (var delILA in toDeleteILA)
                {
                    await _ila_taskService.DeleteAsync(delILA);
                }

                foreach (var ila in ilaHist)
                {
                    var ila_link = new ILA_TaskObjective_Link();
                    ila_link.TaskId = ila.Version_Task.TaskId;
                    ila_link.ILAId = ila.Version_ILA.ILAId;
                    var link = await _ila_taskService.FindQuery(x => x.ILAId == ila_link.ILAId && x.TaskId == ila_link.TaskId).FirstOrDefaultAsync();
                    if (link == null)
                    {
                        await _ila_taskService.AddAsync(ila_link);
                    }
                }

                var eoHist = await _version_task_eo_linkService.FindQueryWithIncludeAsync(x => x.Version_TaskId == historyId, new string[] { nameof(_version_task_eo.Version_Task), nameof(_version_task_eo.Version_EnablingObjective) }).ToListAsync();
                var shouldLinkEO = eoHist.Select(s => s.Version_EnablingObjective.EnablingObjectiveId).ToList();
                var toDeleteEO = await _task_eoService.FindQuery(x => x.TaskId == taskId && !shouldLinkEO.Contains(x.EnablingObjectiveId)).ToListAsync();
                foreach (var delEO in toDeleteEO)
                {
                    await _task_eoService.DeleteAsync(delEO);
                }
                foreach (var eo in eoHist)
                {
                    var eo_link = new Task_EnablingObjective_Link();
                    eo_link.EnablingObjectiveId = eo.Version_EnablingObjective.EnablingObjectiveId;
                    eo_link.TaskId = eo.Version_Task.TaskId;
                    var link = await _task_eoService.FindQuery(x => x.TaskId == eo_link.TaskId && x.EnablingObjectiveId == eo_link.EnablingObjectiveId).FirstOrDefaultAsync();
                    if (link == null)
                    {
                        await _task_eoService.AddAsync(eo_link);
                    }
                }

                var shHist = await _version_task_sh_linkService.FindQueryWithIncludeAsync(x => x.Version_TaskId == historyId, new string[] { nameof(_version_task_sh.Version_Task), nameof(_version_task_sh.Version_SaftyHazard) }).ToListAsync();
                var shouldLinkSH = shHist.Select(s => s.Version_SaftyHazard.SaftyHazardId).ToList();
                var toDeleteSH = await _sh_taskService.FindQuery(x => x.TaskId == taskId && !shouldLinkSH.Contains(x.SaftyHazardId)).ToListAsync();
                foreach (var delSH in toDeleteSH)
                {
                    await _sh_taskService.DeleteAsync(delSH);
                }
                foreach (var sh in shHist)
                {
                    var sh_link = new SafetyHazard_Task_Link();
                    sh_link.SaftyHazardId = sh.Version_SaftyHazard.SaftyHazardId;
                    sh_link.TaskId = sh.Version_Task.TaskId;
                    var link = await _sh_taskService.FindQuery(x => x.TaskId == sh_link.TaskId && x.SaftyHazardId == sh_link.SaftyHazardId).FirstOrDefaultAsync();
                    if (link == null)
                    {
                        await _sh_taskService.AddAsync(sh_link);
                    }
                }

                var metaHist = await _version_Task_meta_linkService.FindQueryWithIncludeAsync(x => x.Version_MetaTaskId == historyId, new string[] { nameof(_ver_task_meta.Version_Task), nameof(_ver_task_meta.Version_MetaTask) }).ToListAsync();
                var shouldLinkIds = metaHist.Select(s => s.Version_Task.TaskId).ToList();
                var toDeleteMeta = await _task_metaService.FindQuery(x => x.Meta_TaskId == taskId && !shouldLinkIds.Contains(x.TaskId)).ToListAsync();
                foreach (var toDel in toDeleteMeta)
                {
                    await _task_metaService.DeleteAsync(toDel);
                }
                foreach (var meta in metaHist)
                {
                    var meta_link = new Task_MetaTask_Link();
                    meta_link.TaskId = meta.Version_Task.TaskId;
                    meta_link.Meta_TaskId = meta.Version_MetaTask.TaskId;
                    var link = await _task_metaService.FindQuery(x => x.TaskId == meta_link.TaskId && x.Meta_TaskId == meta_link.Meta_TaskId).FirstOrDefaultAsync();
                    if (link == null)
                    {
                        await _task_metaService.AddAsync(meta_link);
                    }
                }

                var tgHist = await _version_task_tg_linkService.FindQueryWithIncludeAsync(x => x.Version_TaskId == historyId, new string[] { nameof(_version_task_tg.Version_Task), nameof(_version_task_tg.Version_TrainingGroup) }).ToListAsync();
                var shouldLinkTGs = tgHist.Select(s => s.Version_TrainingGroup.Version_TrainingGroupId).ToList();
                var toDeleteTG = await _task_tgService.FindQuery(x => x.TaskId == taskId && !shouldLinkTGs.Contains(x.TrainingGroupId)).ToListAsync();
                foreach (var toDel in toDeleteTG)
                {
                    await _task_tgService.DeleteAsync(toDel);
                }
                foreach (var tg in tgHist)
                {
                    var tg_link = new Task_TrainingGroup();
                    tg_link.TaskId = tg.Version_Task.TaskId;
                    tg_link.TrainingGroupId = tg.Version_TrainingGroup.Version_TrainingGroupId;
                    var link = await _task_tgService.FindQuery(x => x.TaskId == tg_link.TaskId && x.TrainingGroupId == tg_link.TrainingGroupId).FirstOrDefaultAsync();
                    if (link == null)
                    {
                        await _task_tgService.AddAsync(tg_link);
                    }
                }

                var toolHist = await _version_task_tool_linkService.FindQueryWithIncludeAsync(x => x.Version_TaskId == historyId, new string[] { nameof(_version_task_tool.Version_Task), nameof(_version_task_tool.Version_Tool) }).ToListAsync();
                var shouldLinkTools = toolHist.Select(s => s.Version_Tool.ToolId).ToList();
                var toDeleteTool = await _task_toolService.FindQuery(x => x.TaskId == taskId && !shouldLinkTools.Contains(x.ToolId)).ToListAsync();
                foreach (var toDel in toDeleteTool)
                {
                    await _task_toolService.DeleteAsync(toDel);
                }
                foreach (var tool in toolHist)
                {
                    var tool_link = new Task_Tool();
                    tool_link.TaskId = tool.Version_Task.TaskId;
                    tool_link.ToolId = tool.Version_Tool.ToolId;
                    var link = await _task_toolService.FindQuery(x => x.TaskId == tool_link.TaskId && x.ToolId == tool_link.ToolId).FirstOrDefaultAsync();
                    if (link == null)
                    {
                        await _task_toolService.AddAsync(tool_link);
                    }
                }

                var taskSteps = await _task_stepService.FindQuery(x => x.TaskId == taskId).ToListAsync();
                foreach (var step in taskSteps)
                {
                    //var tStep = await _task_stepService.FindQuery(x => x.Id == step.Id, true).FirstOrDefaultAsync();
                    if (step != null)
                    {
                        step.Delete();
                        await _task_stepService.UpdateAsync(step);
                    }
                }
                var histSteps = await _version_task_step_linkService.FindQuery(x => x.VersionTaskId == historyId).ToListAsync();
                foreach (var vStep in histSteps)
                {
                    var step = new Task_Step();
                    step.TaskId = task.Id;
                    step.Description = vStep.Description;
                    step.Number = vStep.Number;
                    step.Active = vStep.Active;
                    step.ParentStepId = null;
                    await _task_stepService.AddAsync(step);
                }

                var taskQuestions = await _task_quesService.FindQuery(x => x.TaskId == taskId).ToListAsync();
                foreach (var question in taskQuestions)
                {
                    //var tQues = await _task_quesService.FindQuery(x => x.Id == question.Id).FirstOrDefaultAsync();
                    if (question != null)
                    {
                        question.Delete();
                        await _task_quesService.UpdateAsync(question);
                    }
                }

                var histQuestions = await _version_task_question_linkService.FindQuery(x => x.VersionTaskId == historyId).ToListAsync();

                foreach (var vQues in histQuestions)
                {
                    var que = new Task_Question();
                    que.Question = vQues.Question;
                    que.Answer = vQues.Answer;
                    que.QuestionNumber = vQues.QuestionNumber;
                    que.Active = vQues.Active;
                    que.TaskId = task.Id;
                    await _task_quesService.AddAsync(que);
                }

                var taskSuggs = await _task_suggService.FindQuery(x => x.TaskId == taskId).ToListAsync();
                foreach (var sugg in taskSuggs)
                {
                    if (sugg != null)
                    {
                        sugg.Delete();
                        await _task_suggService.UpdateAsync(sugg);
                    }
                }

                var histSugg = await _ver_task_suggService.FindQuery(x => x.Version_TaskId == historyId).ToListAsync();
                foreach (var vSugg in histSugg)
                {
                    var sugg = new Task_Suggestion();
                    sugg.Description = vSugg.Description;
                    sugg.Number = vSugg.Number;
                    sugg.TaskId = task.Id;
                    sugg.Active = vSugg.Active;
                    await _task_suggService.AddAsync(sugg);
                }

                await _taskService.UpdateAsync(task);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }


        public async Task<List<TaskVersionVM>> GetLatestActivity(int taskId)
        {
            var tasks = await _taskService.FindQuery(x => x.Id == taskId).ToListAsync();
            var history = await _task_versionService.AllQuery().ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            var persons = await _person_Serivce.AllQuery().ToListAsync();
            var taskNumbers = new Dictionary<int, string>();

            List<TaskVersionVM> latestactivity = new List<TaskVersionVM>();

            foreach (var hist in history)
            {
                var task = tasks.Where(w => w.Id == hist.TaskId).FirstOrDefault();
                if (task != null)
                {
                    var user = await _userManager.Users.Where(x => x.Email == hist.CreatedBy).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        var number = await _task_appService.GetTaskNumberWithLetter(task.SubdutyAreaId);
                        var activity = new TaskVersionVM
                        {
                            Id = hist.Id,
                            TaskDescription = task.Description,
                            Title = number.Letter + " " + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber + " " + task.Description,
                            CreatedBy = user.Email,
                            CreatedDate = (System.DateTime)hist.CreatedDate,
                        };
                        latestactivity.Add(activity);
                    }
                }
            }
            return latestactivity.OrderByDescending(x => x.CreatedDate).ToList();


            //return latestactivity.OrderByDescending(x => x.CreatedDate).ToList();
        }
        public async System.Threading.Tasks.Task ResetAllSteps(int taskId)
        {
            var steps = await _task_stepService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            foreach (var step in steps)
            {
                step.Delete();
                await _task_stepService.UpdateAsync(step);
            }
        }

        public async System.Threading.Tasks.Task ResetAllSuggestions(int taskId)
        {
            var suggs = await _task_suggService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            foreach (var sugg in suggs)
            {
                sugg.Delete();
                await _task_suggService.UpdateAsync(sugg);
            }
        }

        public async System.Threading.Tasks.Task ResetAllQuestions(int taskId)
        {
            var ques = await _task_quesService.FindQuery(x => x.TaskId == taskId).ToListAsync();
            foreach (var que in ques)
            {
                que.Delete();
                await _task_quesService.UpdateAsync(que);
            }
        }
    }
}
