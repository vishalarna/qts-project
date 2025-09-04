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
    public class OnTaskQualification_Evalutor_LinkCreatedHandler : INotificationHandler<OnTaskQualification_Evalutor_LinkCreated>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;

        private readonly IEmployeeService _employeeService;
        private readonly ITQEmpSettingService _tqEmpSettingsService;

        public OnTaskQualification_Evalutor_LinkCreatedHandler(
            INotificationService notificationService, 
            IClientSettings_NotificationService clientSettings_NotificationService,
            IEmployeeService employeeService,
            ITQEmpSettingService tqEmpSettingsService)
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _employeeService = employeeService;
            _tqEmpSettingsService = tqEmpSettingsService;
        }

        public async Task Handle(OnTaskQualification_Evalutor_LinkCreated notification, CancellationToken cancellationToken)
        {
            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Task Qualification - Evaluator");

            if (!clientSettings_Notification.Enabled) return;

            var employee = await _employeeService.GetAsync(notification.TaskQualification_Evaluator_Link.EvaluatorId);
            var tqEmpSettings = await _tqEmpSettingsService.GetTQEmpSettingByTQId(notification.TaskQualification_Evaluator_Link.TaskQualificationId);

            Domain.Entities.Core.EMPTaskQualitificationEvaluatorNotification taskQualitificationEvaluatorNotification = new Domain.Entities.Core.EMPTaskQualitificationEvaluatorNotification(tqEmpSettings.ReleaseDate ?? DateTime.Now, notification.TaskQualification_Evaluator_Link.Id, employee.PersonId, clientSettings_Notification.Steps.First().Id);
            await _notificationService.AddAsync(taskQualitificationEvaluatorNotification);
        }
    }
}
