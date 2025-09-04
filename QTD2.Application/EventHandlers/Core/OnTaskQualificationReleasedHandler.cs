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
    public class OnTaskQualificationReleasedHandler : INotificationHandler<OnTaskQualificationReleased>
    {
        private readonly INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly IEmployeeService _employeeService;
        private readonly ITQEmpSettingService _tqEmpSettingsService;

        public OnTaskQualificationReleasedHandler(
            Domain.Interfaces.Service.Core.INotificationService notificationService, 
            IClientSettings_NotificationService clientSettings_NotificationService,
            IEmployeeService employeeService,
            ITQEmpSettingService tqEmpSettingsService
            )
        {
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _notificationService = notificationService;
            _employeeService = employeeService;
            _tqEmpSettingsService = tqEmpSettingsService;
        }

        public async Task Handle(OnTaskQualificationReleased notification, CancellationToken cancellationToken)
        {
            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Task Qualification - Trainee");

            if (!clientSettings_Notification.Enabled) return;

            var employee = await _employeeService.GetAsync(notification.TaskQualification.EmpId);

            var tqEmpSettings = await _tqEmpSettingsService.GetTQEmpSettingByTQId(notification.TaskQualification.Id);

            Domain.Entities.Core.EMPTaskQualificationTraineeNotification taskQualificationTraineeNotification = new Domain.Entities.Core.EMPTaskQualificationTraineeNotification(tqEmpSettings.ReleaseDate ?? DateTime.Now, notification.TaskQualification.Id, employee.PersonId, clientSettings_Notification.Steps.First().Id);
            await _notificationService.AddAsync(taskQualificationTraineeNotification);
        }
    }
}
