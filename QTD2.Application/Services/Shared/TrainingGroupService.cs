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
using ITrainingGroupDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingGroupService;
using ITrainingGroupCategoryDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingGroup_CategoryService;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Application.Services.Shared
{
    public class TrainingGroupService : Interfaces.Services.Shared.ITrainingGroupService
    {
        private readonly ITrainingGroupDomainService _trainingGroup_service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TraineeEvaluationTypeService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITrainingGroupCategoryDomainService _trainingGroup_Category_Service;
        private readonly TrainingGroup_Category _tr_cat;

        public TrainingGroupService(
            ITrainingGroupDomainService trainingGroup_service,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<TraineeEvaluationTypeService> localizer,
            UserManager<AppUser> userManager,
            ITrainingGroupCategoryDomainService trainingGroup_Category_Service)
        {
            _trainingGroup_service = trainingGroup_service;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _trainingGroup_Category_Service = trainingGroup_Category_Service;
            _tr_cat = new TrainingGroup_Category();
        }

        public async Task<List<TrainingGroup>> GetAllTrainingGroupsAsync()
        {
            var trainingGroups = await _trainingGroup_service.AllQuery().ToListAsync();
            return trainingGroups;
        }

        public async Task<TrainingGroup> GetTrainingGroupAsync(int trId)
        {
            var trainingGroup = await _trainingGroup_service.FindQuery(x => x.Id == trId).FirstOrDefaultAsync();
            return trainingGroup;
        }

        public async Task<List<TrainingGroup>> GetTrainingGroupsInCategoryAsync(int catId)
        {
            var trainingGroups = await _trainingGroup_service.FindQuery(x => x.CategoryId == catId).ToListAsync();
            return trainingGroups;
        }

        public async Task<List<TrainingGroup_Category>> GetAllGroupsWithCategory()
        {
            var categories = await _trainingGroup_Category_Service.AllQueryWithInclude(new string[] { nameof(_tr_cat.TrainingGroups) }).ToListAsync();
            return categories;
        }
    }
}
