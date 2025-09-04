using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Application.Interfaces.Services.Authentication;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Authorization.Operations.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Domain;

namespace QTD2.Application.Services.Shared
{
    public class ClientService : IClientService
    {
        private readonly Domain.Interfaces.Service.Authentication.IClientService _clientService;
        private readonly Domain.Interfaces.Service.Authentication.IInstanceService _instanceService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientService(
            Domain.Interfaces.Service.Authentication.IClientService clientService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            Domain.Interfaces.Service.Authentication.IInstanceService instanceService,
            UserManager<AppUser> userManager)
        {
            _clientService = clientService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _instanceService = instanceService;
            _userManager = userManager;
        }

        public Task<Client> GetAsync(int clientId)
        {
            return _clientService.GetAsync(clientId);
        }

        public async Task<Client> GetAsync(string name)
        {
            var client = (await _clientService.FindAsync(r => r.Name == name)).FirstOrDefault();
            if (client != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, client, ClientOperations.Read);
                if (result.Succeeded)
                {
                    return client;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: "OperationNotAllowed");
                }
            }

            return client;
        }

        public async Task<List<Client>> GetByUserAsync(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var claims = await _userManager.GetClaimsAsync(user);
            var instanceClaims = claims.Where(r => r.Type == CustomClaimTypes.InstanceName).ToList();
            List<string> instanceNames = instanceClaims.Select(r => r.Value).ToList();
            
            var clients = (await _clientService.GetByInstanceListAsync(instanceNames)).ToList();
            clients = clients.Where(client => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, client, ClientOperations.Read).Result.Succeeded).ToList();
            return clients;
            
        }
        public async Task<List<Client>> GetAllClientsAsync(string username)
        {
            var user =  await _userManager.FindByEmailAsync(username);
            var claims = await _userManager.GetClaimsAsync(user);

            var adminClaim = claims.Where(r => r.Type == CustomClaimTypes.IsAdmin).FirstOrDefault();
            var isAdmin = adminClaim == null ? false : Convert.ToBoolean(adminClaim.Value);
            var clients = new List<Client>();
            if (isAdmin)
            {
                clients = (await _clientService.FindAsync(x=>x.Active)).ToList();
                clients = clients.Where(client => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, client, ClientOperations.Read).Result.Succeeded).ToList();
            }
            return clients;
        }

        public async Task<Client> CreateClientAsync(CreateClientOptions clientOptions)
        {
            var client = new Client { Name = clientOptions.Name };
            var clientExists = (await GetAsync(clientOptions.Name)) != null;
            if (clientExists)
            {
                throw new BadHttpRequestException(message: "ClientExists");
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, client, ClientOperations.Create);

            if (result.Succeeded)
            {
                var validationResult = await _clientService.AddAsync(client);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(message: string.Join(',', validationResult.Errors));
                }
                return client;
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task UpdateClientAsync(string name, string updateName)
        {
            var client = await GetAsync(name);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, client, ClientOperations.Update);
            if (result.Succeeded)
            {
                client.Name = updateName;

                var validationResult = await _clientService.UpdateAsync(client);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task DeleteClientAsync(string name)
        {
            var client = await GetAsync(name);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, client, ClientOperations.Delete);
            if (result.Succeeded)
            {
                client.Deactivate();

                var validationResult = await _clientService.UpdateAsync(client);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<List<Instance>> GetInstancesAsync(string name)
        {
            var client = await GetAsync(name);
            var instances = (await _instanceService.FindWithIncludeAsync(x => x.ClientId == client.Id, new string[] { "InstanceSetting.DefaultIdentityProvider" })).Where(x => x.Active);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, client, ClientOperations.Read);
            if (result.Succeeded)
            {
                return instances.ToList();
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }
    }
}
