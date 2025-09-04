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
    public class OnCbtCreatedHandler : INotificationHandler<OnCbtCreated>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly Domain.Interfaces.Service.Core.IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly IILAService _ilaService;

        public OnCbtCreatedHandler(
            INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            IILAService ilaService)
        {
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _notificationService = notificationService;
            _ilaService = ilaService;
        }

        public async Task Handle(OnCbtCreated notification, CancellationToken cancellationToken)
        {
            //var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Online Course");
            //var ilas = await _ilaService.GetWithAllClassesAndStudentsAsync(notification.CBT.ILAId);

            //if (!clientSettings_Notification.Enabled) return;

            //foreach (var cs in ilas.ClassSchedules)
            //{
            //    if (cs.EndDateTime < DateTime.Now) continue;

            //    foreach (var cse in cs.ClassSchedule_Employee)
            //    {
            //        if (cse.IsComplete) continue;

            //        if (!cse.IsEnrolled) continue;

            //        if (cse.Deleted) continue;

            //        if (!cse.Active) continue;

            //        DateTime sendDate = DateTime.Now.ToUniversalTime();

            //        if (notification.CBT.Availablity == Domain.Entities.Core.CBTAvailablity.OnClassEndDateTime)
            //            sendDate = cs.EndDateTime;

            //        if (notification.CBT.Availablity == Domain.Entities.Core.CBTAvailablity.OnClassStartDateTime)
            //            sendDate = cs.StartDateTime;

            //        if (notification.CBT.Availablity == Domain.Entities.Core.CBTAvailablity.AfterPretestComplete)
            //        {
            //            //this will get picked up by the pretest handler
            //            continue;
            //        }

            //        Domain.Entities.Core.EMPOnlineCourseNotification empOnlineCourseNotification = new Domain.Entities.Core.EMPOnlineCourseNotification(DateTime.Now.ToUniversalTime(), cse.Id, notification.CBT.Id, cse.Employee.PersonId, clientSettings_Notification.Steps.First().Id);
            //        await _notificationService.AddAsync(empOnlineCourseNotification);
        }
    }
}
