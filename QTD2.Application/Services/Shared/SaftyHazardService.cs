using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.SafetyHazard_EO_Link;
using QTD2.Infrastructure.Model.SafetyHazard_ILA_Link;
using QTD2.Infrastructure.Model.SafetyHazard_Procedure_Link;
using QTD2.Infrastructure.Model.SafetyHazard_Set_Link;
using QTD2.Infrastructure.Model.SafetyHazard_Task_Link;
using QTD2.Infrastructure.Model.SafetyHazard_Tool_Link;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.SaftyHazard_Abatement;
using QTD2.Infrastructure.Model.SaftyHazard_Control;
using QTD2.Infrastructure.Model.SaftyHazard_RR_Link;
using IEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IProcedureDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedureService;
using IRegulatoryRequirementDomainService = QTD2.Domain.Interfaces.Service.Core.IRegulatoryRequirementService;
using ISafetyHazard_CategortDomainService = QTD2.Domain.Interfaces.Service.Core.ISaftyHazard_CategoryService;
using ISafetyHazard_EO_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_EO_LinkService;
using ISafetyHazard_HistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_HistoryService;
using ISafetyHazard_ILA_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_ILA_LinkService;
//using ISafetyHazard_Procedure_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_Procedure_LinkService;
using IProcedure_SafetyHazard_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_SaftyHazard_LinkService;
using ISafetyHazard_Task_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_Task_LinkService;
using ISafetyHazardSetDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_SetService;
using ISH_EOLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_EO_LinkService;
using ISH_ILALinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_ILA_LinkService;
using ISH_RRLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISaftyHazard_RR_LinkService;
using ISH_TaskLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_Task_LinkService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using ISafetyHazard_Tool_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_Tool_LinkService;
using IILA_TopicDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TopicService;
using QTD2.Application.Utils;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.ILA_Topic;

namespace QTD2.Application.Services.Shared
{
    public class SaftyHazardService : Interfaces.Services.Shared.ISaftyHazardService
    {
        private readonly ISaftyHazardService _saftyHazardService;
        private readonly ISaftyHazard_AbatementService _saftyHazardAbatmentService;
        private readonly ISaftyHazard_ControlService _saftyHazardControlService;
        private readonly ISaftyHazard_CategoryService _saftyHazardCategoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SaftyHazardService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly SaftyHazard _sh;
        private readonly IEnablingObjectiveDomainService _eOService;
        private readonly ITaskDomainService _taskService;
        private readonly ISafetyHazardSetDomainService _safetyHazardSetService;
        private readonly IRegulatoryRequirementDomainService _rrService;
        private readonly IILADomainService _ilaService;
        private readonly IProcedureDomainService _procedureService;
        private readonly ISafetyHazard_HistoryDomainService _sh_Hist_Service;
        private readonly SaftyHazard_Category _sh_cat;
        //private readonly ISafetyHazard_Procedure_LinkDomainService _sh_Proc_Service;
        private readonly IProcedure_SafetyHazard_LinkDomainService _proc_Sh_Service;
        private readonly IToolService _toolService;
        //private readonly SafetyHazard_Procedure_Link _sh_Proc_Link;
        private readonly Procedure_SaftyHazard_Link _proc_sh_Link;
        private readonly ISH_TaskLinkDomainService _shTaskLinkService;
        private readonly SafetyHazard_Task_Link _sh_Task_Link;
        private readonly ISH_RRLinkDomainService _sh_RR_LinkService;
        private readonly SaftyHazard_RR_Link _sh_rr_link;
        private readonly ISH_ILALinkDomainService _shILALinkService;
        private readonly ISH_EOLinkDomainService _shEOLinkService;
        private readonly SafetyHazard_ILA_Link _sh_ILA_Link;
        private readonly SafetyHazard_EO_Link _sh_EO_Link;
        private readonly ISafetyHazard_EO_LinkDomainService _sh_eo_linkService;
        private readonly ISafetyHazard_ILA_LinkDomainService _sh_ila_linkService;
        private readonly ISafetyHazard_Task_LinkDomainService _sh_task_linkService;
        private readonly ISafetyHazard_CategortDomainService _sh_cat_service;
        private readonly ISafetyHazard_Tool_LinkDomainService _sh_tool_linkService;
        private readonly SafetyHazard_Tool_Link _sh_tool_link;
        private readonly IProviderService _providerService;
        private readonly IILA_TopicDomainService _topicService;
        private readonly Provider _provider;
        private readonly ILA_Topic _topics;
        private readonly Domain.Entities.Core.Task _task;
        private readonly Interfaces.Services.Shared.IVersion_TaskService _versionTaskService;

