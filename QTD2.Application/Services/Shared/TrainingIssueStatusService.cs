using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.TrainingIssue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITrainingIssue_StatusDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingIssue_StatusService;

namespace QTD2.Application.Services.Shared
{
    public class TrainingIssueStatusService : ITrainingIssueStatusService
    {
        private readonly ITrainingIssue_StatusDomainService _trainingIssue_StatusDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TrainingIssueStatusService> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public TrainingIssueStatusService(IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<TrainingIssueStatusService> localizer,
            ITrainingIssue_StatusDomainService trainingIssue_StatusDomainService,
            UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _trainingIssue_StatusDomainService = trainingIssue_StatusDomainService;
            _userManager = userManager;
        }

        public async Task<List<TrainingIssue_Status_VM>> GetAllAsync()
        {
            var trainingIssueStatuses = await _trainingIssue_StatusDomainService.AllAsync();
            List<TrainingIssue_Status_VM> trainingIssueStatusVMList = new List<TrainingIssue_Status_VM>();
            foreach (var trainingIssueStatus in trainingIssueStatuses)
            {
                var trainingIssueStatusVM = new TrainingIssue_Status_VM(trainingIssueStatus.Id, trainingIssueStatus.Status);
                trainingIssueStatusVMList.Add(trainingIssueStatusVM);
            }
            return trainingIssueStatusVMList;
        }
    }
}