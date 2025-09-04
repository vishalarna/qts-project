using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnSimulatorScenario_Collaborators_Updated : Common.IDomainEvent, INotification
    {
        public SimulatorScenario SimulatorScenario { get; set; }
        public OnSimulatorScenario_Collaborators_Updated(SimulatorScenario simScenario)
        {
            SimulatorScenario = simScenario;
        }
    }
}
