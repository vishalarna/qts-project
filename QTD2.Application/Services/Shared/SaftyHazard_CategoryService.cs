using System;
using System.Collections.Generic;
using System.Linq;
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
using QTD2.Infrastructure.Model.SafetyHazard_Category;
using QTD2.Infrastructure.Model.SafetyHazard_CategoryHistory;
using QTD2.Infrastructure.Model.SaftyHazard;
using ISafetyHazard_CategoryHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_CategoryHistoryService;
using ISaftyHazard_CategoryDomainService = QTD2.Domain.Interfaces.Service.Core.ISaftyHazard_CategoryService;
using ISaftyHazardDomainService = QTD2.Domain.Interfaces.Service.Core.ISaftyHazardService;
using IProcedure_SaftyHazard_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_SaftyHazard_LinkService;
using ISaftyHazard_EO_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_EO_LinkService;
using ISaftyHazard_RR_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISaftyHazard_RR_LinkService;
using IILA_SafetyHazard_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_SafetyHazard_LinkService;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class SaftyHazard_CategoryService : ISaftyHazard_CategoryService
    {
        private readonly ISaftyHazard_CategoryDomainService _sh_cat_service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SaftyHazard_CategoryService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly SaftyHazard_Category _saftyHazardCat;
        private readonly SaftyHazard _saftyHazard;
        private readonly ISaftyHazardDomainService _shService;
        private readonly ISafetyHazard_CategoryHistoryDomainService _shCatHistService;
        private readonly SaftyHazard_Category _sh_cat;
        private readonly IProcedure_SaftyHazard_LinkDomainService _proc_sh_linkService;
        private readonly ISaftyHazard_EO_LinkDomainService _sh_eo_linkService;
        private readonly ISaftyHazard_RR_LinkDomainService _sh__rr_linkService;
        private readonly IILA_SafetyHazard_LinkDomainService _iLA_SafetyHazard_LinkDomainService;

        public SaftyHazard_CategoryService(
            ISaftyHazard_CategoryDomainService sh_cat_service,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<SaftyHazard_CategoryService> localizer,
            UserManager<AppUser> userManager,
            ISaftyHazardDomainService shService,
            ISafetyHazard_CategoryHistoryDomainService shCatHistService,
            IProcedure_SaftyHazard_LinkDomainService proc_sh_linkService,
            ISaftyHazard_EO_LinkDomainService sh_eo_linkService,
            ISaftyHazard_RR_LinkDomainService sh__rr_linkService, IILA_SafetyHazard_LinkDomainService iLA_SafetyHazard_LinkDomainService)
        {
            _sh_cat_service = sh_cat_service;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _saftyHazardCat = new SaftyHazard_Category();
            _saftyHazard = new SaftyHazard();
            _shService = shService;
            _shCatHistService = shCatHistService;
            _sh_cat = new SaftyHazard_Category();
            _proc_sh_linkService = proc_sh_linkService;
            _sh_eo_linkService = sh_eo_linkService;
            _sh__rr_linkService = sh__rr_linkService;
            _iLA_SafetyHazard_LinkDomainService = iLA_SafetyHazard_LinkDomainService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(SaftyHazardCategoryOptions options)
        {
            foreach (var id in options.SaftyHazardCategoryIds)
            {
                var proc_issu = await _sh_cat_service.GetWithIncludeAsync(id, new string[] { nameof(_saftyHazardCat.SaftyHazards) });
                List<Domain.Entities.Core.SaftyHazard> procList = new List<Domain.Entities.Core.SaftyHazard>();
                procList.AddRange(proc_issu.SaftyHazards);
                if (proc_issu != null)
                {
                    proc_issu.Activate();
                    await _sh_cat_service.UpdateAsync(proc_issu);

                    foreach (var proc in procList)
                    {
                        var options1 = new SaftyHazardOptions();
                        options1.SaftyHazardId = proc.Id;
                        options1.EffectiveDate = options.EffectiveDate;
                        options1.ChangeNotes = "Inactive Due to Category Inactive";
                        await SaftyHazardActivateAsync(proc.Id, options1);

                    }
                }
                else
                {
                    throw new QTDServerException(_localizer["Regulation_IssuingAuthorityNotFound"]);
                }

                //var cat = await _sh_cat_service.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
                //if (cat == null)
                //{
                //    throw new Exception(message: _localizer["SafetyHazardCategoryNotFound"]);
                //}
                //else
                //{
                //    cat.Activate();

                //    var validationResult = await _sh_cat_service.UpdateAsync(cat);
                //    if (!validationResult.IsValid)
                //    {
                //        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                //    }
                //}
            }
        }

        public async System.Threading.Tasks.Task SaftyHazardActivateAsync(int id, SaftyHazardOptions options)
        {
            var procedure = await _shService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, SaftyHazardOperations.Delete);
            if (result.Succeeded)
            {
                procedure.Activate();

                var validationResult = await _shService.UpdateAsync(procedure);
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

        public async System.Threading.Tasks.Task DeleteAsync(SaftyHazardCategoryOptions options)
        {
            foreach (var id in options.SaftyHazardCategoryIds)
            {
                var cat = await _sh_cat_service.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
                if (cat == null)
                {
                    throw new QTDServerException(_localizer["SafetyHazardCategoryNotFound"]);
                }
                else
                {
                    cat.Delete();

                    var validationResult = await _sh_cat_service.UpdateAsync(cat);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
            }
        }

        public async Task<List<SaftyHazard_Category>> GetAsync()
        {
            var sh_cats = await _sh_cat_service.AllQuery().Where(x=>x.Active == true).OrderBy(x=>x.Title).ToListAsync();
            for(int i = 0; i < sh_cats.Count() ; i++)
            {
                sh_cats[i].SaftyHazards = await _shService.FindQuery(x => x.SaftyHazardCategoryId == sh_cats[i].Id).Select(s => new SaftyHazard { Active = s.Active, Id = s.Id, CreatedBy = s.CreatedBy, CreatedDate = s.CreatedDate, Deleted = s.Deleted, EffectiveDate = s.EffectiveDate, FileName = s.FileName, HyperLinks = s.HyperLinks, ModifiedBy = s.ModifiedBy, ModifiedDate = s.ModifiedDate, Number = s.Number, RevisionNumber = s.RevisionNumber, SaftyHazardCategoryId = s.SaftyHazardCategoryId, Text = s.Text, Title = s.Title }).OrderBy(x=>x.Title).ToListAsync();
                for(int j=0; j < sh_cats[i].SaftyHazards.Count(); j++)
                {
                    sh_cats[i].SaftyHazards.ToList()[j].Procedure_SaftyHazard_Links = await _proc_sh_linkService.FindQuery(x => x.SaftyHazardId == sh_cats[i].SaftyHazards.ToList()[j].Id).ToListAsync();
                    sh_cats[i].SaftyHazards.ToList()[j].SafetyHazard_EO_Links = await _sh_eo_linkService.FindQuery(x => x.SafetyHazardId == sh_cats[i].SaftyHazards.ToList()[j].Id).ToListAsync();
                    sh_cats[i].SaftyHazards.ToList()[j].SaftyHazard_RR_Links = await _sh__rr_linkService.FindQuery(x => x.SafetyHazardId == sh_cats[i].SaftyHazards.ToList()[j].Id).ToListAsync();
                    sh_cats[i].SaftyHazards.ToList()[j].ILA_SafetyHazard_Links = await _iLA_SafetyHazard_LinkDomainService.FindQuery(x => x.SafetyHazardId == sh_cats[i].SaftyHazards.ToList()[j].Id).ToListAsync();
                }
            }
            sh_cats = sh_cats.Where(sh_cat => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh_cat, SaftyHazard_CategoryOperations.Read).Result.Succeeded).ToList();
            return sh_cats.OrderBy(o => o.Title).ToList();
        }

        public async Task<SaftyHazard_Category> GetAsync(int id)
        {
            var shCat = await _sh_cat_service.GetWithIncludeAsync(id, new string[] { nameof(_sh_cat.SaftyHazards) });
            return shCat;



            /*var sh_cat = await _sh_cat_service.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (sh_cat != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh_cat, SaftyHazard_CategoryOperations.Read);
                if (result.Succeeded)
                {
                    return sh_cat;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new Exception(message: _localizer["SH_Cat_NotFound"]);
            }*/
        }

        public async Task<List<SaftyHazardCategoryCompactOptions>> GetSHCategoryWithSH()
        {
            var shCategory = await _sh_cat_service.AllQuery().OrderBy(o => o.Number).ToListAsync();
            var sh = await _shService.AllQuery().OrderBy(o => o.Number).Select(s => new SaftyHazard { Active = s.Active, Id = s.Id, CreatedBy = s.CreatedBy, CreatedDate = s.CreatedDate, Deleted = s.Deleted, EffectiveDate = s.EffectiveDate, FileName = s.FileName, HyperLinks = s.HyperLinks, ModifiedBy = s.ModifiedBy, ModifiedDate = s.ModifiedDate, Number = s.Number, RevisionNumber = s.RevisionNumber, SaftyHazardCategoryId = s.SaftyHazardCategoryId, Text = s.Text, Title = s.Title, SafetyHazard_Set_Links = s.SafetyHazard_Set_Links }).ToListAsync();
            var data = shCategory.GroupJoin(sh, x => x.Id, x => x.SaftyHazardCategoryId, (shCategory, sh) => new { shCategory, Hazard = sh.Select(x => new SaftyHazardCompactOptions(x.Id, x.SaftyHazardCategoryId, x.Title, x.Active, x.Number, x.SafetyHazard_Set_Links.Select(shsl=> shsl.SafetyHazardSetId).ToList())).ToList() }).ToList();
            List<SaftyHazardCategoryCompactOptions> shCatCompact = new List<SaftyHazardCategoryCompactOptions>();
            foreach (var item in data)
            {
                var shCompact = new SaftyHazardCategoryCompactOptions();
                shCompact.SaftyHazard_Category = item.shCategory;
                shCompact.SaftyHazardCompactOptions.AddRange(item.Hazard);
                shCatCompact.Add(shCompact);
            }

            return shCatCompact;
        }

        public async Task<int> getCount()
        {
            var result = await _sh_cat_service.AllQueryWitDeletedCount();
            return result;
        }

        public async System.Threading.Tasks.Task InActiveAsync(SaftyHazardCategoryOptions options)
        {
            //foreach (var id in options.SaftyHazardCategoryIds)
            //{
            //    var cat = await _sh_cat_service.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            //    if (cat == null)
            //    {
            //        throw new Exception(message: _localizer["SafetyHazardCategoryNotFound"]);
            //    }
            //    else
            //    {
            //        cat.Deactivate();

            //        var validationResult = await _sh_cat_service.UpdateAsync(cat);
            //        if (!validationResult.IsValid)
            //        {
            //            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //        }
            //    }
            //}

            foreach (var id in options.SaftyHazardCategoryIds)
            {
                var proc_issu = await _sh_cat_service.GetWithIncludeAsync(id, new string[] { nameof(_saftyHazardCat.SaftyHazards) });
                List<Domain.Entities.Core.SaftyHazard> procList = new List<Domain.Entities.Core.SaftyHazard>();
                procList.AddRange(proc_issu.SaftyHazards);
                if (proc_issu != null)
                {
                    proc_issu.Deactivate();
                    await _sh_cat_service.UpdateAsync(proc_issu);

                    foreach (var proc in procList)
                    {
                        var options1 = new SaftyHazardOptions();
                        options1.SaftyHazardId = proc.Id;
                        options1.EffectiveDate = options.EffectiveDate;
                        options1.ChangeNotes = "Inactive Due to Category Inactive";
                        await SaftyHazardDeactivateAsync(proc.Id, options1);

                    }
                }
                else
                {
                    throw new QTDServerException(_localizer["SaftyHazard_CategoryNotFound"]);
                }
            }
        }

        public async System.Threading.Tasks.Task SaftyHazardDeactivateAsync(int id, SaftyHazardOptions options)
        {
            var procedure = await _shService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, SaftyHazardOperations.Delete);
            if (result.Succeeded)
            {
                procedure.Deactivate();

                var validationResult = await _shService.UpdateAsync(procedure);
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

        public async Task<SaftyHazard_Category> SaveCategoryAsync(SafetyHazard_CategoryCreateOptions options)
        {
            var shCat = (await _sh_cat_service.FindAsync(x => x.Title.Trim().ToLower() == options.Title.Trim().ToLower())).FirstOrDefault();
            if (shCat != null)
            {
                throw new QTDServerException(_localizer["SafetyHazardCategoryAlreadyExists"]);
            }
            else
            {
                int dANumber = (await _sh_cat_service.AllQueryWitDeletedCount()) + 1;
                shCat = new SaftyHazard_Category();
                shCat.Description = options.Description;
                shCat.Title = options.Title;
                shCat.Notes = options.Notes;
                shCat.EffectiveDate = options.EffectiveDate;
                shCat.CreatedDate = DateTime.Now;
                shCat.Number = dANumber;
                shCat.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                var validationResult = await _sh_cat_service.AddAsync(shCat);
                if (validationResult.IsValid)
                {
                    return shCat;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
        }

        public async Task<SaftyHazard_Category> UpdateAsync(int id, SafetyHazard_CategoryCreateOptions options)
        {
            var shCat = await _sh_cat_service.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (shCat == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardCategoryNotFound"]);
            }
            else
            {
                var prvSHCat = await _sh_cat_service.FindQuery(x => x.Id != id && x.Title.Trim().ToLower() == options.Title.Trim().ToLower()).FirstOrDefaultAsync();
                if (prvSHCat != null)
                {
                    throw new QTDServerException(_localizer["SafetyHazardCategoryAlreadyExists"]);
                }

                shCat.Title = options.Title;
                shCat.Description = options.Description;
                shCat.Notes = options.Notes;
                shCat.EffectiveDate = options.EffectiveDate;
                shCat.ModifiedDate = DateTime.Now;
                shCat.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;

                var validationResult = await _sh_cat_service.UpdateAsync(shCat);
                if (validationResult.IsValid)
                {
                    return shCat;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
        }
    }
}
