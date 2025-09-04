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
using QTD2.Infrastructure.Model.TrainingProgram_History;
using ITrainingProgramHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingProgram_HistoryService;

namespace QTD2.Application.Services.Shared
{
    public class TrainingProgram_HistoryService : Interfaces.Services.Shared.ITrainingProgram_HistoryService
    {
        private readonly ITrainingProgramHistoryDomainService _trainingProgramHistoryService;
        private readonly IStringLocalizer<PositionHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;

        public TrainingProgram_HistoryService(IStringLocalizer<PositionHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager, ITrainingProgramHistoryDomainService trainingProgramHistoryService)
        {
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _trainingProgramHistoryService = trainingProgramHistoryService;

        }
        public async Task<TrainingProgram_History> CreateAsync(TrainingProgram_HistoryCreateOptions options)
        {
            var hist = new TrainingProgram_History(options.TrainingProgramId,options.TrainingProgramVersionId,options.ChangeNotes,options.ChangeEffectiveDate);

            var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, TrainingProgram_HistoryOperations.Create);
            if (histResult.Succeeded)
            {
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                var validationResult = await _trainingProgramHistoryService.AddAsync(hist);
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
