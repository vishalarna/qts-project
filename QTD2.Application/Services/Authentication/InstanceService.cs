using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Exceptions;
using QTD2.Domain.Interfaces.Service.Authentication;
using QTD2.Infrastructure.Authorization.Operations.Authentication;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model.Instance;
using IClientApplicationService = QTD2.Application.Interfaces.Services.Authentication.IClientService;
using IClientDomainService = QTD2.Domain.Interfaces.Service.Authentication.IClientService;
using IInstanceDomainService = QTD2.Domain.Interfaces.Service.Authentication.IInstanceService;

namespace QTD2.Application.Services.Shared
{
    public class InstanceService : Interfaces.Services.Authentication.IInstanceService
    {
        private readonly IInstanceDomainService _instanceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IInstanceSettingService _instanceSettingService;
        private readonly IClientApplicationService _clientService;
        private readonly IDatabaseManager _databaseManager;
        private readonly IClientDomainService _clientDomainService;
        private readonly IIdentityProviderService _identityProviderService;
        private readonly IInstanceIdentityProviderLinkService _instanceIdentityProviderLinkService;
        private readonly IAdminMessageAuthService _adminMessageAuthService;

        public InstanceService(
            IInstanceDomainService instanceService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IInstanceSettingService instanceSettingService,
            IClientApplicationService clientService,
            IDatabaseManager databaseManager,
            IClientDomainService clientDomainService, IIdentityProviderService identityProviderService,
            IInstanceIdentityProviderLinkService instanceIdentityProviderLinkService, IAdminMessageAuthService adminMessageAuthService)
        {
            _authorizationService = authorizationService;
            _instanceService = instanceService;
            _httpContextAccessor = httpContextAccessor;
            _instanceSettingService = instanceSettingService;
            _clientService = clientService;
            _databaseManager = databaseManager;
            _clientDomainService = clientDomainService;
            _identityProviderService = identityProviderService;
            _instanceIdentityProviderLinkService = instanceIdentityProviderLinkService;
            _adminMessageAuthService = adminMessageAuthService;
        }

