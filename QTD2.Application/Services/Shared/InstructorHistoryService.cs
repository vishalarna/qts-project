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
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Instructor;
using QTD2.Infrastructure.Model.Instructor_History;
using IInstructorDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_Service;
using IInstructorHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_HistoryService;
using IInstructor_CategoryHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_CategoryHistoryService;
using IInstructorCategoryDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_CategoryService;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class InstructorHistoryService : Interfaces.Services.Shared.IInstructorHistoryService
    {
        private readonly IInstructor_CategoryHistoryDomainService _iCatHistoryService;
        private readonly IInstructorHistoryDomainService _insHistoryService;
        private readonly IInstructorDomainService _insService;
        private readonly IInstructorCategoryDomainService _insCatService;
        private readonly IStringLocalizer<InstructorHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;

        public InstructorHistoryService(IInstructorHistoryDomainService insHistoryService, IStringLocalizer<InstructorHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager, IInstructorDomainService insService, IInstructor_CategoryHistoryDomainService iCatHistoryService,IInstructorCategoryDomainService insCatServics)
        {
            _insHistoryService = insHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _insService = insService;
            _iCatHistoryService = iCatHistoryService;
            _insCatService = insCatServics;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var hist = await _insHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["InstructorHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _insHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<Instructor_History> CreateAsync(Instructor_HistoryCreateOptions options)
        {
            var hist = new Instructor_History(options.InstructorId, options.InstructorNotes, options.EffectiveDate);

            // var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, Instructor_CategoryHistoryOperations.Create);
            // if (histResult.Succeeded)
            // {
            hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            hist.CreatedDate = DateTime.Now;
            var validationResult = await _insHistoryService.AddAsync(hist);
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
            var hist = await _insHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["InstructorHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _insHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<Instructor_History>> GetAllInsCatHistories()
        {
            var hist = (await _insHistoryService.AllAsync()).ToList();
            return hist;
        }

        public async Task<List<InstructorLatestActivityVM>> GetHistoryAsync()
        {
            var instructors = await _insService.AllQuery().IgnoreQueryFilters().ToListAsync();
            //var categories = await _insCatService.AllQuery().ToListAsync();
            var history = await _insHistoryService.AllQuery().ToListAsync();
            //var insCatHistory  = await _iCatHistoryService.AllQuery().ToListAsync();
            var users = await _userManager.Users.ToListAsync();

            var latestactivity = from i in instructors
                                 join h in history on i.Id equals h.InstructorId
                                 join u in _userManager.Users on h.CreatedBy equals u.Email
                                 select new InstructorLatestActivityVM
                                 {
                                     Id = h.Id,
                                     InstructorName = i.InstructorName,
                                     InstructorNum = i.InstructorNumber,
                                     ActivityDesc = h.InstructorNotes,
                                     CreatedBy = u.Email,
                                     CreatedDate = h.CreatedDate,
                                 };

            //var latescattactivity = from c in categories
            //                        join h in insCatHistory on c.Id equals h.ICategoryId
            //                        join u in _userManager.Users on h.CreatedBy equals u.Id
            //                     select new InstructorLatestActivityVM
            //                     {
            //                         Id = h.Id,
            //                         InstructorName = c.Instructors?.Select(x=>x.InstructorName).FirstOrDefault(),
            //                         InstructorNum = c.Instructors?.Select(x => x.InstructorNumber).FirstOrDefault(),
            //                         ActivityDesc = h.ICategoryNotes,
            //                         CreatedBy = u.UserName,
            //                         CreatedDate = h.CreatedDate,
            //                     };

            //var combinedActivity = latescattactivity.Union(latescattactivity);
            //var combinedActivity = (latestactivity ?? Enumerable.Empty<InstructorLatestActivityVM>()).Concat(latescattactivity ?? Enumerable.Empty<InstructorLatestActivityVM>());
            return latestactivity.OrderByDescending(x=>x.CreatedDate).ToList();
        }

        public async Task<Instructor_History> GetInsCatHistory(int id)
        {
            var hist = await _insHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var hist = await _insHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["InstructorHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _insHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<Instructor_History> UpdateAsync(int id, Instructor_HistoryCreateOptions options)
        {
            var hist = await _insHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["InstructorHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, Instructor_CategoryHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    // TODO change update logic as required
                    hist.InstructorNotes = options.InstructorNotes;
                    hist.EffectiveDate = options.EffectiveDate;
                    var validationResult = await _insHistoryService.UpdateAsync(hist);
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
