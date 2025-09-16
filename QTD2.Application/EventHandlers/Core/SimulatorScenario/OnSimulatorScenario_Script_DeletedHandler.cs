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
    public class OnSimulatorScenario_Script_DeletedHandler : INotificationHandler<OnSimulatorScenario_Script_Deleted>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly ISimulatorScenario_ScriptService _simulatorScenario_ScriptService;
        private readonly ISimulatorScenario_Script_CriteriaService _simulatorScenarioScriptCriteraService;

        public OnSimulatorScenario_Script_DeletedHandler(Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            ISimulatorScenario_ScriptService simulatorScenario_ScriptService,
            ISimulatorScenario_Script_CriteriaService simulatorScenarioScriptCriteraService
            )
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _simulatorScenario_ScriptService = simulatorScenario_ScriptService;
            _simulatorScenarioScriptCriteraService = simulatorScenarioScriptCriteraService;
        }

        public async Task Handle(OnSimulatorScenario_Script_Deleted notification, CancellationToken cancellationToken)
        {
            var criteria = await _simulatorScenarioScriptCriteraService.GetCriteriasByIdAsync(notification.SimulatorScenario_Script.Id);
            if (criteria != null)
            {
                criteria.Delete();
                await _simulatorScenarioScriptCriteraService.UpdateAsync(criteria);
            }
        }
    }
}
