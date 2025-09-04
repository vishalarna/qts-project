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

namespace QTD2.Application.EventHandlers.Core
{
    public class OnProcedureReviewPublishedHandler : INotificationHandler<OnProcedureReviewPublished>
    {
        private readonly INotificationService _notificationService; 
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly IProcedureReviewService _procedureReviewService;

        public OnProcedureReviewPublishedHandler(
            Domain.Interfaces.Service.Core.INotificationService notificationService, 
            IClientSettings_NotificationService clientSettings_NotificationService,
            IProcedureReviewService procedureReviewService)
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _procedureReviewService = procedureReviewService;
        }

        public async Task Handle(OnProcedureReviewPublished notification, CancellationToken cancellationToken)
        {
            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Procedure Review");

            if (!clientSettings_Notification.Enabled) return;

            var procedureReview = await _procedureReviewService.GetWithEmployeesAsync(notification.ProcedureReview.Id);

            var incompleteEmployees = procedureReview.ProcedureReview_Employee.Where(r => !r.IsCompleted).ToList();

            var notifications = new List<Domain.Entities.Core.Notification>();

            foreach(var pre in incompleteEmployees)
            {
                Domain.Entities.Core.EMPProcedureReviewNotification procedureReviewNotification = new Domain.Entities.Core.EMPProcedureReviewNotification(procedureReview.StartDateTime, pre.Id, pre.Employee.PersonId, clientSettings_Notification.Steps.First().Id);
                notifications.Add(procedureReviewNotification);
            }

            await _notificationService.AddRangeAsync(notifications);
        }
    }
}
