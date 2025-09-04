using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.ClientUser;

namespace QTD2.Application.Services.Shared
{
    public class ClientUserServices : IClientUserService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IPersonService _personService;
        private readonly IStringLocalizer<ClientUserServices> _localizer;
        private readonly Domain.Interfaces.Service.Core.IClientUserService _clientUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public ClientUserServices(
            Domain.Interfaces.Service.Core.IClientUserService clientUserService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IPersonService personService,
            IStringLocalizer<ClientUserServices> localizer,
            UserManager<AppUser> userManager)
        {
            _clientUserService = clientUserService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _personService = personService;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<List<ClientUser>> GetAsync()
        {
            var clientUsers = await _clientUserService.AllAsync();
            clientUsers = clientUsers.Where(cu => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, cu, ClientUserOperations.Read).Result.Succeeded).ToList();
            return clientUsers.ToList();
        }

        public async Task<ClientUser> GetAsync(int clientUserId)
        {
            var clientUser = await _clientUserService.GetAsync(clientUserId);
            if (clientUser != null)
            {
                var result = (await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, clientUser, ClientUserOperations.Read));
                if (result.Succeeded)
                {
                    return clientUser;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return clientUser;
        }

        public async Task<ClientUser> CreateAsync(ClientUserCreateOptions options, bool isReturnConflictExp = false)
        {
            var clientUser = (await _clientUserService.FindAsync(r => r.PersonId == options.PersonId)).FirstOrDefault();
            if (clientUser != null)
            {
                if (isReturnConflictExp)
                {
                    throw new ConflictExceptionHelper(clientUser);
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["ClientUserAlreadyExists"]);
                }
            }

            clientUser = new ClientUser(options.PersonId);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, clientUser, ClientUserOperations.Create);
            if (result.Succeeded)
            {
                clientUser.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                clientUser.CreatedDate = DateTime.Now;
                var validationResult = await _clientUserService.AddAsync(clientUser);
                if (validationResult.IsValid)
                {
                    return clientUser;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var clientUser = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, clientUser, ClientUserOperations.Delete);
            if (result.Succeeded)
            {
                clientUser.Delete();

                var validationResult = await _clientUserService.UpdateAsync(clientUser);

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

        public async Task<ClientUser> ActivateAsync(int personId)
        {
            var clientUser = (await _clientUserService.FindAsync(x => x.PersonId == personId)).FirstOrDefault();
            if (clientUser != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, clientUser, ClientUserOperations.Update);
                if (result.Succeeded)
                {
                    clientUser.Activate();
                    var validationResult = await _clientUserService.UpdateAsync(clientUser);

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
            return clientUser;
        }

        public async Task<ClientUser> DeactivateAsync(int personId)
        {
            var clientUser = (await _clientUserService.FindAsync(x => x.PersonId == personId)).FirstOrDefault();
            if (clientUser != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, clientUser, ClientUserOperations.Update);
                if (result.Succeeded)
                {
                    clientUser.Deactivate();
                    var validationResult = await _clientUserService.UpdateAsync(clientUser);

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
            return clientUser;
        }
    }
}
