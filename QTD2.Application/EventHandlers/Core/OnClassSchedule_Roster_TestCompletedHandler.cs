using QTD2.Domain.Events.Core;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Linq;
using QTD2.Domain.Services.Core;
using QTD2.Domain.ClassScheduleEmployee.GradeEvaluation;
using QTD2.Infrastructure.Model;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnClassSchedule_Roster_TestCompletedHandler : INotificationHandler<OnClassSchedule_Roster_TestCompleted>
    {
        private readonly IMetaILAService _metaILAService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly ICBTService _cbtService;
        private readonly IClassScheduleEmployeeService _classScheduleEmployeeService;
        private readonly INotificationService _notificationService;
        private readonly IGradeEvaluator _gradeEvaluator;

        public OnClassSchedule_Roster_TestCompletedHandler(
            IMetaILAService metaILAService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            ICBTService cbtService,
            IClassScheduleEmployeeService classScheduleEmployeeService,
            INotificationService notificationService,
            IGradeEvaluator gradeEvaluator
            )
        {
            _metaILAService = metaILAService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _cbtService = cbtService;
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _notificationService = notificationService;
            _gradeEvaluator = gradeEvaluator;
        }
        public async Task Handle(OnClassSchedule_Roster_TestCompleted notification, CancellationToken cancellationToken)
        {
            await EvaluateClassScheduleEmployeeResult(notification);
            await HandleMetaILAs(notification);
            await HandleCBTNotifications(notification); 
        }

        private async Task EvaluateClassScheduleEmployeeResult(OnClassSchedule_Roster_TestCompleted notification)
        {
            var roster = notification.ClassSchedule_Roster;

            if (!roster.IsFromClassSchedule) return;

            var classScheduleEmployee = await _classScheduleEmployeeService.GetByEmployeeIdAndClassScheduleIdAsync(roster.EmpId, roster.ClassScheduleId.GetValueOrDefault());

            var gradeResult = await _gradeEvaluator.EvaluateClassScheduleEmployeeAsync(classScheduleEmployee);

            if(gradeResult.IsComplete)
            {
                classScheduleEmployee.CompleteClass(gradeResult.CompletionDate, gradeResult.Grade, gradeResult.ScoreAsInt);
                await _classScheduleEmployeeService.UpdateAsync(classScheduleEmployee);
            }
        }

        protected async Task HandleMetaILAs(OnClassSchedule_Roster_TestCompleted notification) {
            var metaILAs = await _metaILAService.GetMetaILAsByTestIdAsync(notification.ClassSchedule_Roster.TestId);

            foreach (var metaILA in metaILAs)
            {
                metaILA.IsDeleteAllowed = false;
                await _metaILAService.UpdateAsync(metaILA);
            }
        }

        protected async Task HandleCBTNotifications(OnClassSchedule_Roster_TestCompleted notification)
        {
            if (notification.ClassSchedule_Roster.TestTypeId != 1) return;
            if (!notification.ClassSchedule_Roster.IsFromClassSchedule) return;

            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Online Course");
            if (!clientSettings_Notification.Enabled) return;

            var classSchedule = notification.ClassSchedule_Roster.ClassSchedule;
            if (!classSchedule.ILA.CBTRequiredForCourse) return;

            var cbt = await _cbtService.GetActiveByIlaAsync(classSchedule.ILAID.Value);
            if (cbt == null) return;
            if (cbt.Availablity != Domain.Entities.Core.CBTAvailablity.AfterPretestComplete) return;

            var classScheduleEmployee = await _classScheduleEmployeeService.GetEmployeeForClassScheduleAsync(classSchedule.Id, notification.ClassSchedule_Roster.EmpId);

            Domain.Entities.Core.EMPOnlineCourseNotification empOnlineCourseNotification = new Domain.Entities.Core.EMPOnlineCourseNotification(DateTime.Now.ToUniversalTime(), classScheduleEmployee.Id, cbt.Id, notification.ClassSchedule_Roster.Employee.PersonId, clientSettings_Notification.Steps.First().Id);
            await _notificationService.AddAsync(empOnlineCourseNotification);
        }
    }
}