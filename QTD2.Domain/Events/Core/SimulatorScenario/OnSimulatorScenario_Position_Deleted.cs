using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnSimulatorScenario_Position_Deleted : Common.IDomainEvent, INotification
    {
        public SimulatorScenario_Position SimulatorScenario_Position { get; }
        public OnSimulatorScenario_Position_Deleted(SimulatorScenario_Position simScenario_Position)
        {
            SimulatorScenario_Position = simScenario_Position;
        }
    }
}