        public SaftyHazardService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            ISaftyHazardService saftyHazardService,
            IStringLocalizer<SaftyHazardService> localizer,
            ISaftyHazard_AbatementService saftyHazardAbatmentService,
            ISaftyHazard_ControlService saftyHazardControlService,
            ISaftyHazard_CategoryService saftyHazardCategoryService,
            UserManager<AppUser> userManager,
            IEnablingObjectiveDomainService eOService,
            ITaskDomainService taskService,
            ISafetyHazardSetDomainService safetyHazardSetService,
            IProcedureDomainService procedureService,
            IRegulatoryRequirementDomainService rrService,
            IILADomainService ilaService,
            ISafetyHazard_HistoryDomainService sh_Hist_Service,
            //ISafetyHazard_Procedure_LinkDomainService sh_Proc_Service,
            IToolService toolService,
            ISH_TaskLinkDomainService shTaskLinkService,
            ISH_RRLinkDomainService sh_RR_LinkService,
            ISH_ILALinkDomainService shILALinkService,
            ISH_EOLinkDomainService shEOLinkService,
            ISafetyHazard_EO_LinkDomainService sh_eo_linkService,
            ISafetyHazard_ILA_LinkDomainService sh_ila_linkService,
            ISafetyHazard_Task_LinkDomainService sh_task_linkService,
            ISafetyHazard_CategortDomainService sh_cat_service,
            ISafetyHazard_Tool_LinkDomainService sh_tool_linkService, IProviderService providerService, IILA_TopicDomainService topicService,
             IProcedure_SafetyHazard_LinkDomainService proc_Sh_Service,
             Interfaces.Services.Shared.IVersion_TaskService versionTaskService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _saftyHazardService = saftyHazardService;
            _localizer = localizer;
            _saftyHazardAbatmentService = saftyHazardAbatmentService;
            _saftyHazardControlService = saftyHazardControlService;
            _saftyHazardCategoryService = saftyHazardCategoryService;
            _userManager = userManager;
            _sh = new SaftyHazard();
            _eOService = eOService;
            _taskService = taskService;
            _safetyHazardSetService = safetyHazardSetService;
            _procedureService = procedureService;
            _rrService = rrService;
            _ilaService = ilaService;
            _sh_Hist_Service = sh_Hist_Service;
            _sh_cat = new SaftyHazard_Category();
            //_sh_Proc_Service = sh_Proc_Service;
            _proc_Sh_Service = proc_Sh_Service;
            //_sh_Proc_Link = new SafetyHazard_Procedure_Link();
            _proc_sh_Link = new Procedure_SaftyHazard_Link();
            _toolService = toolService;
            _shTaskLinkService = shTaskLinkService;
            _sh_Task_Link = new SafetyHazard_Task_Link();
            _sh_RR_LinkService = sh_RR_LinkService;
            _sh_rr_link = new SaftyHazard_RR_Link();
            _shILALinkService = shILALinkService;
            _sh_ILA_Link = new SafetyHazard_ILA_Link();
            _sh_EO_Link = new SafetyHazard_EO_Link();
            _shEOLinkService = shEOLinkService;
            _sh_eo_linkService = sh_eo_linkService;
            _sh_ila_linkService = sh_ila_linkService;
            _sh_task_linkService = sh_task_linkService;
            _sh_cat_service = sh_cat_service;
            _sh_tool_linkService = sh_tool_linkService;
            _sh_tool_link = new SafetyHazard_Tool_Link();
            _providerService = providerService;
            _topicService = topicService;
            _provider = new Provider();
            _topics = new ILA_Topic();
            _versionTaskService = versionTaskService;
            _task = new Domain.Entities.Core.Task();
        }

