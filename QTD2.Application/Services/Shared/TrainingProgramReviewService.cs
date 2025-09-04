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
using QTD2.Infrastructure.Model.TrainingProgramReview;
namespace QTD2.Application.Services.Shared
{
    public class TrainingProgramReviewService : ITrainingProgramReviewService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TrainingProgram> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly Domain.Interfaces.Service.Core.ITrainingProgramReviewService _trainingProgramReviewService;
        private readonly Domain.Interfaces.Service.Core.ITrainingProgramService _trainingProgramService;

        public TrainingProgramReviewService(
             IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<TrainingProgram> localizer,
        UserManager<AppUser> userManager,
        Domain.Interfaces.Service.Core.ITrainingProgramReviewService trainingProgramReviewService,
        Domain.Interfaces.Service.Core.ITrainingProgramService trainingProgramService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _trainingProgramReviewService = trainingProgramReviewService;
            _trainingProgramService = trainingProgramService;
        }

        public async System.Threading.Tasks.Task<TrainingProgramReview_ViewModel> CreateAsync(TrainingProgramReview_ViewModel tpReview_VM)
        {
            if (tpReview_VM == null)
                throw new ArgumentNullException(nameof(tpReview_VM));
            else
            {
                var trainingProgramReview = new QTD2.Domain.Entities.Core.TrainingProgramReview(tpReview_VM.TrainingProgramId, tpReview_VM.ReviewDate.ToUniversalTime(), tpReview_VM.StartDate.ToUniversalTime(), tpReview_VM.EndDate.ToUniversalTime(), tpReview_VM.Purpose, tpReview_VM.Method, tpReview_VM.HistoricalBackground, tpReview_VM.ProgramDesign, tpReview_VM.ProgramMaterials, tpReview_VM.ProgramImplementation, tpReview_VM.EvaluationOfTraineeLearning, tpReview_VM.StudentEvaluationResults, tpReview_VM.Conclusion, tpReview_VM.Summary, tpReview_VM.TrainerName, tpReview_VM.Title, tpReview_VM.TrainerSignOff, tpReview_VM.Published);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgramReview, AuthorizationOperations.Create);
                if (result.Succeeded)
                {
                    var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                    trainingProgramReview.Create(userName.Id);
                    trainingProgramReview.UpdateEmployeeLinks(tpReview_VM.Reviewers.Select(x => x.EmployeeId).ToList());
                    var validationResult = await _trainingProgramReviewService.AddAsync(trainingProgramReview);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
                var createdTPR = await _trainingProgramReviewService.GetAsync(trainingProgramReview.Id);
                return await MapTrainingProgramReviewToTrainingProgramReviewViewModel(createdTPR);
            }
        }
        
        public async Task<TrainingProgramReview_ViewModel> GetAsync(int id)
        {
            var tpr = await _trainingProgramReviewService.GetAsync(id);
            return await MapTrainingProgramReviewToTrainingProgramReviewViewModel(tpr);
        }
        
