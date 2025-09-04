using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.ILA_Topic;
using QTD2.Infrastructure.Model.Provider;
using Quartz.Impl.AdoJobStore.Common;
using IILA_TopicDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TopicService;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;

namespace QTD2.Application.Services.Shared
{
    public class ILA_TopicService : IILA_TopicService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ILA_TopicService> _locaizer;
        private readonly IILA_TopicDomainService _ilaTopicService;
        private readonly IILADomainService _ilaService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILA_Topic _ilaTopic;

        public ILA_TopicService(IILA_TopicDomainService ilaTopicService, IStringLocalizer<ILA_TopicService> locaizer, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IILADomainService ilaService)
        {
            _ilaTopicService = ilaTopicService;
            _locaizer = locaizer;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _ilaTopic = new ILA_Topic();
            _ilaService = ilaService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var ilatopic = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ilatopic, ILA_TopicOperations.Delete);

            if (result.Succeeded)
            {
                ilatopic.Activate();

                var validationResult = await _ilaTopicService.UpdateAsync(ilatopic);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<ILA_Topic> CreateAsync(ILA_TopicCreateOptions options)
        {
            var ilatopic = (await _ilaTopicService.FindAsync(x => x.Name == options.Name)).FirstOrDefault();
            if (ilatopic == null)
            {
                ilatopic = new ILA_Topic(options.IsPriority, options.Name);
            }
            else
            {
                throw new BadHttpRequestException(message: _locaizer["ILATopicExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ilatopic, ILA_TopicOperations.Create);
            if (result.Succeeded)
            {
                ilatopic.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                ilatopic.CreatedDate = DateTime.Now;
                var validationResult = await _ilaTopicService.AddAsync(ilatopic);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return ilatopic;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var ilatopic = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ilatopic, ILA_TopicOperations.Delete);

            if (result.Succeeded)
            {
                ilatopic.Delete();

                var validationResult = await _ilaTopicService.UpdateAsync(ilatopic);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<List<ILA_Topic>> GetAsync()
        {
            var ilatopics = await _ilaTopicService.AllAsync();
            ilatopics = ilatopics.Where(ilatopic => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ilatopic, ILA_TopicOperations.Read).Result.Succeeded);
            return ilatopics?.Where(x=>x.Active == true).OrderBy(x=>x.Name).ToList();
        }

        public async Task<List<ILA_TopicVM>> GetILA_TopicsWithCountAndFilterAsync(FilterByOptions filterOptions)
        {
            var ilatopics = (await _ilaTopicService.FindAsync(x=>x.Active == filterOptions.ActiveStatus)).OrderByDescending(o => o.CreatedDate).ToList();
            ilatopics = ilatopics.Where(ilatopic => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ilatopic, ILA_TopicOperations.Read).Result.Succeeded).ToList();
            var ilaTopicVm = new List<ILA_TopicVM>();
            foreach (var topic in ilatopics)
            {
                var hasIlas = await _ilaService.FindAsync(x => x.ILA_Topic_Links.Any(a => a.ILATopicId == topic.Id) && x.Active == filterOptions.ActiveILAStatus && (x.Name.Trim().ToLower().Contains(filterOptions.Filter.Trim().ToLower()) || x.Number.Trim().ToLower().Contains(filterOptions.Filter.ToLower()) || x.ILA_Topic_Links.Any(a => a.ILA_Topic.Name.Trim().ToLower().Contains(filterOptions.Filter.ToLower()))));

                if (hasIlas.Any())
                {
                    ilaTopicVm.Add(new ILA_TopicVM
                    {
                        Id = topic.Id,
                        Active = topic.Active,
                        IsPriority = topic.IsPriority,
                        Name = topic.Name,
                        ILACount = hasIlas.Count(),
                    });
                }
                else
                {
                    switch (filterOptions.DoInclude.Trim().ToLower()) 
                    {
                        case "include":
                            var hasAnyIlas = await _ilaService.FindAsync(x => x.ILA_Topic_Links.Any(a=>a.ILATopicId == topic.Id));
                            if (!hasAnyIlas.Any() && topic.Name.Trim().ToLower().Contains(filterOptions.Filter.ToLower()))
                            {
                                ilaTopicVm.Add(new ILA_TopicVM
                                {
                                    Id = topic.Id,
                                    Active = topic.Active,
                                    IsPriority = topic.IsPriority,
                                    Name = topic.Name,
                                    ILACount = hasAnyIlas.Count(),
                                });
                            }
                            break;
                    }
                }
            }

            return ilaTopicVm?.ToList();
        }

        public async Task<ILA_Topic> GetAsync(int id)
        {
            var ilatopic = await _ilaTopicService.GetAsync(id);
            if (ilatopic != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ilatopic, ILA_TopicOperations.Read);
                if (result.Succeeded)
                {
                    return ilatopic;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_locaizer["ILATopicNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var ilatopic = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ilatopic, ILA_TopicOperations.Delete);

            if (result.Succeeded)
            {
                ilatopic.Deactivate();

                var validationResult = await _ilaTopicService.UpdateAsync(ilatopic);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<ILA_Topic> UpdateAsync(int id, ILA_TopicUpdateOptions options)
        {
            var ilatopic = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ilatopic, ILA_TopicOperations.Update);

            var topicExists = (await _ilaTopicService.FindAsync(x => x.Id != id && x.Name == options.Name)).FirstOrDefault() != null;

            if (topicExists)
            {
                throw new UnauthorizedAccessException(message: _locaizer["TopicNameAlreadyExists"].Value);
            }

            if (result.Succeeded)
            {
                ilatopic.Name = options.Name;
                ilatopic.IsPriority = options.IsPriority;
                ilatopic.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                ilatopic.ModifiedDate = DateTime.Now;
                var validationResult = await _ilaTopicService.UpdateAsync(ilatopic);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return ilatopic;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }
    }
}
