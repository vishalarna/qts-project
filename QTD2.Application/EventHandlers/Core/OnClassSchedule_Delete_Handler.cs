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
   public class OnClassSchedule_Delete_Handler : INotificationHandler<OnClassSchedule_Delete>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IDocumentService _documentService;

        public OnClassSchedule_Delete_Handler(Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            IClassScheduleService classScheduleService,
            IDocumentService documentService
            )
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _classScheduleService = classScheduleService;
            _documentService = documentService;
        }

        public async Task Handle(OnClassSchedule_Delete notification, CancellationToken cancellationToken)
        {
            var classSchedule = await _classScheduleService.GetClassScheduleWithEmployeesAndRostersAsync(notification.ClassSchedule.Id);

            var documents = await _documentService.GetActiveByLinkedDataAsync("ClassSchedules", notification.ClassSchedule.Id);
            foreach (var doc in documents)
            {
                doc.Delete();
            }
            await _documentService.BulkUpdateAsync(documents);

            if (classSchedule != null)
            {
                foreach (var employee in classSchedule.ClassSchedule_Employee)
                {
                    employee.Delete();
                }
                foreach (var roster in classSchedule.ClassSchedule_Rosters)
                {
                    roster.Delete();
                }
                foreach (var eval in classSchedule.ClassSchedule_Evaluation_Rosters)
                {
                    eval.Delete();
                }
                foreach (var idp in classSchedule.IDPSchedules)
                {
                    idp.Delete();
                }
                await _classScheduleService.UpdateAsync(classSchedule);
            }
            else
            {
                return;
            }
        }
   }
}
