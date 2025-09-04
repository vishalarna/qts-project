using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.ILA_Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Shared
{
    public class ILAResourceService : IILAResourceService
    {
        private readonly QTD2.Domain.Interfaces.Service.Core.IILAResourceService _resourceService;
        private readonly QTD2.Domain.Interfaces.Service.Core.IILAService _iLAService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStringLocalizer<ILAResourceService> _localizer;
        public ILAResourceService(QTD2.Domain.Interfaces.Service.Core.IILAResourceService resourceService,
            QTD2.Domain.Interfaces.Service.Core.IILAService iLAService,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager,
            IStringLocalizer<ILAResourceService> localizer)
        {
            _resourceService = resourceService;
            _iLAService = iLAService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<ILA_Resource> CreateAsync(int ilaId, ILAResourceCreateOptions options)
        {
            var obj = (await _iLAService.FindAsync(x => x.Id == ilaId)).FirstOrDefault();
            if (obj != null)
            {
                if (!String.IsNullOrEmpty(options.Title))
                {
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
                    if (result.Succeeded)
                    {
                        var resource = new ILA_Resource(ilaId, options.Title, options.comments);
                        var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                        //resource.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                        //resource.CreatedDate = DateTime.Now;
                        resource.Create(userName);
                        var validationResult = await _resourceService.AddAsync(resource);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                        else
                        {
                            return resource;
                        }
                    }
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["UnableToCreateILAResource"].Value);
            }
            throw new NotImplementedException();
        }

        public async Task<ILA_Resource> UpdateAsync(int ilaId, int editILAResourceId, ILAResourceCreateOptions options)
        {
            var resource = await _resourceService.GetAsync(editILAResourceId);
            if (resource == null)
            {
                throw new QTDServerException( _localizer["ILAResourceNotFound"]);
            }
            else
            {
                var obj = (await _iLAService.FindAsync(x => x.Id == ilaId)).FirstOrDefault();
                if (obj != null)
                {
                    if (!String.IsNullOrEmpty(options.Title))
                    {
                        var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);
                        if (result.Succeeded)
                        {
                            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                            resource.Modify(userName);
                            resource.Title = options.Title;
                            resource.Comments = options.comments;
                            var validationResult = await _resourceService.UpdateAsync(resource);
                            if (!validationResult.IsValid)
                            {
                                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                            }
                            else
                            {
                                return resource;
                            }
                        }
                    }
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["UnableToUpdateILAResource"].Value);
                }
                throw new NotImplementedException();
            }
        }

        public async Task<List<ILA_Resource>> GetAsync(int ilAId)
        {
            var ilaResources = await _resourceService.GetAllResourcesAsync(ilAId);
            return ilaResources.ToList();
        }
        public async Task<ILA_Resource> RemoveResourceILA(int ilaResourceId)
        {
            var resourceIla = await _resourceService.GetAsync(ilaResourceId);

            if (resourceIla != null)
            {
                await _resourceService.DeleteAsync(resourceIla);

            }
            return resourceIla;
        }
    }
}
