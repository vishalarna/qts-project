using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QTD2.Application.Utils;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.Procedure_EnablingObjective_Link;
using QTD2.Infrastructure.Model.Procedure_ILA_Link;
using QTD2.Infrastructure.Model.Procedure_IssuingAuthority;
using QTD2.Infrastructure.Model.Procedure_RegRequirement_Link;
using QTD2.Infrastructure.Model.Procedure_SaftyHazard_Link;
using QTD2.Infrastructure.Model.Procedure_StatusHistory;
using QTD2.Infrastructure.Model.Procedure_Task_Link;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.Task;
using IILA_TopicDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TopicService;
using IProcedure_EO_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_EnablingObjective_LinkService;
using IProcedure_ILA_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_ILA_LinkService;
using IProcedure_IssuingAuthority_HistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IProc_IssuingAuthority_HistoryService;
using IProcedure_RR_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_RR_LinkService;
using IProcedure_SaftyHazard_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_SaftyHazard_LinkService;
using IProcedure_StatusHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_StatusHistoryService;
using IProcedure_Task_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_Task_LinkService;
using IProcedureHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_StatusHistoryService;
using IProviderDomainService = QTD2.Domain.Interfaces.Service.Core.IProviderService;
using IRegulatoryRequirementDomainService = QTD2.Domain.Interfaces.Service.Core.IRegulatoryRequirementService;
using IProcedureReviewDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedureReviewService;
using IDutyAreaDomainService = QTD2.Domain.Interfaces.Service.Core.IDutyAreaService;
using ISubDutyAreaDomainService = QTD2.Domain.Interfaces.Service.Core.ISubdutyAreaService;
using IILA_Procedure_LinksDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_Procedure_LinkService;
using DocumentFormat.OpenXml.Bibliography;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.ILA_Topic;
using RazorEngine.Compilation.ImpromptuInterface.InvokeExt;
using QTD2.Infrastructure.Model.Provider;

namespace QTD2.Application.Services.Shared
{
    public class ProcedureService : Interfaces.Services.Shared.IProcedureService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IProcedureService _procedureService;
        private readonly IProcedure_IssuingAuthorityService _issuingAuthorityService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly ISaftyHazardService _saftyHazardService;
        private readonly IStringLocalizer<ProcedureService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly Procedure _procedure;
        private readonly Procedure_IssuingAuthority _issuingAuthority;
        private readonly ITaskService _taskService;
        private readonly IILAService _ilaService;
        private readonly IProcedure_Task_LinkDomainService _procTaskService;
        private readonly IProcedure_ILA_LinkDomainService _procILAService;
        private readonly Procedure_Task_Link _proc_Task_Link;
        private readonly Procedure_ILA_Link _proc_ILA_Link;
        private readonly IProcedure_StatusHistoryDomainService _procHistService;
        private readonly IProcedure_SaftyHazard_LinkDomainService _procShLinkService;
        private readonly IProviderService _providerService;
        private readonly IILA_TopicDomainService _topicService;
        private readonly IProcedureHistoryDomainService _procHistorySrvc;
        private readonly Procedure_SaftyHazard_Link _procShLink;
        private readonly IRegulatoryRequirementDomainService _rrService;
        private readonly IProcedure_RR_LinkDomainService _procRRLinkService;
        private readonly IProcedure_ILA_LinkDomainService _procILALinkService;
        private readonly Procedure_RR_Link _proc_rr_link;
        private readonly Provider _provider;
        private readonly ILA_Topic _topics;
        private readonly Procedure_IssuingAuthority _proc_ia;
        private readonly Procedure_EnablingObjective_Link _procEOLink;
        private readonly IProcedure_IssuingAuthority_HistoryDomainService _proc_ia_histService;
        private readonly Interfaces.Services.Shared.IProc_IssuingAuthority_HistoryService _procIA_HistorySrvc;
        private readonly IProcedure_EO_LinkDomainService _procEoLinkService;
        private readonly Domain.Entities.Core.Task _task;
        private readonly Interfaces.Services.Shared.IVersion_TaskService _versionTaskService;
        private readonly IProcedureReviewDomainService _procedureReviewService;
        private readonly IDutyAreaDomainService _daService;
        private readonly ISubDutyAreaDomainService _sdaService;
        private readonly IILA_Procedure_LinksDomainService _ilaProcedureLinkService;
        private readonly ILA_Procedure_Link _iLA_Proc_Link;