        public async Task<SaftyHazard_Abatement> AddAbatementAsync(int shId, SaftyHazard_AbatementCreateOptions options)
        {
            var saftyHazard = await GetAsync(shId);
            options.Number = (await _saftyHazardAbatmentService.FindAsync(r => r.SaftyHazardId == saftyHazard.Id)).OrderBy(x => x.Number).LastOrDefault()?.Number ?? 0;
            options.Number += 1;
            var saftyHazard_abatment = new SaftyHazard_Abatement(saftyHazard.Id, options.Number, options.Description);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard_abatment, SaftyHazard_AbatementOperations.Create);
            if (result.Succeeded)
            {
                saftyHazard_abatment.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                saftyHazard_abatment.CreatedDate = DateTime.Now;
                var validationResult = await _saftyHazardService.AddAsync(saftyHazard);
                if (validationResult.IsValid)
                {
                    return saftyHazard_abatment;
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

        public async Task<SaftyHazard_Control> AddControlAsync(int shId, SaftyHazard_ControlCreateOptions options)
        {
            var saftyHazard = await GetAsync(shId);
            options.Number = (await _saftyHazardControlService.FindAsync(r => r.SaftyHazardId == saftyHazard.Id)).OrderBy(x => x.Number).LastOrDefault()?.Number ?? 0;
            options.Number += 1;
            var saftyHazard_control = new SaftyHazard_Control(saftyHazard.Id, options.Number, options.Description);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard_control, SaftyHazard_ControlOperations.Create);
            if (result.Succeeded)
            {
                saftyHazard_control.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                saftyHazard_control.CreatedDate = DateTime.Now;
                var validationResult = await _saftyHazardService.AddAsync(saftyHazard);
                if (validationResult.IsValid)
                {
                    return saftyHazard_control;
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

        public async Task<SaftyHazard> CreateAsync(SaftyHazardCreateOptions options)
        {
            var saftyHazard = (await _saftyHazardService.FindQuery(r => r.SaftyHazardCategoryId == options.SaftyHazardCategoryId && r.Number.Trim().ToLower() == options.Number.Trim().ToLower()).ToListAsync()).FirstOrDefault();
            if (saftyHazard != null)
            {
                throw new BadHttpRequestException(message: _localizer["SafetyHazardExists"]);
            }

            saftyHazard = new SaftyHazard(options.SaftyHazardCategoryId, options.Title, options.Number, options.RevisionNumber, options.EffectiveDate, options.HyperLinks, options.Set, options.Text, options.Files, options.Image, options.FileName);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard, SaftyHazardOperations.Create);
            if (result.Succeeded)
            {
                saftyHazard.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                saftyHazard.CreatedDate = DateTime.Now;
                var validationResult = await _saftyHazardService.AddAsync(saftyHazard);
                if (validationResult.IsValid)
                {
                    //var sh = await _saftyHazardService.FindQuery(x => x.Title.Trim().ToLower() == saftyHazard.Title.Trim().ToLower() && x.Number.Trim().ToLower() == saftyHazard.Number.Trim().ToLower()).Select(x => x.Id).FirstOrDefaultAsync();
                    //saftyHazard = new SaftyHazard();
                    //saftyHazard.Set_Id(sh);
                    return saftyHazard;
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

        public async Task<SaftyHazard> CopySafetyHazardWithLinkages(int id, SaftyHazardCreateOptions options)
        {
            var sh = (await _saftyHazardService.FindQuery(r => r.Title.Trim().ToLower() == options.Title.Trim().ToLower()).ToListAsync()).FirstOrDefault();
            if (sh != null)
            {
                throw new BadHttpRequestException(message: _localizer["SafetyHazardExists"]);
            }

            sh = new SaftyHazard(options.SaftyHazardCategoryId, options.Title, options.Number, options.RevisionNumber, options.EffectiveDate, options.HyperLinks, options.Set, options.Text, options.Files, options.Image, options.FileName);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Create);
            if (result.Succeeded)
            {
                sh.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                sh.CreatedDate = DateTime.Now;
                var validationResult = await _saftyHazardService.AddAsync(sh);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    var linkToCopy = await _saftyHazardService
                        .FindQueryWithIncludeAsync(x => x.Id == id,
                        new string[] {
                            nameof(_sh.SafetyHazard_Task_Links),
                            nameof(_sh.SaftyHazard_RR_Links),
                            nameof(_sh.SafetyHazard_EO_Links),
                            nameof(_sh.SafetyHazard_ILA_Links),
                            nameof(_sh.Procedure_SaftyHazard_Links)
                        },true).FirstOrDefaultAsync();
                    sh.SafetyHazard_ILA_Links = linkToCopy.SafetyHazard_ILA_Links.DeepCopy();
                    sh.SafetyHazard_ILA_Links.ToList().ForEach(x => x.Id = 0);
                    sh.SafetyHazard_EO_Links = linkToCopy.SafetyHazard_EO_Links.DeepCopy();
                    sh.SafetyHazard_EO_Links.ToList().ForEach(x => x.Id = 0);
                    sh.SaftyHazard_RR_Links = linkToCopy.SaftyHazard_RR_Links.DeepCopy();
                    sh.SaftyHazard_RR_Links.ToList().ForEach(x => x.Id = 0);
                    sh.SafetyHazard_Task_Links = linkToCopy.SafetyHazard_Task_Links.DeepCopy();
                    sh.SafetyHazard_Task_Links.ToList().ForEach(x => x.Id = 0);
                    sh.Procedure_SaftyHazard_Links = linkToCopy.Procedure_SaftyHazard_Links.DeepCopy();
                    sh.Procedure_SaftyHazard_Links.ToList().ForEach(x => x.Id = 0);
                    await _saftyHazardService.UpdateAsync(sh);
                    return sh;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }

        }


        public async System.Threading.Tasks.Task DeleteAsync(SaftyHazardOptions options)
        {
            foreach (var id in options.SaftyHazardIds)
            {
                var saftyHazard = await GetAsync(id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard, SaftyHazardOperations.Delete);
                if (result.Succeeded)
                {
                    saftyHazard.Delete();
                    var validationResult = await _saftyHazardService.UpdateAsync(saftyHazard);
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

        public async System.Threading.Tasks.Task DeactivateAsync(SaftyHazardOptions options)
        {
            foreach (var id in options.SaftyHazardIds)
            {
                var saftyHazard = await GetAsync(id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard, SaftyHazardOperations.Delete);
                if (result.Succeeded)
                {
                    saftyHazard.Deactivate();
                    var validationResult = await _saftyHazardService.UpdateAsync(saftyHazard);
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

        public async System.Threading.Tasks.Task ActivateAsync(SaftyHazardOptions options)
        {
            foreach (var id in options.SaftyHazardIds)
            {
                var saftyHazard = await GetAsync(id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard, SaftyHazardOperations.Delete);
                if (result.Succeeded)
                {
                    saftyHazard.Activate();
                    var validationResult = await _saftyHazardService.UpdateAsync(saftyHazard);
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

        public async Task<List<SaftyHazard>> GetAsync()
        {
            var shList = await _saftyHazardService.AllAsync();
            shList = shList.Where(sh => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Read).Result.Succeeded);
            return shList?.ToList();
        }

        public async Task<SaftyHazard> GetAsync(int shId)
        {
            var saftyHazard = await _saftyHazardService.AllQueryWithInclude(new string[] { nameof(_sh.SaftyHazard_Category) }).Where(x => x.Id == shId).FirstOrDefaultAsync();
            if (saftyHazard != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard, SaftyHazardOperations.Read);
                if (result.Succeeded)
                {
                    return saftyHazard;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["SaftyHazardNotFound"]);
            }
        }

        public async Task<SaftyHazardWithSet> GetWithSetsAsync(int id)
        {
            var shWithSet = new SaftyHazardWithSet();
            var saftyHazard = await _saftyHazardService.AllQueryWithInclude(new string[] { nameof(_sh.SafetyHazard_Set_Links), nameof(_sh.SafetyHazard_Tool_Links) }).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (saftyHazard != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard, SaftyHazardOperations.Read);
                if (result.Succeeded)
                {
                    shWithSet.SaftyHazard = saftyHazard;
                    foreach (var link in saftyHazard.SafetyHazard_Set_Links)
                    {
                        var shSet = await _safetyHazardSetService.FindQuery(x => x.Id == link.SafetyHazardSetId).FirstOrDefaultAsync();
                        shWithSet.SafetyHazard_Sets.Add(shSet);
                    }

                    shWithSet.SafetyHazard_Sets = shWithSet.SafetyHazard_Sets.OrderBy(x => x.Id).ToList();

                    foreach (var toolLink in saftyHazard.SafetyHazard_Tool_Links)
                    {
                        var tool = await _toolService.FindQuery(x => x.Id == toolLink.ToolId).FirstOrDefaultAsync();
                        shWithSet.Tools.Add(tool);
                    }

                    return shWithSet;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["SaftyHazardNotFound"]);
            }
        }

        public async Task<SH_StatsVM> GetSHStats()
        {
            var linkedTasks = await _sh_task_linkService.AllQuery().Where(w => w.Active == true).Select(x => x.SaftyHazardId).ToListAsync();
            var linkedEOs = await _sh_eo_linkService.AllQuery().Select(x => x.SafetyHazardId).ToListAsync();
            var linkedRRs = await _sh_RR_LinkService.AllQuery().Select(x => x.RegulatoryRequirementId).ToListAsync();
            var linkedProcs = await _proc_Sh_Service.AllQuery().Select(x => x.ProcedureId).ToListAsync();
            var linkedILAs = await _sh_ila_linkService.AllQuery().Select(x => x.ILAId).ToListAsync();

            var stats = new SH_StatsVM()
            {
                CatActive = await _sh_cat_service.GetCount(x => x.Active == true),
                CatInactive = await _sh_cat_service.GetCount(x => x.Active == false),
                SHActive = await _saftyHazardService.GetCount(x => x.Active == true),
                SHInactive = await _saftyHazardService.GetCount(x => x.Active == false),
                SHNotLinkedToTasks = await _saftyHazardService.GetCount(x => !linkedTasks.Contains(x.Id) && x.Active == true),
                SHNotLinkedToEOs = await _saftyHazardService.GetCount(x => !linkedEOs.Contains(x.Id) && x.Active == true),
                SHNotLinkedToRRs = await _saftyHazardService.GetCount(x => !linkedRRs.Contains(x.Id) && x.Active == true),
                SHNotLinkedToProcs = await _saftyHazardService.GetCount(x => !linkedProcs.Contains(x.Id) && x.Active == true),
                SHNotLinkedToILAs = await _saftyHazardService.GetCount(x => !linkedILAs.Contains(x.Id) && x.Active == true),
            };

            return stats;
        }

        public async Task<List<Tool>> GetToolsLinkedToShAsync(int id)
        {
            var tools = await _sh_tool_linkService.AllQueryWithInclude(new string[] { nameof(_sh_tool_link.Tool) }).Where(x => x.SafetyHazardId == id).Select(x => x.Tool).ToListAsync();
            return tools;
        }

        public async Task<SaftyHazard_Abatement> RemoveAbatementAsync(int shId, int abatementId)
        {
            var saftyHazard = await GetAsync(shId);
            var saftyHazard_abatment = await _saftyHazardAbatmentService.GetAsync(abatementId);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard_abatment, SaftyHazard_AbatementOperations.Delete);
            if (result.Succeeded)
            {
                saftyHazard.RemoveAbatement(saftyHazard_abatment);
                var validationResult = await _saftyHazardService.UpdateAsync(saftyHazard);
                if (validationResult.IsValid)
                {
                    return saftyHazard_abatment;
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

        public async Task<SaftyHazard_Control> RemoveControlAsync(int shId, int controlId)
        {
            var saftyHazard = await GetAsync(shId);
            var saftyHazard_control = await _saftyHazardControlService.GetAsync(controlId);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard_control, SaftyHazard_ControlOperations.Delete);
            if (result.Succeeded)
            {
                saftyHazard.RemoveControl(saftyHazard_control);
                var validationResult = await _saftyHazardService.UpdateAsync(saftyHazard);
                if (validationResult.IsValid)
                {
                    return saftyHazard_control;
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

        public async Task<SaftyHazard> UpdateAsync(int shId, SaftyHazardCreateOptions options)
        {
            var saftyHazard = await _saftyHazardService.AllQueryWithInclude(new string[] { nameof(_sh.SafetyHazard_Set_Links), nameof(_sh.SafetyHazard_Tool_Links) }).Where(x => x.Id == shId).FirstOrDefaultAsync();
            var previous = await _saftyHazardService.FindQuery(x => x.Number.Trim().ToLower() == options.Number.Trim().ToLower() && (x.Id != saftyHazard.Id || x.SaftyHazardCategoryId != options.SaftyHazardCategoryId)).FirstOrDefaultAsync();
            //if(previous != null)
            //{
            //    throw new BadHttpRequestException(message: _localizer["SafetyHazardExists"]);
            //}

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard, SaftyHazardOperations.Update);
            if (result.Succeeded)
            {
                // Todo update logic
                saftyHazard.Number = options.Number;
                saftyHazard.Title = options.Title;
                saftyHazard.Text = options.Text;
                saftyHazard.EffectiveDate = options.EffectiveDate;
                saftyHazard.Files = options.Files;
                saftyHazard.HyperLinks = options.HyperLinks;
                saftyHazard.RevisionNumber = options.RevisionNumber;
                saftyHazard.FileName = options.FileName; 
                saftyHazard.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                saftyHazard.ModifiedDate = DateTime.Now;
                var setLinks = new List<int>(saftyHazard.SafetyHazard_Set_Links.Select(x => x.SafetyHazardSetId));
                foreach (var set in setLinks)
                {
                    var obj = await _safetyHazardSetService.FindQuery(x => x.Id == set).FirstOrDefaultAsync();
                    saftyHazard.UnlinkSafetyHazardSet(obj);
                }

                var toolLinks = new List<int>(saftyHazard.SafetyHazard_Tool_Links.Select(x => x.ToolId).ToList());

                foreach (var tool in toolLinks)
                {
                    var obj = await _toolService.FindQuery(x => x.Id == tool).FirstOrDefaultAsync();
                    saftyHazard.UnlinkSafetyHazardTool(obj);
                }

                var validationResult = await _saftyHazardService.UpdateAsync(saftyHazard);
                foreach (var set in setLinks)
                {
                    var obj = await _safetyHazardSetService.FindQuery(x => x.Id == set).FirstOrDefaultAsync();
                    await _safetyHazardSetService.DeleteAsync(obj);
                }

                if (validationResult.IsValid)
                {
                    return saftyHazard;
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

        public async System.Threading.Tasks.Task UpdateDescriptionAsync(int id, SaftyHazardCreateOptions options)
        {
            var saftyHazard = await _saftyHazardService.AllQueryWithInclude(new string[] { nameof(_sh.SafetyHazard_Set_Links), nameof(_sh.SafetyHazard_Tool_Links) }).Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, saftyHazard, SaftyHazardOperations.Update);
            if (result.Succeeded)
            {
                saftyHazard.Text = options.Text;
                var validationResult = await _saftyHazardService.UpdateAsync(saftyHazard);
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

        public async Task<SaftyHazard> LinkEO(int id, SafetyHazard_EO_LinkOptions options)
        {
            var sh = await _saftyHazardService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();

            foreach (var eoId in options.EOIDs)
            {
                var eo = await _eOService.GetAsync(eoId);
                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, AuthorizationOperations.Update);

                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, AuthorizationOperations.Read);

                if (shResult.Succeeded && eoResult.Succeeded)
                {
                    sh.LinkEO(eo);
                    await _saftyHazardService.UpdateAsync(sh);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return null;
        }

        public async System.Threading.Tasks.Task UnlinkEO(int shId, SafetyHazard_EO_LinkOptions options)
        {
            var sh = await _saftyHazardService.GetWithIncludeAsync(shId, new string[] { nameof(_sh.SafetyHazard_EO_Links) });

            foreach (var eoId in options.EOIDs)
            {
                var eo = await _eOService.GetAsync(eoId);

                if (eo == null)
                {
                    throw new QTDServerException(_localizer["EONotFound"]);
                }

                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, AuthorizationOperations.Update);

                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, AuthorizationOperations.Read);

                if (rrResult.Succeeded && eoResult.Succeeded)
                {
                    sh.UnlinkEO(eo);
                    await _saftyHazardService.UpdateAsync(sh);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<List<SafetyHazardWithLinkCount>> GetLinkedEOsWithCount(int id)
        {
            var eoList = await _shEOLinkService.FindQueryWithIncludeAsync(x => x.SafetyHazardId == id, new string[] { nameof(_sh_EO_Link.EnablingObjective) }).Select(x=>x.EnablingObjective).ToListAsync();
           

            List<SafetyHazardWithLinkCount> eoWithCount = new List<SafetyHazardWithLinkCount>();
            foreach (var eo in eoList)
            {
                var data = await _shEOLinkService.GetCount(x => x.EOID == eo.Id);
                eoWithCount.Add(new SafetyHazardWithLinkCount(eo.Id, eo.Description, Convert.ToString(eo.Number), data, eo.Active));
            }

            return eoWithCount;
        }

        public async Task<List<SaftyHazardCompactOptions>> getSHLinkedToEO(int id)
        {
            var data = await _shEOLinkService.AllQueryWithInclude(new string[] { nameof(_sh_EO_Link.SaftyHazard) }).Where(x => x.EOID == id)
                .Select(x => new SaftyHazardCompactOptions(
                    x.SafetyHazardId,
                    x.SaftyHazard.SaftyHazardCategoryId,
                    x.SaftyHazard.Title,
                    x.SaftyHazard.Active,
                    x.SaftyHazard.Number)).ToListAsync();
            return data;
        }

        public async Task<SaftyHazard> LinkTask(int id, SafetyHazard_Task_LinkOptions options)
        {
            var sh = await _saftyHazardService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();

            foreach (var taskId in options.TaskIds)
            {
                var task = await _taskService.GetAsync(taskId);
                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, AuthorizationOperations.Update);

                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, AuthorizationOperations.Read);

                if (shResult.Succeeded && taskResult.Succeeded)
                {
                    sh.LinkTask(task);
                    await _saftyHazardService.UpdateAsync(sh);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return null;
        }

        public async System.Threading.Tasks.Task UnlinkTask(int shId, SafetyHazard_Task_LinkOptions options)
        {
            var sh = await _saftyHazardService.GetWithIncludeAsync(shId, new string[] { nameof(_sh.SafetyHazard_Task_Links) });

            foreach (var taskId in options.TaskIds)
            {
                var task = await _taskService.GetAsync(taskId);

                if (task == null)
                {
                    throw new QTDServerException(_localizer["TaskNotFound"]);
                }

                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, AuthorizationOperations.Update);

                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, AuthorizationOperations.Read);

                if (rrResult.Succeeded && taskResult.Succeeded)
                {
                    sh.UnlinkTask(task);
                    await _saftyHazardService.UpdateAsync(sh);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<List<SafetyHazardWithLinkCount>> GetLinkedTasksWithCount(int id)
        {
            var links = await _shTaskLinkService.FindQueryWithIncludeAsync(x => x.SaftyHazardId == id, new string[] { nameof(_sh_Task_Link.Task) }).Select(x=>x.Task).ToListAsync();
            
            List<SafetyHazardWithLinkCount> taskWithCount = new List<SafetyHazardWithLinkCount>();
            foreach (var task in links)
            {
                var data = await _shTaskLinkService.GetCount(x => x.TaskId == task.Id);
                var taskNumber = await _taskService.FindQueryWithIncludeAsync(x => x.Id == task.Id, new string[] { "SubdutyArea.DutyArea" }).FirstOrDefaultAsync();

                var num = taskNumber.SubdutyArea.DutyArea.Letter + taskNumber.SubdutyArea.DutyArea.Number.ToString() + '.' + taskNumber.SubdutyArea.SubNumber.ToString() + '.' + task.Number.ToString();
                taskWithCount.Add(new SafetyHazardWithLinkCount(task.Id, task.Description, num, data, task.Active));
            }

            return taskWithCount;
        }

        public async Task<List<SaftyHazardCompactOptions>> getSHLinkedToTask(int id)
        {
            var data = await _shTaskLinkService.AllQueryWithInclude(new string[] { nameof(_sh_Task_Link.SaftyHazard) }).Where(x => x.TaskId == id)
                .Select(x => new SaftyHazardCompactOptions(
                    x.SaftyHazardId,
                    x.SaftyHazard.SaftyHazardCategoryId,
                    x.SaftyHazard.Title,
                    x.SaftyHazard.Active,
                    x.SaftyHazard.Number)).ToListAsync();
            return data;
        }

        public async Task<SaftyHazard> LinkSet(int id, SafetyHazard_Set_LinkOptions options)
        {
            var sh = await _saftyHazardService.GetWithIncludeAsync(id, new string[] { nameof(_sh.SafetyHazard_EO_Links) });
            var shSet = await _safetyHazardSetService.GetAsync(options.SafetyHazardSetId);
            if (shSet == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardSetNotFound"]);
            }
            else
            {
                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Update);
                var shSetResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, shSet, SafetyHazard_SetOperations.Read);
                if (shResult.Succeeded && shSetResult.Succeeded)
                {
                    sh.LinkSafetyHazardSet(shSet);
                    await _saftyHazardService.UpdateAsync(sh);
                    return sh;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkSet(int shId, int shSetId)
        {
            var sh = await _saftyHazardService.GetWithIncludeAsync(shId, new string[] { nameof(_sh.SafetyHazard_Set_Links) });
            var shSet = await _safetyHazardSetService.GetAsync(shSetId);
            if (shSet == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardSetNotFound"]);
            }
            else
            {
                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Update);
                var shSetResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, shSet, SafetyHazard_SetOperations.Read);
                if (shResult.Succeeded && shSetResult.Succeeded)
                {
                    sh.UnlinkSafetyHazardSet(shSet);
                    await _saftyHazardService.UpdateAsync(sh);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<SaftyHazard> LinkILA(int id, SafetyHazard_ILA_LinkOptions options)
        {
            var sh = await _saftyHazardService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();

            foreach (var ilaId in options.ILAIds)
            {
                var ila = await _ilaService.GetAsync(ilaId);
                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, AuthorizationOperations.Update);

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Read);

                if (shResult.Succeeded && ilaResult.Succeeded)
                {
                    sh.LinkILA(ila);
                    await _saftyHazardService.UpdateAsync(sh);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return null;
        }

        public async System.Threading.Tasks.Task UnlinkILA(int shId, SafetyHazard_ILA_LinkOptions options)
        {
            var sh = await _saftyHazardService.GetWithIncludeAsync(shId, new string[] { nameof(_sh.SafetyHazard_ILA_Links) });

            foreach (var ilaId in options.ILAIds)
            {
                var ila = await _ilaService.GetAsync(ilaId);

                if (ila == null)
                {
                    throw new QTDServerException(_localizer["ILANotFound"]);
                }

                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, AuthorizationOperations.Update);

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Read);

                if (shResult.Succeeded && ilaResult.Succeeded)
                {
                    sh.UnLinkILA(ila);
                    await _saftyHazardService.UpdateAsync(sh);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<List<SafetyHazardWithLinkCount>> GetLinkedILAsWithCount(int id)
        {
            var links = await _shILALinkService.FindWithIncludeAsync(x => x.SafetyHazardId == id, new string[] { nameof(_sh_ILA_Link.ILA) });
            List<Domain.Entities.Core.ILA> ilaList = new List<Domain.Entities.Core.ILA>();
            ilaList.AddRange(links.Select(x => x.ILA));

            List<SafetyHazardWithLinkCount> iLAWithCount = new List<SafetyHazardWithLinkCount>();
            foreach (var ila in ilaList)
            {
                var data = await _shILALinkService.GetCount(x => x.ILAId == ila.Id);
                iLAWithCount.Add(new SafetyHazardWithLinkCount(ila.Id, ila.Name, Convert.ToString(ila.Number), data, ila.Active));
            }

            return iLAWithCount;
        }

        public async Task<List<SaftyHazardCompactOptions>> getSHLinkedToILA(int id)
        {
            var data = await _shILALinkService.AllQueryWithInclude(new string[] { nameof(_sh_ILA_Link.SaftyHazard) }).Where(x => x.ILAId == id)
                .Select(x => new SaftyHazardCompactOptions(
                    x.SafetyHazardId,
                    x.SaftyHazard.SaftyHazardCategoryId,
                    x.SaftyHazard.Title,
                    x.SaftyHazard.Active,
                    x.SaftyHazard.Number)).ToListAsync();
            return data;
        }

        public async Task<List<SaftyHazardCompactOptions>> getSHLinkedToProcedure(int id)
        {
            var data = await _proc_Sh_Service.AllQueryWithInclude(new string[] { nameof(_proc_sh_Link.SaftyHazard) }).Where(x => x.ProcedureId == id)
                .Select(x => new SaftyHazardCompactOptions(
                    x.SaftyHazardId,
                    x.SaftyHazard.SaftyHazardCategoryId,
                    x.SaftyHazard.Title,
                    x.SaftyHazard.Active,
                    x.SaftyHazard.Number)).ToListAsync();
            return data;

        }

        public async Task<List<SaftyHazardCompactOptions>> getSHLinkedToRR(int id)
        {
            var data = await _sh_RR_LinkService.AllQueryWithInclude(new string[] { nameof(_sh_rr_link.SaftyHazard) }).Where(x => x.RegulatoryRequirementId == id)
               .Select(x => new SaftyHazardCompactOptions(
                   x.SafetyHazardId,
                   x.SaftyHazard.SaftyHazardCategoryId,
                   x.SaftyHazard.Title,
                   x.SaftyHazard.Active,
                   x.SaftyHazard.Number)).ToListAsync();
            return data;
        }

        public async Task<List<ProviderIlaVM>> GetProviderWithILAs()
        {
            var ilas = await _ilaService.AllQuery().Select(y => new IlaVM
            {
                Id = y.Id,
                providerId=y.ProviderId,
                Description = y.Description,
                Number = y.Number,
                Name = y.Name,
                Active = y.Active
            }).ToListAsync();
            var providerILAs = await _providerService.AllQuery().Select(x=>new ProviderIlaVM
            {
                Id=x.Id,
                Name=x.Name,
                Active=x.Active,
            }).ToListAsync();
            foreach (var item in providerILAs)
            {
                item.ILAs = ilas.Where(i => i.providerId == item.Id).ToList();
            }
            return providerILAs;
        }

        public async Task<List<ILATopicDataVM>> GetTopicWithILAs()
        {
            var topicILAs = await _topicService.AllWithIncludeAsync(new string[] { "ILA_Topic_Links.ILA" });
            var listILAtopicVM = topicILAs.Select(topic =>
                                    new ILATopicDataVM(topic.Id, topic.Name, topic.Active, topic.ILA_Topic_Links.Select(ilaLink =>
                                    new ILADetailsVM(ilaLink.ILA.Id, ilaLink.ILA.Name, ilaLink.ILA.Number, ilaLink.ILA.Active)).ToList())
            ).ToList();

            return listILAtopicVM;
        }

        public async Task<List<ProceduresWithLinkCount>> GetLinkedProcedureWithCount(int id)
        {
            var links = await _proc_Sh_Service.AllQueryWithInclude(new string[] { nameof(_proc_sh_Link.Procedure) }).Where(x => x.SaftyHazardId == id).Select(x => new { x.Procedure.Id, x.Procedure.Title, x.Procedure.Number, x.Procedure.Active }).ToListAsync();
            List<ProceduresWithLinkCount> procList = new List<ProceduresWithLinkCount>();
            foreach (var data in links)
            {
                var count = await _proc_Sh_Service.GetCount(x => x.ProcedureId == data.Id);
                procList.Add(new ProceduresWithLinkCount(data.Id, count, data.Number, data.Title, data.Active));
            }

            return procList;
        }

        public async Task<SaftyHazard> LinkProcedure(int id, SafetyHazard_Procedure_LinkOptions options)
        {
            var sh = await _saftyHazardService.GetWithIncludeAsync(id, new string[] { nameof(_sh.Procedure_SaftyHazard_Links) });
            foreach (var procId in options.ProcedureIds)
            {
                var proc = await _procedureService.GetAsync(procId);
                if (proc == null)
                {
                    throw new QTDServerException(_localizer["ProcedureNotFound"]);
                }
                else
                {
                    var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Update);
                    var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, proc, ProcedureOperations.Read);
                    if (shResult.Succeeded && procResult.Succeeded)
                    {
                        sh.LinkProcedure(proc);
                        await _saftyHazardService.UpdateAsync(sh);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }

            return sh;
        }

        public async System.Threading.Tasks.Task UnlinkProcedure(int shId, SafetyHazard_Procedure_LinkOptions procedures)
        {
            var sh = await _saftyHazardService.GetWithIncludeAsync(shId, new string[] { nameof(_sh.Procedure_SaftyHazard_Links) });
            foreach (var procId in procedures.ProcedureIds)
            {
                var proc = await _procedureService.GetAsync(procId);
                if (proc == null)
                {
                    throw new QTDServerException(_localizer["ProcedureNotFound"]);
                }
                else
                {
                    var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Update);
                    var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, proc, ProcedureOperations.Read);
                    if (shResult.Succeeded && procResult.Succeeded)
                    {
                        sh.UnLinkProcedure(proc);
                        await _saftyHazardService.UpdateAsync(sh);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }
        }

        public async Task<SaftyHazard> LinkRR(int id, SaftyHazard_RR_LinkOptions options)
        {
            var sh = await _saftyHazardService.GetWithIncludeAsync(id, new string[] { nameof(_sh.SaftyHazard_RR_Links) });
            foreach (var rrId in options.RegulatoryRequirementIds)
            {
                var rr = await _rrService.FindQuery(x => x.Id == rrId).FirstOrDefaultAsync();
                if (rr == null)
                {
                    throw new QTDServerException(_localizer["RegulatoryRequirementNotFound"]);
                }
                else
                {
                    var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Update);
                    var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Read);
                    if (shResult.Succeeded && rrResult.Succeeded)
                    {
                        sh.LinkRegRequirement(rr);
                        await _saftyHazardService.UpdateAsync(sh);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }

            return null;
        }

        public async System.Threading.Tasks.Task UnlinkRR(int shId, SaftyHazard_RR_LinkOptions options)
        {
            var sh = await _saftyHazardService.GetWithIncludeAsync(shId, new string[] { nameof(_sh.SaftyHazard_RR_Links) });
            foreach (var rrId in options.RegulatoryRequirementIds)
            {
                var rr = await _rrService.GetAsync(rrId);
                if (rr == null)
                {
                    throw new QTDServerException(_localizer["RegulatoryRequirementNotFound"]);
                }
                else
                {
                    var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Update);
                    var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Read);
                    if (shResult.Succeeded && rrResult.Succeeded)
                    {
                        sh.UnLinkRegRequirement(rr);
                        await _saftyHazardService.UpdateAsync(sh);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }
        }

        public async Task<List<RegulatoryRequirementWithLinkCount>> getLinkedRRWithCount(int id)
        {
            var links = await _sh_RR_LinkService.AllQueryWithInclude(new string[] { nameof(_sh_rr_link.RegulatoryRequirement) }).Where(x => x.SafetyHazardId == id).Select(x => new { x.RegulatoryRequirement.Id, x.RegulatoryRequirement.Title, x.RegulatoryRequirement.Number, x.RegulatoryRequirement.Active }).ToListAsync();
            List<RegulatoryRequirementWithLinkCount> rrList = new List<RegulatoryRequirementWithLinkCount>();
            foreach (var data in links)
            {
                var count = await _sh_RR_LinkService.GetCount(x => x.RegulatoryRequirementId == data.Id);
                rrList.Add(new RegulatoryRequirementWithLinkCount(data.Number, data.Title, data.Id, count, data.Active));
            }

            return rrList;
        }

        public async Task<List<SaftyHazard>> GetSHWithSHCatId(int id)
        {
            var sh = await _saftyHazardService.FindAsync(x => x.SaftyHazardCategoryId == id);
            return sh.ToList();
        }

        public async Task<List<SaftyHazard_Category>> GetSHNotLinkedTo(string option)
        {
            var notLinkedSH = new List<SaftyHazard_Category>();
            List<int> linkedSHIds = new List<int>();
            List<int> notLinkedSHIds = new List<int>();

            switch (option.ToLower().Trim())
            {
                case "tasks":
                    {
                        linkedSHIds = await _sh_task_linkService.AllQuery().Select(x => x.SaftyHazardId).Distinct().ToListAsync();
                        notLinkedSHIds = await _saftyHazardService.FindQuery(x => !linkedSHIds.Contains(x.Id))
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }

                case "enabling objectives":
                    {
                        linkedSHIds = await _sh_eo_linkService.AllQuery().Select(x => x.SafetyHazardId).Distinct().ToListAsync();
                        notLinkedSHIds = await _saftyHazardService.FindQuery(x => !linkedSHIds.Contains(x.Id))
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }

                case "procedures":
                    {
                        linkedSHIds = await _proc_Sh_Service.AllQuery().Select(x => x.SaftyHazardId).Distinct().ToListAsync();
                        notLinkedSHIds = await _saftyHazardService.FindQuery(x => !linkedSHIds.Contains(x.Id))
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }

                case "regulatory requiremnents":
                    {
                        linkedSHIds = await _sh_RR_LinkService.AllQuery().Select(x => x.SafetyHazardId).Distinct().ToListAsync();
                        notLinkedSHIds = await _saftyHazardService.FindQuery(x => !linkedSHIds.Contains(x.Id))
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }

                case "ilas":
                    {
                        linkedSHIds = await _sh_ila_linkService.AllQuery().Select(x => x.SafetyHazardId).Distinct().ToListAsync();
                        notLinkedSHIds = await _saftyHazardService.FindQuery(x => !linkedSHIds.Contains(x.Id))
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }
            }

            notLinkedSH = await _sh_cat_service.AllQuery().ToListAsync();

            foreach (var ia in notLinkedSH)
            {
                ia.SaftyHazards = _saftyHazardService.FindQuery(x => x.SaftyHazardCategoryId == ia.Id && notLinkedSHIds.Contains(x.Id)).ToList();
            }

            return notLinkedSH;
        }

        public async Task<SaftyHazard> LinkPPETool(int id, SafetyHazard_Tool_LinkOptions options)
        {
            var sh = await _saftyHazardService.GetWithIncludeAsync(id, new string[] { nameof(_sh.SafetyHazard_Tool_Links) });
            foreach (var toolId in options.ToolIds)
            {
                var tool = await _toolService.GetAsync(toolId);
                if (tool == null)
                {
                    throw new QTDServerException(_localizer["ToolNotFound"]);
                }
                else
                {
                    var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Update);
                    var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, AuthorizationOperations.Read);
                    if (shResult.Succeeded && rrResult.Succeeded)
                    {
                        sh.LinkSafetyHazardTool(tool);
                        await _saftyHazardService.UpdateAsync(sh);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }

            return sh;
        }

        public async System.Threading.Tasks.Task UnlinkPPETool(int shId, SafetyHazard_Tool_LinkOptions options)
        {
            var sh = await _saftyHazardService.GetWithIncludeAsync(shId, new string[] { nameof(_sh.SafetyHazard_Tool_Links) });
            foreach (var toolId in options.ToolIds)
            {
                var tool = await _toolService.GetAsync(toolId);
                if (tool == null)
                {
                    throw new QTDServerException(_localizer["ToolNotFound"]);
                }
                else
                {
                    var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Update);
                    var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, AuthorizationOperations.Read);
                    if (shResult.Succeeded && rrResult.Succeeded)
                    {
                        sh.UnlinkSafetyHazardTool(tool);
                        await _saftyHazardService.UpdateAsync(sh);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }
        }



        //active inactive sh and categories
        public async Task<List<SaftyHazard>> GetSHActiveInactive(string option)
        {
            var shList = new List<SaftyHazard>();

            switch (option.ToLower().Trim())
            {
                case "shactive":
                    {
                        shList = await _saftyHazardService.FindQuery(x => x.Active == true).Select(s => new SaftyHazard
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Number = s.Number
                        }).ToListAsync();
                        break;
                    }

                case "shinactive":
                    {
                        shList = await _saftyHazardService.FindQuery(x => x.Active == false).Select(s => new SaftyHazard
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Number = s.Number
                        }).ToListAsync();

                        break;
                    }
            }
            return shList.OrderBy(x=>x.Number).ToList();
        }

        public async Task<List<SaftyHazard_Category>> GetIAActiveInactive(string option)
        {
            var shList = new List<SaftyHazard_Category>();

            switch (option.ToLower().Trim())
            {
                case "catactive":
                    {
                        shList = await _saftyHazardCategoryService.FindQuery(x => x.Active == true).Select(s => new SaftyHazard_Category
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Number = s.Number
                        }).ToListAsync();
                        break;
                    }

                case "catinactive":
                    {
                        shList = await _saftyHazardCategoryService.FindQuery(x => x.Active == false).Select(s => new SaftyHazard_Category
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Number = s.Number
                        }).ToListAsync();

                        break;
                    }
            }
            return shList.OrderBy(x=>x.Number).ToList();
        }
    }
}
