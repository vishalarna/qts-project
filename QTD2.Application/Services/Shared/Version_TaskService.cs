using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Version_Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IVersionTaskDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_TaskService;
using ITaskService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using IVersion_TaskService = QTD2.Application.Interfaces.Services.Shared.IVersion_TaskService;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using IVersionTask_StepDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_StepService;
using IVersionTask_QuestionDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_QuestionService;
using IVersionTask_ProcedureLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_ProcedureService;
using IVersionILADomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_ILAService;
using IVersionRRDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_RegulatoryRequirementService;
using IVersionEODomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_EnablingObjectiveService;
using IVersionSHDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_SaftyHazardService;
using IVersionToolDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_ToolService;
using IVersion_TrainingGroupDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_TrainingGroupService;
using IVersionTask_SuggestionDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_SuggestionService;
using IVersionPositionsDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_PositionService;
using ITask_HistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_HistoryService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using ITaskStepDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_StepService;
using ITaskQuestionDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_QuestionService;
using IProcedureTaskLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_Task_LinkService;
using IILATaskLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TaskObjective_LinkService;
using IRRTaskLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IRR_Task_LinkService;
using ITaskEOLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_EnablingObjective_LinkService;
using ISafetyHazardTaskDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_Task_LinkService;
using ITaskSuggesDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_SuggestionService;
using ITaskToolDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_ToolService;
using IPosTaskDomainService = QTD2.Domain.Interfaces.Service.Core.IPosition_TaskService;
using ITaskMetaLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_MetaTask_LinkService;
using QTD2.Domain.Services.Core;
using IProcedureDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedureService;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IRRDomainService = QTD2.Domain.Interfaces.Service.Core.IRegulatoryRequirementService;
using ISHDomainService = QTD2.Domain.Interfaces.Service.Core.ISaftyHazardService;
using IEODomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using IPosDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using ITask_TGDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_TrainingGroupService;
using ITrainingGroupDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingGroupService;
using IToolDomainService = QTD2.Domain.Interfaces.Service.Core.IToolService;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Model.Task_History;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class Version_TaskService : IVersion_TaskService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Version_TaskService> _localizer;
        private readonly IVersionTaskDomainService _versionTaskService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITaskService _taskService;
        private readonly Domain.Entities.Core.Task _task;
        private readonly IVersionTask_StepDomainService _ver_task_stepService;
        private readonly IVersionTask_QuestionDomainService _ver_task_quesService;
        private readonly IVersionTask_ProcedureLinkDomainService _ver_task_procService;
        private readonly IVersionILADomainService _ver_task_ilaService;
        private readonly IVersionRRDomainService _ver_task_rrService;
        private readonly IVersionEODomainService _ver_task_eoService;
        private readonly IVersionSHDomainService _ver_task_shService;
        private readonly IVersionToolDomainService _ver_task_toolService;
        private readonly IVersion_TrainingGroupDomainService _ver_tgService;
        private readonly IVersionTask_SuggestionDomainService _ver_task_suggService;
        private readonly IVersionPositionsDomainService _ver_posService;
        private readonly Version_Task _ver_task;
        private readonly ITask_HistoryDomainService _task_hist_service;
        private readonly IPersonDomainService _person_Serivce;
        private readonly ISubdutyAreaService _subdutyAreaService;
        private readonly ITaskStepDomainService _task_stepService;
        private readonly ITaskQuestionDomainService _task_qestionService;
        private readonly IProcedureTaskLinkDomainService _proc_task_linkService;
        private readonly IILATaskLinkDomainService _ila_task_linkService;
        private readonly IRRTaskLinkDomainService _rr_task_linkService;
        private readonly ITaskEOLinkDomainService _task_eo_linkService;
        private readonly ISafetyHazardTaskDomainService _sh_task_linkService;
        private readonly ITaskSuggesDomainService _task_suggService;
        private readonly ITaskToolDomainService _task_toolService;
        private readonly IPosTaskDomainService _pos_task_linkService;
        private readonly ITaskMetaLinkDomainService _task_metaService;
        private readonly IProcedureDomainService _procedureService;
        private readonly IVersioningService _verService;
        private readonly IILADomainService _ilaService;
        private readonly IRRDomainService _rrService;
        private readonly ISHDomainService _shService;
        private readonly IEODomainService _eoService;
        private readonly Interfaces.Services.Shared.IVersion_EnablingObjectiveService _ver_eoService;
        private readonly ITask_TGDomainService _task_tgService;
        private readonly ITrainingGroupDomainService _tgService;
        private readonly IToolDomainService _tool_Service;
        private readonly IPosDomainService _posService;
        private readonly Interfaces.Services.Shared.IVersion_PositionService _ver_pos_AppService;
        private readonly Interfaces.Services.Shared.ITask_HistoryService _historyService;

        public Version_TaskService(IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<Version_TaskService> localizer,
            IVersionTaskDomainService versionTaskService,
            UserManager<AppUser> userManager,
            ITaskService taskService,
            IVersionTask_StepDomainService ver_task_stepService,
            IVersionTask_QuestionDomainService ver_task_quesService,
            IVersionTask_ProcedureLinkDomainService ver_task_procService,
            IVersionILADomainService ver_task_ilaService,
            IVersionRRDomainService ver_task_rrService,
            IVersionEODomainService ver_task_eoService,
            IVersionSHDomainService ver_task_shService,
            IVersionToolDomainService ver_task_toolService,
            IVersion_TrainingGroupDomainService ver_tgService,
            IVersionTask_SuggestionDomainService ver_task_suggService,
            IVersionPositionsDomainService ver_posService,
            ITask_HistoryDomainService task_hist_service,
            IPersonDomainService person_Serivce,
            ISubdutyAreaService subdutyAreaService,
            ITaskStepDomainService task_stepService,
            ITaskQuestionDomainService task_qestionService,
            IProcedureTaskLinkDomainService task_proc_linkService,
            IILATaskLinkDomainService ila_task_linkService,
            IRRTaskLinkDomainService rr_task_linkService,
            ITaskEOLinkDomainService task_eo_linkService,
            ISafetyHazardTaskDomainService sh_task_linkService,
            ITaskSuggesDomainService task_suggService,
            ITaskToolDomainService task_toolService,
            IPosTaskDomainService pos_task_linkService,
            ITaskMetaLinkDomainService task_metaService,
            IProcedureDomainService procedureService,
            IVersioningService verService,
            IILADomainService ilaService,
            IRRDomainService rrService,
            ISHDomainService shService,
            IEODomainService eoService,
            ITask_TGDomainService task_tgService,
            ITrainingGroupDomainService tgService,
            IToolDomainService tool_Service,
            IPosDomainService posService,
            Interfaces.Services.Shared.IVersion_PositionService ver_pos_AppService,
            Interfaces.Services.Shared.IVersion_EnablingObjectiveService ver_eoService,
            Interfaces.Services.Shared.ITask_HistoryService historyService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _versionTaskService = versionTaskService;
            _userManager = userManager;
            _taskService = taskService;
            _task = new Domain.Entities.Core.Task();
            _ver_task_stepService = ver_task_stepService;
            _ver_task_quesService = ver_task_quesService;
            _ver_task_procService = ver_task_procService;
            _ver_task_ilaService = ver_task_ilaService;
            _ver_task_rrService = ver_task_rrService;
            _ver_task_eoService = ver_task_eoService;
            _ver_task_shService = ver_task_shService;
            _ver_task_toolService = ver_task_toolService;
            _ver_tgService = ver_tgService;
            _ver_task_suggService = ver_task_suggService;
            _ver_posService = ver_posService;
            _ver_task = new Version_Task();
            _task_hist_service = task_hist_service;
            _person_Serivce = person_Serivce;
            _subdutyAreaService = subdutyAreaService;
            _subdutyAreaService = subdutyAreaService;
            _task_stepService = task_stepService;
            _task_qestionService = task_qestionService;
            _proc_task_linkService = task_proc_linkService;
            _ila_task_linkService = ila_task_linkService;
            _rr_task_linkService = rr_task_linkService;
            _task_eo_linkService = task_eo_linkService;
            _sh_task_linkService = sh_task_linkService;
            _task_suggService = task_suggService;
            _task_toolService = task_toolService;
            _pos_task_linkService = pos_task_linkService;
            _task_metaService = task_metaService;
            _procedureService = procedureService;
            _verService = verService;
            _ilaService = ilaService;
            _rrService = rrService;
            _shService = shService;
            _eoService = eoService;
            _task_tgService = task_tgService;
            _tgService = tgService;
            _tool_Service = tool_Service;
            _posService = posService;
            _ver_pos_AppService = ver_pos_AppService;
            _ver_eoService = ver_eoService;
            _historyService = historyService;
        }


        //public async Task<Version_Task> CreateAsync(Version_EnablingObjective_StepCreateOptions options)
        //{
        //    var obj = (await _versionTaskService.FindAsync(x => x.TaskId == options.TaskId && x.TaskNumber == options.TaskNumber)).FirstOrDefault();
        //    var task = await _taskService.GetAsync(options.TaskId);
        //    string versionNumber = "";
        //    if (obj == null)
        //    {
        //        if(task != null)
        //        {
        //            versionNumber = "1.0";
        //            obj = new Version_Task(task, false, versionNumber);
        //        }
        //        else
        //        {
        //            throw new BadHttpRequestException(message: _localizer["TaskNotFound"].Value);
        //        }
        //    }
        //    else
        //    {
        //        if (task != null)
        //        {
        //            Double verNumber = Convert.ToDouble(obj.VersionNumber);
        //            verNumber += 1;
        //            versionNumber = verNumber.ToString() + ".0";
        //            obj = new Version_Task(task, false, versionNumber);
        //        }
        //        else
        //        {
        //            throw new BadHttpRequestException(message: _localizer["TaskNotFound"].Value);
        //        }
        //    }

        //    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Create);
        //    if (result.Succeeded)
        //    {
        //        obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
        //        obj.CreatedDate = DateTime.Now;
        //        var validationResult = await _versionTaskService.AddAsync(obj);
        //        if (!validationResult.IsValid)
        //        {
        //            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
        //        }
        //        else
        //        {
        //            return obj;
        //        }
        //    }
        //    else
        //    {
        //        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
        //    }
        //}

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _versionTaskService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<List<Version_Task>> GetAsync()
        {
            var obj_list = await _versionTaskService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<Version_Task> GetAsync(int id)
        {
            var obj = await _versionTaskService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Read);
                if (result.Succeeded)
                {
                    return obj;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async Task<Version_Task> UpdateAsync(int id, Version_TaskUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Update);

            if (result.Succeeded)
            {
                obj.TaskId = options.TaskId;
                obj.VersionNumber = options.VersionNumber;
                obj.TaskNumber = options.TaskNumber;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                var validationresult = await _versionTaskService.UpdateAsync(obj);
                if (!validationresult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationresult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }
        public async Task<Version_Task> UpdateVersionTaskRequalificationInfoAsync(int id, Version_TaskRequalificationInfo options)
        {

            var obj = await _versionTaskService.FindQuery(x => x.Id== options.VersionId).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Update);

            if (result.Succeeded)
            {
                obj.RequalificationNotes = options.RequalificationNotes;
                obj.RequalificationRequired = options.RequalificationRequired;
                obj.RequalificationDueDate = options.RequalificationDueDate; 
                obj.EffectiveDate = options.EffectiveDate;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                if (obj.IsInUse)
                {
                    var taskobj = await _taskService.GetAsync(obj.TaskId);
                    taskobj.RequalificationDueDate = options.RequalificationDueDate;
                    taskobj.RequalificationNotes = options.RequalificationNotes;
                    taskobj.RequalificationRequired = options.RequalificationRequired;
                    taskobj.EffectiveDate = options.EffectiveDate;
                    var taskValidationResult = await _taskService.UpdateAsync(taskobj);
                    if (!taskValidationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', taskValidationResult.Errors));
                    }
                }
                var validationresult = await _versionTaskService.UpdateAsync(obj);
                if (!validationresult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationresult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<Version_Task> CreateTaskVersion(Domain.Entities.Core.Task task, int state, bool useCurrentUTCEffectiveDate = true)
        {
            var verTask = task.CreateSnapshot(state);
            //Version_Task verTask = new Version_Task(task, false, "", state);
            var version = await _versionTaskService.FindQuery(x => x.TaskId == task.Id).OrderBy(o => o.Id).LastOrDefaultAsync();
            if (version == null)
            {
                verTask.VersionNumber = "1.0";
                verTask.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                verTask.CreatedDate = DateTime.Now;
                verTask.EffectiveDate = useCurrentUTCEffectiveDate ? DateOnly.FromDateTime(DateTime.UtcNow) :task.EffectiveDate;
                verTask.IsInUse = true;
                await _versionTaskService.AddAsync(verTask);
            }
            else
            {
                var inUseVersion = await _versionTaskService.FindQuery(x => x.IsInUse && x.TaskId == task.Id).FirstOrDefaultAsync();
                if (inUseVersion == null)
                {
                    var totalVersions = await _versionTaskService.FindQuery(x => x.TaskId == task.Id).OrderBy(o => o.VersionNumber).ToListAsync();
                    var greaterVersion = new Version_Task();
                    foreach(var totalVersion in totalVersions)
                    {
                        if(greaterVersion == null || greaterVersion?.Id == null || greaterVersion?.Id == 0)
                        {
                            greaterVersion = totalVersion;
                        }
                        else
                        {
                            if (greaterVersion.VersionNumber == null || totalVersion.VersionNumber == null)
                            {
                                if(greaterVersion.Id < totalVersion.Id)
                                {
                                    greaterVersion = totalVersion;
                                }
                            }
                            else
                            {
                                if (int.Parse(greaterVersion.VersionNumber.ToString().Split(".")[0]) < int.Parse(totalVersion.VersionNumber.ToString().Split(".")[0]))
                                {
                                    greaterVersion = totalVersion;
                                }
                            }
                        }
                    }
                    if(greaterVersion != null && greaterVersion.Id != 0)
                    {
                        greaterVersion.IsInUse = true;
                        await _versionTaskService.UpdateAsync(greaterVersion);
                        inUseVersion = greaterVersion;
                    }
                }
                //inUseVersion = await _versionTaskService.FindQuery(x => x.IsInUse && x.TaskId == task.Id).FirstOrDefaultAsync();
                if(inUseVersion != null && inUseVersion.IsInUse != false)
                {
                    inUseVersion.IsInUse = false;
                    await _versionTaskService.UpdateAsync(inUseVersion);

                    //var histOptions = new Task_HistoryOptions();
                    //histOptions.EffectiveDate = System.DateTime.Now.Date;
                    //histOptions.TaskIds = new int[] { inUseVersion.TaskId };

                    //histOptions.ChangeNotes = "Removed From In Use";
                    //histOptions.EffectiveDate = System.DateTime.Now.Date;
                    //histOptions.Version_TaskId = inUseVersion.Id;
                    //await _historyService.SaveHistoryAsync(histOptions);
                }
                var num = (double)(await _versionTaskService.FindQuery(x => x.TaskId == task.Id).OrderBy(o => o.VersionNumber).CountAsync());
                if(version.VersionNumber != null)
                {
                    num = Double.Parse(version.VersionNumber);
                }
                num += 1;
                verTask.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                verTask.ModifiedDate = DateTime.Now;
                verTask.VersionNumber = num.ToString() + ".0";
                verTask.EffectiveDate = useCurrentUTCEffectiveDate? DateOnly.FromDateTime(DateTime.UtcNow) : task.EffectiveDate; 
                await _versionTaskService.AddAsync(verTask);
            }
            return verTask;
        }

        public async Task<Version_Task> VersionAndCreateCopy(Domain.Entities.Core.Task task, int state = 2, bool useCurrentUTCEffectiveDate = true)
        {
            var copy = await _taskService.FindQuery(x => x.Id == task.Id, true).FirstOrDefaultAsync();

            var version = await CreateTaskVersion(task, state, useCurrentUTCEffectiveDate);

            // Copy Steps
            var steps = await _task_stepService.FindQuery(x => x.TaskId == copy.Id,true).ToListAsync();
            foreach (var copySteps in steps)
            {
                //var verStep = await _ver_task_stepService.FindQuery(x => x.TaskStepId == copySteps.Id).OrderBy(o => o.Id).LastOrDefaultAsync();

                version.Version_Task_Steps.Add(new Version_Task_Step(version, copySteps));
            }

            // Copy Questions
            var ques = await _task_qestionService.FindQuery(x => x.TaskId == copy.Id, true).ToListAsync();
            foreach (var copyQues in ques)
            {
                //var verQues = await _ver_task_quesService.FindQuery(x => x.TaskQuestionId == copyQues.Id).OrderBy(o => o.Id).LastOrDefaultAsync();
                version.Version_Task_Questions.Add(new Version_Task_Question(version, copyQues));
            }

            // Copy Procedures Links
            var procs = await _proc_task_linkService.FindQuery(x => x.TaskId == copy.Id, true).ToListAsync();
            foreach (var copyProc in procs)
            {
                var verProc = await _ver_task_procService.FindQuery(x => x.ProcedureId == copyProc.ProcedureId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                if (verProc != null)
                {
                    version.Version_Task_Procedure_Links.Add(new Version_Task_Procedure_Link(version.Id, verProc.Id));
                }
                else
                {
                    var proc = await _procedureService.FindQuery(x => x.Id == copyProc.ProcedureId).FirstOrDefaultAsync();
                    if(proc != null)
                    {
                        var newProcVer = await _verService.VersionProcedureAsync(proc);
                        version.Version_Task_Procedure_Links.Add(new Version_Task_Procedure_Link(version.Id, newProcVer.Id));
                    }
                }
            }

            // Copy ILA Links
            var ilas = await _ila_task_linkService.FindQuery(x => x.TaskId == copy.Id, true).ToListAsync();
            foreach (var copyIla in ilas)
            {
                var verIla = await _ver_task_ilaService.FindQuery(x => x.ILAId == copyIla.ILAId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                if (verIla != null)
                {
                    version.Version_Task_ILA_Links.Add(new Version_Task_ILA_Link(version.Id, verIla.Id, "1.0"));
                }
                else
                {
                    var ila = await _ilaService.FindQuery(x => x.Id == copyIla.ILAId).FirstOrDefaultAsync();
                    if(ila != null)
                    {
                        var newILAVer = await _verService.VersionILAAsync(ila,"ILA Added",DateTime.Now,1);
                        version.Version_Task_ILA_Links.Add(new Version_Task_ILA_Link(version.Id, newILAVer.Id, "1.0"));
                    }
                }
            }

            // Copy RR Links
            var rrs = await _rr_task_linkService.FindQuery(x => x.TaskId == copy.Id, true).ToListAsync();
            foreach (var copyRR in rrs)
            {
                var verRR = await _ver_task_rrService.FindQuery(x => x.RegulatoryRequirementId == copyRR.RegRequirementId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                if (verRR != null)
                {
                    version.Version_Task_RR_Links.Add(new Version_Task_RR_Link(version.Id, verRR.Id));
                }
                else
                {
                    var rr = await _rrService.FindQuery(x => x.Id == copyRR.RegRequirementId).FirstOrDefaultAsync();
                    if(rr != null)
                    {
                        var newRRVer = await _verService.VersionRRAsync(rr);
                        version.Version_Task_RR_Links.Add(new Version_Task_RR_Link(version.Id, newRRVer.Id));
                    }
                }
            }

            // Copy EO Links
            var eos = await _task_eo_linkService.FindQuery(x => x.TaskId == copy.Id, true).ToListAsync();
            foreach (var copyEO in eos)
            {
                var verEO = await _ver_task_eoService.FindQuery(x => x.EnablingObjectiveId == copyEO.EnablingObjectiveId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                if (verEO != null)
                {
                    version.Version_Task_EnablingObjective_Links.Add(new Version_Task_EnablingObjective_Link(verEO.Id, version.Id, "1.0"));
                }
                else
                {
                    var eo = await _eoService.FindQuery(x => x.Id == copyEO.EnablingObjectiveId).FirstOrDefaultAsync();
                    if(eo != null)
                    {
                        var newEOVer = await _ver_eoService.VersionAndCreateCopy(eo,1);
                        version.Version_Task_EnablingObjective_Links.Add(new Version_Task_EnablingObjective_Link(newEOVer.Id, version.Id, "1.0"));
                    }
                }
            }

            // Copy SH Links
            var shs = await _sh_task_linkService.FindQuery(x => x.TaskId == copy.Id, true).ToListAsync();
            foreach (var copySH in shs)
            {
                var verSH = await _ver_task_shService.FindQuery(x => x.SaftyHazardId == copySH.SaftyHazardId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                if (verSH != null)
                {
                    version.Version_Task_SaftyHazard_Links.Add(new Version_Task_SaftyHazard_Link(version.Id, verSH.Id, "1.0"));
                }
                else
                {
                    var sh = await _shService.FindQuery(x => x.Id == copySH.SaftyHazardId).FirstOrDefaultAsync();
                    if(sh != null)
                    {
                        var newSHVer = await _verService.VersionSaftyHazardAsync(sh);
                        version.Version_Task_SaftyHazard_Links.Add(new Version_Task_SaftyHazard_Link(version.Id, newSHVer.Id, "1.0"));
                    }
                }
            }

            // Copy Tools Links
            var tools = await _task_toolService.FindQuery(x => x.TaskId == copy.Id, true).ToListAsync();
            foreach (var copyTool in tools)
            {
                var verTool = await _ver_task_toolService.FindQuery(x => x.ToolId == copyTool.ToolId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                if (verTool != null)
                {
                    version.Version_Task_Tool_Links.Add(new Version_Task_Tool_Link(version.Id, verTool.Id));
                }
                else
                {
                    var tool = await _tool_Service.FindQuery(x => x.Id == copyTool.ToolId).FirstOrDefaultAsync();
                    if(tool != null)
                    {
                        var newToolVer = await _verService.VersionToolAsync(tool);
                        version.Version_Task_Tool_Links.Add(new Version_Task_Tool_Link(version.Id, newToolVer.Id));
                    }
                }
            }

            // Copy Suggestions
            var suggs = await _task_suggService.FindQuery(x => x.TaskId == copy.Id, true).ToListAsync();
            foreach (var copySugg in suggs)
            {
                //var verSugg = await _ver_task_suggService.FindQuery(x => x.Task_SuggestionId == copySugg.Id).OrderBy(o => o.Id).LastOrDefaultAsync();
                version.Version_Task_Suggestions.Add(new Version_Task_Suggestion(version, copySugg));
            }

            // Copy Training Groups We dont have any panel to add/create groups so we need that for versioning
            var tgs = await _task_tgService.FindQuery(x => x.TaskId == copy.Id, true).ToListAsync();
            foreach (var copyTG in tgs)
            {
                var verTG = await _ver_tgService.FindQuery(x => x.Version_TrainingGroupId == copyTG.TrainingGroupId).OrderBy(o => o.Id).LastOrDefaultAsync();
                if(verTG != null)
                {
                    version.Version_Task_TrainingGroups.Add(new Version_Task_TrainingGroup(version.Id, verTG.Id));
                }
                else
                {
                    var tg = await _tgService.FindQuery(x => x.Id == copyTG.TrainingGroupId).FirstOrDefaultAsync();
                    if(tg != null)
                    {
                        var newTGVer = await _verService.VersionTrainingGroupAsync(tg);
                        version.Version_Task_TrainingGroups.Add(new Version_Task_TrainingGroup(version.Id, newTGVer.Id));
                    }
                }
            }

            // Copy Positions
            var poss = await _pos_task_linkService.FindQuery(x => x.TaskId == copy.Id, true).ToListAsync();
            foreach (var copyPos in poss)
            {
                var verPos = await _ver_posService.FindQuery(x => x.PositionId == copyPos.PositionId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                if (verPos != null)
                {
                    version.Version_Task_Position_Links.Add(new Version_Task_Position_Link(version.Id, verPos.Id));
                }
                else
                {
                    var pos = await _posService.FindQuery(x => x.Id == copyPos.PositionId).FirstOrDefaultAsync();
                    if(pos != null)
                    {
                        var newPosVer = await _ver_pos_AppService.CreateVersionAsync(pos);
                        version.Version_Task_Position_Links.Add(new Version_Task_Position_Link(version.Id, newPosVer.Id));
                    }
                }
            }

            // Copy Meta Task Links
            var metas = await _task_metaService.FindQuery(x => x.Meta_TaskId == copy.Id, true).ToListAsync();
            foreach (var copymeta in metas)
            {
                var verTask = await _versionTaskService.FindQuery(x => x.TaskId == copymeta.TaskId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                if (verTask != null)
                {
                    version.Version_Task_MetaTask_Links.Add(new Version_Task_MetaTask_Link(verTask, version));
                }
                else
                {
                    var mTask = await _taskService.FindQuery(x => x.Id == copymeta.TaskId).FirstOrDefaultAsync();
                    if(mTask != null)
                    {
                        var newVerTask = await CreateTaskVersion(mTask, 1, useCurrentUTCEffectiveDate);
                        version.Version_Task_MetaTask_Links.Add(new Version_Task_MetaTask_Link(newVerTask, version));
                    }
                }
            }
            await _versionTaskService.UpdateAsync(version);
            return version;
        }

        public async Task<List<Version_Task>> GetTaskVersionsWithHistoryAsync(int taskId)
        {
            //var tasks = await _taskService.AllQuery().ToListAsync();
           // var history = await _task_hist_service.AllQuery().ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            var persons = await _person_Serivce.AllQuery().ToListAsync();
            var versions = await _versionTaskService.FindQueryWithIncludeAsync(x => x.TaskId == taskId, new string[] { nameof(_ver_task.Task_Histories) }).ToListAsync();
            versions = versions.Where(version => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, version, Version_TaskOperations.Read).Result.Succeeded).ToList();
            //versions = (from v in versions
            //           join h in history on v.Id equals h.Version_TaskId
            //           join u in _userManager.Users on h.CreatedBy equals u.Id
            //           join p in persons on u.UserName equals p.Username
            //           select v).ToList();
            //if(!versions.Any(x => x.IsInUse == true))
            //{
            //    var greaterVersion = versions.OrderByDescending(o => o.Id).LastOrDefault();
            //    greaterVersion.IsInUse = true;
            //    await _versionTaskService.UpdateAsync(greaterVersion);
            //}
            foreach (var version in versions)
            {
                if (version.ModifiedDate == null)
                {
                    var user = users.Where(x => x.Id == version.CreatedBy).Select(s => s.UserName).FirstOrDefault();
                    version.CreatedBy = persons.Where(x => x.Username == user).Select(s => s.FirstName).FirstOrDefault();
                }
                else
                {
                    var user = users.Where(x => x.Id == version.ModifiedBy).Select(s => s.UserName).FirstOrDefault();
                    version.CreatedBy = persons.Where(x => x.Username == user).Select(s => s.FirstName).FirstOrDefault();
                }
                foreach (var hist in version.Task_Histories)
                {
                    if (hist.ModifiedBy == null)
                    {
                        var user = users.Where(x => x.Id == hist.CreatedBy).Select(s => s.UserName).FirstOrDefault();
                        hist.CreatedBy = persons.Where(x => x.Username == user).Select(s => s.FirstName).FirstOrDefault();
                    }
                    else
                    {
                        var user = users.Where(x => x.Id == hist.ModifiedBy).Select(s => s.UserName).FirstOrDefault();
                        hist.CreatedBy = persons.Where(x => x.Username == user).Select(s => s.FirstName).FirstOrDefault();
                    }
                }
            }
            return versions.OrderByDescending(o => o.Id).ToList();
        }
    }
}
