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
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.ActivityNotification;
using QTD2.Infrastructure.Model.TaskListReview;
using ITaskListReviewTypeDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskListReview_TypeService;

namespace QTD2.Application.Services.Shared
{
    public class TaskListReviewTypeService : ITaskListReviewTypeService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TaskListReviewTypeService> _localizer;
        private readonly ITaskListReviewTypeDomainService _taskListReviewTypeDomainService;

        public TaskListReviewTypeService(IAuthorizationService authorizationService,
        IStringLocalizer<TaskListReviewTypeService> localizer,
        ITaskListReviewTypeDomainService taskListReviewTypeDomainService
        )
        {
            _authorizationService = authorizationService;
            _localizer = localizer;
            _taskListReviewTypeDomainService = taskListReviewTypeDomainService;
            
        }

        public async Task<List<TaskListReviewType_VM>> GetAllAsync()
        {
            var taskListReviewTypes = await _taskListReviewTypeDomainService.AllAsync();
            return taskListReviewTypes.Select(x=> new TaskListReviewType_VM(x.Id,x.Type)).ToList();
        }

    }

}
