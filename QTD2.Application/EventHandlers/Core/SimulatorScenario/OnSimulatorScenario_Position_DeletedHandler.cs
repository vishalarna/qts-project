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
    public class OnSimulatorScenario_Position_DeletedHandler : INotificationHandler<OnSimulatorScenario_Position_Deleted>
    {

        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly ISimulatorScenario_Task_CriteriaService _simulatorScenarioTaskCriteraService;
        private readonly IPositionService _positionService;

        public OnSimulatorScenario_Position_DeletedHandler(Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            ISimulatorScenario_Task_CriteriaService simulatorScenarioTaskCriteraService,
            IPositionService positionService
            )
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _simulatorScenarioTaskCriteraService = simulatorScenarioTaskCriteraService;
        }

        public async Task Handle(OnSimulatorScenario_Position_Deleted notification, CancellationToken cancellationToken)
        {
            var position = await _positionService.GetWithIncludeAsync(notification.SimulatorScenario_Position.PositionID, new[] { "Position_Tasks" });

            if (position != null)
            {
                foreach (var task in position.Position_Tasks)
                {
                    var taskCriteria = (await _simulatorScenarioTaskCriteraService.FindAsync(r => r.SimulatorScenarioId == notification.SimulatorScenario_Position.SimulatorScenarioID
                                            && r.TaskId == task.Id)).FirstOrDefault();
                    
                    taskCriteria.Delete();
                    await _simulatorScenarioTaskCriteraService.UpdateAsync(taskCriteria);
                }
            }
            else
            {
                return;
            }
        }
    }
}
