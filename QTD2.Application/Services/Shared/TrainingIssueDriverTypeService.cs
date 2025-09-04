using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TrainingIssue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITrainingIssue_DriverTypeDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingIssue_DriverTypeService;

namespace QTD2.Application.Services.Shared
{
    public class TrainingIssueDriverTypeService : ITrainingIssueDriverTypeService
    {
        private readonly ITrainingIssue_DriverTypeDomainService _trainingIssue_DriverTypeDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TrainingIssueDriverTypeService> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public TrainingIssueDriverTypeService(IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<TrainingIssueDriverTypeService> localizer,
            ITrainingIssue_DriverTypeDomainService trainingIssue_DriverTypeDomainService,
            UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _trainingIssue_DriverTypeDomainService = trainingIssue_DriverTypeDomainService;
            _userManager = userManager;
        }

        public async Task<List<TrainingIssue_DriverType_VM>> GetAllWithSubTypesAsync()
        {
            var trainingIssueDriverTypes = await _trainingIssue_DriverTypeDomainService.AllWithIncludeAsync(new string[] { "DriverSubTypes" });

            var trainingIssueDriverTypeVMList = new List<TrainingIssue_DriverType_VM>();
            foreach (var trainingIssueDriverType in trainingIssueDriverTypes)
            {
                var trainingIssueDriverTypeVM = new TrainingIssue_DriverType_VM(trainingIssueDriverType.Id, trainingIssueDriverType.Type);
                foreach (var driverSubType in trainingIssueDriverType.DriverSubTypes)
                {
                    var trainingIssueDriverSubTypeVM = new TrainingIssue_DriverSubType_VM(driverSubType.Id, driverSubType.SubType);
                    trainingIssueDriverTypeVM.SubTypes.Add(trainingIssueDriverSubTypeVM);
                }
                trainingIssueDriverTypeVMList.Add(trainingIssueDriverTypeVM);
            }
            return trainingIssueDriverTypeVMList;
        }
    }
}