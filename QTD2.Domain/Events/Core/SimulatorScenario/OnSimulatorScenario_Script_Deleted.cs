using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnSimulatorScenario_Script_Deleted : Common.IDomainEvent, INotification
    {
        public SimulatorScenario_Script SimulatorScenario_Script { get; }
        public OnSimulatorScenario_Script_Deleted(SimulatorScenario_Script simulatorScenario_Script)
        {
            SimulatorScenario_Script = simulatorScenario_Script;
        }
    }

}
