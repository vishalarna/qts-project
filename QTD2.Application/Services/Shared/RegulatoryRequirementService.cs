using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Utils;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.RegRequirement_EO_Link;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.RR_EO_Link;
using QTD2.Infrastructure.Model.RR_ILA_Link;
using QTD2.Infrastructure.Model.RR_IssuingAuthority;
using QTD2.Infrastructure.Model.RR_Procedure_Link;
using QTD2.Infrastructure.Model.RR_SafetyHazard_Link;
using QTD2.Infrastructure.Model.RR_StatusHistory;
using QTD2.Infrastructure.Model.RR_Task_Link;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.Task;
using IEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using IILA_TopicDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TopicService;
using IILAServiceDomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IProcedureDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedureService;
using IProviderService = QTD2.Domain.Interfaces.Service.Core.IProviderService;
using IRegulatoryRequirementDomainService = QTD2.Domain.Interfaces.Service.Core.IRegulatoryRequirementService;
using IRR_EO_DomainService = QTD2.Domain.Interfaces.Service.Core.IRegRequirement_EO_LinkService;
using IRR_EO_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IRegRequirement_EO_LinkService;
using IRR_IA_DomainService = QTD2.Domain.Interfaces.Service.Core.IRR_IssuingAuthorityService;
using IILA_RR_DomainService = QTD2.Domain.Interfaces.Service.Core.IILA_RegRequirement_LinkService;
//using IRR_Proc_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IRR_Procedure_LinkService;
using IProc_RR_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_RR_LinkService;
//using IRR_SafetyHazard_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IRR_SafetyHazard_LinkService;
using ISafetyHazard_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISaftyHazard_RR_LinkService;
using IRR_StatusHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IRR_StatusHistoryService;
using IRR_TaskLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IRR_Task_LinkService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using ITaskSubDutyAreaDomainService = QTD2.Domain.Interfaces.Service.Core.ISubdutyAreaService;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Provider;
using QTD2.Infrastructure.Model.ILA_Topic;

namespace QTD2.Application.Services.Shared
{
    public class RegulatoryRequirementService : IRegulatoryRequirementService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<RegulatoryRequirementService> _localizer;
        private readonly IRegulatoryRequirementDomainService _regulatoryRequirementService;
        private readonly ISaftyHazardService _saftyHazardService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProcedureDomainService _procedureService;
        private readonly IILAServiceDomainService _ilaService;
        private readonly RegulatoryRequirement _rr;
        private readonly IEnablingObjectiveDomainService _eOService;
        private readonly ITaskDomainService _taskService;
        private readonly IRR_StatusHistoryDomainService _rrStatusHistoryService;
        //private readonly IRR_Proc_LinkDomainService _rr_proc_linkService;
        private readonly IProc_RR_LinkDomainService _proc_rr_linkService;
        //private readonly RR_Procedure_Link _rr_proc_link;
        private readonly Procedure_RR_Link _proc_rr_Link;
        private readonly IRR_TaskLinkDomainService _rrTaskLinkService;
        private readonly IRR_EO_LinkDomainService _rrEOLinkService;
        private readonly RR_Task_Link _rr_Task_Link;
        //private readonly IRR_SafetyHazard_LinkDomainService _rr_sh_Service;
        private readonly ISafetyHazard_LinkDomainService _sh_RR_Service;
        //private readonly RR_SafetyHazard_Link _rr_sh_link;
        private readonly SaftyHazard_RR_Link _sh_RR_Link;
        private readonly IRR_EO_DomainService _rr_eo_service;
        private readonly IILA_RR_DomainService _ila_rr_service;
        private readonly IRR_IA_DomainService _rr_ia_Service;
        private readonly RR_IssuingAuthority _rr_IA;
        private readonly RegRequirement_EO_Link _rr_EO_Link;
        private readonly IProviderService _providerService;
        private readonly IILA_TopicDomainService _topicService;
        private readonly Provider _provider;
        private readonly ILA_Topic _topics;
        private readonly ILA_RegRequirement_Link _ILA_rr_Link;
        private readonly Domain.Entities.Core.Task _task;
        private readonly ITaskSubDutyAreaDomainService _sdaService;

