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
using Microsoft.Extensions.Options;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.RR_IssuingAuthority;
using IRR_IssuingAuthority_StatusHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IRR_IssuingAuthority_StatusHistoryService;
using IRR_IssuingAuthorityDomainService = QTD2.Domain.Interfaces.Service.Core.IRR_IssuingAuthorityService;

namespace QTD2.Application.Services.Shared
{
    public class RR_IssuingAuthorityService : IRR_IssuingAuthorityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<RR_IssuingAuthorityService> _localizer;
        private readonly IRR_IssuingAuthorityDomainService _rr_IssuingAuthorityService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RR_IssuingAuthority _rrIssuingAuthority;
        private readonly IRR_IssuingAuthority_StatusHistoryDomainService _rrIAStatusHistoryService;
        private readonly IRegulatoryRequirementService _regulatoryRequirementService;

        public RR_IssuingAuthorityService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<RR_IssuingAuthorityService> localizer, IRR_IssuingAuthorityDomainService rr_IssuingAuthorityService, UserManager<AppUser> userManager, IRR_IssuingAuthority_StatusHistoryDomainService rrIAStatusHistoryService, IRegulatoryRequirementService regulatoryRequirementService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _rr_IssuingAuthorityService = rr_IssuingAuthorityService;
            _userManager = userManager;
            _rrIssuingAuthority = new RR_IssuingAuthority();
            _rrIAStatusHistoryService = rrIAStatusHistoryService;
            _regulatoryRequirementService = regulatoryRequirementService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            //var obj = await GetAsync(id);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            //if (result.Succeeded)
            //{
            //    obj.Activate();

