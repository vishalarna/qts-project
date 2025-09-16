using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnSimulatorScenario_Event_Deleted : Common.IDomainEvent, INotification
    {
        public SimulatorScenario_Event SimulatorScenario_Event { get; }
        public OnSimulatorScenario_Event_Deleted(SimulatorScenario_Event simulatorScenario_Event)
        {
            SimulatorScenario_Event = simulatorScenario_Event;
        }
    }
}