        public RegulatoryRequirementService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<RegulatoryRequirementService> localizer, IRegulatoryRequirementDomainService regulatoryRequirementService, ISaftyHazardService saftyHazardService, UserManager<AppUser> userManager, IProcedureDomainService procedureService, IILAServiceDomainService ilaService, IEnablingObjectiveDomainService eOService, ITaskDomainService taskService, IRR_StatusHistoryDomainService rrStatusHistoryService, /*IRR_Proc_LinkDomainService rr_proc_linkService*/ IRR_TaskLinkDomainService rrTaskLinkService, /*IRR_SafetyHazard_LinkDomainService rr_sh_Service,*/ IRR_EO_DomainService rr_eo_service, IILA_RR_DomainService ila_rr_service, IRR_IA_DomainService rr_ia_Service, IRR_EO_LinkDomainService rrEOLinkService, IProviderService providerService, IILA_TopicDomainService topicService, ITaskSubDutyAreaDomainService sdaService, IProc_RR_LinkDomainService proc_rr_linkService, ISafetyHazard_LinkDomainService sh_RR_Service)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _regulatoryRequirementService = regulatoryRequirementService;
            _saftyHazardService = saftyHazardService;
            _userManager = userManager;
            _rr = new RegulatoryRequirement();
            _procedureService = procedureService;
            _ilaService = ilaService;
            _eOService = eOService;
            _taskService = taskService;
            _rrStatusHistoryService = rrStatusHistoryService;
            //_rr_proc_linkService = rr_proc_linkService;
            //_rr_proc_link = new RR_Procedure_Link();
            _proc_rr_Link = new Procedure_RR_Link();
            _rrTaskLinkService = rrTaskLinkService;
            _rr_Task_Link = new RR_Task_Link();
            _rr_EO_Link = new RegRequirement_EO_Link();
            _rrEOLinkService = rrEOLinkService;
            //_rr_sh_link = new RR_SafetyHazard_Link();
            _sh_RR_Link = new SaftyHazard_RR_Link();
            //_rr_sh_Service = rr_sh_Service;
            _rr_eo_service = rr_eo_service;
            _ila_rr_service = ila_rr_service;
            _rr_ia_Service = rr_ia_Service;
            _rr_IA = new RR_IssuingAuthority();
            _providerService = providerService;
            _topicService = topicService;
            _provider = new Provider();
            _topics = new ILA_Topic();
            _ILA_rr_Link = new ILA_RegRequirement_Link();
            _task = new Domain.Entities.Core.Task();
            _sdaService = sdaService;
            _proc_rr_linkService = proc_rr_linkService;
            _sh_RR_Service = sh_RR_Service;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id, RegulatoryRequirementOptions options)
        {
            foreach (var rrId in options.RegulatoryRequirementIds)
            {
                var obj = await GetAsync(rrId);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

                if (result.Succeeded)
                {
                    obj.Activate();
                    var validationResult = await _regulatoryRequirementService.UpdateAsync(obj);
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
        }

        public async Task<RegulatoryRequirement> CreateAsync(RegulatoryRequirementCreateOptions options)
        {
            var obj = (await _regulatoryRequirementService.FindAsync(x => x.Number.Trim().ToLower() == options.Number.Trim().ToLower())).FirstOrDefault();
            if (obj == null)
            {
                var data = options.File;
                var byteData = new byte[] { };
                if (data != null && data.Contains(","))
                {
                    data = data.Substring(data.IndexOf(",") + 1);
                    byteData = Convert.FromBase64String(data);
                }

                obj = new RegulatoryRequirement(options.IssuingAuthorityId, options.Number, options.Title, options.Description, options.RevisionNumber, options.EffectiveDate, byteData, options.HyperLink,options.FileName);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _regulatoryRequirementService.AddAsync(obj);
                var histOptions = new RegulatoryRequirementCreateOptions();
                histOptions.ChangeNotes = options.ChangeNotes;
                histOptions.EffectiveDate = options.EffectiveDate;
                await SaveRRHistory(obj.Id, histOptions);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
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

        public async Task<RegulatoryRequirement> CopyRRWithLinkages(int id, RegulatoryRequirementCreateOptions options)
        {
            //var obj = (await _regulatoryRequirementService.FindQuery(r => r.Title.Trim().ToLower() == options.Title.Trim().ToLower()).ToListAsync()).FirstOrDefault();
            //if (obj != null)
            //{
            //    throw new BadHttpRequestException(message: _localizer["RegulatoryRequirementExists"]);
            //}

            var data = options.File;
            var byteData = new byte[] { };
            if (data != null && data.Contains(","))
            {
                data = data.Substring(data.IndexOf(",") + 1);
                byteData = Convert.FromBase64String(data);
            }

            var obj = new RegulatoryRequirement(options.IssuingAuthorityId, options.Number, options.Title, options.Description, options.RevisionNumber, options.EffectiveDate, byteData, options.HyperLink, options.FileName);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, RegulatoryRequirementOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _regulatoryRequirementService.AddAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    var linkToCopy = await _regulatoryRequirementService
                        .FindQueryWithIncludeAsync(x => x.Id == id,
                        new string[] {
                            nameof(_rr.RR_Task_Links),
                            nameof(_rr.SaftyHazard_RR_Links),
                            nameof(_rr.Procedure_RegRequirement_Links),
                            nameof(_rr.ILA_RegRequirement_Links),
                            nameof(_rr.RegRequirement_EO_Links)
                        }).FirstOrDefaultAsync();
                    obj.RR_Task_Links = linkToCopy.RR_Task_Links.DeepCopy();
                    obj.RR_Task_Links.ToList().ForEach(x => x.Id = 0);
                    obj.SaftyHazard_RR_Links = linkToCopy.SaftyHazard_RR_Links.DeepCopy();
                    obj.SaftyHazard_RR_Links.ToList().ForEach(x => x.Id = 0);
                    obj.Procedure_RegRequirement_Links = linkToCopy.Procedure_RegRequirement_Links.DeepCopy();
                    obj.Procedure_RegRequirement_Links.ToList().ForEach(x => x.Id = 0);
                    obj.ILA_RegRequirement_Links = linkToCopy.ILA_RegRequirement_Links.DeepCopy();
                    obj.ILA_RegRequirement_Links.ToList().ForEach(x => x.Id = 0);
                    obj.RegRequirement_EO_Links = linkToCopy.RegRequirement_EO_Links.DeepCopy();
                    obj.RegRequirement_EO_Links.ToList().ForEach(x => x.Id = 0);
                    await _regulatoryRequirementService.UpdateAsync(obj);
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }

        }

        public async System.Threading.Tasks.Task DeleteAsync(int id, RegulatoryRequirementOptions options)
        {
            foreach (var rrId in options.RegulatoryRequirementIds)
            {
                var obj = await GetAsync(rrId);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

                if (result.Succeeded)
                {
                    obj.Delete();
                    var validationResult = await _regulatoryRequirementService.UpdateAsync(obj);
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
        }

        public async Task<List<RegulatoryRequirement>> GetAsync()
        {
            var obj_list = await _regulatoryRequirementService.AllQuery().Select(r => new RegulatoryRequirement 
            { 
            Id = r.Id,
            IssuingAuthorityId = r.IssuingAuthorityId,
            Number = r.Number,
            Title = r.Title,
            Description = r.Description,
            RevisionNumber = r.RevisionNumber,
            EffectiveDate = r.EffectiveDate,
            FileName = r.FileName,
            HyperLink = r.HyperLink,
            }).Where(r=>r.Deleted != true).ToListAsync();

            if(obj_list != null && obj_list.Count > 0)
            {
                for (int i = 0; i < obj_list.Count; i++)
                {
                    obj_list[i].RR_IssuingAuthority = await _rr_ia_Service.FindQuery(x=>x.Id == obj_list[i].IssuingAuthorityId).FirstOrDefaultAsync();
                }
            }
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, RegulatoryRequirementOperations.Read).Result.Succeeded).ToList();
            return obj_list;
        }

        public async Task<RegulatoryRequirement> GetAsync(int id)
        {
            var obj = await _regulatoryRequirementService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read);
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

        public async System.Threading.Tasks.Task InActiveAsync(int id, RegulatoryRequirementOptions options)
        {
            foreach (var rrId in options.RegulatoryRequirementIds)
            {
                var obj = await GetAsync(rrId);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

                if (result.Succeeded)
                {
                    obj.Deactivate();
                    var validationResult = await _regulatoryRequirementService.UpdateAsync(obj);
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
        }

        public async Task<RegulatoryRequirementCompact> GetRegulatoryRequirementCompactDataAsync(int id)
        {
            var data = await _regulatoryRequirementService.AllQueryWithInclude(new string[] { nameof(_rr.RR_IssuingAuthority) }).Where(x => x.Id == id).Select(x => new { rr = new RegulatoryRequirementCompact(x.Description, x.Title, x.Id, x.IssuingAuthorityId, x.Active, x.RevisionNumber, x.EffectiveDate, x.Number, x.HyperLink, x.FileName), rrIa = new RR_IssuingAuthorityCompact(x.RR_IssuingAuthority.Description, x.RR_IssuingAuthority.Title, x.RR_IssuingAuthority.Id, x.RR_IssuingAuthority.Active) }).FirstOrDefaultAsync();
            var rrCompact = new RegulatoryRequirementCompact();
            rrCompact = data.rr;
            rrCompact.IssuingAuthorityCompact = data.rrIa;
            if (rrCompact == null)
            {
                throw new QTDServerException(_localizer["RegulatoryRequirementDataNotFound"]);
            }
            else
            {
                return rrCompact;
            }
        }

        public async Task<RegulatoryRequirement> LinkILA(int rrId, RR_ILA_LinkOptions options)
        {
            var regulatoryRequirement = await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.ILA_RegRequirement_Links) });
            foreach (var id in options.IlaIds)
            {
                var ila = await _ilaService.GetAsync(id);

                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, regulatoryRequirement, ILAOperations.Update);
                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, ILAOperations.Read);
                if (rrResult.Succeeded && ilaResult.Succeeded)
                {
                    regulatoryRequirement.LinkILA(ila);
                    var validationResult = await _regulatoryRequirementService.UpdateAsync(regulatoryRequirement);
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

            return regulatoryRequirement;
        }

        public async System.Threading.Tasks.Task UnlinkILA(int rrId, RR_ILA_LinkOptions options)
        {
            var rr = await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.ILA_RegRequirement_Links) });

            foreach (var ilaId in options.IlaIds)
            {
                var ila = await _ilaService.GetAsync(ilaId);

                if (ila == null)
                {
                    throw new QTDServerException(_localizer["ILANotFound"]);
                }

                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Update);

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Read);

                if (rrResult.Succeeded && ilaResult.Succeeded)
                {
                    rr.UnlinkIla(ila);
                    await _regulatoryRequirementService.UpdateAsync(rr);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            var tempOptions = new RegulatoryRequirementCreateOptions();
            tempOptions.ChangeNotes = options.IlaIds.Length + " ILA UnLinked to RR";
            tempOptions.EffectiveDate = options.EffectiveDate;
            await SaveRRHistory(rrId, tempOptions);
        }

        public async Task<List<ILAProviderDataVM>> GetProviderWithILAs()
        {
            var providers = (await _providerService.GetCompactedProvider()).OrderBy(r=>!r.IsPriority).ThenBy(r=>r.Name).ToList();
            var ilas = await _ilaService.GetCompactedILA();
            foreach(var prov in providers)
            {
                prov.ILAs = ilas.Where(w => w.ProviderId == prov.Id).OrderBy(r=>r.Name).ToList();
            }
            var listILAProviderVM = providers.Select(prov => new ILAProviderDataVM(prov.Id, prov.Name, prov.Active,prov.ILAs.Select(ila => new ILADetailsVM(ila.Id, ila.Name, ila.Number, ila.Active)).ToList()));
            return listILAProviderVM.ToList();
        }

        public async Task<List<ILATopicDataVM>> GetTopicWithILAs()
        {
            var topicILAs = await _topicService.AllWithIncludeAsync(new string[] { "ILA_Topic_Links.ILA" });
            var listILAtopicVM = topicILAs.Select(topic =>
                                    new ILATopicDataVM(topic.Id, topic.Name, topic.Active, topic.ILA_Topic_Links.Select(ilaLink =>
                                    new ILADetailsVM(ilaLink.ILA.Id, ilaLink.ILA.Name, ilaLink.ILA.Number, ilaLink.ILA.Active)).ToList())
            ).ToList();
            return listILAtopicVM.ToList();
        }

        public async Task<List<ILAWithCountOptions>> GetLinkedILAs(int id)
        {
            var links = await _ila_rr_service.FindWithIncludeAsync(x => x.RegulatoryRequirementId == id, new string[] { nameof(_ILA_rr_Link.ILA) });
            List<Domain.Entities.Core.ILA> ilaList = new List<Domain.Entities.Core.ILA>();
            ilaList.AddRange(links.Select(x => x.ILA));
            List<ILAWithCountOptions> iLAWithCount = new List<ILAWithCountOptions>();
            foreach (var ila in ilaList)
            {
                var data = await _ila_rr_service.GetCount(x => x.ILAId == ila.Id);
                iLAWithCount.Add(new ILAWithCountOptions(ila.Number, ila.Name, ila.Id, data, ila.Active));
            }

            return iLAWithCount;
        }

        public async Task<List<RegulatoryRequirement>> GetRRILAIsLinkedTo(int id)
        {
            var data = await _ila_rr_service.AllQueryWithInclude(new string[] { nameof(_ILA_rr_Link.RegulatoryRequirement) }).Where(x => x.ILAId == id).Select(x => x.RegulatoryRequirement).ToListAsync();
            return data;
        }

        public async Task<RegulatoryRequirement> LinkEnablingObjective(int rrId, RegRequirement_EO_LinkOptions options)
        {
            var regulatoryRequirement = await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.RegRequirement_EO_Links) });
            foreach (var id in options.EOIds)
            {
                var eo = await _eOService.GetAsync(id);

                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, regulatoryRequirement, ILAOperations.Update);
                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, ILAOperations.Read);
                if (rrResult.Succeeded && eoResult.Succeeded)
                {
                    regulatoryRequirement.LinkEO(eo);
                    var validationResult = await _regulatoryRequirementService.UpdateAsync(regulatoryRequirement);
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

            return regulatoryRequirement;
        }

        public async System.Threading.Tasks.Task UnlinkEnablingObjective(int rrId, RR_EO_LinkOptions options)
        {
            var rr = await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.RegRequirement_EO_Links) });

            foreach (var eoId in options.EOIds)
            {
                var eo = await _eOService.GetAsync(eoId);

                if (eo == null)
                {
                    throw new QTDServerException(_localizer["TaskNotFound"]);
                }

                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Update);

                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, AuthorizationOperations.Read);

                if (rrResult.Succeeded && eoResult.Succeeded)
                {
                    rr.UnlinkEO(eo);
                    await _regulatoryRequirementService.UpdateAsync(rr);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            var tempOptions = new RegulatoryRequirementCreateOptions();
            tempOptions.ChangeNotes = options.EOIds.Length + "RR Unlinked from EO";
            tempOptions.EffectiveDate = options.EffectiveDate;
            await SaveRRHistory(rrId, tempOptions);
        }

        public async Task<RegulatoryRequirement> LinkTask(int rrId, RR_Task_LinkOptions tasks)
        {
            var rr = await _regulatoryRequirementService.FindQuery(x => x.Id == rrId).FirstOrDefaultAsync();

            foreach (var taskId in tasks.TaskIds)
            {
                var task = await _taskService.GetAsync(taskId);
                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Update);

                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, AuthorizationOperations.Read);

                if (rrResult.Succeeded && taskResult.Succeeded)
                {
                    rr.LinkTask(task);
                    await _regulatoryRequirementService.UpdateAsync(rr);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return rr;
        }

        public async System.Threading.Tasks.Task UnlinkTask(int rrId, RR_Task_LinkOptions tasks)
        {
            var rr = await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.RR_Task_Links) });

            foreach (var taskId in tasks.TaskIds)
            {
                var task = await _taskService.GetAsync(taskId);

                if (task == null)
                {
                    throw new QTDServerException(_localizer["TaskNotFound"]);
                }

                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Update);

                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, AuthorizationOperations.Read);

                if (rrResult.Succeeded && taskResult.Succeeded)
                {
                    rr.UnlinkTask(task);
                    await _regulatoryRequirementService.UpdateAsync(rr);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            var tempOptions = new RegulatoryRequirementCreateOptions();
            tempOptions.ChangeNotes = tasks.TaskIds.Length + "Task UnLinked to RR ";
            tempOptions.EffectiveDate = tasks.EffectiveDate;
            await SaveRRHistory(rrId, tempOptions);
        }

        public async Task<RegulatoryRequirement> LinkProcedure(int rrId, RR_Procedure_LinkOptions options)
        {
            var rr = await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.Procedure_RegRequirement_Links) });
            foreach (var procId in options.ProcedureIds)
            {
                var proc = await _procedureService.GetAsync(procId);
                if (proc == null)
                {
                    throw new QTDServerException(_localizer["ProcedureNotFound"]);
                }

                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Update);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, proc, AuthorizationOperations.Read);

                if (rrResult.Succeeded && procResult.Succeeded)
                {
                    rr.LinkProcedure(proc);
                    await _regulatoryRequirementService.UpdateAsync(rr);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return rr;
        }

        public async System.Threading.Tasks.Task UnlinkProcedure(int rrId, RR_Procedure_LinkOptions options)
        {
            var rr = await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.Procedure_RegRequirement_Links) });
            foreach (var procId in options.ProcedureIds)
            {
                var proc = await _procedureService.GetAsync(procId);

                if (proc == null)
                {
                    throw new QTDServerException(_localizer["ProcedureNotFound"]);
                }

                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Update);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, proc, AuthorizationOperations.Read);

                if (rrResult.Succeeded && procResult.Succeeded)
                {
                    rr.UnlinkProcedure(proc);
                    await _regulatoryRequirementService.UpdateAsync(rr);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<RegulatoryRequirement> LinkSafetyHazardAsync(int id, RR_SafetyHazard_LinkOptions safetyHazards)
        {
            var rr = await _regulatoryRequirementService.AllQueryWithInclude(new string[] { nameof(_rr.SaftyHazard_RR_Links) }).Where(x => x.Id == id).FirstOrDefaultAsync(); ;

            foreach (var safetyHazardId in safetyHazards.SafetyHazardIds)
            {
                var sh = await _saftyHazardService.GetAsync(safetyHazardId);

                if (sh == null)
                {
                    throw new QTDServerException(_localizer["SafetyHazardNotFound"]);
                }

                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Update);

                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, AuthorizationOperations.Read);

                if (rrResult.Succeeded && shResult.Succeeded)
                {
                    rr.LinkSafetyHazard(sh);

                    await _regulatoryRequirementService.UpdateAsync(rr);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return rr;
        }

        public async System.Threading.Tasks.Task UnlinkSafetyHazardAsync(int id, RR_SafetyHazard_LinkOptions safetyHazards)
        {
            var rr = await _regulatoryRequirementService.AllQueryWithInclude(new string[] { nameof(_rr.SaftyHazard_RR_Links) }).Where(x => x.Id == id).FirstOrDefaultAsync();

            foreach (var safetyHazardId in safetyHazards.SafetyHazardIds)
            {
                var sh = await _saftyHazardService.GetAsync(safetyHazardId);

                if (sh == null)
                {
                    throw new QTDServerException(_localizer["SafetyHazardNotFound"]);
                }

                var rrResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr, AuthorizationOperations.Update);

                var shResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, AuthorizationOperations.Read);

                if (rrResult.Succeeded && shResult.Succeeded)
                {
                    rr.UnlinkSafetyHazard(sh);

                    await _regulatoryRequirementService.UpdateAsync(rr);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<List<SafetyHazardWithLinkCount>> GetSafetyHazardLinkedToRRWithCount(int id)
        {
            var data = await _sh_RR_Service.AllQueryWithInclude(new string[] { nameof(_sh_RR_Link.SaftyHazard) }).Where(x => x.RegulatoryRequirementId == id).Select(x => x.SaftyHazard).ToListAsync();
            List<SafetyHazardWithLinkCount> list = new List<SafetyHazardWithLinkCount>();
            foreach (var item in data)
            {
                var count = await _sh_RR_Service.GetCount(x => x.SafetyHazardId == item.Id);
                var sh = new SafetyHazardWithLinkCount(item.Id, item.Title, item.Number, count, item.Active);
                list.Add(sh);
            }

            return list;
        }

        public async Task<RegulatoryRequirement> UpdateAsync(int id, RegulatoryRequirementCreateOptions options)
        {
            var obj = await _regulatoryRequirementService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);
            var prev = await _regulatoryRequirementService.FindQuery(x => x.Number.Trim().ToLower() == options.Number.Trim().ToLower() && (x.Id != obj.Id || x.IssuingAuthorityId != options.IssuingAuthorityId)).FirstOrDefaultAsync();
            if (prev != null)
            {
                throw new BadHttpRequestException(message: _localizer["Regulatory Requirement Already Exists"]);
            }

            if (result.Succeeded)
            {
                obj.Number = options.Number;
                obj.HyperLink = options.HyperLink;
                obj.RevisionNumber = options.RevisionNumber;
                obj.Description = options.Description;
                obj.EffectiveDate = options.EffectiveDate;
                var data = options.File;
                var byteData = new byte[] { };
                if (data != null && data.Contains(","))
                {
                    data = data.Substring(data.IndexOf(",") + 1);
                    obj.Uploads = Convert.FromBase64String(data);
                }
                else
                {
                    obj.Uploads = byteData;
                }

                obj.Title = options.Title;
                obj.FileName = options.FileName;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                var validationResult = await _regulatoryRequirementService.UpdateAsync(obj);
                await SaveRRHistory(obj.Id, options);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
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

        public async System.Threading.Tasks.Task SaveRRHistory(int id, RegulatoryRequirementCreateOptions obj)
        {
            var hist = new RR_StatusHistory();
            hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            hist.CreatedDate = DateTime.Now;
            hist.ChangeEffectiveDate = (DateTime)obj.EffectiveDate;
            hist.ChangeNotes = obj.ChangeNotes;
            hist.RegulatoryRequirementId = id;

            var histValidation = await _rrStatusHistoryService.AddAsync(hist);
            if (!histValidation.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', histValidation.Errors));
            }
        }

        public async Task<List<SaftyHazard_RR_Link>> GetSafetyHazardLinkedToRR(int rrId)
        {
            var obj = (await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.SaftyHazard_RR_Links) })).SaftyHazard_RR_Links.ToList();
            return obj;
        }

        public async Task<SaftyHazard_RR_Link> GetSafetyHazardLink(int rrId, int id)
        {
            var obj = (await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.SaftyHazard_RR_Links) })).SaftyHazard_RR_Links.Where(x => x.Id == id).FirstOrDefault();
            return obj;
        }

        public async Task<List<ProceduresWithLinkCount>> GetProcedureLinkedToRR(int rrId)
        {

            var links = await _proc_rr_linkService.AllQueryWithInclude(new string[] { nameof(_proc_rr_Link.Procedure) }).Where(x => x.RegulatoryRequirementId == rrId).Select(x => new { x.Procedure.Id, x.Procedure.Title, x.Procedure.Number, x.Procedure.Active }).ToListAsync();
            List<ProceduresWithLinkCount> procList = new List<ProceduresWithLinkCount>();
            foreach (var data in links)
            {
                var count = await _proc_rr_linkService.GetCount(x => x.ProcedureId == data.Id);
                procList.Add(new ProceduresWithLinkCount(data.Id, count, data.Number, data.Title, data.Active));
            }

            return procList;
        }

        public async Task<List<RegulatoryRequirementCompact>> GetRRLinkedWithProcedure(int id)
        {
            var data = await _proc_rr_linkService.AllQueryWithInclude(new string[] { nameof(_proc_rr_Link.RegulatoryRequirement) }).Where(x => x.ProcedureId == id)
                .Select(x => new RegulatoryRequirementCompact(
                    x.RegulatoryRequirement.Description,
                    x.RegulatoryRequirement.Title,
                    x.RegulatoryRequirementId,
                    x.RegulatoryRequirement.IssuingAuthorityId,
                    x.RegulatoryRequirement.Active,
                    x.RegulatoryRequirement.RevisionNumber,
                    x.RegulatoryRequirement.EffectiveDate,
                    x.RegulatoryRequirement.Number,
                    x.RegulatoryRequirement.HyperLink,
                    x.RegulatoryRequirement.FileName)).ToListAsync();

            return data;
        }

        public async Task<List<RegulatoryRequirement>> GetEnablingObjectiveLinkedToRR(int id)
        {
            var data = await _rrEOLinkService.AllQueryWithInclude(new string[] { nameof(_rr_EO_Link.RegulatoryRequirement) }).Where(x => x.EOID == id).Select(x => x.RegulatoryRequirement).ToListAsync();
            return data;
        }

        public async Task<RegRequirement_EO_Link> GetEnablingObjectiveLink(int rrId, int id)
        {
            var obj = (await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.RegRequirement_EO_Links) })).RegRequirement_EO_Links.Where(x => x.Id == id).FirstOrDefault();
            return obj;
        }

        public async Task<List<RegulatoryRequirementWithLinkCount>> GetLinkedEOsWithCount(int id)
        {
            var links = await _rrEOLinkService.FindWithIncludeAsync(x => x.RegulatoryRequirementId == id, new string[] { nameof(_rr_EO_Link.EO) });
            List<Domain.Entities.Core.EnablingObjective> eoList = new List<Domain.Entities.Core.EnablingObjective>();
            eoList.AddRange(links.Select(x => x.EO).OrderBy(x=>x.Number));

            List<RegulatoryRequirementWithLinkCount> eOWithCount = new List<RegulatoryRequirementWithLinkCount>();
            foreach (var eo in eoList)
            {
                var data = await _rrEOLinkService.GetCount(x => x.EOID == eo.Id);
                eOWithCount.Add(new RegulatoryRequirementWithLinkCount(Convert.ToString(eo.Number), eo.Description, eo.Id, data, eo.Active));
            }

            return eOWithCount;
        }

        public async Task<List<RR_Task_Link>> GetTaskLinkedToRR(int rrId)
        {
            var obj = (await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.RR_Task_Links) })).RR_Task_Links.ToList();
            return obj;
        }

        public async Task<List<RegulatoryRequirementCompact>> getRRLinkedToTask(int id)
        {
            var data = await _rrTaskLinkService.AllQueryWithInclude(new string[] { nameof(_rr_Task_Link.RegulatoryRequirement) }).Where(x => x.TaskId == id)
                .Select(x => new RegulatoryRequirementCompact(
                    x.RegulatoryRequirement.Description,
                    x.RegulatoryRequirement.Title,
                    x.RegRequirementId,
                    x.RegulatoryRequirement.IssuingAuthorityId,
                    x.RegulatoryRequirement.Active,
                    x.RegulatoryRequirement.RevisionNumber,
                    x.RegulatoryRequirement.EffectiveDate,
                    x.RegulatoryRequirement.Number,
                    x.RegulatoryRequirement.HyperLink,
                    x.RegulatoryRequirement.FileName)).ToListAsync();
            return data;
        }

        public async Task<List<RegulatoryRequirementCompact>> getRRLinkedToSH(int id)
        {
            var data = await _sh_RR_Service.AllQueryWithInclude(new string[] { nameof(_rr_Task_Link.RegulatoryRequirement) }).Where(x => x.SafetyHazardId == id)
                .Select(x => new RegulatoryRequirementCompact(
                    x.RegulatoryRequirement.Description,
                    x.RegulatoryRequirement.Title,
                    x.RegulatoryRequirementId,
                    x.RegulatoryRequirement.IssuingAuthorityId,
                    x.RegulatoryRequirement.Active,
                    x.RegulatoryRequirement.RevisionNumber,
                    x.RegulatoryRequirement.EffectiveDate,
                    x.RegulatoryRequirement.Number,
                    x.RegulatoryRequirement.HyperLink,
                    x.RegulatoryRequirement.FileName)).ToListAsync();
            return data;
        }

        public async Task<List<TaskWithCountOptions>> GetLinkedTasksWithCount(int id)
        {
            var links = await _rrTaskLinkService.FindWithIncludeAsync(x => x.RegRequirementId == id, new string[] { nameof(_rr_Task_Link.Task) });
            
            
            
            List<Domain.Entities.Core.Task> taskList = new List<Domain.Entities.Core.Task>();
            taskList.AddRange(links.Select(x => x.Task));

            // return taskList;
            List<TaskWithCountOptions> taskWithCount = new List<TaskWithCountOptions>();
            foreach (var task in taskList)
            {
                var data = await _rrTaskLinkService.GetCount(x => x.TaskId == task.Id);
                var sda = await _sdaService.FindQueryWithIncludeAsync(x => x.Id == task.SubdutyAreaId, new string[] { "DutyArea" }).FirstOrDefaultAsync();
                var num  = sda.DutyArea.Number.ToString() + '.' + sda.SubNumber.ToString() + '.' + task.Number.ToString();

                //var num = taskNumber.SubdutyArea.DutyAreaId + taskNumber.SubdutyArea.Id + task.Number;
                taskWithCount.Add(new TaskWithCountOptions(num, task.Description, task.Id, data, task.Active, 0));
            }

            return taskWithCount;
        }

        public async Task<RR_Task_Link> GetTaskLink(int rrId, int id)
        {
            var obj = (await _regulatoryRequirementService.GetWithIncludeAsync(rrId, new string[] { nameof(_rr.RR_Task_Links) })).RR_Task_Links.Where(x => x.Id == id).FirstOrDefault();
            return obj;
        }

        public async Task<RR_StatsVM> GetStatsCount()
        {
            var eoIds = await _regulatoryRequirementService.AllQuery().Where(s => s.Active == true).Select(x => x.Id).ToListAsync();
            var linkedTasks = await _rrTaskLinkService.AllQuery().Select(x => x.RegRequirementId).ToListAsync();
            var linkedEOs = await _rr_eo_service.AllQuery().Select(x => x.RegulatoryRequirementId).ToListAsync();
            var linkedSHs = await _sh_RR_Service.AllQuery().Select(x => x.RegulatoryRequirementId).ToListAsync();
            var linkedProcs = await _proc_rr_linkService.AllQuery().Select(x => x.RegulatoryRequirementId).ToListAsync();
            var linkedILAs = await _ila_rr_service.AllQuery().Select(x => x.RegulatoryRequirementId).ToListAsync();

            var stats = new RR_StatsVM()
            {
                IAActive = await _rr_ia_Service.GetCount(x => x.Active == true),
                IAInactive = await _rr_ia_Service.GetCount(x => x.Active == false),
                RRActive = await _regulatoryRequirementService.GetCount(x => x.Active == true),
                RRInactive = await _regulatoryRequirementService.GetCount(x => x.Active == false),
                RRNotLinkedToTasks = await _regulatoryRequirementService.GetCount(x => !linkedTasks.Contains(x.Id) && x.Active == true),
                RRNotLinkedToEOs = await _regulatoryRequirementService.GetCount(x => !linkedEOs.Contains(x.Id) && x.Active),
                RRNotLinkedToSHs = await _regulatoryRequirementService.GetCount(x => !linkedSHs.Contains(x.Id) && x.Active),
                RRNotLinkedToProcs = await _regulatoryRequirementService.GetCount(x => !linkedProcs.Contains(x.Id) && x.Active),
                RRNotLinkedToILAs = await _regulatoryRequirementService.GetCount(x => !linkedILAs.Contains(x.Id) && x.Active),
            };

            return stats;
        }

        public async Task<List<RR_IssuingAuthority>> GetNotLinkedTo(string option)
        {
            var notLinkedRR = new List<RR_IssuingAuthority>();
            List<int> linkedRRIds = new List<int>();
            List<int> notLinkedRRIds = new List<int>();

            switch (option.ToLower().Trim())
            {
                case "ilas":
                    linkedRRIds = await _ila_rr_service.FindQuery(x=>x.Active==true).Select(x => x.RegulatoryRequirementId).Distinct().ToListAsync();
                    notLinkedRRIds = await _regulatoryRequirementService.FindQuery(x => !linkedRRIds.Contains(x.Id) && x.Active == true).Select(x => x.Id).ToListAsync();
                    break;
                case "tasks":
                    linkedRRIds = await _rrTaskLinkService.FindQuery(x => x.Active == true).Select(x => x.RegRequirementId).Distinct().ToListAsync();
                    notLinkedRRIds = await _regulatoryRequirementService.FindQuery(x => !linkedRRIds.Contains(x.Id) && x.Active == true).Select(x => x.Id).ToListAsync();
                    break;
                case "procedures":
                    linkedRRIds = await _proc_rr_linkService.FindQuery(x => x.Active == true).Select(x => x.RegulatoryRequirementId).Distinct().ToListAsync();
                    notLinkedRRIds = await _regulatoryRequirementService.FindQuery(x => !linkedRRIds.Contains(x.Id) && x.Active == true).Select(x => x.Id).ToListAsync();
                    break;
                case "safety hazards":
                    linkedRRIds = await _sh_RR_Service.FindQuery(x => x.Active == true).Select(x => x.RegulatoryRequirementId).Distinct().ToListAsync();
                    notLinkedRRIds = await _regulatoryRequirementService.FindQuery(x => !linkedRRIds.Contains(x.Id) && x.Active == true).Select(x => x.Id).ToListAsync();
                    break;
                case "enabling objectives":
                    linkedRRIds = await _rr_eo_service.FindQuery(x => x.Active == true).Select(x => x.RegulatoryRequirementId).Distinct().ToListAsync();
                    notLinkedRRIds = await _regulatoryRequirementService.FindQuery(x => !linkedRRIds.Contains(x.Id) && x.Active == true).Select(x => x.Id).ToListAsync();
                    break;
            }

            notLinkedRR = await _rr_ia_Service.AllQuery().OrderBy(x=>x.Title).ToListAsync();

            foreach (var ia in notLinkedRR)
            {
                ia.RegulatoryRequirements = await _regulatoryRequirementService.FindQuery(x => x.IssuingAuthorityId == ia.Id && notLinkedRRIds.Contains(x.Id)).OrderBy(x=>x.Number).ThenBy(x=>x.Title).ToListAsync();
            }

            return notLinkedRR;
        }

        public async Task<List<RRLatestActivityVM>> GetHistoryAsync()
        {
            var regRequirements = await _regulatoryRequirementService.AllQuery().IgnoreQueryFilters().ToListAsync();
            var history = await _rrStatusHistoryService.AllQuery().ToListAsync();
            var users = await _userManager.Users.ToListAsync();

            var latestactivity = from rr in regRequirements
                                 join h in history on rr.Id equals h.RegulatoryRequirementId
                                 join u in _userManager.Users on h.CreatedBy equals u.Email
                                 select new RRLatestActivityVM
                                 {
                                     Id = rr.Id,
                                     ActivityDesc = h.ChangeNotes,
                                     Title = rr.Number + " - " + rr.Title,
                                     CreatedBy = u.Email,
                                     CreatedDate = h.CreatedDate,
                                 };

            return latestactivity.OrderByDescending(x=>x.CreatedDate).ToList();
        }

        public async System.Threading.Tasks.Task RegulationDeactivateAsync(int id, RegulatoryRequirementOptions options)
        {
            var procedure = await _regulatoryRequirementService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, RegulatoryRequirementOperations.Delete);
            if (result.Succeeded)
            {
                procedure.Deactivate();

                var validationResult = await _regulatoryRequirementService.UpdateAsync(procedure);
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

        public async System.Threading.Tasks.Task RegulationActivateAsync(int id, RegulatoryRequirementOptions options)
        {
            var procedure = await _regulatoryRequirementService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, RegulatoryRequirementOperations.Delete);
            if (result.Succeeded)
            {
                procedure.Activate();

                var validationResult = await _regulatoryRequirementService.UpdateAsync(procedure);
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

        public async Task<List<RegulatoryRequirement>> GetRRActiveInactive(string option)
        {
            var rrList = new List<RegulatoryRequirement>();

            switch (option.ToLower().Trim())
            {
                case "rractive":
                    rrList = await _regulatoryRequirementService.FindQuery(x => x.Active == true).Select(s => new RegulatoryRequirement
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Number = s.Number,
                    }).ToListAsync();
                    break;
                case "rrinactive":
                    rrList = await _regulatoryRequirementService.FindQuery(x => x.Active == false).Select(s => new RegulatoryRequirement
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Number = s.Number,
                    }).ToListAsync();
                    break;

            }

            return rrList.OrderBy(x=>x.Number).ThenBy(x=>x.Title).ToList();

        }

        public async Task<List<RR_IssuingAuthority>> GetIAActiveInactive(string option)
        {
            var rrList = new List<RR_IssuingAuthority>();

            switch (option.ToLower().Trim())
            {
                case "catactive":
                    rrList = await _rr_ia_Service.FindQuery(x => x.Active == true).Select(s => new RR_IssuingAuthority
                    {
                        Id = s.Id,
                        Title = s.Title
                    }).ToListAsync();
                    break;
                case "catinactive":
                    rrList = await _rr_ia_Service.FindQuery(x => x.Active == false).Select(s => new RR_IssuingAuthority
                    {
                        Id = s.Id,
                        Title = s.Title
                    }).ToListAsync();
                    break;
               
            }

            return rrList.OrderBy(x=>x.Title).ToList();
        }







    }
}
