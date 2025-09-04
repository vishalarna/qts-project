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
    public class OnClassSchedule_StudentEvaluations_EvaluationReleasedHandler : INotificationHandler<OnClassSchedule_StudentEvaluations_StudentEvaluationReleased>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly IEmployeeService _employeeService;

        private readonly IClassScheduleService _classScheduleService;

        public OnClassSchedule_StudentEvaluations_EvaluationReleasedHandler(
            Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            IClassScheduleService classScheduleService,
             IEmployeeService employeeService
            )
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _classScheduleService = classScheduleService;
            _employeeService = employeeService;
        }

        public async Task Handle(OnClassSchedule_StudentEvaluations_StudentEvaluationReleased notification, CancellationToken cancellationToken)
        {
            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Student Evaluation");

            var employee = await _employeeService.GetWithPersonAsync(notification.ClassSchedule_Evaluation_Roster.EmployeeId);

            if (!clientSettings_Notification.Enabled) return;

            var classSchedule = await _classScheduleService.GetAsync(notification.ClassSchedule_Evaluation_Roster.ClassScheduleId.Value);

            Domain.Entities.Core.EMPStudentEvaluationNotication idpReviewNotification = new Domain.Entities.Core.EMPStudentEvaluationNotication(notification.ClassSchedule_Evaluation_Roster.ReleaseDate ?? classSchedule.StartDateTime, notification.ClassSchedule_Evaluation_Roster.Id, employee.PersonId, clientSettings_Notification.Steps.First().Id);
            await _notificationService.AddAsync(idpReviewNotification);
        }
    }
}
