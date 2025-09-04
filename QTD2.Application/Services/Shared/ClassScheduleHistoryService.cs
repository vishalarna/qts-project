using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Authorization.Operations.Core;
using IClassScheduleHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleHistoryService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService ;
using QTD2.Infrastructure.Model.ClassScheduleHistory;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Application.Services.Shared
{
    public class ClassScheduleHistoryService : IClassScheduleHistoryService
    {
        private readonly IClassScheduleHistoryDomainService _classScheduleHistoryService;
        private readonly IClassScheduleDomainService _classScheduleService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ClassScheduleHistoryService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IILADomainService _ilaService;

        public ClassScheduleHistoryService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<ClassScheduleHistoryService> localizer, IClassScheduleHistoryDomainService classScheduleHistoryDomainService, UserManager<AppUser> userManager, IClassScheduleDomainService classScheduleService, IILADomainService ilaService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _classScheduleHistoryService = classScheduleHistoryDomainService;
            _classScheduleService = classScheduleService;
            _userManager = userManager;
            _ilaService = ilaService;
        }

        public async Task<ClassScheduleHistory> CreateAsync(ClassScheduleHistoryCreateOptions options)
        {
            var hist = new ClassScheduleHistory(options.ClassScheduleId, options.ChangeNotes, options.ChangeEffectiveDate);
            var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, ClassScheduleHistoryOperations.Create);
            if (histResult.Succeeded)
            {
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                var validationResult = await _classScheduleHistoryService.AddAsync(hist);
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

        public async Task<List<ClassScheduleHistory>> GetAsync()
        {
            var hist = await _classScheduleHistoryService.AllAsync();
            return hist.ToList();
        }

        public async Task<List<ClassScheduleLatestActivityVM>> GetLatestActivityAsync()
        {

            var history = await _classScheduleHistoryService.FindQueryWithIncludeAsync(x => x.CreatedDate > DateTime.UtcNow.AddDays(-7), new string[] {"ClassSchedule","ClassSchedule.ILA" }, false)
                
                .Select(s => new ClassScheduleLatestActivityVM 
                { 
                    Id =s.Id,
                    ActivityDesc = s.ChangeNotes, 
                    CreatedDate = s.CreatedDate,
                    ILAName = s.ClassSchedule !=null & s.ClassSchedule.ILA!= null? s.ClassSchedule.ILA.Number + " " + s.ClassSchedule.ILA.Name : "N/A",
                    CreatedBy = s.CreatedBy }
                ).ToListAsync();

            
            return history.OrderByDescending(o => o.CreatedDate).ToList();
        }
    }
}
