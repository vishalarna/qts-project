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
    public class OnSkillQualificationCompletedHandler : INotificationHandler<OnSkillQualificationCompleted>
    {
        private readonly INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly ISkillQualificationService _skillQualificationService;

        public OnSkillQualificationCompletedHandler(
            Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            ISkillQualificationService skillQualificationService)
        {
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _notificationService = notificationService;
            _skillQualificationService = skillQualificationService;
        }

        public async Task Handle(OnSkillQualificationCompleted notification, CancellationToken cancellationToken)
        {
            if (notification.SkillQualification.ClassScheduleId != null && notification.SkillQualification.Sequence != null)
            {
                var customSequence = notification.SkillQualification.Sequence + 1;
                var skillQualification = await _skillQualificationService.GetSkillQualificationByScheduleAndEmployeeAsync(notification.SkillQualification.ClassScheduleId, notification.SkillQualification.EmployeeId, customSequence);
                if (skillQualification != null)
                {
                    skillQualification.IsReleasedToEMP = true;
                    await _skillQualificationService.UpdateAsync(skillQualification);
                }
            }
            else
            {
                return;
            }
        }
    }
}
