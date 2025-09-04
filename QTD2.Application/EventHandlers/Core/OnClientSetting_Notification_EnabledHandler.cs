using MediatR;
using QTD2.Domain.Events.Core;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnClientSetting_Notification_EnabledHandler : INotificationHandler<OnClientSetting_Notification_Enabled>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;

        private readonly Domain.Interfaces.Service.Core.ITaskQualificationService _taskQualificationService;
        private readonly Domain.Interfaces.Service.Core.IIDPService _idpService;
        private readonly Domain.Interfaces.Service.Core.IProcedureReviewService _procedureReviewService;
        private readonly Domain.Interfaces.Service.Core.IClassScheduleService _classScheduleService;
        private readonly Domain.Interfaces.Service.Core.IClassSchedule_RosterService _classScheduleRosterService;

        private readonly Domain.Interfaces.Service.Core.ICBTService _cbtService;


        public OnClientSetting_Notification_EnabledHandler(
            Domain.Interfaces.Service.Core.INotificationService notificationService,
            Domain.Interfaces.Service.Core.IClassScheduleService classScheduleService,
            Domain.Interfaces.Service.Core.ICBTService cbtService,
            Domain.Interfaces.Service.Core.IClassSchedule_RosterService classScheduleRosterService,
             Domain.Interfaces.Service.Core.IProcedureReviewService procedureReviewService,
            Domain.Interfaces.Service.Core.IIDPService idpService,
            Domain.Interfaces.Service.Core.ITaskQualificationService taskQualificationService)
        {
            _notificationService = notificationService;
            _classScheduleService = classScheduleService;
            _cbtService = cbtService;
            _classScheduleRosterService = classScheduleRosterService;
            _idpService = idpService;
            _taskQualificationService = taskQualificationService;
            _procedureReviewService = procedureReviewService;
        }

        public async Task Handle(OnClientSetting_Notification_Enabled notification, CancellationToken cancellationToken)
        {

        }
    }
}
