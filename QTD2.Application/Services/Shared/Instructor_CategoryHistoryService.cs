using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Instructor_CategoryHistory;
using IInstructor_CategoryHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_CategoryHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class Instructor_CategoryHistoryService : Interfaces.Services.Shared.IInstructor_CategoryHistoryService
    {
        private readonly IInstructor_CategoryHistoryDomainService _iCatHistoryService;
        private readonly IStringLocalizer<Instructor_CategoryHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;

        public Instructor_CategoryHistoryService(IInstructor_CategoryHistoryDomainService iCatHistoryService, IStringLocalizer<Instructor_CategoryHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager)
        {
            _iCatHistoryService = iCatHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var hist = await _iCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["InstructorCategoryHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _iCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<Instructor_CategoryHistory> CreateAsync(Instructor_CategoryHistoryCreateOptions options)
        {
            var hist = new Instructor_CategoryHistory(options.ICategoryId, options.CategoryNotes, options.EffectiveDate);

            // var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, Instructor_CategoryHistoryOperations.Create);
            // if (histResult.Succeeded)
            // {
            hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            hist.CreatedDate = DateTime.Now;
            var validationResult = await _iCatHistoryService.AddAsync(hist);
            if (validationResult.IsValid)
            {
                return hist;
            }
            else
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }

            // }
            // else
            // {
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            // }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var hist = await _iCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["InstructorCategoryHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _iCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<Instructor_CategoryHistory>> GetAllInsCatHistories()
        {
            var hist = (await _iCatHistoryService.AllAsync()).ToList();
            return hist;
        }

        public async Task<Instructor_CategoryHistory> GetInsCatHistory(int id)
        {
            var hist = await _iCatHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var hist = await _iCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["InstructorCategoryHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _iCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<Instructor_CategoryHistory> UpdateAsync(int id, Instructor_CategoryHistoryCreateOptions options)
        {
            var hist = await _iCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["InstructorCategoryHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, Instructor_CategoryHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    // TODO change update logic as required
                    hist.ICategoryNotes = options.CategoryNotes;
                    hist.EffectiveDate = options.EffectiveDate;
                    var validationResult = await _iCatHistoryService.UpdateAsync(hist);
                    if (validationResult.IsValid)
                    {
                        return hist;
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
        }

    }
}
