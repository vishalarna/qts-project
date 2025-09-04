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
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Tool_StatusHistory;
using ITool_StatusHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ITool_StatusHistoryService;
using IToolDomainService = QTD2.Domain.Interfaces.Service.Core.IToolService;


namespace QTD2.Application.Services.Shared
{
    public class Tool_StatusHistoryService : ITool_StatusHistoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Tool_StatusHistoryService> _localizer;
        private readonly ITool_StatusHistoryDomainService _tool_StatusHistoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IToolDomainService _toolDomainService;


        public Tool_StatusHistoryService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<Tool_StatusHistoryService> localizer, ITool_StatusHistoryDomainService tool_StatusHistoryService, UserManager<AppUser> userManager, IToolDomainService toolDomainService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _tool_StatusHistoryService = tool_StatusHistoryService;
            _userManager = userManager;
            _toolDomainService = toolDomainService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _tool_StatusHistoryService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task CreateAsync(Tool_StatusHistoryCreateOptions options)
        {
            foreach (var id in options.ToolIds)
            {
                var hist = new Tool_StatusHistory();
                hist.ToolId = id;
                hist.ChangeEffectiveDate = options.ChangeEffectiveDate;
                hist.ChangeNotes = options.ChangeNotes;
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, Task_HistoryOperations.Create);
                if (result.Succeeded)
                {
                    var validationResult = await _tool_StatusHistoryService.AddAsync(hist);
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
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _tool_StatusHistoryService.UpdateAsync(obj);
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

        public async Task<List<Tool_StatusHistory>> GetAsync()
        {
            var obj_list = await _tool_StatusHistoryService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }


        public async Task<List<ToolLatestActivityVM>> GetAllToolHistories()
        {
            var tools = await _toolDomainService.AllQuery().IgnoreQueryFilters().Select(s => new Tool
            {
                Id = s.Id,
                ToolCategoryId = s.ToolCategoryId,
                Name = s.Name,
                Number = s.Number
            }).ToListAsync();
            //var history = await _shHistoryService.AllQuery().ToListAsync();
            //var users = await _userManager.Users.ToListAsync();
            List<ToolLatestActivityVM> latestActivities = new List<ToolLatestActivityVM>();
            foreach (var tool in tools)
            {
                var latestActivity = new ToolLatestActivityVM();
                var hists = await _tool_StatusHistoryService.FindQuery(x => x.ToolId == tool.Id).ToListAsync();
                foreach (var hist in hists)
                {
                    var user = await _userManager.Users.Where(w => w.UserName == hist.CreatedBy).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        latestActivity.ActivityDesc = hist.ChangeNotes;
                        latestActivity.Title =  tool.Name;
                        latestActivity.Number = tool.Number;
                        latestActivity.ModifiedBy = user.Email;
                        latestActivity.ModifiedDate = hist.CreatedDate;
                        latestActivity.Id = tool.ToolCategoryId;
                        latestActivities.Add(latestActivity);
                    }
                }
            }

            //var latestactivity = from sh in saftyHazards
            //                     join h in history on sh.Id equals h.SafetyHazardId
            //                     join u in _userManager.Users on h.CreatedBy equals u.Email
            //                     select new SaftyHazardLatestActivityVM
            //                     {
            //                         Id = sh.Id,
            //                         ActivityDesc = h.ChangeNotes,
            //                         Title = sh.Number + " - " + sh.Title,
            //                         CreatedBy = u.Email,
            //                         CreatedDate = h.CreatedDate,
            //                     };

            return latestActivities.OrderByDescending(x => x.CreatedDate).ToList();
        }



        public async Task<Tool_StatusHistory> GetAsync(int id)
        {
            var obj = await _tool_StatusHistoryService.GetAsync(id);
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

                var validationResult = await _tool_StatusHistoryService.UpdateAsync(obj);
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

        public async Task<Tool_StatusHistory> UpdateAsync(int id, Tool_StatusHistoryUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                obj.ChangeNotes = options.ChangeNotes;
                obj.ChangeEffectiveDate = options.ChangeEffectiveDate;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;

                var validationResult = await _tool_StatusHistoryService.UpdateAsync(obj);
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
