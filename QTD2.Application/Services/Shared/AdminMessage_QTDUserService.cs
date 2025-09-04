using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.AdminMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAdminMessageQTDUserDomainService = QTD2.Domain.Interfaces.Service.Core.IAdminMessage_QTDUserService;
using IQTDUserDomainService = QTD2.Domain.Interfaces.Service.Core.IQTDUserService;

namespace QTD2.Application.Services.Shared
{
    public class AdminMessage_QTDUserService : IAdminMessage_QTDUserService
    {
        private readonly IAdminMessageQTDUserDomainService _adminMessageQTDUserDomainService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IQTDUserDomainService _qTDUserDomainService;
        
        public AdminMessage_QTDUserService(IAdminMessageQTDUserDomainService adminMessageQTDUserDomainService, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IQTDUserDomainService qTDUserDomainService)
        {
            _adminMessageQTDUserDomainService = adminMessageQTDUserDomainService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _qTDUserDomainService = qTDUserDomainService;
        }

        public async Task<List<AdminMessageVM>> GetAdminMessagesAsync()
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;
            var qTDUser = await _qTDUserDomainService.GetQTDUserByUsername(userName);
            var adminMessages = new List<AdminMessage_QTDUser>();
            if (qTDUser != null) 
            {
                adminMessages = (await _adminMessageQTDUserDomainService.GetAdminMessageByUserIdAsync(qTDUser.Id)).ToList();
            }
            var messages = new List<AdminMessageVM>();
            foreach (var message in adminMessages)
            {
                var msg = new AdminMessageVM
                {
                    Id = message.Id,
                    Message = message.AdminMessage.Message,
                };
                messages.Add(msg);
            }
            return messages;
        }

        public async Task<AdminMessage_QTDUser> UpdateAdminMessagesAsync(AdminMessageQTDUserUpdateOptions options)
        {
            var adminMessage = await _adminMessageQTDUserDomainService.GetAdminMessageAsync(options.AdminMessage_QTDUserId);
            if (adminMessage != null)
            {
                adminMessage.Dismissed = true;
            }
            await _adminMessageQTDUserDomainService.UpdateAsync(adminMessage);
            return adminMessage;
        }
    }
}