        public ProcedureService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IProcedureService procedureService,
            IProcedure_IssuingAuthorityService issuingAuthorityService,
            IEnablingObjectiveService enablingObjectiveService,
            ISaftyHazardService saftyHazardService,
            IStringLocalizer<ProcedureService> localizer,
            UserManager<AppUser> userManager,
            ITaskService taskService,
            IProcedure_Task_LinkDomainService procTaskService,
            IProcedure_StatusHistoryDomainService procHistService,
            IProcedure_SaftyHazard_LinkDomainService procShLinkService,
            IRegulatoryRequirementDomainService rrService,
            IProcedure_RR_LinkDomainService procRRLinkService,
            IILAService ilaService,
            IProcedure_ILA_LinkDomainService procILAService,
            IILA_TopicDomainService topicService,
            IProviderDomainService providerService,
            IProcedure_EO_LinkDomainService procEOLinkService,
            IProcedure_ILA_LinkDomainService procILALinkService,
            IProcedureHistoryDomainService procHistorySrvc,
            IProcedure_IssuingAuthority_HistoryDomainService proc_ia_histService,
            Interfaces.Services.Shared.IProc_IssuingAuthority_HistoryService procIA_HistorySrvc,
            IProcedureReviewDomainService procedureReviewService,
            Interfaces.Services.Shared.IVersion_TaskService versionTaskService,
            IDutyAreaDomainService daService,
            ISubDutyAreaDomainService sdaService,
            IILA_Procedure_LinksDomainService ilaProcedureLinkService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _procedureService = procedureService;
            _issuingAuthorityService = issuingAuthorityService;
            _enablingObjectiveService = enablingObjectiveService;
            _saftyHazardService = saftyHazardService;
            _localizer = localizer;
            _userManager = userManager;
            _procedure = new Procedure();
            _issuingAuthority = new Procedure_IssuingAuthority();
            _taskService = taskService;
            _procTaskService = procTaskService;
            _proc_Task_Link = new Procedure_Task_Link();
            _proc_ILA_Link = new Procedure_ILA_Link();
            _procHistService = procHistService;
            _procShLinkService = procShLinkService;
            _procShLink = new Procedure_SaftyHazard_Link();
            _procEOLink = new Procedure_EnablingObjective_Link();
            _rrService = rrService;
            _procRRLinkService = procRRLinkService;
            _proc_rr_link = new Procedure_RR_Link();
            _proc_ia = new Procedure_IssuingAuthority();
            _provider = new Provider();
            _topics = new ILA_Topic();
            _ilaService = ilaService;
            _procILAService = procILAService;
            _topicService = topicService;
            _providerService = providerService;
            _procILALinkService = procILALinkService;
            _procHistorySrvc = procHistorySrvc;
            _proc_ia_histService = proc_ia_histService;
            _procIA_HistorySrvc = procIA_HistorySrvc;
            _procEoLinkService = procEOLinkService;
            _task = new Domain.Entities.Core.Task();
            _procedureReviewService = procedureReviewService;
            _versionTaskService = versionTaskService;
            _daService = daService;
            _sdaService = sdaService;
            _ilaProcedureLinkService = ilaProcedureLinkService;
        }

        public async Task<Procedure> CreateAsync(ProcedureCreateOptions options)
        {
            var procedure = (await _procedureService.FindAsync(r => r.Number.Trim().ToLower() == options.Number.Trim().ToLower())).FirstOrDefault();
            if (procedure != null)
            {
                throw new QTDServerException(_localizer["Procedure Information Already Exists"]);
            }

            var data = options.File;
            var byteData = new byte[] { };
            if (data != null && data.Contains(","))
            {
                data = data.Substring(data.IndexOf(",") + 1);
                byteData = Convert.FromBase64String(data);
            }

            procedure = new Procedure(options.IssuingAuthorityId, options.Number, options.Title, options.Description, options.RevisionNumber, DateOnly.FromDateTime(options.EffectiveDate), byteData, options.IsDeleted, options.IsPublished, options.Hyperlink, options.FileName);
            var proc_ia = await _issuingAuthorityService.FindQuery(x => x.Id == options.IssuingAuthorityId).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Create);
            if (result.Succeeded)
            {
                procedure.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                procedure.CreatedDate = DateTime.Now;
                proc_ia.AddProcedure(procedure);
                var validationResult = await _issuingAuthorityService.UpdateAsync(proc_ia);
                var histOptions = new Procedure_StatusHistory();
                histOptions.CreatedBy = procedure.CreatedBy;
                histOptions.CreatedDate = DateTime.Now;
                histOptions.ChangeEffectiveDate = options.EffectiveDate;
                histOptions.ChangeNotes = options.ChangeNotes;
                histOptions.ProcedureId = procedure.Id;
                var histValidation = await _procHistService.AddAsync(histOptions);
                if (!validationResult.IsValid || !histValidation.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return procedure;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<Procedure> CopyProcedureWithLinkages(int id, ProcedureCreateOptions options)
        {
            /*var procedureExists = (await _procedureService.FindAsync(r => r.Number.Trim().ToLower() == options.Number.Trim().ToLower() && r.IssuingAuthorityId == options.IssuingAuthorityId)).FirstOrDefault();
            if (procedureExists != null)
            {
                throw new BadHttpRequestException(message: _localizer["ProcedureExists"]);
            }*/
            var data = options.File;
            var byteData = new byte[] { };
            if (data != null)
            {
                /*data = data.Substring(data.IndexOf(",") + 1);
                byteData = Convert.FromBase64String(data); */
                byteData = Convert.FromBase64String(options.File);
            }

            else if (data != null && data.Contains(","))
            {
                data = data.Substring(data.IndexOf(",") + 1);
                byteData = Convert.FromBase64String(data);
            }



            var procedure = new Procedure(options.IssuingAuthorityId, options.Number, options.Title, options.Description, options.RevisionNumber, DateOnly.FromDateTime(options.EffectiveDate), byteData, options.IsDeleted, options.IsPublished, options.Hyperlink, options.FileName);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Create);
            if (result.Succeeded)
            {
                procedure.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                procedure.CreatedDate = DateTime.Now;

                var validationResult = await _procedureService.AddAsync(procedure);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    var linkToCopy = await _procedureService
                        .FindQueryWithIncludeAsync(x => x.Id == id,
                        new string[] {
                            nameof(_procedure.Procedure_Task_Links),
                            nameof(_procedure.Procedure_RegRequirement_Links),
                            nameof(_procedure.Procedure_SaftyHazard_Links),
                            nameof(_procedure.Procedure_EnablingObjective_Links),
                            nameof(_procedure.Procedure_ILA_Links),
                        }).FirstOrDefaultAsync();
                    procedure.Procedure_ILA_Links = linkToCopy.Procedure_ILA_Links.DeepCopy();
                    procedure.Procedure_ILA_Links.ToList().ForEach(x => x.Id = 0);
                    procedure.Procedure_EnablingObjective_Links = linkToCopy.Procedure_EnablingObjective_Links.DeepCopy();
                    procedure.Procedure_EnablingObjective_Links.ToList().ForEach(x => x.Id = 0);
                    procedure.Procedure_RegRequirement_Links = linkToCopy.Procedure_RegRequirement_Links.DeepCopy();
                    procedure.Procedure_RegRequirement_Links.ToList().ForEach(x => x.Id = 0);
                    procedure.Procedure_Task_Links = linkToCopy.Procedure_Task_Links.DeepCopy();
                    procedure.Procedure_Task_Links.ToList().ForEach(x => x.Id = 0);
                    procedure.Procedure_SaftyHazard_Links = linkToCopy.Procedure_SaftyHazard_Links.DeepCopy();
                    procedure.Procedure_SaftyHazard_Links.ToList().ForEach(x => x.Id = 0);
                    await _procedureService.UpdateAsync(procedure);
                    return procedure;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<Procedure_IssuingAuthority> CreateProcedure_IssuingAuthorityAsync(Procedure_IssuingAuthorityCreateOptions options)
        {
            var checkProcIA = await _issuingAuthorityService.FindQuery(x => x.Title.ToLower().Trim() == options.Title.ToLower().Trim()).FirstOrDefaultAsync();
            if (checkProcIA != null)
            {
                throw new QTDServerException(_localizer["Procedure IssuingAuthority Title Already Exists"]);
            }
            else
            {
                var proc_IA = new Procedure_IssuingAuthority(options.Description, options.Title, options.Website, DateOnly.FromDateTime(options.EffectiveDate), options.Notes, options.IsActive, options.IsDeleted);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, proc_IA, Procedure_IssuingAuthorityOperations.Create);
                if (result.Succeeded)
                {
                    proc_IA.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    proc_IA.CreatedDate = DateTime.Now;
                    var validationResult = await _issuingAuthorityService.AddAsync(proc_IA);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return proc_IA;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id, ProcedureOptions options)
        {
            foreach (var proc_id in options.procedureIds)
            {
                var procedure = await _procedureService.GetAsync(proc_id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Delete);
                if (result.Succeeded)
                {
                    procedure.Delete();
                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

        public async System.Threading.Tasks.Task DeactivateAsync(int id, ProcedureOptions options)
        {
            foreach (var proc_id in options.procedureIds)
            {
                var procedure = await _procedureService.FindQuery(x => x.Id == proc_id).FirstOrDefaultAsync();
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Delete);
                if (result.Succeeded)
                {
                    procedure.Deactivate();

                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

        public async System.Threading.Tasks.Task ActivateAsync(int id, ProcedureOptions options)
        {
            foreach (var proc_id in options.procedureIds)
            {
                var procedure = await _procedureService.GetAsync(proc_id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Delete);
                if (result.Succeeded)
                {
                    procedure.Activate();

                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

        public async Task<List<Procedure>> GetAsync()
        {
            var procList = (await _procedureService.AllWithIncludeAsync(new string[] { nameof(_procedure.Procedure_IssuingAuthority) })).Select(s => new Procedure
            {
                Id = s.Id,
                IsPublished = s.IsPublished,
                IssuingAuthorityId = s.IssuingAuthorityId,
                Number = s.Number,
                Title = s.Title,
                Description = s.Description,
                RevisionNumber = s.RevisionNumber,
                EffectiveDate = s.EffectiveDate,
                IsDeleted = s.IsDeleted,
                Hyperlink = s.Hyperlink,
                FileName = s.FileName,
                Active = s.Active,
                Deleted = s.Deleted
            }).Where(r => !r.Deleted);
            procList = procList.Where(proc => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, proc, ProcedureOperations.Read).Result.Succeeded);
            return procList?.ToList();
        }

        public async Task<Procedure> GetOnlyProc(int id)
        {
            var procedure = await _procedureService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (procedure != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Read);
                if (result.Succeeded)
                {
                    return procedure;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["ProcedureNotFound"]);
            }
        }

        public async Task<Procedure> GetAsync(int id)
        {
            var procedure = await _procedureService.GetWithIncludeAsync(id, new string[] { nameof(Procedure_IssuingAuthority) });
            if (procedure != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Read);
                if (result.Succeeded)
                {
                    return procedure;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["ProcedureNotFound"]);
            }
        }

        public async Task<List<Procedure_IssuingAuthority>> GetProcedure_IssuingAuthoritiesAsync()
        {
            var proc_IA = await _issuingAuthorityService.AllQuery().ToListAsync();
            for (int i = 0; i < proc_IA.Count; i++)
            {
                proc_IA[i].Procedures = await _procedureService.FindQuery(x => x.IssuingAuthorityId == proc_IA[i].Id).Select(s => new Procedure
                {
                    Id = s.Id,
                    IsPublished = s.IsPublished,
                    IssuingAuthorityId = s.IssuingAuthorityId,
                    Number = s.Number,
                    Title = s.Title,
                    Description = s.Description,
                    RevisionNumber = s.RevisionNumber,
                    EffectiveDate = s.EffectiveDate,
                    IsDeleted = s.IsDeleted,
                    Hyperlink = s.Hyperlink,
                    FileName = s.FileName,
                    Active = s.Active,
                    Deleted = s.Deleted
                }).OrderBy(x => x.Number).ToListAsync();
            }
            proc_IA = proc_IA.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Procedure_IssuingAuthorityOperations.Read).Result.Succeeded).OrderBy(x => x.Title).ToList();
            return proc_IA;
        }

        public async Task<Procedure_IssuingAuthority> GetProcedure_IssuingAuthorityAsync(int id)
        {
            var proc_IA = await _issuingAuthorityService.GetWithIncludeAsync(id, new string[] { nameof(_proc_ia.Procedures) });
            return proc_IA;
        }

        public async Task<Procedure> LinkEnablingObjectiveAsync(int procId, Procedure_EnablingObjective_LinkOptions options)
        {
            var procedure = await _procedureService.GetWithIncludeAsync(procId, new string[] { nameof(_procedure.Procedure_EnablingObjective_Links) });
            foreach (var id in options.EOIds)
            {
                var eo = await _enablingObjectiveService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Update);
                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, ILAOperations.Read);
                if (procResult.Succeeded && eoResult.Succeeded)
                {
                    procedure.LinkEnablingObjective(eo);
                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

            return procedure;
        }

        public async System.Threading.Tasks.Task UnlinkEnablingObjectiveAsync(int procId, int[] enablingObjectiveId)
        {
            var procedure = await _procedureService.GetWithIncludeAsync(procId, new string[] { nameof(_procedure.Procedure_EnablingObjective_Links) });
            foreach (var id in enablingObjectiveId)
            {
                var eo = await _enablingObjectiveService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Update);
                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, TaskOperations.Read);
                if (eoResult.Succeeded && procResult.Succeeded)
                {
                    procedure.UnlinkEnablingObjective(eo);
                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

        public async Task<Procedure> LinkTask(int procId, Procedure_Task_LinkCreateOptions options)
        {
            var procedure = await _procedureService.FindQuery(x => x.Id == procId).FirstOrDefaultAsync();
            foreach (var id in options.TaskIds)
            {
                var task = await _taskService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Update);
                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                if (procResult.Succeeded && taskResult.Succeeded)
                {
                    procedure.LinkTask(task);
                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

            return procedure;
        }

        public async System.Threading.Tasks.Task UnlinkTask(int procId, int[] taskId)
        {
            var procedure = await _procedureService.GetWithIncludeAsync(procId, new string[] { nameof(_procedure.Procedure_Task_Links) });
            foreach (var id in taskId)
            {
                var task = await _taskService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Update);
                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                if (taskResult.Succeeded && procResult.Succeeded)
                {
                    procedure.UnlinkTask(task);
                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

        public async Task<List<TaskWithCountOptions>> GetLinkedTasks(int id)
        {
            var links = await _procTaskService.FindWithIncludeAsync(x => x.ProcedureId == id, new string[] { nameof(_proc_Task_Link.Task) });
            List<Domain.Entities.Core.Task> taskList = new List<Domain.Entities.Core.Task>();
            taskList.AddRange(links.Select(x => x.Task));

            // return taskList;
            List<TaskWithCountOptions> taskWithCount = new List<TaskWithCountOptions>();
            foreach (var task in taskList)
            {
                var data = await _procTaskService.GetCount(x => x.TaskId == task.Id);
                //var taskNumber = await _taskService.FindQueryWithIncludeAsync(x => x.Id == task.Id, new string[] { "SubdutyArea.DutyArea" }).FirstOrDefaultAsync();
                var sda = await _sdaService.FindQuery(x => x.Id == task.SubdutyAreaId).FirstOrDefaultAsync();
                if (sda != null)
                {
                    var da = await _daService.FindQuery(x => x.Id == sda.DutyAreaId).FirstOrDefaultAsync();
                    if (da != null)
                    {
                        var num = da.Letter + da.Number.ToString() + '.' + sda.SubNumber.ToString() + '.' + task.Number.ToString();
                        taskWithCount.Add(new TaskWithCountOptions(num, task.Description, task.Id, data, task.Active, 0, task.IsReliability));
                    }
                }
            }

            return taskWithCount.OrderBy(o => o.Number.Split(".")[0][0]).ThenBy(t => t.Number.Split(".")[0][1]).ThenBy(t => t.Number.Split(".")[1]).ThenBy(t => t.Number.Split(".")[2]).ToList();
        }

        public async Task<List<ILAWithCountOptions>> GetLinkedILAs(int id)
        {
            var links = await _ilaProcedureLinkService.FindWithIncludeAsync(x => x.ProcedureId == id, new string[] {"ILA"});
            List<Domain.Entities.Core.ILA> ilaList = new List<Domain.Entities.Core.ILA>();
            ilaList.AddRange(links.Select(x => x.ILA));
            List<ILAWithCountOptions> iLAWithCount = new List<ILAWithCountOptions>();
            foreach (var ila in ilaList)
            {
                var data = await _ilaProcedureLinkService.GetCount(x => x.ILAId == ila.Id);
                iLAWithCount.Add(new ILAWithCountOptions(ila.Number, ila.Name, ila.Id, data, ila.Active));
            }

            return iLAWithCount;
        }

        public async Task<Procedure> LinkILA(int procId, Procedure_ILA_LinkCreateOptions options)
        {

            var procedure = await _procedureService.FindQuery(x => x.Id == procId).FirstOrDefaultAsync();
            foreach (var id in options.ILAIds)
            {
                var ila = await _ilaService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Update);
                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, ILAOperations.Read);
                if (procResult.Succeeded && ilaResult.Succeeded)
                {
                    //procedure.UnlinkIla(ila);
                    procedure.LinkProcedureToIla(ila);
                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

            return procedure;
        }

        public async System.Threading.Tasks.Task UnlinkILA(int procId, int[] ilaId)
        {
            var procedure = await _procedureService.GetWithIncludeAsync(procId, new string[] { "ILA_Procedure_Links" });
            foreach (var id in ilaId)
            {
                var ila = await _ilaService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Update);
                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, TaskOperations.Read);
                if (ilaResult.Succeeded && procResult.Succeeded)
                {
                    procedure.UnlinkIlaFromProcedure(ila);
                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

        public async Task<Procedure> LinkSaftyHazardAsync(int procId, Procedure_SaftyHazard_LinkOptions options)
        {
            var procedure = await _procedureService.FindQuery(x => x.Id == procId).FirstOrDefaultAsync();
            foreach (var id in options.SaftyHazardIds)
            {
                var saftHazard = await _saftyHazardService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Update);
                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftHazard, SaftyHazardOperations.Read);
                if (shResult.Succeeded && procResult.Succeeded)
                {
                    procedure.LinkSaftyHazard(saftHazard);
                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

            return procedure;
        }

        public async System.Threading.Tasks.Task UnlinkSaftyHazardAsync(int procId, Procedure_SaftyHazard_LinkOptions options)
        {
            var procedure = await _procedureService.GetWithIncludeAsync(procId, new string[] { nameof(_procedure.Procedure_SaftyHazard_Links) });
            foreach (var id in options.SaftyHazardIds)
            {
                var saftHazard = await _saftyHazardService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Update);
                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftHazard, SaftyHazardOperations.Read);
                if (shResult.Succeeded && procResult.Succeeded)
                {
                    procedure.UnlinkSaftyHazard(saftHazard);
                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

            var hist = new Procedure_StatusHistory();
            hist.OldStatus = true;
            hist.NewStatus = false;
            hist.ProcedureId = procId;
            hist.ChangeEffectiveDate = (DateTime)options.EffectiveDate;
            hist.ChangeNotes = options.ChangeNotes;
            hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            hist.CreatedDate = DateTime.Now;
            var histValidation = await _procHistService.AddAsync(hist);
            if (!histValidation.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', histValidation.Errors));
            }
        }

        public async Task<List<SafetyHazardWithLinkCount>> GetLinkedSaftyHazardsAsync(int id)
        {
            var proc = await _procShLinkService.AllQueryWithInclude(new string[] { nameof(_procShLink.SaftyHazard) }).Where(x => x.ProcedureId == id).Select(x => new { x.SaftyHazard.Title, x.SaftyHazard.Number, x.SaftyHazard.Id, x.SaftyHazard.Active }).ToListAsync();
            List<SafetyHazardWithLinkCount> shList = new List<SafetyHazardWithLinkCount>();
            foreach (var data in proc)
            {
                var count = await _procShLinkService.GetCount(x => x.SaftyHazardId == data.Id);
                var sh = new SafetyHazardWithLinkCount(data.Id, data.Title, data.Number, count, data.Active);
                shList.Add(sh);
            }

            return shList;
        }

        public async Task<List<EnablingObjectiveWithCountOptions>> GetLinkedEnablingObjectivesAsync(int id)
        {
            List<Domain.Entities.Core.EnablingObjective> enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesLinkedToProcedureAsync(id);
            List<EnablingObjectiveWithCountOptions> eoWithCount = new List<EnablingObjectiveWithCountOptions>();
            foreach (var eo in enablingObjectives)
            {
                var data = await _procEoLinkService.GetCount(x => x.EnablingObjectiveId == eo.Id);
                eoWithCount.Add(new EnablingObjectiveWithCountOptions(eo.FullNumber, eo.Description, eo.Id, data, eo.Active));
            }

            return eoWithCount;
        }

        public async Task<Procedure> UpdateAsync(int procId, ProcedureUpdateOptions options)
        {
            var procedure = await GetAsync(procId);
            if (procedure == null)
            {
                throw new QTDServerException(_localizer["ProcedureNotFound"]);
            }

            else
            {
                var duplicate = await _procedureService.GetProceduresByIDAndNumberAsync(procId, options.Number);
                if (duplicate != null)
                {
                    throw new QTDServerException(_localizer["Procedure Information Already Exists"]);
                }

                procedure.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                procedure.ModifiedDate = DateTime.Now;
                procedure.Title = options.Title;
                procedure.Description = options.Description;
                procedure.Number = options.Number;
                procedure.RevisionNumber = options.RevisionNumber;
                procedure.Title = options.Title;
                procedure.Description = options.Description;
                procedure.EffectiveDate = DateOnly.FromDateTime(options.EffectiveDate);
                procedure.IssuingAuthorityId = options.IssuingAuthorityId;
                procedure.Hyperlink = options.Hyperlink;
                procedure.FileName = options.FileName;

                var data = options.File;

                if (data != null && !IsBase64String(data))
                {
                    var byteData = new byte[] { };
                    if (data.Contains(","))
                    {
                        data = data.Substring(data.IndexOf(",") + 1);
                    }
                    byteData = Convert.FromBase64String(data);
                    procedure.ProceduresFileUpload = byteData;
                }
                else
                {
                    procedure.ProceduresFileUpload = null;
                }

                var histOptions = new Procedure_StatusHistory();
                histOptions.CreatedBy = procedure.CreatedBy;
                histOptions.CreatedDate = DateTime.Now;
                histOptions.ChangeEffectiveDate = options.EffectiveDate;
                histOptions.ChangeNotes = options.ChangeNotes;
                histOptions.ProcedureId = procedure.Id;
                var histValidation = await _procHistService.AddAsync(histOptions);
                var validationResult = await _procedureService.UpdateAsync(procedure);
                if (!validationResult.IsValid || !histValidation.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return procedure;
                }
                // }
            }
            /*else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }*/
        }

        private bool IsBase64String(string base64)
        {
            if (string.IsNullOrEmpty(base64)) return false;

            if (base64.Length % 4 != 0) return false;

            foreach (char c in base64)
            {
                if (!"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        public async System.Threading.Tasks.Task ActiveProcedure_IssuingAuthorityAsync(int id)
        {
            //var proc_issu = await _issuingAuthorityService.GetAsync(id);
            //if (proc_issu != null)
            //{
            //    proc_issu.Activate();
            //    await _issuingAuthorityService.UpdateAsync(proc_issu);
            //}
            //else
            //{
            //    throw new Exception(message: _localizer["Procedure_IssuingAuthorityNotFound"]);
            //}
            var proc_issu = await _issuingAuthorityService.GetWithIncludeAsync(id, new string[] { nameof(_proc_ia.Procedures) });
            List<Domain.Entities.Core.Procedure> procList = new List<Domain.Entities.Core.Procedure>();
            procList.AddRange(proc_issu.Procedures);
            if (proc_issu != null)
            {
                proc_issu.Activate();
                await _issuingAuthorityService.UpdateAsync(proc_issu);

                foreach (var proc in procList)
                {
                    var options = new ProcedureOptions();
                    options.ProcedureId = proc.Id;
                    options.ChangeEffectiveDate = DateTime.Now;
                    options.ChangeNotes = "Inactive Due to Issuing Authority Inactive";
                    await ProcedureActivateAsync(proc.Id, options);

                }
            }
            else
            {
                throw new QTDServerException(_localizer["Procedure_IssuingAuthorityNotFound"]);
            }
        }



        public async System.Threading.Tasks.Task ProcedureDeactivateAsync(int id, ProcedureOptions options)
        {
            var procedure = await _procedureService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Delete);
            if (result.Succeeded)
            {
                procedure.Deactivate();

                var validationResult = await _procedureService.UpdateAsync(procedure);
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

        public async System.Threading.Tasks.Task ProcedureActivateAsync(int id, ProcedureOptions options)
        {
            var procedure = await _procedureService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Delete);
            if (result.Succeeded)
            {
                procedure.Activate();

                var validationResult = await _procedureService.UpdateAsync(procedure);
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

        public async System.Threading.Tasks.Task InActiveProcedure_IssuingAuthorityAsync(int id)
        {
            var proc_issu = await _issuingAuthorityService.GetWithIncludeAsync(id, new string[] { nameof(_proc_ia.Procedures) });
            List<Domain.Entities.Core.Procedure> procList = new List<Domain.Entities.Core.Procedure>();
            procList.AddRange(proc_issu.Procedures);
            if (proc_issu != null)
            {
                proc_issu.Deactivate();
                await _issuingAuthorityService.UpdateAsync(proc_issu);

                foreach (var proc in procList)
                {
                    var options = new ProcedureOptions();
                    options.ProcedureId = proc.Id;
                    options.ChangeEffectiveDate = DateTime.Now;
                    options.ChangeNotes = "Inactive Due to Issuing Authority Inactive";
                    await ProcedureDeactivateAsync(proc.Id, options);

                }
            }
            else
            {
                throw new QTDServerException(_localizer["Procedure_IssuingAuthorityNotFound"]);
            }

        }

        public async System.Threading.Tasks.Task DeleteProcedure_IssuingAuthorityAsync(int id, Procedure_IssuingAuthorityOptions options)
        {
            var proc_issu = await _issuingAuthorityService.GetAsync(id);
            if (proc_issu != null)
            {
                proc_issu.Delete();
                await _issuingAuthorityService.UpdateAsync(proc_issu);
                //var hist = new Proc_IssuingAuthority_History();
                //hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                //hist.CreatedDate = DateTime.Now;
                //hist.ChangeEffectiveDate = (DateTime)options.ChangeEffectiveDate;
                //hist.ChangeNotes = options.ChangeNotes;
                //hist.ProcedureIssuingAuthorityId = id;
                //hist.OldStatus = true;
                //hist.NewStatus = false;
                //var histValidation = await _proc_ia_histService.AddAsync(hist);
                //if (!histValidation.IsValid)
                //{
                //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                //}
            }
            else
            {
                throw new QTDServerException(_localizer["Procedure_IssuingAuthorityNotFound"]);
            }
        }

        public async Task<Procedure_IssuingAuthority> UpdateProcedure_IssuingAuthorityAsync(int id, Procedure_IssuingAuthorityCreateOptions options)
        {
            var procIA = await _issuingAuthorityService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (procIA == null)
            {
                throw new QTDServerException(_localizer["ProcedureIssuingAuthorityNotFound"]);
            }
            else
            {
                // var existingIA = await _issuingAuthorityService.FindQuery(x => x.Title.Trim().ToLower() == options.Title.Trim().ToLower()).FirstOrDefaultAsync();
                // var existingIA = await _issuingAuthorityService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
                /* if (existingIA != null)
                 {
                     throw new Exception(message: _localizer["ProcedureIssuingAuthorityAlreadyExistsException"]);
                 }
                 else
                 {*/
                procIA.Title = options.Title;
                procIA.Description = options.Description;
                procIA.EffectiveDate = DateOnly.FromDateTime(options.EffectiveDate);
                procIA.Website = options.Website;
                procIA.Notes = options.Notes;
                var validationResult = await _issuingAuthorityService.UpdateAsync(procIA);
                var hist = new Proc_IssuingAuthority_History();
                hist.ProcedureIssuingAuthorityId = procIA.Id;
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                hist.ChangeNotes = options.Notes;
                hist.ChangeEffectiveDate = options.EffectiveDate;
                hist.NewStatus = true;
                hist.OldStatus = false;
                var histValidation = await _proc_ia_histService.AddAsync(hist);
                if (validationResult.IsValid && histValidation.IsValid)
                {
                    return procIA;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
                // }
            }
        }

        public async System.Threading.Tasks.Task StoreStatusHistoryAsync(Procedure_StatusHistoryCreateOptions options)
        {
            foreach (var proc_id in options.ProcedureIds)
            {
                var hist = new Procedure_StatusHistory();

                hist.NewStatus = options.NewStatus;
                hist.OldStatus = options.OldStatus;
                hist.ChangeEffectiveDate = options.ChangeEffectiveDate;
                hist.ChangeNotes = options.ChangeNotes;
                hist.CreatedDate = DateTime.Now;
                hist.ProcedureId = proc_id;
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                var validationResult = await _procHistService.AddAsync(hist);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
        }

        public async Task<Procedure> LinkRR(int procId, Procedure_RR_LinkOptions options)
        {
            var procedure = await _procedureService.FindQuery(x => x.Id == procId).FirstOrDefaultAsync();
            foreach (var id in options.RegulatoryRequirementIds)
            {
                var rr = await _rrService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Update);
                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Read);
                if (rrResult.Succeeded && procResult.Succeeded)
                {
                    procedure.LinkRegulatoryRequirement(rr);
                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

            return procedure;
        }

        public async System.Threading.Tasks.Task UnlinkRR(int procId, Procedure_RR_LinkOptions options)
        {
            var procedure = await _procedureService.GetWithIncludeAsync(procId, new string[] { nameof(_procedure.Procedure_RegRequirement_Links) });
            foreach (var id in options.RegulatoryRequirementIds)
            {
                var rr = await _rrService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Update);
                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Read);
                if (rrResult.Succeeded && procResult.Succeeded)
                {
                    procedure.UnlinkRegulatoryRequirement(rr);
                    var validationResult = await _procedureService.UpdateAsync(procedure);
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

        public async Task<List<RegulatoryRequirementWithLinkCount>> GetLinkedRR(int id)
        {
            var links = await _procRRLinkService.AllQueryWithInclude(new string[] { nameof(_proc_rr_link.RegulatoryRequirement) }).Where(x => x.ProcedureId == id).Select(x => new { x.RegulatoryRequirement.Id, x.RegulatoryRequirement.Title, x.RegulatoryRequirement.Number, x.RegulatoryRequirement.Active }).ToListAsync();
            List<RegulatoryRequirementWithLinkCount> rrList = new List<RegulatoryRequirementWithLinkCount>();
            foreach (var data in links)
            {
                var count = await _procRRLinkService.GetCount(x => x.RegulatoryRequirementId == data.Id);
                rrList.Add(new RegulatoryRequirementWithLinkCount(data.Number, data.Title, data.Id, count, data.Active));
            }

            return rrList;
        }

        public async Task<List<ILAProviderDataVM>> GetProviderWithILAs()
        {
            var providers = (await _providerService.GetCompactedProvider()).OrderBy(r => !r.IsPriority).ThenBy(r => r.Name).ToList();
            var ilas = await _ilaService.GetCompactedILA();
            foreach (var prov in providers)
            {
                prov.ILAs = ilas.Where(w => w.ProviderId == prov.Id).OrderBy(r => r.Name).ToList();
            }
            var listILAProviderVM = providers.Select(prov=>new ILAProviderDataVM(prov.Id,prov.Name,prov.Active,prov.ILAs.Select(m=>new ILADetailsVM(m.Id,m.Name,m.Number,m.Active)).ToList())
            ).ToList();
            return listILAProviderVM?.ToList();
        }

        public async Task<List<ILATopicDataVM>> GetTopicWithILAs()
        {
            var topicILAs = await _topicService.AllWithIncludeAsync(new string[] { "ILA_Topic_Links.ILA" });
            var listILAtopicVM = topicILAs.Select(topic => 
                                    new ILATopicDataVM(topic.Id,topic.Name,topic.Active,topic.ILA_Topic_Links.Select(ilaLink =>
                                    new ILADetailsVM(ilaLink.ILA.Id,ilaLink.ILA.Name,ilaLink.ILA.Number,ilaLink.ILA.Active)).ToList())
            ).ToList();

            return listILAtopicVM;
        }

        public async Task<ProcedureStatsVM> GetStatsCount()
        {
            var eoIds = await _procedureService.AllQuery().Where(s => s.Active == true).Select(x => x.Id).ToListAsync();
            var linkedTasks = await _procTaskService.AllQuery().Select(x => x.ProcedureId).ToListAsync();
            var linkedEOs = await _procEoLinkService.AllQuery().Select(x => x.ProcedureId).ToListAsync();
            var linkedSHs = await _procShLinkService.AllQuery().Select(x => x.ProcedureId).ToListAsync();
            var linkedRRs = await _procRRLinkService.AllQuery().Select(x => x.ProcedureId).ToListAsync();
            var linkedILAs = await _ilaProcedureLinkService.AllQuery().Select(x => x.ProcedureId).ToListAsync();

            var stats = new ProcedureStatsVM()
            {
                IAActive = await _issuingAuthorityService.GetCount(x => x.Active == true),
                IAInactive = await _issuingAuthorityService.GetCount(x => x.Active == false),
                ProcActive = await _procedureService.GetCount(x => x.Active == true),
                ProcInactive = await _procedureService.GetCount(x => x.Active == false),
                ProcNotLinkedToTasks = eoIds.Except(linkedTasks).Count(),
                ProcNotLinkedToEOs = eoIds.Except(linkedEOs).Count(),
                ProcNotLinkedToSHs = eoIds.Except(linkedSHs).Count(),
                ProcNotLinkedToRRs = eoIds.Except(linkedRRs).Count(),
                ProcNotLinkedToILAs = eoIds.Except(linkedILAs).Count(),
            };

            return stats;
        }

        public async Task<List<ProcedureLatestActivityVM>> GetHistoryAsync(bool getLatest)
        {
            var procedures = await _procedureService.AllQuery().IgnoreQueryFilters().ToListAsync();
            var history = await _procHistorySrvc.AllQuery().ToListAsync(); 
            var users = await _userManager.Users.ToListAsync();

            var latestactivity = from p in procedures
                                 join h in history on p.Id equals h.ProcedureId
                                 join u in _userManager.Users on h.CreatedBy equals u.Email orderby h.CreatedDate descending
                                 select new ProcedureLatestActivityVM
                                 {
                                     Id = p.Id,
                                     ActivityDesc = h.ChangeNotes,
                                     ProcedureTitle = p.Number + " - " + p.Title,
                                     CreatedBy = u.Email,
                                     CreatedDate = h.CreatedDate
                                 };

            if (getLatest)
            {
                latestactivity = latestactivity.Take(5).ToList(); 
            }

            return latestactivity.ToList();
        }

        public async Task<List<IssueAuthorityTreeVM>> GetNotLinked(string notLinkedWith)
        {
            List<int> linkedProcIds = new List<int>();
            List<int> notLinkedProcIds = new List<int>();

            switch (notLinkedWith.ToLower().Trim())
            {
                case "tasks":
                    {
                        notLinkedProcIds = await _procedureService.FindQuery(x => x.Procedure_Task_Links.Count == 0)
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }

                case "enabling objectives":
                    {
                        notLinkedProcIds = await _procedureService.FindQuery(x => x.Procedure_EnablingObjective_Links.Count == 0)
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }

                case "safety hazards":
                    {
                        notLinkedProcIds = await _procedureService.FindQuery(x => x.Procedure_SaftyHazard_Links.Count == 0)
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }

                case "regulatory requiremnents":
                    {
                        notLinkedProcIds = await _procedureService.FindQuery(x=>x.Procedure_RegRequirement_Links.Count==0)
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }

                case "ilas":
                    {
                        notLinkedProcIds = await _procedureService.FindQuery(x => x.ILA_Procedure_Links.Count==0)
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }
            }

            var notLinkedProcedures = await _issuingAuthorityService.FindQuery(x => x.Procedures.Any(p => notLinkedProcIds.Contains(p.Id) && p.Active)).
                Select(ia=> new IssueAuthorityTreeVM
                {
                    Active = ia.Active,
                    Id = ia.Id,
                    Title = ia.Title,
                    Procedures = ia.Procedures.Where(p => notLinkedProcIds.Contains(p.Id) && p.Active).Select(pp => new ProcedureSummaryVM
                    {
                        Active = pp.Active,
                        Id = pp.Id,
                        Title = pp.Title,
                        Number = pp.Number
                    }).ToList()
                }).ToListAsync();

            return notLinkedProcedures;
        }

        public async Task<List<Procedure>> GetSHLinkedProcedures(int id)
        {
            var data = await _procShLinkService.AllQueryWithInclude(new string[] { nameof(_procShLink.Procedure), "Procedure.Procedure_IssuingAuthority" }).Where(x => x.SaftyHazardId == id).Select(x => x.Procedure).ToListAsync();
            return data;
        }

        public async Task<List<Procedure>> GetEOLinkedProcedures(int id)
        {
            var data = await _procEoLinkService.AllQueryWithInclude(new string[] { nameof(_procEOLink.Procedure), "Procedure.Procedure_IssuingAuthority" }).Where(x => x.EnablingObjectiveId == id).Select(x => x.Procedure).ToListAsync();
            return data;
        }

        public async Task<List<Procedure>> GetProcTaskIsLinkedTo(int id)
        {
            var data = await _procTaskService.AllQueryWithInclude(new string[] { nameof(_proc_Task_Link.Procedure), "Procedure.Procedure_IssuingAuthority" }).Where(x => x.TaskId == id).Select(x => x.Procedure).ToListAsync();
            return data;
        }

        public async Task<List<Procedure>> GetProcILAIsLinkedTo(int id)
        {
            var data = await _ilaProcedureLinkService.AllQueryWithInclude(new string[] {"Procedure", "Procedure.Procedure_IssuingAuthority" }).Where(x => x.ILAId == id).Select(x => x.Procedure).ToListAsync();
            return data;
        }

        public async Task<List<Procedure>> GetProceduresRRIsLinkedTo(int id)
        {
            var data = await _procRRLinkService.AllQueryWithInclude(new string[] { nameof(_proc_rr_link.Procedure), "Procedure.Procedure_IssuingAuthority" }).Where(x => x.RegulatoryRequirementId == id).Select(x => x.Procedure).ToListAsync();
            return data;
        }

        public async Task<bool> IsIssuingAuthorityReleasedToEmp(int issuingAuthorityId)
        {
            var issuingAuthority = await _procedureReviewService.FindQueryWithIncludeAsync(x => x.Procedure.IssuingAuthorityId == issuingAuthorityId, new string[] { "Procedure.Procedure_IssuingAuthority" }).ToListAsync();
            foreach (var procedureReview in issuingAuthority)
            {
                if (procedureReview.IsPublished == true)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> IsProcedureReleasedToEmp(int procedureId)
        {
            var currentDate = DateTime.Now;
            var procedureReviews = await _procedureReviewService.FindQueryWithIncludeAsync(x => x.ProcedureId == procedureId && x.Active && currentDate <= x.EndDateTime , new string[] { "Procedure" }).ToListAsync();
            foreach (var procedureReview in procedureReviews)
            {
                if (procedureReview.IsPublished == true)
                {
                    return true;
                }
            }
            return false;
        }

        //active inactive procedures and issuing authorities
        public async Task<List<ProcedureSummaryVM>> GetActiveInactiveIA(string notLinkedWith)
        {
            var ProcList = new List<ProcedureSummaryVM>();

            switch (notLinkedWith.ToLower().Trim())
            {
                case "activeia":
                    {
                        ProcList = await _issuingAuthorityService.FindQuery(x => x.Active == true).Select(s => new ProcedureSummaryVM
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Active = s.Active,
                        }).OrderBy(s => s.Title).ToListAsync();
                        break;
                    }

                case "inactiveia":
                    {
                        ProcList = await _issuingAuthorityService.FindQuery(x => x.Active == false).Select(s => new ProcedureSummaryVM
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Active = s.Active,
                        }).OrderBy(s => s.Title).ToListAsync();

                        break;
                    }
            }

            return ProcList;
        }

        public async Task<List<ProcedureSummaryVM>> GetActiveInactiveProcedure(string notLinkedWith)
        {
            var ProcList = new List<ProcedureSummaryVM>();

            switch (notLinkedWith.ToLower().Trim())
            {
                case "activeproc":
                    {
                        ProcList = await _procedureService.FindQuery(x => x.Active == true).Select(s => new ProcedureSummaryVM
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Number = s.Number,
                            Active = s.Active,
                        }).OrderBy(s => s.Number).ToListAsync();
                        break;
                    }

                case "inactiveproc":
                    {
                        ProcList = await _procedureService.FindQuery(x => x.Active == false).Select(s => new ProcedureSummaryVM
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Number = s.Number,
                            Active = s.Active,
                        }).OrderBy(s => s.Number).ToListAsync();

                        break;
                    }
            }

            return ProcList;
        }
    }
}
