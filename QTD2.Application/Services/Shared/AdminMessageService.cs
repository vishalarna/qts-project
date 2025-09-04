using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.HttpClients;
using QTD2.Infrastructure.Model.AdminMessage;
using QTD2.Infrastructure.Model.AdminMessageAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAdminMessageDomainService = QTD2.Domain.Interfaces.Service.Core.IAdminMessageService;
using IAdminMessageQTDUserDomainService = QTD2.Domain.Interfaces.Service.Core.IAdminMessage_QTDUserService;
namespace QTD2.Application.Services.Shared
{
    public class AdminMessageService : IAdminMessageService
    {
        private readonly IAdminMessageDomainService _adminMessageDomainService;
        private readonly QtdAuthenticationService _qtdAuthenticationService;
        private readonly IQTDService _qtdService;
        IAdminMessageQTDUserDomainService _adminMessageQTDUserDomainService;
        public AdminMessageService(IAdminMessageDomainService adminMessageDomainService, QtdAuthenticationService qtdAuthenticationService, IQTDService qtdService, IAdminMessageQTDUserDomainService adminMessageQTDUserDomainService)
        {
            _adminMessageDomainService = adminMessageDomainService;
            _qtdAuthenticationService = qtdAuthenticationService;
            _qtdService = qtdService;
            _adminMessageQTDUserDomainService = adminMessageQTDUserDomainService;
        }

        public async Task CreateAdminMessageAsync(AdminMessageCreateOptions option)
        {
            var adminMessages = new List<Domain.Entities.Core.AdminMessage>();
            var receivedMessageIds = new List<int>();
            var authMessages = await _qtdAuthenticationService.AuthMessages.GetAdminMessageAsync(option.Instance);
            var qTDUser = await _qtdService.GetQTDUserByUsernameAsync(option.Username);
            if (qTDUser != null)
            {
                var insertAdminMessagesQTD = (await _adminMessageDomainService.GetAllAdminMessageAsync()).ToList();
                var existingSourceIds = insertAdminMessagesQTD
                    .Where(x => x.SourceAdminMessageId != null)
                    .Select(x => x.SourceAdminMessageId) 
                    .ToHashSet();

                 adminMessages = authMessages
                    .Where(authMsg => !existingSourceIds.Contains(authMsg.Id))
                    .Select(authMsg => new Domain.Entities.Core.AdminMessage
                    {
                        SourceAdminMessageId = authMsg.Id,
                        Message = authMsg.Message,
                        ExpirationDate = authMsg.ExpirationDate,
                        ReceivedDate = DateTime.UtcNow
                    })
                    .ToList();

                if (adminMessages.Any())
                {
                    await _adminMessageDomainService.AddRangeAsync(adminMessages);
                }

                var adminMessagesQTD = (await _adminMessageDomainService.GetAllAdminMessageAsync()).ToList();
                var existingMessageIds = (await _adminMessageQTDUserDomainService.GetExistingAdminMessageByUserIdAsync(qTDUser.Id)).Select(r => r.AdminMessageId).ToList();
                var unmappedMessages = adminMessagesQTD.Where(msg => !existingMessageIds.Contains(msg.Id)).ToList();
                var adminMessageQtdUsers = unmappedMessages.Select(msg => new Domain.Entities.Core.AdminMessage_QTDUser
                  (
                          adminMessageId: msg.Id,
                          qtdUserId: qTDUser.Id,
                          dismissed: false)
                  ).ToList();
                if (adminMessageQtdUsers.Any())
                {
                    await _adminMessageQTDUserDomainService.AddRangeAsync(adminMessageQtdUsers);
                }

                receivedMessageIds = adminMessagesQTD.Select(m => m.SourceAdminMessageId).ToList();
                var options = new AdminMessageSourceIdOptions
                {
                    SourceMessageIds = receivedMessageIds
                };
                if (options.SourceMessageIds.Any())
                {
                    await _qtdAuthenticationService.AuthMessages.UpdateAdminMessageAuthReceivedStatusAsync(option.Instance, options);
                }
            }
        }
    }
}
