using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QTD2.Domain.Events.Core;
using QTD2.Infrastructure.Notification.Interfaces;
using QTD2.Infrastructure.Notification.Notifications;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Domain.Entities.Core;
using Microsoft.AspNetCore.SignalR;

namespace QTD2.Application.EventHandlers.Core
{
    public class OniLACertificationLinkDeletedHandler : INotificationHandler<OnlaILACertificationLinkDeleted>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly IClassScheduleEmployee_ILACertificationLink_PartialCreditService _classScheduleEmployee_ILACertificationLink_PartialCreditService;

        public OniLACertificationLinkDeletedHandler(Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            IClassScheduleEmployee_ILACertificationLink_PartialCreditService classScheduleEmployee_ILACertificationLink_PartialCreditService
            )
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _classScheduleEmployee_ILACertificationLink_PartialCreditService = classScheduleEmployee_ILACertificationLink_PartialCreditService;
        }

        public async System.Threading.Tasks.Task Handle(OnlaILACertificationLinkDeleted notification, CancellationToken cancellationToken)
        {
            var partialCredits = await _classScheduleEmployee_ILACertificationLink_PartialCreditService.GetByILACertificationLinkIdAsync(notification.ILACertificationLink.Id);

            foreach (var partialCredit in partialCredits)
            {
                foreach(var sub in partialCredit.ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits)
                {
                    sub.Delete();
                }
                partialCredit.Delete();
            }
            await _classScheduleEmployee_ILACertificationLink_PartialCreditService.BulkUpdateAsync(partialCredits);
        }
    }
}
