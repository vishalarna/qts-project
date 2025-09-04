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
using ITrainingIssue_SeverityDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingIssue_SeverityService;

namespace QTD2.Application.Services.Shared
{
    public class TrainingIssueSeverityService : ITrainingIssueSeverityService
    {
        private readonly ITrainingIssue_SeverityDomainService _trainingIssue_SeverityDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TrainingIssueSeverityService> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public TrainingIssueSeverityService(IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<TrainingIssueSeverityService> localizer,
            ITrainingIssue_SeverityDomainService trainingIssue_SeverityDomainService,
            UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _trainingIssue_SeverityDomainService = trainingIssue_SeverityDomainService;
            _userManager = userManager;
        }

        public async Task<List<TrainingIssue_Severity_VM>> GetAllAsync()
        {
            var trainingIssue_Severities = await _trainingIssue_SeverityDomainService.AllAsync();
            List<TrainingIssue_Severity_VM> trainingIssue_Severity_VMList = new List<TrainingIssue_Severity_VM>();
            foreach (var trainingIssue_Severity in trainingIssue_Severities)
            {
                var trainingIssue_Severity_VM = new TrainingIssue_Severity_VM(trainingIssue_Severity.Id, trainingIssue_Severity.Severity);
                trainingIssue_Severity_VMList.Add(trainingIssue_Severity_VM);
            }
            return trainingIssue_Severity_VMList;
        }
    }
}