        public async Task<TrainingProgramReview_ViewModel> UpdateAsync(int id, TrainingProgramReview_ViewModel trainingProgramReview_ViewModel)
        {
            var trainingProgramReview = await _trainingProgramReviewService.GetAsync(id);
            if (trainingProgramReview != null)
            {
                UpdateTrainingProgramReview(trainingProgramReview, trainingProgramReview_ViewModel);
                trainingProgramReview.UpdateEmployeeLinks(trainingProgramReview_ViewModel.Reviewers.Select(reviewer => reviewer.EmployeeId).ToList());
                var validationResult = await _trainingProgramReviewService.UpdateAsync(trainingProgramReview);
                if (validationResult.IsValid)
                {
                    trainingProgramReview = await _trainingProgramReviewService.GetAsync(id);
                    return await MapTrainingProgramReviewToTrainingProgramReviewViewModel(trainingProgramReview);
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new QTDServerException("Training Program Review Id not found");
            }
        }
        
        public async Task<TrainingProgramReview_ViewModel> CopyAsync(int id)
        {
            var originalTpr = await _trainingProgramReviewService.GetAsync(id);
            var copiedTpr = new TrainingProgramReview(originalTpr.TrainingProgramId, originalTpr.ReviewDate, originalTpr.StartDate, originalTpr.EndDate, originalTpr.Purpose, originalTpr.Method, originalTpr.HistoricalBackground, originalTpr.ProgramDesign, originalTpr.ProgramMaterials, originalTpr.ProgramImplementation, originalTpr.EvaluationOfTraineeLearning, originalTpr.StudentEvaluationResults, originalTpr.Conclusion, originalTpr.Summary, originalTpr.TrainerName, "Copy - " + originalTpr.Title, originalTpr.TrainerSignOff, false);
            copiedTpr.Reviewers = originalTpr.Reviewers
                .Select(reviewer => new TrainingProgramReview_Employee_Link
                {
                    TrainingProgramReviewId = reviewer.TrainingProgramReviewId,
                    EmployeeId = reviewer.EmployeeId
                })
                .ToList();
            copiedTpr.Id = 0;
            var result = await _trainingProgramReviewService.AddAsync(copiedTpr);
            if (!result.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', result.Errors));
            }

            return await MapTrainingProgramReviewToTrainingProgramReviewViewModel(copiedTpr);
        }
        
        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var trainingProgramReview = await _trainingProgramReviewService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgramReview, TrainingProgramOperations.Delete);
            if (result.Succeeded)
            {
                trainingProgramReview.Delete();
                var validationResult = await _trainingProgramReviewService.UpdateAsync(trainingProgramReview);
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
        
        public async System.Threading.Tasks.Task ActivateAsync(int id)
        {
            var trainingProgramReview = await _trainingProgramReviewService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgramReview, TrainingProgramReviewOperations.Delete);
            if (result.Succeeded)
            {
                trainingProgramReview.Activate();
                var validationResult = await _trainingProgramReviewService.UpdateAsync(trainingProgramReview);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async System.Threading.Tasks.Task InactivateAsync(int id)
        {
            var trainingProgramReview = await _trainingProgramReviewService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgramReview, TrainingProgramReviewOperations.Delete);
            if (result.Succeeded)
            {
                trainingProgramReview.Deactivate();
                var validationResult = await _trainingProgramReviewService.UpdateAsync(trainingProgramReview);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<TrainingProgramReview_OverviewViewModel> GetOverviewAsync()
        {
            var trainingProgramsList = await _trainingProgramService.GetActiveVersionTitleWithNoReviewAsync();
            var trainingProgramsReviews = await _trainingProgramReviewService.GetAllAsync();
            var stats = new TrainingProgramReview_OverviewViewModel()
            {
                ActiveInitialTrainingProgramReviews = await _trainingProgramReviewService.GetActiveInitialTrainingProgramReviewsAsync(),
                InactiveInitialTrainingProgramReviews = await _trainingProgramReviewService.GetInactiveInitialTrainingProgramReviewsAsync(),
                ActiveContinuingTrainingProgramReviews = await _trainingProgramReviewService.GetActiveContinuingTrainingProgramReviewsAsync(),
                InactiveContinuingTrainingProgramReviews = await _trainingProgramReviewService.GetInactiveContinuingTrainingProgramReviewsAsync(),
                ActiveCycleTrainingProgramReviews = await _trainingProgramReviewService.GetActiveCycleTrainingProgramReviewsAsync(),
                InactiveCycleTrainingProgramReviews = await _trainingProgramReviewService.GetInactiveCycleTrainingProgramReviewsAsync(),
                NoReviewTrainingPrograms = trainingProgramsList.Count(),
                WithReviewInDraftTrainingPrograms = await _trainingProgramService.GetActiveTrainingProgramsWithReviewInDraftCountAsync(),
                TrainingProgramReviewOverviewReviews = trainingProgramsReviews.Select(s => new TrainingProgramReview_OverviewReviewViewModel
                {
                    TrainingProgramReviewId = s.Id,
                    TrainingProgramId = s.TrainingProgramId,
                    TrainingProgramTypeId = s.TrainingProgram.TrainingProgramTypeId,
                    TrainingProgramType = s.TrainingProgram?.TrainingProgramType?.TrainingProgramTypeTitle,
                    PositionId = s.TrainingProgram.PositionId,
                    PositionAbbreviation = s.TrainingProgram?.Position?.PositionAbbreviation,
                    PositionName = s.TrainingProgram?.Position?.PositionTitle,
                    StartDate = s.StartDate ?? DateTime.MinValue,
                    EndDate = s.EndDate ?? DateTime.MinValue,
                    Reviewers = s.Reviewers?.Select(r => new TrainingProgramReview_Employee_Link_ViewModel
                    {
                        Id = r.Id,
                        TrainingProgramReviewId = r.TrainingProgramReviewId,
                        EmployeeId = r.EmployeeId,
                        EmployeePersonFullName = r.Employee.Person.FirstName + " " + r.Employee.Person.LastName
                    }).ToList(),
                    ReviewDate = s.ReviewDate.HasValue ? s.ReviewDate.Value : DateTime.MinValue,
                    Published = s.Published,
                    Active = s.Active
                }).ToList()
            };
            return stats;
        }

        public Task<TrainingProgramReview_ViewModel> MapTrainingProgramReviewToTrainingProgramReviewViewModel(TrainingProgramReview tPR)
        {
            var trainingProgramReview_ViewModel = new TrainingProgramReview_ViewModel
            {
                Id = tPR.Id,
                PositionId = tPR.TrainingProgram.PositionId,
                PositionName = tPR.TrainingProgram.Position.PositionTitle,
                TrainingProgramTypeId= tPR.TrainingProgram.TrainingProgramTypeId,
                TrainingProgramType=tPR.TrainingProgram.TrainingProgramType.TrainingProgramTypeTitle,
                TrainingProgramId=tPR.TrainingProgramId,
                TrainingProgram_ProgramTitle=tPR.TrainingProgram.ProgramTitle,
                TrainingProgram_Version=tPR.TrainingProgram.Version,
                ReviewDate = tPR.ReviewDate ?? DateTime.MinValue,
                Reviewers = tPR.Reviewers.Select(s => new TrainingProgramReview_Employee_Link_ViewModel
                {
                    Id = s.Id,
                    TrainingProgramReviewId = s.TrainingProgramReviewId,
                    EmployeeId = s.EmployeeId,
                    EmployeePersonFullName = s.Employee.Person.FirstName+" "+s.Employee.Person.LastName
                }
                ).ToList(),
                StartDate = tPR.StartDate ?? DateTime.MinValue,
                EndDate= tPR.EndDate ?? DateTime.MinValue,
                Purpose = tPR.Purpose,
                Method = tPR.Method,
                HistoricalBackground = tPR.HistoricalBackground,
                ProgramDesign=tPR.ProgramDesign,
                ProgramMaterials=tPR.ProgramMaterials,
                ProgramImplementation=tPR.ProgramImplementation,
                EvaluationOfTraineeLearning=tPR.EvaluationOfTraineeLearning,
                StudentEvaluationResults=tPR.StudentEvaluationResults,
                Conclusion=tPR.Conclusion,
                Summary=tPR.Summary,
                TrainerName=tPR.TrainerName,
                Title=tPR.Title,
                TrainerSignOff= tPR.TrainerSignOff ?? false,
                Published = tPR.Published
            };
            return System.Threading.Tasks.Task.FromResult(trainingProgramReview_ViewModel);
        }

        protected void UpdateTrainingProgramReview(Domain.Entities.Core.TrainingProgramReview tpr, TrainingProgramReview_ViewModel options)
        {
            if (tpr.TrainingProgramId != options.TrainingProgramId)
            tpr.SetTrainingProgramId(options.TrainingProgramId);

            if (tpr.ReviewDate != options.ReviewDate)
                tpr.SetReviewDate(options.ReviewDate.ToUniversalTime());

            if (tpr.StartDate != options.StartDate)
                tpr.SetStartDate(options.StartDate.ToUniversalTime());

            if (tpr.EndDate != options.EndDate)
                tpr.SetEndDate(options.EndDate.ToUniversalTime());

            if (tpr.Purpose != options.Purpose)
                tpr.SetPurpose(options.Purpose);

            if (tpr.Method != options.Method)
                tpr.SetMethod(options.Method);

            if (tpr.HistoricalBackground != options.HistoricalBackground)
                tpr.SetHistoricalBackground(options.HistoricalBackground);

            if (tpr.ProgramDesign != options.ProgramDesign)
                tpr.SetProgramDesign(options.ProgramDesign);

            if (tpr.ProgramMaterials != options.ProgramMaterials)
                tpr.SetProgramMaterials(options.ProgramMaterials);

            if (tpr.ProgramImplementation != options.ProgramImplementation)
                tpr.SetProgramImplementation(options.ProgramImplementation);

            if (tpr.EvaluationOfTraineeLearning != options.EvaluationOfTraineeLearning)
                tpr.SetEvaluationOfTraineeLearning(options.EvaluationOfTraineeLearning);

            if (tpr.StudentEvaluationResults != options.StudentEvaluationResults)
                tpr.SetStudentEvaluationResults(options.StudentEvaluationResults);

            if (tpr.Conclusion != options.Conclusion)
                tpr.SetConclusion(options.Conclusion);

            if (tpr.Summary != options.Summary)
                tpr.SetSummary(options.Summary);

            if (tpr.TrainerName != options.TrainerName)
                tpr.SetTrainerName(options.TrainerName);

            if (tpr.Title != options.Title)
                tpr.SetTitle(options.Title);

            if (tpr.TrainerSignOff != options.TrainerSignOff)
                tpr.SetTrainerSignOff(options.TrainerSignOff);

            if (tpr.Published != options.Published)
                tpr.SetPublished(options.Published);
            var modifiedBy = _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (modifiedBy != null)
            {
                tpr.Modify(modifiedBy.Result.UserName);
            }
        }
    }
}