        public async Task<Instance> CreateAsync(InstanceCreateOptions options)
        {
            var client = await _clientService.GetAsync(options.ClientName);
            var instance = new Instance(client.Id, options.Name, options.IsInBeta);
            var isExists = await GetAsync(options.Name) != null;
            if (isExists)
            {
                throw new BadHttpRequestException(message: "InstanceExists");
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, instance, InstanceOperations.Create);
            if (result.Succeeded)
            {
                var validationResult = await _instanceService.AddAsync(instance);
                if (validationResult.IsValid)
                {
                    var instanceIdentityProviderLink = new InstanceIdentityProviderLink(instance.Id,options.IdentityProviderId);
                    await _instanceIdentityProviderLinkService.AddAsync(instanceIdentityProviderLink);
                    if (options.CreateDatabase)
                    {
                        string dbName = options.DatabaseName;
                        var instanceSetting = new InstanceSetting { InstanceId = instance.Id, DatabaseName = dbName, Instance = instance ,ScormTenant = options.ScormTenant,ClientAccountNumber = options.ClientAccountNumber,DefaultIdentityProviderId = options.IdentityProviderId,MFAEnabled =options.MFAEnabled};
                        var instanceSettingsValidResult = await _instanceSettingService.AddAsync(instanceSetting);
                        if (!instanceSettingsValidResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                        try
                        {
                            await AddMessageAsync(instance.Name);
                            await CreateDatabaseAsync(options.Name, new DatabaseCreateOptions());
                            instance = await GetAsync(options.Name);
                        }
                        catch (Exception e)
                        {
                            instanceSetting.Active = false;
                            instance.Active = false;
                            await _instanceSettingService.UpdateAsync(instanceSetting);
                            await _instanceService.UpdateAsync(instance);
                            await _instanceIdentityProviderLinkService.UpdateAsync(instanceIdentityProviderLink);
                            throw new QTDServerException("Instance migration failed");
                        }
                        
                    }

                    return instance;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<string> CreateDatabaseAsync(string name, DatabaseCreateOptions options)
        {
            var instance = (await _instanceService.FindAsync(r => r.Name == name)).FirstOrDefault();

            if (instance == null)
            {
                throw new KeyNotFoundException(message: $"InstanceNotFound {name}");
            }

            bool authorized = false;

            if (!authorized)
            {
                AuthorizationResult result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, instance, InstanceOperations.CreateDatabase);
                authorized = result.Succeeded;
            }

            if (authorized)
            {
                var instanceSettings = (await _instanceSettingService.FindAsync(r => r.InstanceId == instance.Id)).FirstOrDefault();

                if (instanceSettings == null)
                {
                    // to do -> change this to create the settings
                    throw new KeyNotFoundException(message: $"InstanceSettings {name}");
                }

                return await _databaseManager.MigrateDatabaseAsync(instanceSettings.DatabaseName);
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task DeleteAsync(string name)
        {
            var instance = await GetAsync(name);
            var result = _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, instance, InstanceOperations.Delete).Result;
            if (result.Succeeded)
            {
                instance.Deactivate();

                var validationResult = await _instanceService.UpdateAsync(instance);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<Instance> GetAsync(string name)
        {
            var instance = (await _instanceService.FindAsync(r => r.Name == name)).FirstOrDefault();
            if (instance != null)
            {
                var result = _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, instance, InstanceOperations.Read).Result;
                if (result.Succeeded)
                {
                    return instance;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: "OperationNotAllowed");
                }
            }

            return instance;
        }

        public async Task<List<Instance>> GetAsync(bool onlyActive = false)
        {
            List<Instance> instances = new List<Instance>();

            if(onlyActive)
            {
                instances = (await _instanceService.GetActiveInstancesAsync()).ToList();
            }
            else
            {
                instances = (await _instanceService.AllAsync()).ToList();
            }
            instances = instances.Where(client => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, client, ClientOperations.Read).Result.Succeeded).ToList();

            return instances;
        }

        public async Task<InstanceSetting> GetInstanceSettingsAsync(string name)
        {
            var instance = (await _instanceService.FindWithIncludeAsync(r => r.Name == name, new[] { "InstanceSetting.DefaultIdentityProvider" })).FirstOrDefault();
            return instance == null ? null : instance.InstanceSetting;
        }

        public async Task<List<InstanceSetting>> GetAllInstanceSettingAsync()
        {
            var allInstanceSetting = await _instanceService.AllWithIncludeAsync(new[] { "InstanceSetting" });
            var instanceSetting = allInstanceSetting.Where(r => r.InstanceSetting != null).Select(e => e.InstanceSetting).ToList();
            return instanceSetting;
        }

        public async Task<List<InstanceSetting>> GetAllActiveInstanceSettingAsync()
        {
            var allActiveInstanceSetting = await _instanceService.FindWithIncludeAsync(i => i.Active == true ,new[] { "InstanceSetting" });
            var instanceSetting = allActiveInstanceSetting.Where(r => r.InstanceSetting != null).Select(e => e.InstanceSetting).ToList();
            return instanceSetting;
        }

        public async Task<Instance> UpdateAsync(string name, InstanceUpdateOptions options)
        {
            var instance = (await _instanceService.FindWithIncludeAsync(r => r.Name == name, new[] { "InstanceSetting.DefaultIdentityProvider" })).FirstOrDefault();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, instance, InstanceOperations.Update);
            if (result.Succeeded)
            {
                instance.Name = options.Name;
                instance.IsInBeta = options.IsInBeta;
                if(options.ClientAccountNumber != null)
                {
                    instance.InstanceSetting.ClientAccountNumber = Convert.ToInt32(options.ClientAccountNumber);
                }
                if (options.MFAEnabled != null)
                {
                    instance.InstanceSetting.MFAEnabled = options.MFAEnabled;
                }
                instance.InstanceSetting.DefaultIdentityProviderId = options.IdentityProviderId;
                var validationResult = await _instanceService.UpdateAsync(instance);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    var instanceSettings = await GetInstanceSettingsAsync(instance.Name);
                    instance.InstanceSetting = instanceSettings;
                    return instance;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<Client> GetClientByInstanceNameAsync(string instanceName)
        {
            var instance = await _instanceService.FindAsync(r => r.Name == instanceName);
            var client = await _clientDomainService.FindAsync(r => r.Id == instance.FirstOrDefault().ClientId);
            return client.FirstOrDefault();
        }
        public Task UpdateDatabaseAsync(string name, DatabaseUpdateOptions options)
        {
            throw new NotImplementedException();
        }

        public async Task<InstanceSetting> GetInstanceSettingsByScormTenantAsync(string tenantName)
        {
            var instance = (await _instanceService.FindWithIncludeAsync(r => r.InstanceSetting.ScormTenant == tenantName, new[] { "InstanceSetting" })).FirstOrDefault();
            return instance == null ? null : instance.InstanceSetting;
        }

        public async Task<List<Instance>> GetAllInstancesWithInstanceSettingsAsync()
        {
            var allInstances = await _instanceService.AllWithIncludeAsync(new[] { "InstanceSetting" });
            return allInstances.ToList();
        }

        public async Task<InstanceSetting> UpdateInstanceSettingAsync(string name, PublicInstanceSettingUpdatOptions options)
        {
            var instance = (await _instanceService.FindWithIncludeAsync(x => x.Name == name, new[] { "InstanceSetting" })).FirstOrDefault();
            var instanceSetting = instance.InstanceSetting;
            if(instance != null && instanceSetting != null)
            {
                instanceSetting.PublicUrl = options.PublicUrl;
                instance.InstanceSetting = instanceSetting;
                var valid = await _instanceService.UpdateAsync(instance);
                return instance.InstanceSetting;
            }
            else
            {
                throw new QTDServerException("OperationNotAllowed", false);
            }
        }

        public async Task AddMessageAsync(string name)
        {
            var messages = (await _adminMessageAuthService.GetAllAdminMessagesAsync()).GroupBy(m => m.Message).Select(g => g.First()).ToList();
            var newInstanceMessages = new List<AdminMessageAuth>();
            foreach (var message in messages) 
            {
                var newInstanceMessage = new AdminMessageAuth
                {
                    Message = message.Message,
                    Instance = name,
                    Received = message.Received,
                    ReceivedDate = message.ReceivedDate,
                    ExpirationDate = message.ExpirationDate,
                    Active = true,
                    Deleted = false
                };   
                newInstanceMessages.Add(newInstanceMessage);
            }

            await _adminMessageAuthService.AddRangeAsync(newInstanceMessages);

        }
    }
}
