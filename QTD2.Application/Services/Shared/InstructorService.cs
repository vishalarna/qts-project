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
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.Instructor;
using QTD2.Infrastructure.Model.Instructor_History;
using QTD2.Infrastructure.Model.Person;
using IInstructorCategoryDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_CategoryService;
using IInstructorDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_Service;

namespace QTD2.Application.Services.Shared
{
    public class InstructorService : IInstructorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<InstructorService> _localizer;
        private readonly IInstructorDomainService _instructorService;
        private readonly IInstructorCategoryDomainService _instructorCategoryService;
        private readonly UserManager<AppUser> _userManager;

        public InstructorService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<InstructorService> localizer, IInstructorDomainService instructorService, UserManager<AppUser> userManager, IInstructorCategoryDomainService instructorCategoryService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _instructorService = instructorService;
            _userManager = userManager;
            _instructorCategoryService = instructorCategoryService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(Instructor_HistoryCreateOptions options)
        {
            if (options != null && options.instructorIds.Count() > 0)
            {
                foreach (var instructor in options.instructorIds)
                {
                    var obj = await GetAsync(instructor);
                    obj.Activate();

                    var validationResult = await _instructorService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }

                }

            }
            else
            {
                throw new QTDServerException("Instructor Ids not found");
            }

        }

        public async Task<Instructor> CreateAsync(Instructor_CreateOptions options, bool isReturnConflictExp = false)
        {
            var obj = (await _instructorService.FindAsync(x => !string.IsNullOrEmpty(options.Email) && x.InstructorEmail == options.Email)).FirstOrDefault();
            if (obj == null)
            {
                obj = new Instructor(options.ICategoryId, options.Num, options.Name, options.Email, options.Description, options.Isworkbookadmin, options.EffectiveDate);
            }
            else
            {
                if (isReturnConflictExp)
                {
                    throw new ConflictExceptionHelper(obj);
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["Instructor Email Already Exists"].Value);
                }
            }

            // var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            // if (result.Succeeded)
            // {
            obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            obj.CreatedDate = DateTime.Now;
            var validationResult = await _instructorService.AddAsync(obj);
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

        public async System.Threading.Tasks.Task DeleteAsync(Instructor_HistoryCreateOptions options)
        {
            if (options != null && options.instructorIds.Count() > 0)
            {
                foreach (var instructor in options.instructorIds)
                {
                    var obj = await GetAsync(instructor);
                    obj.Delete();

                    var validationResult = await _instructorService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }

                }

            }
            else
            {
                throw new QTDServerException("Instructor Ids not found");
            }



        }

        public async Task<List<Instructor>> GetAsync()
        {
            var obj_list = await _instructorService.AllAsync();
            //obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.OrderBy(x => x.InstructorName).ToList();
        }

        public async Task<Instructor> GetAsync(int id)
        {
            var obj = await _instructorService.GetWithIncludeAsync(id, new string[] { "Instructor_Category", "ClassSchedules" });
            if (obj != null)
            {
                // var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read);
                //  if (result.Succeeded)
                //  {//
                return obj;

                // }
                //  else
                //  {
                //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                // }
            }
            else
            {
                throw new QTDServerException( _localizer["RecordNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(Instructor_HistoryCreateOptions options)
        {
            if (options != null && options.instructorIds.Count() > 0)
            {
                foreach (var instructor in options.instructorIds)
                {
                    var obj = await GetAsync(instructor);

                    // var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

                    // if (result.Succeeded)
                    // {
                    obj.Deactivate();

                    var validationResult = await _instructorService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }

                }

            }
            else
            {
                throw new QTDServerException("Instructor Ids not found");
            }

            //var obj = await GetAsync(id);

            //// var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            //// if (result.Succeeded)
            //// {
            //obj.Deactivate();

            //var validationResult = await _instructorService.UpdateAsync(obj);
            //if (validationResult.IsValid)
            //    {
            //        return obj;
            //    }
            //    else
            //    {
            //        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //    }

            // }
            // else
            // {
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            // }
        }

        public async Task<Instructor> UpdateAsync(int id, Instructor_CreateOptions options, bool isReturnConflictExp = false)
        {
            var instructorExists = (await _instructorService.FindAsync(x => !string.IsNullOrEmpty(options.Email) && x.InstructorEmail == options.Email && x.Id != id)).FirstOrDefault();
            if (instructorExists == null)
            {
                var obj = await GetAsync(id);

                obj.InstructorNumber = options.Num;
                obj.ICategoryId = options.ICategoryId;
                obj.InstructorName = options.Name;
                obj.InstructorEmail = options.Email;
                obj.InstructorDescription = options.Description;
                obj.IsWorkBookAdmin = options.Isworkbookadmin;
                obj.EffectiveDate = options.EffectiveDate;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;

                var validationResult = await _instructorService.UpdateAsync(obj);
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
                if (isReturnConflictExp)
                {
                    throw new ConflictExceptionHelper(instructorExists);
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["Instructor Email Already Exists"].Value);
                }
            }
        }

        public async Task<Instructor> UpdateByEmailAsync(Instructor_UpdateByEmailOptions options)
        {
            var instructor = (await _instructorService.FindAsync(x => x.InstructorEmail == options.Email)).FirstOrDefault();
            if (instructor != null)
            {
                instructor.InstructorName = options.Name;

                instructor.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                instructor.ModifiedDate = DateTime.Now;

                var validationResult = await _instructorService.UpdateAsync(instructor);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return instructor;
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Instructor not found"].Value, StatusCodes.Status404NotFound);
            }
        }

        public async Task<int> getCount()
        {
            var result = await _instructorService.AllQueryWitDeletedCount();
            return result;
        }

        public async Task<InstructorStatsVM> GetStatsCount()
        {
            var instructors = await _instructorService.AllQuery().Select(x => x.Id).ToListAsync();
            var categories = await _instructorCategoryService.AllQuery().Select(x => x.Id).ToListAsync();

            var stats = new InstructorStatsVM()
            {
                InstructorActive = await _instructorService.GetCount(x => x.Active == true),
                InstructorInactive = await _instructorService.GetCount(x => x.Active == false),
                ICategoryActive = await _instructorCategoryService.GetCount(x => x.Active == true),
                ICategoryInactive = await _instructorCategoryService.GetCount(x => x.Active == false),
                TotalWorkbookAdmins = await _instructorService.GetCount(x => x.IsWorkBookAdmin == true && x.Active == true),
            };

            return stats;
        }

        public async System.Threading.Tasks.Task InstructorDeactivateAsync(int id, InstructorOptions options)
        {
            var procedure = await _instructorService.GetAsync(id);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, InstructorOperations.Delete);
            //if (result.Succeeded)
            //{
            procedure.Deactivate();

            var validationResult = await _instructorService.UpdateAsync(procedure);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //}

        }

        public async System.Threading.Tasks.Task InstructorActivateAsync(int id, InstructorOptions options)
        {
            var procedure = await _instructorService.GetAsync(id);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, InstructorOperations.Delete);
            //if (result.Succeeded)
            //{
            procedure.Activate();

            var validationResult = await _instructorService.UpdateAsync(procedure);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //}

        }

        public async Task<List<Instructor>> GetInsActiveInactive(string option)
        {
            var rrList = new List<Instructor>();

            switch (option.ToLower().Trim())
            {
                case "insactive":
                    rrList = await _instructorService.FindQuery(x => x.Active == true).Select(s => new Instructor
                    {
                        Id = s.Id,
                        InstructorNumber = s.InstructorNumber,
                        InstructorName = s.InstructorName,
                    }).OrderBy(o => o.InstructorNumber).ThenBy(x => x.InstructorName).ToListAsync();
                    break;
                case "insinactive":
                    rrList = await _instructorService.FindQuery(x => x.Active == false).Select(s => new Instructor
                    {
                        Id = s.Id,
                        InstructorNumber = s.InstructorNumber,
                        InstructorName = s.InstructorName,
                    }).OrderBy(o => o.InstructorNumber).ThenBy(x => x.InstructorName).ToListAsync();
                    break;
                case "workbook":
                    rrList = await _instructorService.FindQuery(x => x.IsWorkBookAdmin == true && x.Active == true).Select(s => new Instructor
                    {
                        Id = s.Id,
                        InstructorNumber = s.InstructorNumber,
                        InstructorName = s.InstructorName,
                    }).OrderBy(o => o.InstructorNumber).ThenBy(x => x.InstructorName).ToListAsync();
                    break;

            }

            return rrList;

        }

        public async Task<List<Instructor_Category>> GetCatActiveInactive(string option)
        {
            var rrList = new List<Instructor_Category>();

            switch (option.ToLower().Trim())
            {
                case "catactive":
                    rrList = await _instructorCategoryService.FindQuery(x => x.Active == true).Select(s => new Instructor_Category
                    {
                        Id = s.Id,
                        ICategoryTitle = s.ICategoryTitle,
                    }).ToListAsync();
                    break;
                case "catinactive":
                    rrList = await _instructorCategoryService.FindQuery(x => x.Active == false).Select(s => new Instructor_Category
                    {
                        Id = s.Id,
                        ICategoryTitle = s.ICategoryTitle,
                    }).ToListAsync();
                    break;
            }

            return rrList.OrderBy(x => x.ICategoryTitle).ToList();
        }
                
        public async Task<Instructor> ActivateAsync(int id)
        {
            Instructor instructor = await _instructorService.GetAsync(id);
            if (instructor != null)
            {
                instructor.Activate();
                await _instructorService.UpdateAsync(instructor);
                instructor = await _instructorService.GetAsync(instructor.Id);
                return instructor;
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["InstructorNotFound"]);
            }

        }

        public async Task<Instructor> DeactivateAsync(int id)
        {
            Instructor instructor = await _instructorService.GetAsync(id);
            if (instructor != null)
            {
                instructor.Deactivate();
                var validationResult = await _instructorService.UpdateAsync(instructor);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            return instructor;
        }

    }
}
