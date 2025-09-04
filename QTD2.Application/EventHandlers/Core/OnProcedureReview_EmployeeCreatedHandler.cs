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
    public class OnProcedureReview_EmployeeCreatedHandler : INotificationHandler<OnProcedureReview_EmployeeCreated>
    {
        private readonly INotificationService _notificationService; 
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;

        private readonly IProcedureReviewService _procedureReviewService;

        public OnProcedureReview_EmployeeCreatedHandler(
            Domain.Interfaces.Service.Core.INotificationService notificationService, 
            IClientSettings_NotificationService clientSettings_NotificationService,
            IProcedureReviewService procedureReviewService)
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _procedureReviewService = procedureReviewService;
        }

        public async Task Handle(OnProcedureReview_EmployeeCreated notification, CancellationToken cancellationToken)
        {
            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Procedure Review");

            if (!clientSettings_Notification.Enabled) return;

            var procedureReview = await _procedureReviewService.GetAsync(notification.ProcedureReview_Employee.ProcedureReviewId);

            if (procedureReview == null || !procedureReview.Active || !procedureReview.IsPublished || procedureReview.Deleted) return;

            Domain.Entities.Core.EMPProcedureReviewNotification procedureReviewNotification = new Domain.Entities.Core.EMPProcedureReviewNotification(procedureReview.StartDateTime, notification.ProcedureReview_Employee.Id, notification.ProcedureReview_Employee.Employee.PersonId, clientSettings_Notification.Steps.First().Id);
            await _notificationService.AddAsync(procedureReviewNotification);
        }
    }
}
