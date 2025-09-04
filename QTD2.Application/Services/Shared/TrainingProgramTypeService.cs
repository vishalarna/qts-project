using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.TrainingProgramType;

using TrTypeDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingProgramTypeService;
namespace QTD2.Application.Services.Shared
{
    public class TrainingProgramTypeService : ITrainingProgramTypeService
    {
        private readonly TrTypeDomainService _trainingProgramTypeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TrainingProgramType> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public TrainingProgramTypeService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<TrainingProgramType> localizer,
            UserManager<AppUser> userManager,
            TrTypeDomainService trainingProgramTypeService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _trainingProgramTypeService = trainingProgramTypeService;
        }

        public async Task<TrainingProgramType> CreateAsync(TrainingProgramTypeCreateOptions options)
        {
            var db_trainingProgram = (await _trainingProgramTypeService.FindAsync(x => x.TrainingProgramTypeTitle == options.TrainingProgramTypeTitle)).FirstOrDefault();
            var tpExist = db_trainingProgram != null;

            if (tpExist)
            {
                throw new BadHttpRequestException(message: _localizer["TrainingProgramExistsWithSameName"]);
            }

            var trainingProgram = new TrainingProgramType(options.TrainingProgramTypeTitle);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgram, TrainingProgramTypeOperations.Create);

            if (result.Succeeded)
            {
                trainingProgram.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                trainingProgram.CreatedDate = DateTime.Now;
                var validationResult = await _trainingProgramTypeService.AddAsync(trainingProgram);
                if (validationResult.IsValid)
                {
                    return trainingProgram;
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

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var trainingProgram = await GetAsync(id);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgram, TrainingProgramTypeOperations.Delete);
            if (result.Succeeded)
            {
                trainingProgram.Delete();
                var validationResult = await _trainingProgramTypeService.UpdateAsync(trainingProgram);
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

        public async Task<List<TrainingProgramType>> GetAsync()
        {
            var trainingPrograms = await _trainingProgramTypeService.FindAsync(x => x.Active);
            trainingPrograms = trainingPrograms.Where(tp => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tp, TrainingProgramTypeOperations.Read).Result.Succeeded).ToList();
            return trainingPrograms.OrderBy(x=>x.TrainingProgramTypeTitle).ToList();
        }

        public async Task<TrainingProgramType> GetAsync(int id)
        {
            var trainingProgram = await _trainingProgramTypeService.GetAsync(id);
            if (trainingProgram != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgram, TrainingProgramTypeOperations.Read);
                if (result.Succeeded)
                {
                    return trainingProgram;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            throw new QTDServerException(_localizer["TrainingProgramtypeNotFound"]);
        }


        public async Task<TrainingProgramType> UpdateAsync(int id, TrainingProgramTypeCreateOptions options)
        {
            var trainingProgram = await GetAsync(id);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgram, TrainingProgramTypeOperations.Update);
            if (result.Succeeded)
            {
                // Todo Update Logic
                trainingProgram.TrainingProgramTypeTitle = options.TrainingProgramTypeTitle;
                trainingProgram.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                trainingProgram.ModifiedDate = DateTime.Now;
                var validationResult = await _trainingProgramTypeService.UpdateAsync(trainingProgram);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return trainingProgram;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }
    }
}

