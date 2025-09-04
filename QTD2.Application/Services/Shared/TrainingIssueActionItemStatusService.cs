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
using ITrainingIssue_ActionItemStatusDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingIssue_ActionItemStatusService;

namespace QTD2.Application.Services.Shared
{
    public class TrainingIssueActionItemStatusService : ITrainingIssueActionItemStatusService
    {
        private readonly ITrainingIssue_ActionItemStatusDomainService _trainingIssue_ActionItemStatusDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TrainingIssueActionItemStatusService> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public TrainingIssueActionItemStatusService(IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<TrainingIssueActionItemStatusService> localizer,
            ITrainingIssue_ActionItemStatusDomainService trainingIssue_ActionItemStatusDomainService,
            UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _trainingIssue_ActionItemStatusDomainService = trainingIssue_ActionItemStatusDomainService;
            _userManager = userManager;
        }

        public async Task<List<TrainingIssue_ActionItemStatus_VM>> GetAllAsync()
        {
            var trainingIssueActionItemStatuses = await _trainingIssue_ActionItemStatusDomainService.AllAsync();
            List<TrainingIssue_ActionItemStatus_VM> trainingIssueActionItemStatusVMList = new List<TrainingIssue_ActionItemStatus_VM>();
            foreach (var trainingIssueActionItemStatus in trainingIssueActionItemStatuses)
            {
                var trainingIssueActionItemStatusVM = new TrainingIssue_ActionItemStatus_VM(trainingIssueActionItemStatus.Id, trainingIssueActionItemStatus.Status);
                trainingIssueActionItemStatusVMList.Add(trainingIssueActionItemStatusVM);
            }
            return trainingIssueActionItemStatusVMList;
        }
    }
}
