using MediatR;
using QTD2.Domain.Entities.Core;
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
    public class OnSimulatorScenario_Collaborators_UpdatedHandler : INotificationHandler<OnSimulatorScenario_Collaborators_Updated>
    {

        private readonly INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly ISimulatorScenario_CollaboratorService _simulatorScenarioCollaboratorService;

        public OnSimulatorScenario_Collaborators_UpdatedHandler(
            INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            ISimulatorScenario_CollaboratorService simulatorScenarioCollaboratorService
            )
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _simulatorScenarioCollaboratorService = simulatorScenarioCollaboratorService;
        }

        public async System.Threading.Tasks.Task Handle(OnSimulatorScenario_Collaborators_Updated notification, CancellationToken cancellationToken)
        {
            var clientSettings_notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("Simulator Scenario Collaboration");

            if (!clientSettings_notification.Enabled) return;

            var simScenarioCollaborators = await _simulatorScenarioCollaboratorService.FindWithIncludeAsync(x => !x.Deleted && x.SimulatorScenarioId == notification.SimulatorScenario.Id, new string[] { "User" });
            foreach(var simScenarioCollaborator in simScenarioCollaborators)
            {
                var collaboratorNotification = (await _notificationService.FindAsync(x => (x as SimulatorScenarioCollaborationNotification) != null  && (x as SimulatorScenarioCollaborationNotification).SimulatorScenarioCollaboratorId == simScenarioCollaborator.Id && x.Status == NotificationSendStatus.Sent)).FirstOrDefault();
                if(collaboratorNotification == null)
                {
                    collaboratorNotification = new SimulatorScenarioCollaborationNotification(simScenarioCollaborator.Id, DateTime.UtcNow, simScenarioCollaborator.User.PersonId, clientSettings_notification.Steps.First().Id);
                    await _notificationService.AddAsync(collaboratorNotification);
                }
            }
        }
    }
}