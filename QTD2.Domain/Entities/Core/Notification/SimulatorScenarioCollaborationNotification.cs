using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenarioCollaborationNotification : Notification
    {
        public int SimulatorScenarioCollaboratorId { get; set; }

        public virtual SimulatorScenario_Collaborator SimulatorScenarioCollaborator { get; set; }

        public SimulatorScenarioCollaborationNotification() { }

        public SimulatorScenarioCollaborationNotification( int simulatorScenarioCollaboratorId, DateTime dueDate, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            SimulatorScenarioCollaboratorId = simulatorScenarioCollaboratorId;
        }
    }
}
