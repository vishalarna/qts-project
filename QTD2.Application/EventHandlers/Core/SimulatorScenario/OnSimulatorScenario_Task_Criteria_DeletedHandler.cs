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
    public class OnSimulatorScenario_Task_Criteria_DeletedHandler : INotificationHandler<OnSimulatorScenario_Task_Criteria_Deleted>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly ISimulatorScenario_EventAndScript_CriteriaService _simulatorScenarioEventAndScriptCriteraService;
        private readonly IPositionService _positionService;

        public OnSimulatorScenario_Task_Criteria_DeletedHandler(Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            ISimulatorScenario_EventAndScript_CriteriaService simulatorScenarioEventAndScriptCriteraService,
            IPositionService positionService
            )
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _simulatorScenarioEventAndScriptCriteraService = simulatorScenarioEventAndScriptCriteraService;
        }

        public async Task Handle(OnSimulatorScenario_Task_Criteria_Deleted notification, CancellationToken cancellationToken)
        {
            var eventCriterias = (await _simulatorScenarioEventAndScriptCriteraService.FindAsync(k => k.CriteriaId == notification.SimulatorScenario_Task_Criteria.Id)).ToList();
            if (eventCriterias.Count != 0 && eventCriterias != null)
            {
                foreach (var eventCriteria in eventCriterias)
                {
                    eventCriteria.Delete();
                    await _simulatorScenarioEventAndScriptCriteraService.UpdateAsync(eventCriteria);
                }
            }
        }
    }
}
