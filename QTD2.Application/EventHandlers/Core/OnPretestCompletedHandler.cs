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
    public class OnPretestCompletedHandler : INotificationHandler<OnPretestCompleted>
    {
        private readonly INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly IClassScheduleEmployeeService _classEmployeeService;

        public OnPretestCompletedHandler(
            Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            IClassScheduleEmployeeService classEmployeeService)
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _classEmployeeService = classEmployeeService;
        }

        public async Task Handle(OnPretestCompleted notification, CancellationToken cancellationToken)
        {
            var clientSettings_notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Pretest");

            if (!clientSettings_notification.Enabled) return;

            var classScheduleEmployee = await _classEmployeeService.GetEmployeeForClassScheduleAsync(notification.ClassSchedule_Roster.ClassScheduleId.Value, notification.ClassSchedule_Roster.EmpId);

            Domain.Entities.Core.EMPPretestNotificiation classScheduleNotification = new Domain.Entities.Core.EMPPretestNotificiation(DateTime.Now, classScheduleEmployee.Id, classScheduleEmployee.Employee.PersonId, clientSettings_notification.Steps.First().Id);
            await _notificationService.AddAsync(classScheduleNotification);
        }
    }
}
