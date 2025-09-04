using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Model.ClientUser;
using Microsoft.AspNetCore.Identity;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using System.Security.Claims;
using QTD2.Infrastructure.Reports.Generation;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Hashing.Interfaces;

namespace QTD2.Application.Services.QTD
{
    public class NotificationService : Interfaces.Services.QTD.INotificationService
    {
        private readonly Infrastructure.Notification.Interfaces.INotificationFactory _notificationFactory;
        private readonly Infrastructure.Notification.Interfaces.INotifierFactory _notifierFactory;
        private Infrastructure.Notification.Interfaces.INotifier _notifier;
        private readonly UserManager<AppUser> _userManager;
        private readonly IClientUserSettings_GeneralSettingService _clientUserSettings_GeneralSettingService;
        private readonly IHasher _hasher;

        public NotificationService(
            Infrastructure.Notification.Interfaces.INotificationFactory notificationFactory,
            Infrastructure.Notification.Interfaces.INotifierFactory notifierFactory,
            UserManager<AppUser> userManager,
            IClientUserSettings_GeneralSettingService clientUserSettings_GeneralSettingService,
            IHasher hasher
            )
        {
            _notificationFactory = notificationFactory;
            _notifierFactory = notifierFactory;
            _userManager = userManager;
            _clientUserSettings_GeneralSettingService = clientUserSettings_GeneralSettingService;
            _hasher = hasher;
        }

        public async Task<bool> SendReportAsync(Domain.Entities.Core.Report report, string file, List<string> tos)
        {
            var notification = _notificationFactory.CreateSendReportNotification(report, file, tos);
            return await sendNotificationAsync(notification);
        }

        protected async Task<bool> sendNotificationAsync(Infrastructure.Notification.Interfaces.INotification notification)
        {
            try
            {
                _notifier = _notifierFactory.GetNotifier(notification);
                return await _notifier.NotifyAsync(notification);
            }
            catch (Exception e)
            {
                // TODO Log
                var ex = e;
                return false;
            }
        }
    }
}
