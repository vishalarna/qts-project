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
    public class OnTestReleasedHandler : INotificationHandler<OnTestReleased>
    {
        private readonly INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly IEmployeeService _employeeService;

        public OnTestReleasedHandler(
            Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            IEmployeeService employeeService)
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _employeeService = employeeService;
        }

        public async Task Handle(OnTestReleased notification, CancellationToken cancellationToken)
        {
            try
            {
                await _handle(notification, cancellationToken);

            }
            catch (Exception e)
            {
                string s = e.Message;
            }
        }

        private async Task _handle(OnTestReleased notification, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetWithPersonAsync(notification.ClassScheduleRoster.EmpId);

            if (notification.ClassScheduleRoster.TestTypeId == 1)
            {
                var clientSettings_PreTestNotification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Pretest");   
                if (clientSettings_PreTestNotification.Enabled)
                {
                    Domain.Entities.Core.EMPPretestNotificiation pretestNotificiation = new Domain.Entities.Core.EMPPretestNotificiation(notification.ClassScheduleRoster.ReleaseDate.Value, notification.ClassScheduleRoster.Id, employee.PersonId, clientSettings_PreTestNotification.Steps.First().Id);
                    await _notificationService.AddAsync(pretestNotificiation);
                }
            }
            if (notification.ClassScheduleRoster.TestTypeId == 2)
            {
                var clientSettings_TestNotification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Test");
                if (clientSettings_TestNotification.Enabled)
                {
                    Domain.Entities.Core.EMPTestNotification testNotification = new Domain.Entities.Core.EMPTestNotification(DateTime.Now.ToUniversalTime(), notification.ClassScheduleRoster.Id, employee.PersonId, clientSettings_TestNotification.Steps.First().Id);
                    await _notificationService.AddAsync(testNotification);
                }
            }
            if (notification.ClassScheduleRoster.TestTypeId == 6)
            {
                var clientSettings_TestNotification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("Meta ILA - Test Released");
                if (clientSettings_TestNotification.Enabled)
                {
                    Domain.Entities.Core.EMPTestNotification testNotification = new Domain.Entities.Core.EMPTestNotification(DateTime.Now.ToUniversalTime(), notification.ClassScheduleRoster.Id, employee.PersonId, clientSettings_TestNotification.Steps.First().Id);
                    await _notificationService.AddAsync(testNotification);
                }
            }
        }
    }
}
