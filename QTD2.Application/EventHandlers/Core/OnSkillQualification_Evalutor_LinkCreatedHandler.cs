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
    public class OnSkillQualification_Evalutor_LinkCreatedHandler : INotificationHandler<OnSkillQualification_Evalutor_LinkCreated>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;

        private readonly IEmployeeService _employeeService;
        private readonly ISkillQualificationEmpSettingService _sqEmpSettingsService;

        public OnSkillQualification_Evalutor_LinkCreatedHandler(
            INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            IEmployeeService employeeService,
            ISkillQualificationEmpSettingService sqEmpSettingsService)
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _employeeService = employeeService;
            _sqEmpSettingsService = sqEmpSettingsService;
        }

        public async Task Handle(OnSkillQualification_Evalutor_LinkCreated notification, CancellationToken cancellationToken)
        {
            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Task And Skill Qualification - Evaluator");

            if (!clientSettings_Notification.Enabled) return;

            var employee = await _employeeService.GetAsync(notification.SkillQualification_Evaluator_Link.EvaluatorId);
            var sqEmpSettings = await _sqEmpSettingsService.GetSQSettingBySkillQualificationIdAsync(notification.SkillQualification_Evaluator_Link.SkillQualificationId);

            Domain.Entities.Core.EMPSkillQualitificationEvaluatorNotification taskQualitificationEvaluatorNotification = new Domain.Entities.Core.EMPSkillQualitificationEvaluatorNotification(sqEmpSettings.ReleaseDate ?? DateTime.Now, notification.SkillQualification_Evaluator_Link.Id, employee.PersonId, clientSettings_Notification.Steps.First().Id);
            await _notificationService.AddAsync(taskQualitificationEvaluatorNotification);
        }
    }
}
