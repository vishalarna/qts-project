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
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Instructor;
using QTD2.Infrastructure.Model.Instructor_Category;
using IInstructor_CategoryDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_CategoryService;
using IInstructorDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_Service;

namespace QTD2.Application.Services.Shared
{
    public class Instructor_CategoryService : IInstructor_CategoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Instructor_CategoryService> _localizer;
        private readonly IInstructor_CategoryDomainService _instructor_CategoryService;
        private readonly IInstructorDomainService _instructorService;
        private readonly UserManager<AppUser> _userManager;
        private readonly Instructor_Category _instructor_Category;
        private readonly IInstructorService _instructorService1;

        public Instructor_CategoryService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<Instructor_CategoryService> localizer, IInstructor_CategoryDomainService instructor_CategoryService, UserManager<AppUser> userManager,IInstructor_CategoryDomainService ins_cat_service, IInstructorDomainService instructorService, IInstructorService instructorService1)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _instructor_CategoryService = instructor_CategoryService;
            _userManager = userManager;
            _instructorService = instructorService;
            _instructorService1 = instructorService1;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            //var obj = await GetAsync(id);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            //if (result.Succeeded)
            //{
            //obj.Activate();

            //var validationResult = await _instructor_CategoryService.UpdateAsync(obj);
            //if (!validationResult.IsValid)
            //{
            //    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //}
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            //}
            var proc_issu = await _instructor_CategoryService.GetWithIncludeAsync(id, new string[] { nameof(_instructor_Category.Instructors) });
            List<Domain.Entities.Core.Instructor> procList = new List<Domain.Entities.Core.Instructor>();
            procList.AddRange(proc_issu.Instructors);
            if (proc_issu != null)
            {
                proc_issu.Activate();
                await _instructor_CategoryService.UpdateAsync(proc_issu);

                foreach (var proc in procList)
                {
                    var options = new InstructorOptions();
                    options.InstructorId = proc.Id;
                    options.EffectiveDate = (DateTime)proc_issu.IEffectiveDate;
                    options.ChangeNotes = "Inactive Due to Category Inactive";
                    await _instructorService1.InstructorActivateAsync(proc.Id, options);

                }
            }
            else
            {
                throw new QTDServerException(_localizer["Instructor_IssuingAuthorityNotFound"]);
            }
        }

        public async Task<Instructor_Category> CreateAsync(Instructor_CategoryCreateOptions options)
        {
            var obj = (await _instructor_CategoryService.FindAsync(x => x.ICategoryTitle == options.Title)).FirstOrDefault();
            if (obj == null)
            {
                obj = new Instructor_Category(options.Title, options.Description, options.website, options.EffectiveDate);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            // var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            // if (result.Succeeded)
            // {
            obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            obj.CreatedDate = DateTime.Now;
            var validationResult = await _instructor_CategoryService.AddAsync(obj);
            if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return obj;
                }

            // }
            // else
            // {
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            // }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            //if (result.Succeeded)
            //{
                obj.Delete();

                var validationResult = await _instructor_CategoryService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            //}
        }

        public async Task<List<Instructor_Category>> GetAsync()
        {
            var obj_list = await _instructor_CategoryService.AllAsync();

            // obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<Instructor_Category> GetAsync(int id)
        {
            var obj = await _instructor_CategoryService.GetWithIncludeAsync(id, new string[] { nameof(_instructor_Category.Instructors) });
            if (obj != null)
            {
                // var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read);
                //  if (result.Succeeded)
                //  {
                    return obj;

                // }
                // else
                // {
                //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                // }
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
            //obj.Deactivate();

            //var validationResult = await _instructor_CategoryService.UpdateAsync(obj);
            //if (!validationResult.IsValid)
            //{
            //    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //}
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            //}
            var proc_issu = await _instructor_CategoryService.GetWithIncludeAsync(id, new string[] { nameof(_instructor_Category.Instructors) });
            List<Domain.Entities.Core.Instructor> procList = new List<Domain.Entities.Core.Instructor>();
            procList.AddRange(proc_issu.Instructors);
            if (proc_issu != null)
            {
                proc_issu.Deactivate();
                await _instructor_CategoryService.UpdateAsync(proc_issu);

                foreach (var proc in procList)
                {
                    var options = new InstructorOptions();
                    options.InstructorId = proc.Id;
                    options.EffectiveDate = (DateTime)proc_issu.IEffectiveDate;
                    options.ChangeNotes = "Inactive Due to Category Inactive";
                    await _instructorService1.InstructorDeactivateAsync(proc.Id, options);

                }
            }
            else
            {
                throw new QTDServerException(_localizer["Regulation_IssuingAuthorityNotFound"]);
            }
        }

        public async Task<Instructor_Category> UpdateAsync(int id, Instructor_CategoryCreateOptions options)
        {
            var obj = await GetAsync(id);

            // var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            // if (result.Succeeded)
            // {
            obj.ICategoryTitle = options.Title;
            obj.ICategoryUrl = options.website;
            obj.ICategoryDescription = options.Description;
            obj.IEffectiveDate = options.EffectiveDate;
            obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            obj.ModifiedDate = DateTime.Now;

            var validationResult = await _instructor_CategoryService.UpdateAsync(obj);
            if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return obj;
                }

            // }
            // else
            // {
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            // }
        }

        public async Task<int> getCount()
        {
            var result = await _instructor_CategoryService.AllQueryWitDeletedCount();
            return result;
        }

        public async Task<List<InstructorCategoryCompactOptions>> GetInsCategoryWithIns()
        {
            var shCategories = await _instructor_CategoryService.AllQuery().OrderBy(o => o.ICategoryTitle).ToListAsync();
            //shCategory = shCategory.OrderBy(o => o.ICategoryTitle).ToList();
            //var sh = _instructorService.AllQuery();
            //var data = shCategory.GroupJoin(sh, x => x.Id, x => x.ICategoryId, (shCategory, sh) => new { shCategory, Category = sh.Select(x => new InstructorCompactOptions(x.Id, x.ICategoryId, x.InstructorName, x.Active, x.InstructorNumber)).ToList() }).ToList();
            List<InstructorCategoryCompactOptions> shCatCompact = new List<InstructorCategoryCompactOptions>();
            foreach (var item in shCategories)
            {
                var shCompact = new InstructorCategoryCompactOptions();
                shCompact.Instructor_Category = item;
                shCompact.InstructorCompactOptions = (await _instructorService.FindQuery(x => x.ICategoryId == item.Id).Select(s => new InstructorCompactOptions(s.Id, s.ICategoryId, s.InstructorName, s.Active, s.InstructorNumber,s.InstructorName)).ToListAsync()).OrderBy(o => o.Number).ThenBy(x=>x.Title).ToList();
                //shCompact.InstructorCompactOptions = shCompact.InstructorCompactOptions.OrderBy(o => o.Title).ToList();
                shCatCompact.Add(shCompact);
            }

            return shCatCompact.OrderBy(o => o.Instructor_Category.ICategoryTitle).ToList();
        }

    }
}
