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
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.DeliveryMethod;
using IDeliveryMethodDomainService = QTD2.Domain.Interfaces.Service.Core.IDeliveryMethodService;

namespace QTD2.Application.Services.Shared
{
    public class DeliveryMethodService : IDeliveryMethodService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<DeliveryMethodService> _localizer;
        private readonly IDeliveryMethodDomainService _deliveryMethodService;
        private readonly UserManager<AppUser> _userManager;

        public DeliveryMethodService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<DeliveryMethodService> localizer, IDeliveryMethodDomainService deliveryMethodService, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _deliveryMethodService = deliveryMethodService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DeliveryMethodOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _deliveryMethodService.UpdateAsync(obj);
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

        public async Task<DeliveryMethod> CreateAsync(DeliveryMethodCreateOptions options)
        {
            var obj = (await _deliveryMethodService.FindAsync(x => x.Name == options.Name)).FirstOrDefault();
            if (obj == null)
            {
                obj = new DeliveryMethod(options.Name, options.DisplayName);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DeliveryMethodOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _deliveryMethodService.AddAsync(obj);
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

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DeliveryMethodOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _deliveryMethodService.UpdateAsync(obj);
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

        public async Task<List<DeliveryMethod>> GetAsync()
        {
            var obj_list = await _deliveryMethodService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DeliveryMethodOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<DeliveryMethod> GetAsync(int id)
        {
            var obj = await _deliveryMethodService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DeliveryMethodOperations.Read);
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

        public async Task<List<DeliveryMethod>> GetNercAsync(bool isNerc)
        {
            var obj = _deliveryMethodService.FindQuery(x => x.IsNerc == isNerc).ToList();
            if (obj != null)
            {
                //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DeliveryMethodOperations.Read);
                //if (result.Succeeded)
                //{
                    return obj;
                //}
                //else
                //{
                //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                //}
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DeliveryMethodOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _deliveryMethodService.UpdateAsync(obj);
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

        public async Task<DeliveryMethod> UpdateAsync(int id, DeliveryMethodUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DeliveryMethodOperations.Update);

            if (result.Succeeded)
            {
                obj.Name = options.Name;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                var validationResult = await _deliveryMethodService.UpdateAsync(obj);
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
