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
    public class OnSimulatorScenario_Event_DeletedHandler : INotificationHandler<OnSimulatorScenario_Event_Deleted>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly ISimulatorScenario_ScriptService _simulatorScenario_ScriptService;

        public OnSimulatorScenario_Event_DeletedHandler(Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            ISimulatorScenario_ScriptService simulatorScenario_ScriptService
            )
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _simulatorScenario_ScriptService = simulatorScenario_ScriptService;
        }

        public async Task Handle(OnSimulatorScenario_Event_Deleted notification, CancellationToken cancellationToken)
        {
            var scripts = await _simulatorScenario_ScriptService.GetScriptsByEventIdAsync(notification.SimulatorScenario_Event.Id);
            if (scripts != null && scripts.Any())
            {
                foreach (var script in scripts)
                {
                    script.Delete();
                    await _simulatorScenario_ScriptService.UpdateAsync(script);
                }
            }
        }
    }
}
