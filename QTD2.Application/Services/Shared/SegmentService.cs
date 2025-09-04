using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Segment;
using QTD2.Infrastructure.Model.SegmentObjective_Link;
using IEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using ISegmentDomainService = QTD2.Domain.Interfaces.Service.Core.ISegmentService;
using ISegmentObjectiveLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISegmentObjective_LinkService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using ISubDutyAreaDomainService = QTD2.Domain.Interfaces.Service.Core.ISubdutyAreaService;
using IDutyAreaDomainService = QTD2.Domain.Interfaces.Service.Core.IDutyAreaService;
using ICustomEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.ICustomEnablingObjectiveService;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    internal class SegmentService : Interfaces.Services.Shared.ISegmentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SegmentService> _localizer;
        private readonly ISegmentDomainService _segmentService;
        private readonly ITaskDomainService _taskService;
        private readonly IEnablingObjectiveDomainService _enablingObjectiveService;
        private readonly ISegmentObjectiveLinkDomainService _segmentObjectiveLinkService;
        private readonly UserManager<AppUser> _userManager;
        private readonly Segment _segment;
        private readonly SegmentObjective_Link _segmentObjectiveLink;
        private readonly ISubDutyAreaDomainService _subDutyareaService;
        private readonly IDutyAreaDomainService _dutyAreaService;
        private readonly ICustomEnablingObjectiveDomainService _customEOService;

        public SegmentService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<SegmentService> localizer, ISegmentDomainService segmentService, UserManager<AppUser> userManager, ITaskDomainService taskService, IEnablingObjectiveDomainService enablingObjectiveService, ISegmentObjectiveLinkDomainService segmentObjectiveLinkService, ISubDutyAreaDomainService subDutyareaService, IDutyAreaDomainService dutyAreaService, ICustomEnablingObjectiveDomainService customEOService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _segmentService = segmentService;
            _userManager = userManager;
            _taskService = taskService;
            _enablingObjectiveService = enablingObjectiveService;
            _segment = new Segment();
            _segmentObjectiveLink = new SegmentObjective_Link();
            _segmentObjectiveLinkService = segmentObjectiveLinkService;
            _subDutyareaService = subDutyareaService;
            _dutyAreaService = dutyAreaService;
            _customEOService = customEOService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _segmentService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<Segment> CreateAsync(SegmentCreateOptions options)
        {
            var obj =new Segment(options.Title, options.Duration, options.IsNercStandard, options.IsNercOperatingTopics, options.IsNercSimulation, options.Content, options.Uploads,options.IsPartialCredit);
          
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                if(options.SegmentId == null)
                {
                    obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    obj.CreatedDate = DateTime.Now;
                    var validationResult = await _segmentService.AddAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return obj;
                    }
                }
                else
                {
                    var segment = await _segmentService.FindQuery(x => x.Id == options.SegmentId).FirstOrDefaultAsync();
                    segment.Content = options.Content;
                    segment.Title = options.Title;
                    segment.IsNercOperatingTopics = options.IsNercOperatingTopics;
                    segment.IsNercSimulation = options.IsNercSimulation;
                    segment.IsNercStandard = options.IsNercStandard;
                    segment.IsPartialCredit = options.IsPartialCredit;
                    segment.Duration = options.Duration;
                    segment.Uploads = options.Uploads;
                    segment.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    segment.ModifiedDate = DateTime.Now;
                    var authResult = await _segmentService.UpdateAsync(segment);
                    if (authResult.IsValid)
                    {
                        return segment;
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                    }
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _segmentService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<List<Segment>> GetAsync()
        {
            var obj_list = await _segmentService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public Segment GetWithObjectives(int id)
        {
            var obj = _segmentService.AllQueryWithInclude(new string[] { nameof(_segment.SegmentObjective_Links) }).Where(x => x.Id == id).FirstOrDefault();
            return obj;
        }

        public async Task<Segment> GetAsync(int id)
        {
            var obj = await _segmentService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read);
                if (result.Succeeded)
                {
                    return obj;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _segmentService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task LinkObjective(int id, UpdateSegmentObjectiveOrderListVM options)
        {
            var segment = await _segmentService.GetWithIncludeAsync(id, new string[] { nameof(Segment.SegmentObjective_Links) });
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            foreach (var opt in options.SegmentObjectives)
            {
                var existingLink = segment.SegmentObjective_Links.FirstOrDefault(x => x.Id == opt.SegmentObjectiveLinkId);

                if (existingLink != null)
                {
                    existingLink.SetOrder(opt.Order);
                }
                else
                {
                    SegmentObjective_Link newLink = null;

                    switch (opt.Type)
                    {
                        case "EO":
                            var alreadyLinkedEO = segment.SegmentObjective_Links.Any(x => x.EnablingObjectiveId == opt.ObjectiveId);
                            if (alreadyLinkedEO) continue;
                            var enablingObjective = await _enablingObjectiveService.GetAsync(opt.ObjectiveId);
                            if (enablingObjective != null)
                            {
                                newLink = new SegmentObjective_Link(segment, null, enablingObjective, null)
                                {
                                    Order = opt.Order
                                };
                            }
                            break;

                        case "Task":
                            var alreadyLinkedTask = segment.SegmentObjective_Links.Any(x => x.TaskId == opt.ObjectiveId);
                            if (alreadyLinkedTask) continue;
                            var task = await _taskService.GetAsync(opt.ObjectiveId);
                            if (task != null)
                            {
                                newLink = new SegmentObjective_Link(segment, task, null, null)
                                {
                                    Order = opt.Order
                                };
                            }
                            break;

                        case "Custom":
                            var alreadyLinkedCustomEO = segment.SegmentObjective_Links.Any(x => x.CustomEOId == opt.ObjectiveId);
                            if (alreadyLinkedCustomEO) continue;
                            var customObjective = await _customEOService.FindQuery(x => x.Id == opt.ObjectiveId).FirstOrDefaultAsync();
                            if (customObjective != null)
                            {
                                newLink = new SegmentObjective_Link(segment, null, null, customObjective)
                                {
                                    Order = opt.Order
                                };
                            }
                            break;

                        default:
                            throw new ArgumentException($"Unsupported ObjectiveType: {opt.Type}");
                    }

                    if (newLink != null)
                    {
                        segment.SegmentObjective_Links.Add(newLink);
                    }
                }
            }

            var validationResult = await _segmentService.UpdateAsync(segment);

            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }


        public async System.Threading.Tasks.Task UnlinkObjective(int id, SegmentObjective_LinkOptions options)
        {
            var obj = await _segmentService.GetWithIncludeAsync(id, new string[] { nameof(_segment.SegmentObjective_Links) });
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                foreach (var eoId in options.EnablingObjectiveIds)
                {
                    var segObj = obj.SegmentObjective_Links.FirstOrDefault(x => x.EnablingObjectiveId == eoId);
                    if (segObj != null)
                    {
                        segObj.Delete();
                        await _segmentObjectiveLinkService.UpdateAsync(segObj);
                    }
                }

                foreach (var taskId in options.TaskIds)
                {
                    var segObj = obj.SegmentObjective_Links.FirstOrDefault(x => x.TaskId == taskId);
                    if (segObj != null)
                    {
                        segObj.Delete();
                        await _segmentObjectiveLinkService.UpdateAsync(segObj);
                    }
                }

                foreach (var coId in options.CustomEOIds)
                {
                    var segObj = obj.SegmentObjective_Links.FirstOrDefault(x => x.CustomEOId == coId);
                    if (segObj != null)
                    {
                        segObj.Delete();
                        await _segmentObjectiveLinkService.UpdateAsync(segObj);
                    }
                }

            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task<List<SegmentObjective_Link>> GetLinkedObjectives(int id)
        {
            var obj = await _segmentObjectiveLinkService.FindQueryWithIncludeAsync(x => x.SegmentId == id,new string[] {"CustomEnablingObjective.EnablingObjective_Topic", "CustomEnablingObjective.EnablingObjective_Category", "CustomEnablingObjective.EnablingObjective_SubCategory", "EnablingObjective.EnablingObjective_Topic", "EnablingObjective.EnablingObjective_Category", "EnablingObjective.EnablingObjective_SubCategory" }).ToListAsync();
            foreach(var link in obj)
            {
                if(link.TaskId != null)
                {
                    link.Task = await _taskService.FindQuery(x => x.Id == link.TaskId).FirstOrDefaultAsync();
                    link.Task.SubdutyArea = await _subDutyareaService.FindQuery(x => x.Id == link.Task.SubdutyAreaId).FirstOrDefaultAsync();
                    link.Task.SubdutyArea.DutyArea = await _dutyAreaService.FindQuery(x => x.Id == link.Task.SubdutyArea.DutyAreaId).FirstOrDefaultAsync();
                }
                if (link.EnablingObjective != null)
                {
                    link.EnablingObjective.EnablingObjective_Category.EnablingObjective_SubCategories = null;
                    link.EnablingObjective.EnablingObjective_Category.EnablingObjectives = null;
                    link.EnablingObjective.EnablingObjective_SubCategory.EnablingObjective_Topics = null;
                    link.EnablingObjective.EnablingObjective_SubCategory.EnablingObjectives = null;
                }
            }
            return obj.OrderBy(o => o.Order).ToList();
        }

        public async Task<Segment> UpdateAsync(int id, SegmentUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                // Todo Update logic
                obj.Title = options.Title;
                obj.Content = options.Content;
                obj.Duration = options.Duration;
                obj.IsNercStandard = options.IsNercStandard;
                obj.IsNercSimulation = options.IsNercSimulation;
                obj.IsNercOperatingTopics = options.IsNercOperatingTopics;
                obj.IsPartialCredit = options.IsPartialCredit;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                var validationResult = await _segmentService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

    }
}
