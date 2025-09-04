using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnSimulatorScenario_Prerequisites_Updated : Common.IDomainEvent, INotification
    {
        public SimulatorScenario SimulatorScenario { get; }
        public OnSimulatorScenario_Prerequisites_Updated(SimulatorScenario simScenario)
        {
            SimulatorScenario = simScenario;
        }
    }
}
