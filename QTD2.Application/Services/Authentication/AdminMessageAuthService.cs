using Microsoft.AspNetCore.Authorization;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Service.Authentication;
using QTD2.Infrastructure.Authorization.Operations.Authentication;
using QTD2.Infrastructure.Model.AdminMessageAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAdminMessageAuthDomainService = QTD2.Domain.Interfaces.Service.Authentication.IAdminMessageAuthService;

namespace QTD2.Application.Services.Authentication
{
    public class AdminMessageAuthService: Interfaces.Services.Authentication.IAdminMessageAuthService
    {
        private readonly Interfaces.Services.Authentication.IInstanceService _instanceService;
        private readonly IAdminMessageAuthDomainService _adminMessageAuthDomainService;
        public AdminMessageAuthService(Interfaces.Services.Authentication.IInstanceService instanceService, IAdminMessageAuthDomainService adminMessageAuthDomainService)
        {
            _instanceService = instanceService;
            _adminMessageAuthDomainService = adminMessageAuthDomainService;
        }

        public async Task CreateAdminMessageAsyn(AdminMessageAuthCreateOptions options)
        {
            var messages = new List<AdminMessageAuth>();
            var instances = await _instanceService.GetAllInstancesWithInstanceSettingsAsync();

            foreach (var instance in instances)
            {
                var MessageAuth = new AdminMessageAuth
                {
                    Message = options.Message,
                    Instance = instance.Name,
                    ExpirationDate = options.ExpiryDate,
                };
                messages.Add(MessageAuth);
            }
            await _adminMessageAuthDomainService.AddRangeAsync(messages);
            
        }
        
        public async Task<List<AdminMessageAuth>> GetAllAdminMessageForInstanceAsync(string instanceName)
        {
            var messages = (await _adminMessageAuthDomainService.GetAdminMessagesForInstanceAsync(instanceName)).ToList();
            return messages;
        }

        public async Task<List<AdminMessageAuth>> UpdateAdminMessageAuthReceivedStatusAsync(string instance, AdminMessageSourceIdOptions options)
        {
            var messagesToUpdate = await _adminMessageAuthDomainService.GetAdminMessagesBySourceMessageIdAsync(instance, options.SourceMessageIds);
            var messages = new List<AdminMessageAuth>();
            foreach (var message in messagesToUpdate)
            {
                message.Received = true;
                message.ReceivedDate = DateTime.UtcNow;
                messages.Add(message);
            }

            await _adminMessageAuthDomainService.BulkUpdateAsync(messages);
            return messages;
        }

        public async Task<List<AdminMessageAuth>> GetAllAdminMessageAsync()
        {
            var messages = (await _adminMessageAuthDomainService.GetAllAdminMessagesAsync()).ToList();
            return messages;
        }
    }
}
