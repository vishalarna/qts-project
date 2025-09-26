using MediatR;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnSkillQualificationReleasedHandler : INotificationHandler<OnSkillQualificationReleased>
    {
        private readonly INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly IEmployeeService _employeeService;
        private readonly ISkillQualificationEmpSettingService _sqEmpSettingsService;

        public OnSkillQualificationReleasedHandler(
            Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            IEmployeeService employeeService,
            ISkillQualificationEmpSettingService sqEmpSettingsService
            )
        {
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _notificationService = notificationService;
            _employeeService = employeeService;
            _sqEmpSettingsService = sqEmpSettingsService;
        }
        public async Task Handle(OnSkillQualificationReleased notification, CancellationToken cancellationToken)
        {
            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Task And Skill Qualification - Trainee");

            if (!clientSettings_Notification.Enabled) return;

            var employee = await _employeeService.GetAsync(notification.SkillQualification.EmployeeId);

            var sqEmpSettings = await _sqEmpSettingsService.GetSQSettingBySkillQualificationIdAsync(notification.SkillQualification.Id);

            Domain.Entities.Core.EMPSkillQualificationTraineeNotification taskQualificationTraineeNotification = new Domain.Entities.Core.EMPSkillQualificationTraineeNotification(sqEmpSettings.ReleaseDate ?? DateTime.Now, notification.SkillQualification.Id, employee.PersonId, clientSettings_Notification.Steps.First().Id);
            await _notificationService.AddAsync(taskQualificationTraineeNotification);
        }
    }
}