            //    var validationResult = await _rr_IssuingAuthorityService.UpdateAsync(obj);
            //    if (!validationResult.IsValid)
            //    {
            //        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //    }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            //}
            var proc_issu = await _rr_IssuingAuthorityService.GetWithIncludeAsync(id, new string[] { nameof(_rrIssuingAuthority.RegulatoryRequirements) });
            List<Domain.Entities.Core.RegulatoryRequirement> procList = new List<Domain.Entities.Core.RegulatoryRequirement>();
            procList.AddRange(proc_issu.RegulatoryRequirements);
            if (proc_issu != null)
            {
                proc_issu.Activate();
                await _rr_IssuingAuthorityService.UpdateAsync(proc_issu);

                foreach (var proc in procList)
                {
                    var options = new RegulatoryRequirementOptions();
                    options.RegulatoryRequirementId = proc.Id;
                    options.EffectiveDate = proc_issu.EffectiveDate;
                    options.ChangeNotes = "Inactive Due to Issuing Authority Inactive";
                    await _regulatoryRequirementService.RegulationActivateAsync(proc.Id, options);

                }
            }
            else
            {
                throw new QTDServerException(_localizer["Regulation_IssuingAuthorityNotFound"]);
            }
        }

        public async Task<RR_IssuingAuthority> CreateAsync(RR_IssuingAuthorityCreateOptions options)
        {
            var obj = (await _rr_IssuingAuthorityService.FindAsync(x => x.Title == options.Title)).FirstOrDefault();
            if (obj == null)
            {
                obj = new RR_IssuingAuthority(options.Title, options.Description, options.Website, options.EffectiveDate, options.Notes);
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
                var validationResult = await _rr_IssuingAuthorityService.AddAsync(obj);
                await SaveRRIAHistory(obj.Id, obj);
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

        public async System.Threading.Tasks.Task SaveRRIAHistory(int id, RR_IssuingAuthority obj)
        {
            var hist = new RR_IssuingAuthority_StatusHistory();
            hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id; ;
            hist.CreatedDate = DateTime.Now;
            hist.ChangeEffectiveDate = (DateTime)obj.EffectiveDate;
            hist.RRIssuingAuthorityId = obj.Id;
            hist.ChangeNotes = obj.Notes;
            var histValidation = await _rrIAStatusHistoryService.AddAsync(hist);
            if (!histValidation.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', histValidation.Errors));
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _rr_IssuingAuthorityService.UpdateAsync(obj);
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

        public async Task<List<RR_IssuingAuthority>> GetAsync()
        {
            var obj_list = await _rr_IssuingAuthorityService.AllWithIncludeAsync(new string[] { nameof(_rrIssuingAuthority.RegulatoryRequirements) });
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.OrderBy(x=>x.Title).ToList();
        }

        public async Task<List<RR_IssuingAuthority>> GetComapnctRR_IAWithoutIncludesAsync()
        {
            var rrIAs = await _rr_IssuingAuthorityService.GetAllCompacted();
            rrIAs = rrIAs.Where(w => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, w, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return rrIAs?.ToList();
        }

        public async Task<RR_IssuingAuthority> GetAsync(int id)
        {
            var obj = await _rr_IssuingAuthorityService.GetWithIncludeAsync(id, new string[] { nameof(_rrIssuingAuthority.RegulatoryRequirements) });
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

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            //var obj = await GetAsync(id);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            //if (result.Succeeded)
            //{
            //    obj.Deactivate();

            //    var validationResult = await _rr_IssuingAuthorityService.UpdateAsync(obj);
            //    if (!validationResult.IsValid)
            //    {
            //        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //    }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            //}
            var proc_issu = await _rr_IssuingAuthorityService.GetWithIncludeAsync(id, new string[] { nameof(_rrIssuingAuthority.RegulatoryRequirements) });
            List<Domain.Entities.Core.RegulatoryRequirement> procList = new List<Domain.Entities.Core.RegulatoryRequirement>();
            procList.AddRange(proc_issu.RegulatoryRequirements);
            if (proc_issu != null)
            {
                proc_issu.Deactivate();
                await _rr_IssuingAuthorityService.UpdateAsync(proc_issu);

                foreach (var proc in procList)
                {
                    var options = new RegulatoryRequirementOptions();
                    options.RegulatoryRequirementId = proc.Id;
                    options.EffectiveDate = proc_issu.EffectiveDate;
                    options.ChangeNotes = "Inactive Due to Issuing Authority Inactive";
                    await _regulatoryRequirementService.RegulationDeactivateAsync(proc.Id, options);

                }
            }
            else
            {
                throw new QTDServerException(_localizer["Regulation_IssuingAuthorityNotFound"]);
            }

        }

        public async Task<RR_IssuingAuthority> UpdateAsync(int id, RR_IssuingAuthorityCreateOptions options)
        {
            var obj = await _rr_IssuingAuthorityService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            var existing = await _rr_IssuingAuthorityService.FindQuery(x => x.Title.Trim().ToLower() == options.Title.Trim().ToLower() && x.Id != id).FirstOrDefaultAsync();
            if (existing != null)
            {
                throw new QTDServerException(_localizer["RRIssuingAuthorityAlreadyExists"]);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                // Todo Update logic
                obj.Title = options.Title;
                obj.Description = options.Description;
                obj.EffectiveDate = options.EffectiveDate;
                obj.Notes = options.Notes;
                obj.Website = options.Website;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                var validationResult = await _rr_IssuingAuthorityService.UpdateAsync(obj);

                await SaveRRIAHistory(obj.Id, obj);
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

        public async Task<List<RR_IssuingAuthorityCompact>> getWithRR()
        {
            var issuingAuthorities = await _rr_IssuingAuthorityService.AllQueryWithInclude(new string[] { nameof(_rrIssuingAuthority.RegulatoryRequirements) }).OrderBy(x=>x.Title).Select(x => new { rrIA = new RR_IssuingAuthorityCompact(x.Description, x.Title, x.Id, x.Active), rr = x.RegulatoryRequirements.OrderBy(x=>x.Number).Select(y => new RegulatoryRequirementCompact(y.Description, y.Title, y.Id, y.IssuingAuthorityId, y.Active, y.RevisionNumber, y.EffectiveDate, y.Number, y.HyperLink, y.FileName)).ToList() }).ToListAsync();
            List<RR_IssuingAuthorityCompact> rr_IA_Comp_List = new List<RR_IssuingAuthorityCompact>();
            foreach (var data in issuingAuthorities)
            {
                var iACompact = new RR_IssuingAuthorityCompact();
                iACompact.Description = data.rrIA.Description;
                iACompact.Active = data.rrIA.Active;
                iACompact.Title = data.rrIA.Title;
                iACompact.Id = data.rrIA.Id;
                iACompact.regulatoryRequirementCompacts.AddRange(data.rr);
                rr_IA_Comp_List.Add(iACompact);
            }

            return rr_IA_Comp_List;
        }
    }
}
